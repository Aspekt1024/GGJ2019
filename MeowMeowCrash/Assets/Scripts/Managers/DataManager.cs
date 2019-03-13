using RobotCat.Data;
using System.Collections.Generic;
using UnityEngine;

namespace RobotCat
{
    public class DataManager : MonoBehaviour
    {
        private ScoreData scoreData;

        private static DataManager instance;

        private void Awake()
        {
            if (instance != null)
            {
                instance = this;
                DestroyImmediate(gameObject);
                return;
            }

            DontDestroyOnLoad(this);
            Init();
        }

        private void Init()
        {
            scoreData = new ScoreData();
        }

        public void AddScore(string name, int score)
        {
            scoreData.AddScore(name, score);
        }

        public List<ScoreItem> GetScores()
        {
            return scoreData.Scores;
        }

        public void SaveData()
        {
            scoreData.Save();
        }
    }
}
