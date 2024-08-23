// File: HistoryPage.xaml.cs

using CountDownGame.Models;
using CountDownGame.Services;
using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace CountDownGame
{
    public partial class HistoryPage : ContentPage
    {
        private readonly HistoryService _historyService;

        public HistoryPage()
        {
            InitializeComponent();
            _historyService = new HistoryService();
            LoadHistory(); // Load the game history when the page is initialized.
        }

        // Method to load game history and display it on the page.
        private void LoadHistory()
        {
            List<GameHistory> gameHistories = _historyService.GetGameHistory();
            HistoryListView.ItemsSource = gameHistories; // Bind the game history list to the ListView.
        }
    }
}
