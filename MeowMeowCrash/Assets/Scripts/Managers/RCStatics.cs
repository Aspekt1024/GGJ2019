﻿using RobotCat.Audio;
using RobotCat.UI;
using UnityEngine;

namespace RobotCat
{
    public class RCStatics
    {
        private static RCStatics instance;

        private GameManager gameManager;
        private ScoreManager score;
        private UIManager ui;
        private MMCAudioManager audio;
        private SFXManager sfx;
        private SettingsManager settings;
        private DataManager data;

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
            sfx = Object.FindObjectOfType<SFXManager>();
        }

        // DontDestroyOnLoad objects go here
        public void OnStart()
        {
            audio = Object.FindObjectOfType<MMCAudioManager>();
            settings = Object.FindObjectOfType<SettingsManager>();
            data = Object.FindObjectOfType<DataManager>();
        }

        public static GameManager GameManager { get { return instance?.gameManager; } }
        public static ScoreManager Score { get { return instance?.score; } }
        public static UIManager UI { get { return instance?.ui; } }
        public static MMCAudioManager Audio { get { return instance?.audio; } }
        public static SFXManager SFX { get { return instance?.sfx; } }
        public static SettingsManager Settings { get { return instance?.settings; } }
        public static DataManager Data { get { return instance?.data; } }

    }
}
