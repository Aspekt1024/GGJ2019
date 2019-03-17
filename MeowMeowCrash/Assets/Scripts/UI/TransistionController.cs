using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace RobotCat
{
    public class TransistionController : MonoBehaviour
    {
        public static TransistionController instance = null;

        public CanvasGroup canvasController;

        public float currentTime = 0.0f;
        public float timeToFade = 5.0f;
        
        public AnimationCurve fadeCurve;

        private bool transistioning = false;

        void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }
            instance = this;
        }

        void Start()
        {
            GameStart();
        }

        public void GameOut()
        {
            if(!transistioning)
            {
                StartCoroutine(TransitionOut());
            }

        }

        public void GameStart()
        {
            if (!transistioning)
            {
                StartCoroutine(TransitionIn());
            }
        }

        private IEnumerator TransitionOut()
        {
            //Transistion Out Music
            currentTime = 0.0f;
            transistioning = true;
            while (currentTime < timeToFade)
            {
                currentTime += Time.unscaledDeltaTime;
                currentTime = Mathf.Min(currentTime, timeToFade);
                canvasController.alpha = fadeCurve.Evaluate(currentTime / timeToFade);
                yield return null;
            }
            //Cleanup Scene and call Scene Manager
            transistioning = false;
            Time.timeScale = 1f;

            //Load sleeping scene
            SceneManager.LoadScene("Transmission'", LoadSceneMode.Single);

            RCStatics.Audio.CueSleepTheme();
        }

        private IEnumerator TransitionIn()
        {
            //Start the music maybe while increasing volume?
            currentTime = 0.0f;
            transistioning = true;

            while (currentTime < timeToFade)
            {
                currentTime += Time.deltaTime;
                currentTime = Mathf.Min(currentTime, timeToFade);
                canvasController.alpha = fadeCurve.Evaluate(1 - (currentTime / timeToFade));
                RCStatics.Audio.CueMainTheme();

                yield return null;
            }
            transistioning = false;
        }
    }
}