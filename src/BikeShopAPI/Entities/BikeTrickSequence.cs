using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BikeShopAPI.Entities
{
    public class BikeTrickSequence
    {
        public List<Trick> Tricks { get; set; } = new List<Trick>();
        public double Difficulty { get; set; }

        public static BikeTrickSequence Parse(string bikeTrickSignature)
        {
            var bikeTrickSequence = new BikeTrickSequence();
            
            // Regex pattern to match trick format: Letter + Number + Letter (e.g., L4B, H2C, R3A)
            var trickPattern = @"([LHRST])(\d+)([A-E])";
            var tricks = Regex.Matches(bikeTrickSignature, trickPattern);

            foreach (Match trickMatch in tricks)
            {
                var actionLetter = trickMatch.Groups[1].Value;
                var repetitionCount = int.Parse(trickMatch.Groups[2].Value);
                var difficultyModifier = trickMatch.Groups[3].Value[0];

                var trick = new Trick
                {
                    ActionLetter = actionLetter,
                    Action = GetActionName(actionLetter),
                    RepetitionCount = repetitionCount,
                    DifficultyModifier = difficultyModifier
                };

                var score = CalculateScore(trick, bikeTrickSequence);
                trick.Score = score;
                
                bikeTrickSequence.Tricks.Add(trick);
                bikeTrickSequence.Difficulty += score;
            }

            bikeTrickSequence.Difficulty = Math.Round(bikeTrickSequence.Difficulty, 2);
            return bikeTrickSequence;
        }

        private static string GetActionName(string actionLetter)
        {
            return actionLetter switch
            {
                "L" => "360",
                "H" => "Tuck No-Hander",
                "R" => "Cash Roll",
                "S" => "Barspin",
                "T" => "Table",
                _ => throw new ArgumentException($"Unknown action letter: {actionLetter}")
            };
        }

        private static double GetDifficultyMultiplier(char difficultyModifier)
        {
            return difficultyModifier switch
            {
                'A' => 1.0,
                'B' => 1.2,
                'C' => 1.4,
                'D' => 1.6,
                'E' => 1.8,
                _ => throw new ArgumentException($"Invalid difficulty modifier: {difficultyModifier}")
            };
        }

        private static double CalculateScore(Trick currentTrick, BikeTrickSequence sequence)
        {
            var baseScore = currentTrick.RepetitionCount * GetDifficultyMultiplier(currentTrick.DifficultyModifier);
            var multiplier = 1.0;

            // Apply special scoring rules
            if (sequence.Tricks.Count > 0)
            {
                var previousTrick = sequence.Tricks[^1];
                
                // A Cash Roll after a 360 is scored double
                if (previousTrick.ActionLetter == "L" && currentTrick.ActionLetter == "R")
                {
                    multiplier = 2.0;
                }
                
                // A Barspin after a Table is scored triple
                if (previousTrick.ActionLetter == "T" && currentTrick.ActionLetter == "S")
                {
                    multiplier = 3.0;
                }
            }

            return baseScore * multiplier;
        }

        public class Trick
        {
            public string ActionLetter { get; set; } = string.Empty;
            public string Action { get; set; } = string.Empty;
            public int RepetitionCount { get; set; }
            public char DifficultyModifier { get; set; }
            public double Score { get; set; }
        }
    }
}