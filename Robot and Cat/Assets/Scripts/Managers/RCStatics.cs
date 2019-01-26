﻿using RobotCat.UI;
using UnityEngine;

namespace RobotCat
{
    public class RCStatics
    {
        private static RCStatics instance;

        private GameManager gameManager;
        private ScoreManager score;
        private UIManager ui;

        public RCStatics(GameManager gameManager)
        {
            if (instance != null)
            {
                Debug.LogWarning($"Detected multiple versions of {nameof(RCStatics)}. This should only be created from the GameManager.");
            }

            instance = this;
            this.gameManager = gameManager;
            score = Object.FindObjectOfType<ScoreManager>();
            ui = Object.FindObjectOfType<UIManager>();
        }

        public void Tick()
        {
        }

        public static GameManager GameManager { get { return instance.gameManager; } }
        public static ScoreManager Score { get { return instance.score; } }
        public static UIManager UI { get { return instance.ui; } }

    }
}
