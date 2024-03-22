using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

public class SocietyManagementForm : Form
{
    private ListBox eventsListBox = new ListBox();
    private Button approveButton = new Button();
    private Button rejectButton = new Button();
    private int selectedEventId = -1; // Default value indicating no selection

    public SocietyManagementForm()
    {
        InitializeComponent();
        LoadPendingEvents();
    }

    private void InitializeComponent()
    {
        this.Text = "Society Management";
        this.Size = new System.Drawing.Size(500, 300);

        eventsListBox.Location = new System.Drawing.Point(10, 10);
        eventsListBox.Size = new System.Drawing.Size(480, 200);
        eventsListBox.SelectedIndexChanged += (sender, e) => { selectedEventId = GetSelectedEventId(); };

        approveButton.Text = "Approve";
        approveButton.Location = new System.Drawing.Point(10, 220);
        approveButton.Click += new EventHandler(ApproveButtonClick);

        rejectButton.Text = "Reject";
        rejectButton.Location = new System.Drawing.Point(110, 220);
        rejectButton.Click += new EventHandler(RejectButtonClick);

        this.Controls.Add(eventsListBox);
        this.Controls.Add(approveButton);
        this.Controls.Add(rejectButton);
    }

    private void LoadPendingEvents()
    {
        eventsListBox.Items.Clear();
        string connectionString = "server=localhost;database=se;uid=root;pwd=1234567;";
        using (MySqlConnection con = new MySqlConnection(connectionString))
        {
            try
            {
                con.Open();
                string sql = "SELECT event_id, title FROM Events WHERE status = 'pending'";
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int eventId = reader.GetInt32("event_id");
                            string title = reader.GetString("title");
                            eventsListBox.Items.Add(new ListItem { Text = title, Value = eventId });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pending events: {ex.Message}");
            }
        }
    }

    private int GetSelectedEventId()
    {
        if (eventsListBox.SelectedItem is ListItem item)
        {
            return item.Value;
        }
        return -1; // Indicates no selection or an error
    }

    private void ApproveButtonClick(object sender, EventArgs e)
    {
        if (selectedEventId < 0)
        {
            MessageBox.Show("Please select an event.");
            return;
        }
        UpdateEventStatus(selectedEventId, "approved");
        LoadPendingEvents(); // Refresh the list
    }

    private void RejectButtonClick(object sender, EventArgs e)
    {
        if (selectedEventId < 0)
        {
            MessageBox.Show("Please select an event.");
            return;
        }
        UpdateEventStatus(selectedEventId, "rejected");
        LoadPendingEvents(); // Refresh the list
    }

    private void UpdateEventStatus(int eventId, string status)
    {
        string connectionString = "server=localhost;database=se;uid=root;pwd=1234567;";
        using (MySqlConnection con = new MySqlConnection(connectionString))
        {
            try
            {
                con.Open();
                string sql = "UPDATE Events SET status = @Status WHERE event_id = @EventId";

                if (status == "approved")
                {
                    // If approved, also insert an announcement
                    sql += "; INSERT INTO Announcements (content, posted_by, visible_to_all) SELECT title, created_by, TRUE FROM Events WHERE event_id = @EventId";
                }

                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@EventId", eventId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show($"Event has been {status}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating event status: {ex.Message}");
            }
        }
    }

    // Helper class to display items in the ListBox
    private class ListItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
