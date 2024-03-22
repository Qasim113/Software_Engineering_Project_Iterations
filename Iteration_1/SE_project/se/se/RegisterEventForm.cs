using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public partial class RegisterEventForm : Form
{
    private string userEmail;
    private TextBox titleTextBox; // Correctly declared at class level
    private TextBox descriptionTextBox; // Correctly declared at class level

    public RegisterEventForm(string email)
    {
        userEmail = email;
        InitializeFormComponents();
    }

    private void InitializeFormComponents()
    {
        // Form properties
        this.Text = "Register New Event";
        this.Size = new Size(400, 250); // Adjust the size as needed

        // Title label and textbox
        Label titleLabel = new Label
        {
            Text = "Event Title:",
            Location = new Point(10, 20),
            AutoSize = true
        };

        titleTextBox = new TextBox
        {
            Location = new Point(120, 20),
            Size = new Size(250, 20)
        };

        // Description label and textbox
        Label descriptionLabel = new Label
        {
            Text = "Description:",
            Location = new Point(10, 50),
            AutoSize = true
        };

        descriptionTextBox = new TextBox
        {
            Location = new Point(120, 50),
            Size = new Size(250, 100), // Adjust size as needed
            Multiline = true,
            ScrollBars = ScrollBars.Vertical
        };

        // Submit button
        Button submitButton = new Button
        {
            Text = "Submit",
            Location = new Point(120, 160)
        };
        submitButton.Click += new EventHandler(SubmitButton_Click);

        // Adding controls to the form
        this.Controls.Add(titleLabel);
        this.Controls.Add(titleTextBox);
        this.Controls.Add(descriptionLabel);
        this.Controls.Add(descriptionTextBox);
        this.Controls.Add(submitButton);
    }

    private void SubmitButton_Click(object sender, EventArgs e)
    {
        string title = titleTextBox.Text.Trim();
        string description = descriptionTextBox.Text.Trim();

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
        {
            MessageBox.Show("Please fill in all fields.");
            return;
        }

        string connectionString = "server=localhost;database=se;uid=root;pwd=1234567;";
        using (MySqlConnection con = new MySqlConnection(connectionString))
        {
            try
            {
                con.Open();
                string sql = $"INSERT INTO Events (society_id, title, description, status, created_by) VALUES (@SocietyId, @Title, @Description, 'pending', (SELECT user_id FROM Users WHERE email = @UserEmail))";

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@SocietyId", 1); // Placeholder for society_id, adjust as necessary
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@UserEmail", userEmail);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Event registered successfully. Status is pending.");
                        this.Close(); // Optionally close the form
                    }
                    else
                    {
                        MessageBox.Show("Failed to register event.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering event: {ex.Message}");
            }

        }
    }
}
