using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Diagnostics;


namespace Timing_software
{
    public partial class Form1 : Form
    {
        private int startingNumber;
        DataTable dataTable;

        private Process cmdProcess;

        // Global variable to store tags encountered between button clicks
        private List<string> previousTagEntries = new List<string>();
        public Form1()
        {
            InitializeComponent();
            InitializeDataTable();

            //filter clear
            clearparticipants.Click += clearparticipants_Click;

            //gender selector
            comboBoxFilterGender.SelectedIndexChanged += comboBoxFilterGender_SelectedIndexChanged;


        }
        private void InitializeDataTable()
        {
            // Create DataTable
            dataTable = new DataTable();
            dataTable.Columns.Add("bib", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("lastname", typeof(string));
            dataTable.Columns.Add("gender", typeof(string)); // Add Gender column
            dataTable.Columns.Add("birthday", typeof(DateTime));
            dataTable.Columns.Add("age", typeof(int));
            dataTable.Columns.Add("rfidbib", typeof(int));

            // Bind DataTable to DataGridView
            participantsgrid.DataSource = dataTable;

        }
        private void LoadDataFromFile(string filePath)
        {
            startingNumber = (int)startingrfid.Value;
            try
            {
                // Clear existing data
                dataTable.Rows.Clear();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split(',');
                        if (data.Length == 5)
                        {
                            DataRow row = dataTable.NewRow();
                            row["bib"] = data[0];
                            row["name"] = data[1];
                            row["lastname"] = data[2];
                            row["gender"] = data[3];

                            // Parse birthdate with different formats
                            string[] dateFormats = { "dd/MM/yyyy", "d/M/yyyy" };
                            DateTime birthdate;
                            if (DateTime.TryParseExact(data[4], dateFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthdate))
                            {
                                row["birthday"] = birthdate;
                                row["age"] = CalculateAge(birthdate);

                                // Generate and store random number
                                int randomNumber = GenerateRandomNumber();
                                row["rfidbib"] = randomNumber;

                                dataTable.Rows.Add(row);
                            }
                            else
                            {
                                MessageBox.Show($"Invalid date format: {data[4]}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private int GenerateRandomNumber()
        {
            int randomNumber = startingNumber;
            startingNumber++;
            return randomNumber;
        }
        private int CalculateAge(DateTime birthdate)
        {
            DateTime eventDate = eventDatePicker.Value;
            int age = eventDate.Year - birthdate.Year;
            if (eventDate < birthdate.AddYears(age))
            {
                age--;
            }
            return age;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
            {
                
            // Load data from text file
            openFileDialog1.Filter = "Text files (*.txt)|*.txt";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    LoadDataFromFile(openFileDialog1.FileName);
                }
        }

        private void clearparticipants_Click(object sender, EventArgs e)
        {
            // Clear the DataGridView
            dataTable.Rows.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Export data to text file
            saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportDataToTextFile(saveFileDialog1.FileName);
            }
        }
        private void ExportDataToTextFile(string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write column headers
                    string headers = string.Join(",", dataTable.Columns.Cast<DataColumn>().Select(column => column.ColumnName));
                    writer.WriteLine(headers);

                    // Write data rows
                    foreach (DataGridViewRow dgvRow in participantsgrid.Rows)
                    {
                        if (!dgvRow.IsNewRow) // Skip the new row if any
                        {
                            List<string> rowData = new List<string>();
                            foreach (DataGridViewCell cell in dgvRow.Cells)
                            {
                                if (cell.OwningColumn.Name == "birthday") // Format the date column
                                {
                                    rowData.Add(((DateTime)cell.Value).ToString("MM/dd/yyyy")); // or your desired format
                                }
                                else
                                {
                                    rowData.Add(cell.Value.ToString());
                                }
                            }
                            writer.WriteLine(string.Join(",", rowData));
                        }
                    }
                }
                MessageBox.Show("Data exported successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ProcessTags();
        }

        private void ProcessTags()
        {
            try
            {
                // Create a new DataTable for the new DataGridView
                DataTable newDataGridDataTable = new DataTable();
                newDataGridDataTable.Columns.Add("Participant ID", typeof(string));
                newDataGridDataTable.Columns.Add("First Name", typeof(string));
                newDataGridDataTable.Columns.Add("Last Name", typeof(string));
                newDataGridDataTable.Columns.Add("RFID", typeof(string));
                newDataGridDataTable.Columns.Add("Birthdate", typeof(DateTime)); // Add Birthdate column
                newDataGridDataTable.Columns.Add("Age", typeof(int)); // Add Age column
                newDataGridDataTable.Columns.Add("Finish Time", typeof(TimeSpan)); // Change type to TimeSpan
                newDataGridDataTable.Columns.Add("Gender", typeof(string)); // Add Gender column

                // Read the log file using FileStream with file-sharing mode to allow reading even when the file is open by another app
                string filePath = @"output.txt";
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    // Maintain a list of processed participant IDs
                    HashSet<string> processedParticipantIds = new HashSet<string>();

                    // Read each line and process tag entries
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains("tag:"))
                        {
                            string[] parts = line.Split(' '); // Split line by spaces
                            if (parts.Length >= 2) // Ensure at least two parts after splitting by spaces
                            {
                                string rfidPart = parts[0].Split(':')[1].Trim().Substring(20); // Extract the RFID part after the first 20 characters
                                long rfidNumber = long.Parse(rfidPart);
                                long participantId = rfidNumber; // Participant ID is now the remaining part of the RFID

                                // Check if participant ID has already been processed
                                if (!processedParticipantIds.Contains(participantId.ToString()))
                                {
                                    // Find the participant in participantsgrid DataTable based on Participant ID
                                    DataRow[] foundRows = dataTable.Select($"rfidbib = '{rfidPart}'");

                                    if (foundRows.Length > 0)
                                    {
                                        DataRow participantRow = foundRows[0];
                                        string firstName = participantRow["name"].ToString();
                                        string lastName = participantRow["lastname"].ToString();
                                        DateTime birthdate = (DateTime)participantRow["birthday"]; // Get birthdate from participantsgrid DataTable

                                        // Extract finish time from the last part of the line (time part only)
                                        string[] timeParts = parts[1].Split(':');
                                        int hour, minute, second;
                                        if (timeParts.Length == 3 &&
                                            int.TryParse(timeParts[0], out hour) &&
                                            int.TryParse(timeParts[1], out minute) &&
                                            int.TryParse(timeParts[2], out second))
                                        {
                                            TimeSpan finishTime = new TimeSpan(hour, minute, second);

                                            // Calculate age based on birthdate and current date
                                            DateTime selectedDate = eventDatePicker.Value;
                                            int age = selectedDate.Year - birthdate.Year;
                                            if (selectedDate < birthdate.AddYears(age))
                                            {
                                                age--;
                                            }
                                            // Add Gender information
                                            string gender = participantRow["gender"].ToString();

                                            // Add a row to newDataGridDataTable
                                            newDataGridDataTable.Rows.Add(participantId, firstName, lastName, rfidPart, birthdate, age, finishTime, gender);

                                            // Add participant ID to processed set
                                            processedParticipantIds.Add(participantId.ToString());
                                        }
                                        else
                                        {
                                            MessageBox.Show($"Failed to parse finish time from line: {line}");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"No participant found for RFID: {rfidPart}");
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Invalid line format: {line}");
                            }
                        }
                    }
                }

                // Bind newDataGridDataTable to the new DataGridView
                dataTableTags.DataSource = newDataGridDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the data table from the data source of dataGridViewTags
                DataTable dataTable = (DataTable)dataTableTags.DataSource;

                // Create a StringBuilder to hold the HTML content
                StringBuilder htmlBuilder = new StringBuilder();

                // Append HTML headers
                htmlBuilder.AppendLine("<!DOCTYPE html>");
                htmlBuilder.AppendLine("<html>");
                htmlBuilder.AppendLine("<head>");
                htmlBuilder.AppendLine("<title>Exported Data</title>");
                htmlBuilder.AppendLine("<meta charset=\"UTF-8\">"); // Add meta charset tag
                htmlBuilder.AppendLine("<style>");
                htmlBuilder.AppendLine("table {");
                htmlBuilder.AppendLine("  border-collapse: collapse;");
                htmlBuilder.AppendLine("  width: 100%;");
                htmlBuilder.AppendLine("  font-family: Arial, sans-serif;"); // Adjust font family as needed
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("th, td {");
                htmlBuilder.AppendLine("  text-align: left;");
                htmlBuilder.AppendLine("  padding: 12px;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("th {");
                htmlBuilder.AppendLine("  background-color: #f1f1f1;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("tr:nth-child(even) {");
                htmlBuilder.AppendLine("  background-color: #f9f9f9;");
                htmlBuilder.AppendLine("}");
                htmlBuilder.AppendLine("</style>");
                htmlBuilder.AppendLine("</head>");
                htmlBuilder.AppendLine("<body>");
                htmlBuilder.AppendLine("<table>");

                // Append table headers
                htmlBuilder.AppendLine("<tr>");
                htmlBuilder.AppendLine("<th>Number</th>");
                htmlBuilder.AppendLine("<th>First Name</th>");
                htmlBuilder.AppendLine("<th>Last Name</th>");
                htmlBuilder.AppendLine("<th>Gender</th>");
                htmlBuilder.AppendLine("<th>Finish Time</th>");
                htmlBuilder.AppendLine("</tr>");

                // Append table data
                int number = 1;
                foreach (DataRow row in dataTable.Rows)
                {
                    htmlBuilder.AppendLine("<tr>");
                    htmlBuilder.AppendLine($"<td>{number}</td>"); // Auto-increment number starting from 1
                    htmlBuilder.AppendLine($"<td>{row["First Name"]}</td>");
                    htmlBuilder.AppendLine($"<td>{row["Last Name"]}</td>");
                    htmlBuilder.AppendLine($"<td>{row["Gender"]}</td>");
                    htmlBuilder.AppendLine($"<td>{row["Finish Time"]}</td>");
                    htmlBuilder.AppendLine("</tr>");
                    number++;
                }

                // Close HTML tags
                htmlBuilder.AppendLine("</table>");
                htmlBuilder.AppendLine("</body>");
                htmlBuilder.AppendLine("</html>");

                // Save the HTML content to a file
                string filePath = @"D:\rfid testing\exported_data.html";
                File.WriteAllText(filePath, htmlBuilder.ToString());

                MessageBox.Show("Data exported to HTML successfully. File saved as: " + filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private void comboBoxFilterGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // Get the data table from the data source of dataTableTags
                DataTable dataTable = (DataTable)dataTableTags.DataSource;

                // Set filter criteria based on gender selected in the ComboBox
                string filterCriteria = "";

                // Check if the selected item is not "All"
                if (comboBoxFilterGender.SelectedItem != null && comboBoxFilterGender.SelectedItem.ToString() != "All")
                {
                    string selectedGender = comboBoxFilterGender.SelectedItem.ToString();
                    filterCriteria = $"Gender = '{selectedGender}'"; // Filter by selected gender
                }

                // Apply filter directly to the DataTable
                dataTable.DefaultView.RowFilter = filterCriteria;

                // Refresh the DataGridView to reflect the changes
                dataTableTags.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBoxMinAge_TextChanged(object sender, EventArgs e)
        {
            ApplyAgeFilter();
        }
        private void ApplyAgeFilter()
        {
            try
            {
                // Get the data table from the data source of dataTableTags
                DataTable dataTable = (DataTable)dataTableTags.DataSource;

                // Set filter criteria based on age range specified by the user
                string filterCriteria = "";

                // Check if both input numbers are valid
                if (int.TryParse(textBoxMinAge.Text, out int minAge) && int.TryParse(textBoxMaxAge.Text, out int maxAge))
                {
                    // Filter by age range
                    filterCriteria = $"Age >= {minAge} AND Age <= {maxAge}";
                }
                else
                {
                    // Clear filter if input numbers are invalid
                    dataTable.DefaultView.RowFilter = "";
                    dataTableTags.Refresh();
                    return; // Exit the method if input numbers are invalid
                }

                // Apply filter directly to the DataTable
                dataTable.DefaultView.RowFilter = filterCriteria;

                // Skip the first 3 rows (if there are more than 3 rows)
                if (dataTable.DefaultView.Count > 3)
                {
                    dataTable.DefaultView.RowFilter += $" AND RowNumber > 3";
                }

                // Add a row number column to the DataTable
                if (!dataTable.Columns.Contains("RowNumber"))
                {
                    dataTable.Columns.Add("RowNumber", typeof(int));
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        dataTable.Rows[i]["RowNumber"] = i + 1;
                    }
                }

                // Refresh the DataGridView to reflect the changes
                dataTableTags.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            }

        private void textBoxMaxAge_TextChanged(object sender, EventArgs e)
        {
            ApplyAgeFilter();
        }

        private void filterclear_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the data table from the data source of dataTableTags
                DataTable dataTable = (DataTable)dataTableTags.DataSource;

                // Clear all filters applied to the DataTable
                dataTable.DefaultView.RowFilter = "";

                // Refresh the DataGridView to show unfiltered data
                dataTableTags.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void publish_Click(object sender, EventArgs e)
        {
            try
            {
                string ftpUrl = "ftp://racetime.gr/"; // FTP server URL
                string username = "racetime_results_upload"; // FTP username
                string password = "nxWk14~71"; // FTP password

                string eventName = eventname.Text.Trim(); // Get the value from the eventname textbox

                if (string.IsNullOrEmpty(eventName))
                {
                    MessageBox.Show("Please enter an event name.");
                    return;
                }

                string filePath = @"D:\rfid testing\exported_data.html"; // Path of the file to upload
                string ftpFilePath = $"{ftpUrl}/{eventName}.html"; // Construct the file path on the FTP server using the event name

                // Upload file to FTP server
                UploadFileToFtp(filePath, ftpFilePath, username, password);

                MessageBox.Show("File uploaded successfully with name: " + eventName + ".html");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void UploadFileToFtp(string filePath, string ftpUrl, string username, string password)
        {
            try
            {
                // Read file data as bytes
                byte[] fileContents = File.ReadAllBytes(filePath);

                // Create FTP request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                // Upload file data
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(fileContents, 0, fileContents.Length);
                }

                // Get FTP response
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error uploading file: " + ex.Message);
            }
        }


        private void newtagcheck_Click(object sender, EventArgs e)
        {
            string filePath = @"output.txt";

            try
            {
                // Open the file with read access and file-sharing mode to allow reading even when the file is open by another app
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (StreamReader sr = new StreamReader(fs))
                {
                    List<string> tagEntries = new List<string>();

                    // Read each line and process tag entries
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.StartsWith("tag:"))
                        {
                            string tagEntry = line.Split(':')[1].Trim().Substring(20).Split(' ')[0];
                            tagEntries.Add(tagEntry);
                        }
                    }

                    // Count unique tag entries
                    int uniqueTagCount = tagEntries.Distinct().Count();

                    // Check for duplicates
                    var duplicates = tagEntries.GroupBy(tag => tag).Where(group => group.Count() > 1).Select(group => group.Key).ToList();

                    // Calculate new tags since last click
                    var newTags = tagEntries.Except(previousTagEntries).ToList();
                    int newTagCount = newTags.Count();

                    // Update previousTagEntries for next click
                    previousTagEntries.AddRange(newTags);

                    // Display results
                    string message = $"Total unique tag entries: {uniqueTagCount}\nNew unique tag entries since last click: {newTagCount}";
                    if (newTags.Any())
                    {
                        message += $"\n\nNew tag entries:\n{string.Join("\n", newTags)}";
                    }
                    MessageBox.Show(message,
                                    "Tag Entries Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while processing the file: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private bool autoUpdateEnabled = false;
        private Task autoUpdateTask;
        private CancellationTokenSource cancellationTokenSource;


        private async void autoupdate_CheckedChanged(object sender, EventArgs e)
        {
            if (autoupdate.Checked)
            {
                while (autoupdate.Checked)
                {
                    ProcessTags();
                    await Task.Delay(5000); // Delay for 5 seconds
                }
            }
        }

        private void racestart_Click(object sender, EventArgs e)
        {
            //opens the timing html
            string pathToHtmlFile = @"D:\rfid testing\index.html";
            Process.Start(pathToHtmlFile);
        }

        private void button4_Click(object sender, EventArgs e)
        {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"D:\rfid testing\telnet.exe";
                startInfo.Arguments = "192.168.1.150"; // Replace "192.168.1.150" with the actual IP address
                Process.Start(startInfo);
        }
    }
}
