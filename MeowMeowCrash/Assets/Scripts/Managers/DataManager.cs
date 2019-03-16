using RobotCat.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotCat
{
    public class DataManager : MonoBehaviour
    {
        private ScoreData scoreData;
        private string playername;

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
            scoreData = new ScoreData(this);
        }

        public bool IsNewHighscore(int score)
        {
            return scoreData.IsNewHighscore(score);
        }

        public void PostNewScore(string playerName, int score)
        {
            scoreData.PostNewScore(playerName, score);
        }

        public List<ScoreItem> GetScores()
        {
            return scoreData.GetScores();
        }
        
    }
}
