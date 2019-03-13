using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace RobotCat.Data
{
    public struct ScoreItem
    {
        public string Name;
        public int Score;
    }

    public class ScoreData
    {
        private const int NUM_SCORES = 10;
        private const string SCORES_FILENAME = "scores.json";

        public readonly List<ScoreItem> Scores = new List<ScoreItem>();

        public ScoreData()
        {
            LoadDummyScores();
            Load();
        }

        public void AddScore(string name, int score)
        {
            var newScore = new ScoreItem()
            {
                Name = name,
                Score = score,
            };

            if (Scores.Count < NUM_SCORES)
            {
                Scores.Add(newScore);
            }
            else if (score >= Scores[NUM_SCORES - 1].Score)
            {
                Scores[NUM_SCORES - 1] = newScore;
            }
            else
            {
                return;
            }

            Scores.OrderByDescending(x => x.Score);
            Save();
        }

        public void Save()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SCORES_FILENAME);
            string json = JsonUtility.ToJson(Scores);
            File.WriteAllText(filePath, json);
        }

        public void Load()
        {
            string filePath = Path.Combine(Application.persistentDataPath, SCORES_FILENAME);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var scoreData = JsonUtility.FromJson<List<ScoreItem>>(json);
                foreach (var score in scoreData)
                {
                    AddScore(score.Name, score.Score);
                }
            }
        }

        private void LoadDummyScores()
        {
            AddScore("Mew", 9500);
            AddScore("Meowser", 9000);
            AddScore("Clawdia", 8500);
            AddScore("Mew2", 8000);
            AddScore("Will Feral", 7500);
            AddScore("Cat Damon", 7000);
            AddScore("Picatso", 6500);
            AddScore("Shakespaw", 6000);
            AddScore("Catsanova", 5500);
            AddScore("Hello", 5000);
        }
    }
}
