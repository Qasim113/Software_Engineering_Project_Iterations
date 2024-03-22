namespace se
{
    partial class Form2
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label roleLabel;
        private System.Windows.Forms.ListBox announcementsListBox; // For displaying announcements
        private System.Windows.Forms.MonthCalendar announcementsCalendar;
        private System.Windows.Forms.TextBox notesTextBox;
        private System.Windows.Forms.Label notesLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.emailLabel = new System.Windows.Forms.Label();
            this.roleLabel = new System.Windows.Forms.Label();
            this.announcementsListBox = new System.Windows.Forms.ListBox();
            this.announcementsCalendar = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(650, 10);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(0, 20);
            this.emailLabel.TabIndex = 0;
            // 
            // roleLabel
            // 
            this.roleLabel.AutoSize = true;
            this.roleLabel.Location = new System.Drawing.Point(650, 30);
            this.roleLabel.Name = "roleLabel";
            this.roleLabel.Size = new System.Drawing.Size(0, 20);
            this.roleLabel.TabIndex = 1;
            // 
            // announcementsListBox
            // 
            this.announcementsListBox.ItemHeight = 20;
            this.announcementsListBox.Location = new System.Drawing.Point(15, 80);
            this.announcementsListBox.Name = "announcementsListBox";
            this.announcementsListBox.Size = new System.Drawing.Size(1200, 284);
            this.announcementsListBox.TabIndex = 3;
            // 
            // announcementsCalendar
            // 
            this.announcementsCalendar.BackColor = System.Drawing.Color.White;
            this.announcementsCalendar.ForeColor = System.Drawing.Color.Black;
            this.announcementsCalendar.Location = new System.Drawing.Point(1300, 80);
            this.announcementsCalendar.MaxSelectionCount = 1;
            this.announcementsCalendar.Name = "announcementsCalendar";
            this.announcementsCalendar.TabIndex = 4;
            this.announcementsCalendar.TitleBackColor = System.Drawing.Color.DarkGray;
            // 

            // Inside InitializeComponent method
            this.notesTextBox = new System.Windows.Forms.TextBox();
            this.notesLabel = new System.Windows.Forms.Label();

            // Set properties for the notes Label
            this.notesLabel.AutoSize = true;
            this.notesLabel.Location = new System.Drawing.Point(1300, 460); 
            this.notesLabel.Size = new System.Drawing.Size(200, 100);
            this.notesLabel.Text = "NOTES";
            this.Controls.Add(this.notesLabel);

            // Set properties for the TextBox
            this.notesTextBox.Multiline = true; // Enable multiline
            this.notesTextBox.Location = new System.Drawing.Point(1300, 480); 
            this.notesTextBox.Name = "notesTextBox";
            this.notesTextBox.Size = new System.Drawing.Size(400, 500); 
            this.notesTextBox.TabIndex = 5;
            // Add the TextBox to the form
            this.Controls.Add(this.notesTextBox);
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1429, 450);
            this.Controls.Add(this.announcementsCalendar);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.roleLabel);
            this.Controls.Add(this.announcementsListBox);
            this.Name = "Form2";
            this.Text = "Announcements - FASTies";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
