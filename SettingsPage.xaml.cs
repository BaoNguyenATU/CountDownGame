using CountDownGame.Services;
using Microsoft.Maui.Controls;

namespace CountDownGame
{
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsService _settingsService;

        public SettingsPage()
        {
            InitializeComponent(); // Ensure this is present to initialize components
            _settingsService = new SettingsService();
            LoadSettings();
        }

        // Method to load settings when the page is initialized
        private void LoadSettings()
        {
            DarkModeSwitch.IsToggled = _settingsService.IsDarkModeEnabled();
            RoundsSlider.Value = _settingsService.GetNumberOfRounds();
            RoundsLabel.Text = $"Rounds: {_settingsService.GetNumberOfRounds()}";
        }

        // Event handler for Dark Mode toggle switch
        private void OnDarkModeToggled(object sender, ToggledEventArgs e)
        {
            _settingsService.SetDarkMode(DarkModeSwitch.IsToggled);
        }

        // Event handler for Rounds slider value change
        private void OnRoundsSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            int rounds = (int)e.NewValue;
            RoundsLabel.Text = $"Rounds: {rounds}";
            _settingsService.SetNumberOfRounds(rounds);
        }
    }
}
