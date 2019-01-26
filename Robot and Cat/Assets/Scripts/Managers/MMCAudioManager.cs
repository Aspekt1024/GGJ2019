
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace RobotCat.Audio
{
    public class MMCAudioManager : MonoBehaviour
    {
        public AudioClip MainTheme;
        public AudioClip MainThemeOutro;
        public AudioClip SleepTheme;

        public AudioSource Source1;
        public AudioSource Source2;

        private AudioSource currentSource;

        private bool clipQueued;

        private enum ThemeRequests
        {
            None, MainTheme, SleepTheme
        }
        private ThemeRequests currentRequest = ThemeRequests.None;

        private enum States
        {
            MainThemeLoop, MainOuttro, SleepTheme, SleepThemeLoop
        }
        private States state;
        
        private void Start()
        {
            state = States.MainThemeLoop;
            PlayClip(MainTheme);
        }
        
        public void CueMainTheme()
        {
            currentRequest = ThemeRequests.MainTheme;
            //themeSource.volume = fadeCurve.Evaluate((currentTime / timeToFade));
        }

        public void CueSleepTheme()
        {
            currentRequest = ThemeRequests.SleepTheme;
        }

        private void PlayClip(AudioClip clip)
        {
            currentSource = currentSource == Source1 ? Source2 : Source1;
            currentSource.clip = clip;
            currentSource.Play();
        }

        private void Update()
        {
            if (currentSource.clip == null || clipQueued == true) return;
            float timeLeft = currentSource.clip.length - currentSource.time;
            if (timeLeft < 0.5f)
            {
                currentSource = currentSource == Source1 ? Source2 : Source1;
                currentSource.clip = GetNextClip();
                clipQueued = true;
                currentSource.PlayScheduled(AudioSettings.dspTime + timeLeft);
                Task.Delay((int)(timeLeft * 1000)).ContinueWith(t => clipQueued = false);
            }
        }

        private AudioClip GetNextClip()
        {
            switch (state)
            {
                case States.MainThemeLoop:
                    if (currentRequest == ThemeRequests.SleepTheme)
                    {
                        state = States.MainOuttro;
                        return MainThemeOutro;
                    }
                    else
                    {
                        return MainTheme;
                    }
                case States.MainOuttro:
                    if (currentRequest == ThemeRequests.SleepTheme)
                    {
                        state = States.SleepThemeLoop;
                        currentRequest = ThemeRequests.None;
                        return SleepTheme;
                    }
                    else
                    {
                        state = States.MainThemeLoop;
                        currentRequest = ThemeRequests.None;
                        return MainTheme;
                    }
                case States.SleepTheme:
                    if (currentRequest == ThemeRequests.MainTheme)
                    {
                        state = States.MainThemeLoop;
                        currentRequest = ThemeRequests.None;
                        return MainTheme;
                    }
                    else
                    {
                        return SleepTheme;
                    }
                default:
                    return null;
            }
        }
    }
}
