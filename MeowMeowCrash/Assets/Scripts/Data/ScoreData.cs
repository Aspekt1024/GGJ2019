using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace RobotCat.Data
{
    [Serializable]
    public struct ScoreCollection
    {
        public ScoreItem[] scores;
    }

    [Serializable]
    public struct ScoreItem
    {
        public string name;
        public int score;
    }

    public class ScoreData
    {
        private const int NUM_SCORES = 10;
        private const string SCORES_FILENAME = "scores.json";

        public List<ScoreItem> Scores { get; private set; } = new List<ScoreItem>();

        public ScoreData()
        {
            LoadDummyScores();
            Load();
        }

        public void AddScore(string name, int score, bool skipSave = false)
        {
            var newScore = new ScoreItem()
            {
                name = name,
                score = score,
            };

            if (Scores.Count < NUM_SCORES)
            {
                Scores.Add(newScore);
            }
            else if (score >= Scores[NUM_SCORES - 1].score)
            {
                Scores[NUM_SCORES - 1] = newScore;
            }
            else
            {
                return;
            }

            Scores = Scores.OrderByDescending(x => x.score).ToList();

            if (skipSave) return;

            Save();
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(Scores);



            //string filePath = Path.Combine(Application.persistentDataPath, SCORES_FILENAME);
            //File.WriteAllText(filePath, json);
        }

        public void Load()
        {
            string json = "{\"scores\" : [{\"name\":\"dan\",\"score\":11000},{\"name\":\"dan2\",\"score\":10020}]}";
            ScoreCollection data = JsonUtility.FromJson<ScoreCollection>(json);
            foreach (var score in data.scores)
            {
                AddScore(score.name, score.score, skipSave: true);
            }

            //string filePath = Path.Combine(Application.persistentDataPath, SCORES_FILENAME);
            //if (File.Exists(filePath))
            //{
            //    string json = File.ReadAllText(filePath);
        }

        private void LoadDummyScores()
        {
            AddScore("Shakespaw", 9500);
            AddScore("Meowser", 9000);
            AddScore("Clawdia", 8500);
            AddScore("Mew2", 8000);
            AddScore("Will Feral", 7500);
            AddScore("Cat Damon", 7000);
            AddScore("Picatso", 6500);
            AddScore("Mew", 6000);
            AddScore("Catsanova", 5500);
            AddScore("Hello", 5000);
        }
    }
}
