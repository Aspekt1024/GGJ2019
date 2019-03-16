using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

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
        public ScoreItem(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public string name;
        public int score;
    }

    public class ScoreData
    {
        private const int NUM_SCORES = 10;
        private const string SCORES_FILENAME = "scores.json";

        private DataManager manager;

        public List<ScoreItem> Scores { get; private set; } = new List<ScoreItem>();

        public ScoreData(DataManager manager)
        {
            this.manager = manager;

            LoadDummyScores();
            Load();
        }

        public List<ScoreItem> GetScores()
        {
            return Scores;
        }

        public void PostNewScore(string name, int score)
        {
            var newScore = new ScoreItem { name = name, score = score };
            AddScore(newScore);
            manager.StartCoroutine(PostScoreRoutine(newScore));
        }

        public bool IsNewHighscore(int score)
        {
            if (Scores.Count < NUM_SCORES)
            {
                return true;
            }
            else if (score >= Scores[NUM_SCORES - 1].score)
            {
                return true;
            }
            return false;
        }

        private void AddScore(ScoreItem newScore)
        {
            if (Scores.Count < NUM_SCORES)
            {
                Scores.Add(newScore);
            }
            else if (newScore.score >= Scores[NUM_SCORES - 1].score)
            {
                Scores[NUM_SCORES - 1] = newScore;
            }
            else
            {
                return;
            }

            Scores = Scores.OrderByDescending(x => x.score).ToList();
        }

        private IEnumerator PostScoreRoutine(ScoreItem newScore)
        {
            string uri = "https://qbk1zvwag3.execute-api.ap-southeast-2.amazonaws.com/v1";
            string resource = "scores";

            string json = JsonUtility.ToJson(newScore);
            var req = new UnityWebRequest(uri + "/" + resource, "POST");
            byte[] jsonBytes = new System.Text.UTF8Encoding().GetBytes(json);
            req.uploadHandler = new UploadHandlerRaw(jsonBytes);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            
            yield return req.SendWebRequest();

            if (req.isHttpError || req.isNetworkError)
            {
                Debug.Log("error while saving: " + req.error);
                yield break;
            }
            Debug.Log("new score saved to mongo: " + newScore.name + " : " + newScore.score);
        }

        public void Load()
        {
            manager.StartCoroutine(LoadRoutine());
        }

        private IEnumerator LoadRoutine()
        {
            string uri = "https://qbk1zvwag3.execute-api.ap-southeast-2.amazonaws.com/v1";
            string resource = "scores";
            UnityWebRequest req = UnityWebRequest.Get(uri + "/" + resource);
            yield return req.SendWebRequest();

            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log("network/http error: " + req.error);
                yield break;
            }

            string json = req.downloadHandler.text;
            Debug.Log(json);
            ScoreCollection data = JsonUtility.FromJson<ScoreCollection>(json);
            foreach (var score in data.scores)
            {
                AddScore(score);
            }
        }

        private void LoadDummyScores()
        {
            Scores.Add(new ScoreItem("Shakespaw", 9500));
            Scores.Add(new ScoreItem("Meowser", 9000));
            Scores.Add(new ScoreItem("Clawdia", 8500));
            Scores.Add(new ScoreItem("Mew2", 8000));
            Scores.Add(new ScoreItem("Will Feral", 7500));
            Scores.Add(new ScoreItem("Cat Damon", 7000));
            Scores.Add(new ScoreItem("Picatso", 6500));
            Scores.Add(new ScoreItem("Mew", 6000));
            Scores.Add(new ScoreItem("Catsanova", 5500));
            Scores.Add(new ScoreItem("Hello", 5000));
        }
    }
}
