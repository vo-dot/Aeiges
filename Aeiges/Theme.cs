using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aeiges
{
    public static class Theme
    {
        // Modern purple palette
        public static readonly Color Primary = Color.FromArgb(108, 92, 231);
        public static readonly Color PrimaryDark = Color.FromArgb(72, 52, 212);
        public static readonly Color PrimaryLight = Color.FromArgb(162, 155, 254);
        public static readonly Color Accent = Color.FromArgb(0, 184, 148);
        public static readonly Color AccentHover = Color.FromArgb(0, 150, 120);
        public static readonly Color Success = Color.FromArgb(46, 213, 115);
        public static readonly Color Danger = Color.FromArgb(255, 71, 87);
        public static readonly Color Warning = Color.FromArgb(255, 159, 67);
        public static readonly Color Background = Color.FromArgb(245, 246, 250);
        public static readonly Color CardBg = Color.White;
        public static readonly Color SidebarBg = Color.FromArgb(250, 250, 255);
        public static readonly Color TextPrimary = Color.FromArgb(45, 52, 54);
        public static readonly Color TextSecondary = Color.FromArgb(99, 110, 114);
        public static readonly Color TextMuted = Color.FromArgb(160, 160, 170);
        public static readonly Color TextOnDark = Color.White;
        public static readonly Color Border = Color.FromArgb(230, 230, 240);
        public static readonly Color LeftPanelBg = Color.FromArgb(248, 245, 255);

        public static Font TitleFont = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
        public static Font HeaderFont = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
        public static Font SubHeaderFont = new Font("Segoe UI Semibold", 12F);
        public static Font BodyFont = new Font("Segoe UI", 10F);
        public static Font SmallFont = new Font("Segoe UI", 9F);

        public static void StylePrimaryButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Primary;
            btn.ForeColor = TextOnDark;
            btn.Font = new Font("Segoe UI Semibold", 11F);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = PrimaryDark;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 40, 180);
            if (btn.Height < 42) btn.Height = 42;
        }

        public static void StyleAccentButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Accent;
            btn.ForeColor = TextOnDark;
            btn.Font = new Font("Segoe UI Semibold", 10.5F);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = AccentHover;
        }

        public static void StyleSuccessButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(46, 125, 50);
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI Semibold", 10F);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(56, 142, 60);
        }

        public static void StyleOutlineButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Primary;
            btn.BackColor = Color.White;
            btn.ForeColor = Primary;
            btn.Font = new Font("Segoe UI", 9.5F);
            btn.Cursor = Cursors.Hand;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 237, 255);
        }

        public static void StyleDangerButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Danger;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI Semibold", 9.5F);
            btn.Cursor = Cursors.Hand;
        }

        public static void StyleGhostButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.Transparent;
            btn.ForeColor = TextSecondary;
            btn.Font = new Font("Segoe UI", 10F);
            btn.Cursor = Cursors.Hand;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 237, 255);
        }

        public static void StyleHeaderPanel(Panel pnl)
        {
            pnl.BackColor = PrimaryDark;
        }

        public static void StyleDataGrid(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5F);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);
            dgv.ColumnHeadersHeight = 40;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.DefaultCellStyle.Font = BodyFont;
            dgv.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 225, 255);
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 248, 255);
            dgv.RowTemplate.Height = 36;
            dgv.GridColor = Border;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.RowHeadersVisible = false;
        }

        public static void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = new Font("Segoe UI", 11F);
            txt.BackColor = Color.White;
            txt.ForeColor = TextPrimary;
        }

        public static void ApplyFormBackground(Form form)
        {
            form.BackColor = Background;
            form.Font = BodyFont;
        }
    }
}
