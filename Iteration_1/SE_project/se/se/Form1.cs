using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

// Ensure that your namespace matches the one used throughout your project.
namespace se
{

    public partial class LoginForm : Form
    {
        // Declare your controls here.
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label headerLabel; // Label for the FASTies header

        public LoginForm()
        {
            // Call the InitializeComponent method where the designer will initialize your components.
            InitializeComponent();
            // Perform additional setup that is not handled by the designer.
            CustomizeComponents();
        }


        private void CustomizeComponents()
        {
            // Set the form's background color
            this.BackColor = Color.DarkGray;

            // Initialize the header label for "FASTies"
            headerLabel = new Label();
            headerLabel.Text = "FASTies";
            headerLabel.Font = new Font("Arial", 24, FontStyle.Bold);
            headerLabel.ForeColor = Color.White;
            headerLabel.AutoSize = true;
            headerLabel.Location = new Point((this.Width - headerLabel.Width) / 2, 50);

            // Adjusting properties of usernameTextBox
            usernameTextBox.BackColor = Color.LightGray;
            usernameTextBox.ForeColor = Color.Black;
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.Location = new Point((this.Width - usernameTextBox.Width) / 2, 200);

            // Adjusting properties of passwordTextBox
            passwordTextBox.BackColor = Color.LightGray;
            passwordTextBox.ForeColor = Color.Black;
            passwordTextBox.BorderStyle = BorderStyle.FixedSingle;
            passwordTextBox.Location = new Point((this.Width - passwordTextBox.Width) / 2, 240);

            // Adjusting properties of loginButton
            loginButton.BackColor = Color.LightSlateGray;
            loginButton.ForeColor = Color.White;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.Location = new Point((this.Width - loginButton.Width) / 2, 290);

            // Add the controls to the form
            this.Controls.Add(headerLabel);
            this.Controls.Add(usernameTextBox);
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(loginButton);

            // Center the form on the screen
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Your event handler should stay the same.
        private void loginButton_Click(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=se;uid=root;pwd=1234567;";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string sql = "SELECT email, role FROM Users WHERE email=@username AND password=@password";
                    MySqlCommand cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@username", usernameTextBox.Text);
                    cmd.Parameters.AddWithValue("@password", passwordTextBox.Text);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Login Successful");
                        // Fetch the email and role
                        string userEmail = reader["email"].ToString();
                        string userRole = reader["role"].ToString();
                        // Hide the login form
                        this.Hide();
                        // Open the Announcements Form
                        Form2 announcementsForm = new Form2(userEmail, userRole);
                        announcementsForm.ShowDialog();
                        // Close the login form
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

    }
}
