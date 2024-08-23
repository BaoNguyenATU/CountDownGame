using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CountDownGame.Services
{
    // This service handles loading the dictionary and validating words.
    public class DictionaryService
    {
        private List<string> _words = new List<string>(); // List to store the dictionary words.

        public DictionaryService()
        {
            // Load the dictionary when the service is initialized.
            LoadDictionary().Wait();
        }

        // Method to load the dictionary from a file or the internet.
        private async Task LoadDictionary()
        {
            // Define the path to the dictionary file in the local application data folder.
            string dictionaryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cdwords.txt");

            if (File.Exists(dictionaryPath))
            {
                // If the dictionary file exists locally, load it into the _words list.
                _words = new List<string>(await File.ReadAllLinesAsync(dictionaryPath));
            }
            else
            {
                // If the dictionary file doesn't exist locally, download it from the internet.
                using (var httpClient = new HttpClient())
                {
                    string dictionaryData = await httpClient.GetStringAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/main/cdwords.txt");

                    // Split the downloaded data into lines and add them to the _words list.
                    _words = new List<string>(dictionaryData.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries));

                    // Save the downloaded dictionary to a local file for future use.
                    await File.WriteAllLinesAsync(dictionaryPath, _words);
                }
            }
        }

        // Method to check if a word is valid.
        public bool IsValidWord(string word)
        {
            // Check if the word exists in the dictionary, ignoring case by converting to uppercase.
            return _words.Contains(word.ToUpper());
        }
    }
}
