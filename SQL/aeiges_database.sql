CREATE DATABASE IF NOT EXISTS aeiges_cybercrime;
USE aeiges_cybercrime;

-- ============================================================
-- 1. TABLES
-- ============================================================

-- Users table with role-based access
CREATE TABLE IF NOT EXISTS Users (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    FullName VARCHAR(100) NOT NULL,
    Role ENUM('User', 'Officer', 'Admin') NOT NULL DEFAULT 'User',
    Phone VARCHAR(20),
    IsActive BOOLEAN DEFAULT TRUE,
    ProfilePicture VARCHAR(500) NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Crime Types reference table
CREATE TABLE IF NOT EXISTS CrimeTypes (
    CrimeTypeID INT AUTO_INCREMENT PRIMARY KEY,
    TypeName VARCHAR(100) NOT NULL UNIQUE,
    Description TEXT
);

-- Complaints / Cases table
CREATE TABLE IF NOT EXISTS Complaints (
    ComplaintID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT NULL,                          -- NULL allowed for anonymous reports
    Title VARCHAR(200) NOT NULL,
    Description TEXT NOT NULL,
    CrimeTypeID INT,
    Status ENUM('Submitted', 'Under Review', 'Assigned', 'Investigating', 'Resolved', 'Closed', 'Rejected')
           NOT NULL DEFAULT 'Submitted',
    Priority ENUM('Low', 'Medium', 'High', 'Critical') DEFAULT 'Medium',
    EvidencePath VARCHAR(500),
    AssignedOfficerID INT NULL,
    Location VARCHAR(255),
    IncidentDate DATETIME,
    IsAnonymous BOOLEAN DEFAULT FALSE,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID) ON DELETE SET NULL,
    FOREIGN KEY (CrimeTypeID) REFERENCES CrimeTypes(CrimeTypeID) ON DELETE SET NULL,
    FOREIGN KEY (AssignedOfficerID) REFERENCES Users(UserID) ON DELETE SET NULL
);

-- Case Status History / Updates
CREATE TABLE IF NOT EXISTS CaseUpdates (
    UpdateID INT AUTO_INCREMENT PRIMARY KEY,
    ComplaintID INT NOT NULL,
    UpdatedBy INT NOT NULL,
    OldStatus VARCHAR(50),
    NewStatus VARCHAR(50) NOT NULL,
    Remarks TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ComplaintID) REFERENCES Complaints(ComplaintID) ON DELETE CASCADE,
    FOREIGN KEY (UpdatedBy) REFERENCES Users(UserID) ON DELETE CASCADE
);

-- Evidence files (multiple per complaint)
CREATE TABLE IF NOT EXISTS Evidence (
    EvidenceID INT AUTO_INCREMENT PRIMARY KEY,
    ComplaintID INT NOT NULL,
    FileName VARCHAR(255) NOT NULL,
    FilePath VARCHAR(500) NOT NULL,
    FileType VARCHAR(50),
    UploadedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ComplaintID) REFERENCES Complaints(ComplaintID) ON DELETE CASCADE
);

-- ============================================================
-- 2. FIXES FOR EXISTING DATABASES
--    (formerly fix_database.sql; skips anything already applied)
-- ============================================================

DROP PROCEDURE IF EXISTS aeiges_apply_fixes;

DELIMITER //
CREATE PROCEDURE aeiges_apply_fixes()
BEGIN
    -- Users.ProfilePicture (My Profile feature)
    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS
                   WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Users'
                     AND COLUMN_NAME = 'ProfilePicture') THEN
        ALTER TABLE Users ADD COLUMN ProfilePicture VARCHAR(500) NULL;
    END IF;

    -- Complaints.IsAnonymous flag
    IF NOT EXISTS (SELECT 1 FROM information_schema.COLUMNS
                   WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'Complaints'
                     AND COLUMN_NAME = 'IsAnonymous') THEN
        ALTER TABLE Complaints ADD COLUMN IsAnonymous BOOLEAN DEFAULT FALSE;
    END IF;

    -- Allow anonymous complaints (UserID may be NULL)
    ALTER TABLE Complaints MODIFY UserID INT NULL;
END//
DELIMITER ;

CALL aeiges_apply_fixes();
DROP PROCEDURE aeiges_apply_fixes;

-- ============================================================
-- 3. INDEXES (created only if missing)
-- ============================================================

DROP PROCEDURE IF EXISTS aeiges_add_index;

DELIMITER //
CREATE PROCEDURE aeiges_add_index(IN tbl VARCHAR(64), IN idx VARCHAR(64), IN col VARCHAR(64))
BEGIN
    IF NOT EXISTS (SELECT 1 FROM information_schema.STATISTICS
                   WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = tbl AND INDEX_NAME = idx) THEN
        SET @sql = CONCAT('CREATE INDEX ', idx, ' ON ', tbl, '(', col, ')');
        PREPARE stmt FROM @sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
    END IF;
END//
DELIMITER ;

CALL aeiges_add_index('Complaints', 'idx_complaints_userid', 'UserID');
CALL aeiges_add_index('Complaints', 'idx_complaints_status', 'Status');
CALL aeiges_add_index('Complaints', 'idx_complaints_officer', 'AssignedOfficerID');
CALL aeiges_add_index('CaseUpdates', 'idx_caseupdates_complaint', 'ComplaintID');

DROP PROCEDURE aeiges_add_index;

-- ============================================================
-- 4. DEFAULT DATA (INSERT IGNORE skips rows that already exist)
-- ============================================================

INSERT IGNORE INTO CrimeTypes (TypeName, Description) VALUES
('Phishing', 'Fraudulent attempts to obtain sensitive information via email or fake websites'),
('Hacking', 'Unauthorized access to computer systems or networks'),
('Online Fraud', 'Financial scams and fraudulent online transactions'),
('Identity Theft', 'Stealing and misusing personal identity information'),
('Cyberbullying', 'Harassment, threats or bullying using digital platforms'),
('Data Breach', 'Unauthorized access and exposure of sensitive data'),
('Malware / Ransomware', 'Malicious software attacks including ransomware'),
('Online Harassment', 'Threatening or abusive behavior online'),
('Other', 'Other cyber-related crimes not listed above');
INSERT IGNORE INTO Users (Username, PasswordHash, Email, FullName, Role, Phone) VALUES
('admin', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', 'admin@aeiges.gov', 'System Administrator', 'Admin', '0000000000');

-- Sample Officer
INSERT IGNORE INTO Users (Username, PasswordHash, Email, FullName, Role, Phone) VALUES
('officer1', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', 'officer1@aeiges.gov', 'John Officer', 'Officer', '9876543210');

-- ============================================================
-- 5. VERIFY
-- ============================================================

DESCRIBE Users;
DESCRIBE Complaints;

SELECT 'Aeiges database setup completed successfully!' AS Status;
