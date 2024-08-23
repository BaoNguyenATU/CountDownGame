using CountDownGame.Models;
using CountDownGame.Services;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Timers; // Explicitly using System.Timers.Timer

namespace CountDownGame
{
    public partial class MainPage : ContentPage
    {
        private readonly HistoryService _historyService;
        private readonly DictionaryService _dictionaryService;
        private System.Timers.Timer _timer; // Timer for the countdown
        private string _player1Name = string.Empty;
        private string _player2Name = string.Empty;
        private int _player1Score;
        private int _player2Score;
        private int _currentRound;
        private bool _isPlayer1Turn;

        public MainPage()
        {
            InitializeComponent();
            _historyService = new HistoryService();
            _dictionaryService = new DictionaryService();
            _timer = new System.Timers.Timer(1000); // 1 second interval for countdown
            _timer.Elapsed += OnTimerElapsed;
        }

        // Event handler when the game is started
        private void OnStartGameClicked(object sender, EventArgs e)
        {
            _player1Name = Player1NameEntry.Text;
            _player2Name = Player2NameEntry.Text;
            _player1Score = 0;
            _player2Score = 0;
            _currentRound = 1;
            _isPlayer1Turn = true;

            StartRound();
        }

        // Method to start a new round
        private void StartRound()
        {
            // Randomly select letters (consonants and vowels)
            string letters = GetRandomLetters();
            SelectedLetters.Text = letters;

            // Update game message based on the current player
            UpdateGameMessage($"Round {_currentRound}: {(_isPlayer1Turn ? _player1Name : _player2Name)}'s turn");

            // Start the countdown timer
            StartTimer();
        }

        // Method to generate random letters for the round
        private string GetRandomLetters()
        {
            // Implement logic to generate random letters (consonants and vowels)
            char[] letters = new char[9];
            Random rnd = new Random();

            // Simple example logic, should be expanded
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i] = (char)rnd.Next('A', 'Z' + 1);
            }

            return new string(letters);
        }

        // Method to start the countdown timer
        private void StartTimer()
        {
            _timer.Interval = 1000; // 1 second per tick
            _timer.Start();
        }

        // Event handler for the timer elapsed event
        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                int remainingTime = int.Parse(TimerLabel.Text) - 1;
                TimerLabel.Text = remainingTime.ToString();

                if (remainingTime <= 0)
                {
                    _timer.Stop();
                    OnTimerFinished();
                }
            });
        }

        // Method to handle the timer finishing
        private void OnTimerFinished()
        {
            // After the timer ends, submit the word for the current player
            OnSubmitWordClicked(this, EventArgs.Empty);
        }

        // Event handler when a word is submitted
        private void OnSubmitWordClicked(object sender, EventArgs e)
        {
            string submittedWord = Player1WordEntry.Text?.ToUpper();

            // Check if the word is valid
            if (_dictionaryService.IsValidWord(submittedWord))
            {
                int points = submittedWord.Length;

                // Award points to the current player
                if (_isPlayer1Turn)
                {
                    _player1Score += points;
                    UpdateGameMessage($"{_player1Name} scored {points} points!");
                }
                else
                {
                    _player2Score += points;
                    UpdateGameMessage($"{_player2Name} scored {points} points!");
                }
            }
            else
            {
                UpdateGameMessage($"{submittedWord} is not a valid word.");
            }

            // Switch turns and prepare for the next round or end the game
            _isPlayer1Turn = !_isPlayer1Turn;

            if (_currentRound >= 6)
            {
                EndGame();
            }
            else
            {
                _currentRound++;
                StartRound();
            }
        }

        // Method to update the game message
        private void UpdateGameMessage(string message)
        {
            GameMessage.Text = message;
        }

        // Method to end the game and save history
        private void EndGame()
        {
            UpdateGameMessage("Game over!");
            SaveGameHistory();
        }

        // Method to save the game history
        private void SaveGameHistory()
        {
            var gameHistory = _historyService.GetGameHistory();
            gameHistory.Add(new GameHistory
            {
                Player1Name = _player1Name,
                Player1Score = _player1Score,
                Player2Name = _player2Name,
                Player2Score = _player2Score,
                Timestamp = DateTime.Now
            });
            _historyService.SaveGameHistory(gameHistory);
        }
    }
}
