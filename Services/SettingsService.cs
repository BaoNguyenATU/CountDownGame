using Newtonsoft.Json;
using System;
using System.IO;

namespace CountDownGame.Services
{
    public class SettingsService
    {
        private readonly string _settingsPath;

        public SettingsService()
        {
            // Set path for the settings file
            _settingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "settings.json");
        }

        public bool IsDarkModeEnabled()
        {
            var settings = LoadSettings();
            return settings.DarkMode; // Return whether dark mode is enabled
        }

        public int GetNumberOfRounds()
        {
            var settings = LoadSettings();
            return settings.Rounds; // Return the number of rounds setting
        }

        public void SetDarkMode(bool enabled)
        {
            var settings = LoadSettings();
            settings.DarkMode = enabled;
            SaveSettings(settings); // Save the dark mode setting
        }

        public void SetNumberOfRounds(int rounds)
        {
            var settings = LoadSettings();
            settings.Rounds = rounds;
            SaveSettings(settings); // Save the number of rounds setting
        }

        private Settings LoadSettings()
        {
            if (File.Exists(_settingsPath))
            {
                var settingsJson = File.ReadAllText(_settingsPath);
                return JsonConvert.DeserializeObject<Settings>(settingsJson);
            }
            else
            {
                return new Settings { DarkMode = false, Rounds = 5 }; // Default settings
            }
        }

        private void SaveSettings(Settings settings)
        {
            var settingsJson = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_settingsPath, settingsJson); // Save settings to a file
        }
    }

    public class Settings
    {
        public bool DarkMode { get; set; } // Dark mode enabled/disabled
        public int Rounds { get; set; }    // Number of game rounds
    }
}
