using System;

namespace CountDownGame.Models
{
    // Represents the history of a game session
    public class GameHistory
    {
        // Name of Player 1
        public string Player1Name { get; set; } = string.Empty;

        // Score of Player 1
        public int Player1Score { get; set; }

        // Name of Player 2
        public string Player2Name { get; set; } = string.Empty;

        // Score of Player 2
        public int Player2Score { get; set; }

        // Timestamp of when the game was played
        public DateTime Timestamp { get; set; }

        // Override ToString method to provide a readable format for game history
        public override string ToString()
        {
            return $"{Timestamp:G} - {Player1Name}: {Player1Score} vs {Player2Name}: {Player2Score}";
        }
    }
}
