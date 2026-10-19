[README.md](https://github.com/user-attachments/files/33674093/README.md)
# Aeiges: Cyber Crime Reporting & Management System

A desktop application for reporting, tracking and managing cybercrime complaints, built with C# and a MySQL database.

Cybercrime such as phishing, hacking, online fraud, identity theft, cyberbullying and data breaches is a growing threat, yet many complaints are still handled on paper, which is slow and hard to track. Aeiges replaces that with a single digital system: citizens file complaints and attach evidence, officers investigate, and administrators oversee the whole process.

## Features

**Citizens**
- Register and log in
- Submit a complaint (title, description, crime type, location, incident date)
- Attach evidence files to a complaint
- Report anonymously, without linking the complaint to an account
- Track the status of submitted complaints
- Manage a profile, including a profile picture

**Investigation officers**
- View cases assigned to them
- Update case status and add remarks
- Review evidence submitted with a complaint

**Administrators**
- View and review all complaints
- Assign complaints to officers
- Manage user accounts

**Security**
- Role-based access control (User, Officer, Admin)
- Passwords stored as hashes, never as plain text

## Tech stack

| Layer | Technology |
|---|---|
| Language | C# |
| Framework | TODO: .NET version (for example .NET 8) |
| UI | TODO: WPF or Windows Forms |
| Database | MySQL 8.0+ |
| Tools | Visual Studio 2022, MySQL Workbench |

## Database design

The database is `aeiges_cybercrime` and has five tables. The script is in [`aeiges_database.sql`](aeiges_database.sql).

| Table | Primary key | Foreign keys | Other constraints |
|---|---|---|---|
| `Users` | `UserID` | none | `Username` and `Email` are `UNIQUE`; `Role` is an `ENUM('User','Officer','Admin')` |
| `CrimeTypes` | `CrimeTypeID` | none | `TypeName` is `UNIQUE` |
| `Complaints` | `ComplaintID` | `UserID` → `Users`; `CrimeTypeID` → `CrimeTypes`; `AssignedOfficerID` → `Users` | `Status` and `Priority` are `ENUM`s; `UserID` is nullable for anonymous reports |
| `CaseUpdates` | `UpdateID` | `ComplaintID` → `Complaints`; `UpdatedBy` → `Users` | none |
| `Evidence` | `EvidenceID` | `ComplaintID` → `Complaints` | none |

### Relationships

```
Users       1 ──< Complaints    (the reporter, via UserID)
Users       1 ──< Complaints    (the assigned officer, via AssignedOfficerID)
CrimeTypes  1 ──< Complaints
Complaints  1 ──< CaseUpdates   (status history)
Complaints  1 ──< Evidence      (several files per complaint)
Users       1 ──< CaseUpdates   (who made the update)
```

### Design notes

- **Anonymous reports:** `Complaints.UserID` may be `NULL` and `IsAnonymous` marks the complaint as anonymous.
- **Delete behaviour:** deleting a user sets `UserID`, `AssignedOfficerID` and `CrimeTypeID` on their complaints to `NULL` (`ON DELETE SET NULL`), so complaints are never lost. Deleting a complaint also deletes its updates and evidence (`ON DELETE CASCADE`).
- **Status history:** every status change is recorded in `CaseUpdates` with the old status, the new status, the officer and remarks.
- **Re-runnable script:** the SQL script uses `IF NOT EXISTS` and `INSERT IGNORE`, so running it again will not duplicate data or fail.
- **Indexes:** `Complaints(UserID)`, `Complaints(Status)`, `Complaints(AssignedOfficerID)` and `CaseUpdates(ComplaintID)`.

## Getting started

### Prerequisites

- Visual Studio 2022 with the **.NET desktop development** workload
- MySQL Server 8.0 or later
- MySQL Workbench (or the `mysql` command-line client)

### 1. Create the database

1. Open MySQL Workbench and connect to your local server.
2. Open `aeiges_database.sql`.
3. Run the whole script (lightning-bolt icon, or `Ctrl+Shift+Enter`).
4. You should see `Aeiges database setup completed successfully!` in the result grid.

### 2. Configure the connection string

Open the project's configuration file (`App.config`) and set your own MySQL details:

```xml
<add name="AeigesConnection"
     connectionString="Server=localhost;Port=3306;Database=aeiges_cybercrime;Uid=root;Pwd=YOUR_PASSWORD_HERE;" />
```

> **Do not commit your real password.** Keep a placeholder in the repository and enter your real password only on your own machine.

### 3. Run the application

1. Open `Aeiges.sln` in Visual Studio.
2. Let NuGet restore the packages.
3. Press `F5` to build and run.

### Default accounts

Admin
User :- Obama
Password :- Obama!@#123


## Project structure

```
Aeiges/
├── Aeiges.sln              Visual Studio solution
├── aeiges_database.sql     MySQL schema and seed data
└── Aeiges/                 C# application project
```

## Known limitations

- Evidence files are stored on disk and only their paths are saved in the database.
- No email notifications.


TODO: your name, and a link to your GitHub profile.
