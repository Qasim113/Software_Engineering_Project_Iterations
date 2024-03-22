using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace se
{
    public partial class Form2 : Form
    {
        private string userEmail;
        private string userRole;

        public Form2(string email, string role)
        {
            InitializeComponent();
            userEmail = email;
            userRole = role;
            CustomizeComponents();
        }
        private void CustomizeComponents()
        {
            // Example of setting the title, email, and role
            this.Text = "Announcements - FASTies";
            Label emailLabel = new Label()
            {
                Text = "Email: " + userEmail,
                AutoSize = true,
                Location = new Point(10, 10) // Adjust as needed
            };
            Label roleLabel = new Label()
            {
                Text = "Role: " + userRole,
                AutoSize = true,
                Location = new Point(10, 30) // Adjust as needed
            };
            this.Controls.Add(emailLabel);
            this.Controls.Add(roleLabel);
            this.Controls.Add(announcementsListBox);
            LoadAnnouncements();
            AddRoleSpecificButtons();
        }

        private void AddRoleSpecificButtons()
        {
            Dictionary<string, string[]> buttonTextsByRole = new Dictionary<string, string[]>
    {
        { "mentor", new string[] { "Society Management", "Assign Tasks", "Task Updates", "Private Announcements", "Update Society Heads" } },
        { "head", new string[] { "Register Event", "Check Status", "Mentor Announcements", "Give Feedback", "Select Mentor", "Task Update", "Leave Society" } },
        { "member", new string[] { "Give Feedback", "Task Update", "Leave Society" } }
    };

            if (buttonTextsByRole.ContainsKey(userRole.ToLower()))
            {
                int yPos = 300;
                foreach (var buttonText in buttonTextsByRole[userRole.ToLower()])
                {
                    Button roleButton = new Button
                    {
                        Text = buttonText,
                        Location = new Point(100, yPos),
                        Size = new Size(300, 40),
                        BackColor = Color.Black,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    // Add a click event for each button based on its text
                    roleButton.Click += (sender, e) =>
                    {
                        switch (buttonText)
                        {
                            case "Register Event":
                                if (userRole.ToLower() == "head")
                                {
                                    OpenRegisterEventForm();
                                }
                                break;
                            case "Check Status":
                                if (userRole.ToLower() == "head")
                                {
                                    EventStatusForm statusForm = new EventStatusForm(userEmail);
                                    statusForm.Show();
                                }
                                break;
                            case "Society Management":
                                if (userRole.ToLower() == "mentor")
                                {
                                    SocietyManagementForm managementForm = new SocietyManagementForm();
                                    managementForm.Show();
                                }
                                break;
                        }
                    };

                    this.Controls.Add(roleButton);
                    yPos += 50; // Increment for the next button's Y position
                }
            }
        }

        private void OpenRegisterEventForm()
        {
            // Assuming RegisterEventForm is the form you create for event registration
            RegisterEventForm registerEventForm = new RegisterEventForm(userEmail);
            registerEventForm.Show();
        }

        private void LoadAnnouncements()
        {
            string connectionString = "server=localhost;database=se;uid=root;pwd=1234567;";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT content FROM Announcements WHERE visible_to_all = TRUE ORDER BY announcement_id";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int announcementNumber = 1; // Start counting from 1
                        while (reader.Read())
                        {
                            string announcement = reader["content"].ToString();
                            // Prefix each announcement with its number
                            string formattedAnnouncement = $"Announcement {announcementNumber}: {announcement}";
                            announcementsListBox.Items.Add(formattedAnnouncement);
                            announcementNumber++; // Increment the counter for each announcement
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading announcements: {ex.Message}");
                }
            }
        }

    }
}
