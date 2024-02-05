using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TTF_Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] fileContent;
        private string[] updatedContent;
      
        public MainWindow()
        {
            
            InitializeComponent();
        }
        // Button to Import File (only *.csv)
        public void ButtonClickEventToBrowseFiles(object sender, RoutedEventArgs e)
        {
            // Perform the action you want when the button is clicked
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                 fileContent = File.ReadAllLines(selectedFilePath);

                TextBoxFileContent.Text = string.Join(Environment.NewLine, fileContent);


            }
        }

        //Button to search using sequential Search
        private void ButtonClickEventToSearch(object sender, RoutedEventArgs e)
        {
            // Perform the action you want when the button is clicked
            if (fileContent != null && fileContent.Length > 0)
            {
                string searchTerm = Search.Text.ToLower(); // Convert to lowercase for case-insensitive search

              

                // Perform sequential search
               var searchResults = SequentialSearch(fileContent, searchTerm);

                // Display search results
                TextBoxFileSearchResult.Text = $"Search Results:{Environment.NewLine}{string.Join(Environment.NewLine, searchResults)}";
            }
            else
            {
                MessageBox.Show("Please open a file before searching.");
            }

        }

        // Button to search using Binary Search
        private void ButtonClickEventToBinarySearch(object sender, RoutedEventArgs e)
        {
            // Perform the action you want when the button is clicked
            if (fileContent != null && fileContent.Length > 0)
            {
                string searchTerm = Search.Text.ToLower(); // Convert to lowercase for case-insensitive search

                // Perform search using LINQ
                // var searchResults = fileContent.Where(line => line.ToLower().Contains(searchTerm)).ToList();

                // Perform Binary search
                var searchResults = BinarySearch(fileContent, searchTerm);

                // Display search results
                TextBoxFileSearchResult.Text = $"Search Results:{Environment.NewLine}{string.Join(Environment.NewLine, searchResults)}";
            }
            else
            {
                MessageBox.Show("Please open a file before searching.");
            }

        }

        private void TextBoxForFileContent(object sender, RoutedEventArgs e)
        { 

        }
        private void TextBoxForFileSearchResult(object sender, RoutedEventArgs e)
        {

        }
        private void AddingNewDataToFile(object sender, RoutedEventArgs e)
        {

        }


        // Button for deletation
        private void DeleteFileContent(object sender, RoutedEventArgs e) {
            if (fileContent != null && fileContent.Length > 0)
            {
                // Assuming you have a selected index to delete, replace this with your actual deletion logic
                int selectionStart = TextBoxFileContent.SelectionStart;
                int selectionLength = TextBoxFileContent.SelectionLength;

                if (selectionLength > 0)
                {
                    // Get selected text
                    string selectedText = TextBoxFileContent.Text.Substring(selectionStart, selectionLength);


                    // Delete the record at the selected index

                    updatedContent = fileContent.Where(line => !line.Trim().Equals(selectedText)).ToArray();
                           TextBoxFileContent.Text = string.Join(Environment.NewLine, updatedContent);

                    // Update the displayed content
                    TextBoxFileContent.Text = string.Join(Environment.NewLine, updatedContent);

                    // Also update the search result text
                    TextBoxFileSearchResult.Text = $"Search Results:{Environment.NewLine}{TextBoxFileContent.Text}";
                }
                else
                {
                    MessageBox.Show("Please select a valid record to delete.");
                }
            }
            else
            {
                MessageBox.Show("Please open a file before deleting a record.");
            }
        }

        //Button for deletion using Algorithm
        private void DeleteFileContentUsingDeletationAlgo(object sender, RoutedEventArgs e)
        {
            if (fileContent != null && fileContent.Length > 0)
            {
                // Assuming you have a selected index to delete, replace this with your actual deletion logic
                int selectionStart = TextBoxFileContent.SelectionStart;
                int selectionLength = TextBoxFileContent.SelectionLength;

                if (selectionLength > 0)
                {
                    // Get selected text
                    string selectedText = TextBoxFileContent.Text.Substring(selectionStart, selectionLength);
                    string searchTerm = Search.Text.ToLower();
                    // Find the index of the selected record
                    int selectedIndex = Array.IndexOf(fileContent, selectedText.Trim());


                    // Delete the record at the selected index

                    updatedContent = DeleteRecord(fileContent, selectedIndex);

                    // Update the displayed content
                    TextBoxFileContent.Text = string.Join(Environment.NewLine, updatedContent);

                    // Also update the search result text
                    // Perform search using LINQ
                    var searchResults = updatedContent.Where(line => line.ToLower().Contains(searchTerm)).ToList();
                    TextBoxFileSearchResult.Text = $"Search Results:{Environment.NewLine}{searchResults}";
                }
                else
                {
                    MessageBox.Show("Please select a valid record to delete.");
                }
            }
            else
            {
                MessageBox.Show("Please open a file before deleting a record.");
            }
        }





        //Button to add to records
        private void ButtonClickEventToAdd(object sender, RoutedEventArgs e)
        {

            if (fileContent != null)
            {
                // Get the content from the AddNewDataToFile TextBox
                string newRecordData = AddNewDataToFile.Text;

                // Check if the TextBox is not empty
                if (!string.IsNullOrWhiteSpace(newRecordData))
                {
                    // Add the new record to the array
                    List<string> updatedContent = fileContent.ToList();
                    updatedContent.Add(newRecordData);
                    fileContent = updatedContent.ToArray();

                    // Update the displayed content
                    TextBoxFileContent.Text = string.Join(Environment.NewLine, fileContent);
                }
                else
                {
                    MessageBox.Show("Please enter data in the AddNewDataToFile TextBox.");
                }
            }
            else
            {
                MessageBox.Show("Please open a file before adding a record.");
            }

        }

        //Button For add to record using insertion
        private void ButtonClickEventToAddUsingInsertion(object sender, RoutedEventArgs e)
        {

            if (fileContent != null)
            {
                // Get the content from the AddNewDataToFile TextBox
                string newRecordData = AddNewDataToFile.Text;

                // Check if the TextBox is not empty
                if (!string.IsNullOrWhiteSpace(newRecordData))
                {
                    // Add the new record to the array
                    updatedContent = InsertRecord(fileContent, newRecordData);

                    // Update the displayed content
                    TextBoxFileContent.Text = string.Join(Environment.NewLine, updatedContent);
                }
                else
                {
                    MessageBox.Show("Please enter data in the AddNewDataToFile TextBox.");
                }
            }
            else
            {
                MessageBox.Show("Please open a file before adding a record.");
            }

        }



        // Button to export as *.csv only
        private void ButtonClickEventToExport(object sender, RoutedEventArgs e)
        {
            if (fileContent != null && fileContent.Length > 0)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string exportFilePath = saveFileDialog.FileName;

                    try
                    {
                        // Write the updated records to the selected file
                        File.WriteAllLines(exportFilePath, updatedContent);
                        MessageBox.Show("Records exported successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error exporting records: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("No records to export. Please open a file and add records before exporting.");
            }
        }




        // Sequential Search
        private IEnumerable<string> SequentialSearch(string[] content, string target)
        {
            // Convert the search term to lowercase for case-insensitive search
            string searchTerm = target.ToLower();

            // Perform sequential search
            var searchResults = content
                .Where(line => line.ToLower().Contains(searchTerm))
                .ToList();

            return searchResults;
        }






        //Binary Search
        private IEnumerable<string> BinarySearch(string[] content, string target)
        {
            // Convert the search term to lowercase for case-insensitive search
            string searchTerm = target.ToLower();

            // Sort the content array (assuming each line represents a record)
            Array.Sort(content, StringComparer.InvariantCultureIgnoreCase);

            // Perform binary search
            int low = 0;
            int high = content.Length - 1;
            var searchResults = new List<string>();

            while (low <= high)
            {
                int mid = (low + high) / 2;
                int compareResult = content[mid].ToLower().CompareTo(searchTerm);

                if (compareResult == 0)
                {
                    // Match found at mid
                    searchResults.Add(content[mid]);
                    break;
                }
                else if (compareResult < 0)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }

            return searchResults;
        }






        // Insertion algorithm
        private string[] InsertRecord(string[] content, string newRecord)
        {
            // Create a new array with increased length
            string[] newContent = new string[content.Length + 1];

            // Copy existing records to the new array
            Array.Copy(content, newContent, content.Length);

            // Add the new record at the end
            newContent[newContent.Length - 1] = newRecord;

            return newContent;
        }







        // Deletion algorithm
        private string[] DeleteRecord(string[] content, int indexToDelete)
        {
            // Check if the index is valid
            if (indexToDelete >= 0 && indexToDelete < content.Length)
            {
                // Create a new array with decreased length
                string[] newContent = new string[content.Length - 1];

                // Copy records before the deleted record
                Array.Copy(content, 0, newContent, 0, indexToDelete);

                // Copy records after the deleted record
                Array.Copy(content, indexToDelete + 1, newContent, indexToDelete, content.Length - indexToDelete - 1);

                return newContent;
            }
            else
            {
                return content; // Invalid index, return the original array
            }
        }

    }
}
