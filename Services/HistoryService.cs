// File: Services/HistoryService.cs

using CountDownGame.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CountDownGame.Services
{
    // This service handles loading and saving game history to a JSON file.
    public class HistoryService
    {
        private readonly string _historyFilePath;

        public HistoryService()
        {
            // Set the path for the game history file.
            _historyFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "gameHistory.json");
        }

        // Method to retrieve the game history from the JSON file.
        public List<GameHistory> GetGameHistory()
        {
            if (File.Exists(_historyFilePath))
            {
                var historyJson = File.ReadAllText(_historyFilePath);
                return JsonConvert.DeserializeObject<List<GameHistory>>(historyJson) ?? new List<GameHistory>();
            }
            return new List<GameHistory>();
        }

        // Method to save the current game history to the JSON file.
        public void SaveGameHistory(List<GameHistory> gameHistory)
        {
            var historyJson = JsonConvert.SerializeObject(gameHistory, Formatting.Indented);
            File.WriteAllText(_historyFilePath, historyJson); // Save the history to a file.
        }
    }
}
