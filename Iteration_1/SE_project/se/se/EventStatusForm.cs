using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class EventStatusForm : Form
{
    private ListBox eventsListBox = new ListBox();
    private string userEmail;

    public EventStatusForm(string email)
    {
        userEmail = email;
        InitializeComponent();
        LoadEventStatuses();
    }

    private void InitializeComponent()
    {
        this.Text = "Event Status";
        this.Size = new System.Drawing.Size(400, 300);

        eventsListBox.Location = new System.Drawing.Point(10, 10);
        eventsListBox.Size = new System.Drawing.Size(360, 240);

        this.Controls.Add(eventsListBox);
    }

    private void LoadEventStatuses()
    {
        string connectionString = "server=localhost;database=se;uid=root;pwd=1234567;";
        using (MySqlConnection con = new MySqlConnection(connectionString))
        {
            try
            {
                con.Open();
                string sql = @"SELECT title, status FROM Events 
                               WHERE created_by = (SELECT user_id FROM Users WHERE email = @Email)";

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader["title"].ToString();
                            string status = reader["status"].ToString();
                            eventsListBox.Items.Add($"{title} - {status}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading event statuses: {ex.Message}");
            }
        }
    }
}
