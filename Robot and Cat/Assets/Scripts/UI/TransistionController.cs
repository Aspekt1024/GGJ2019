using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RobotCat
{
    public class TransistionController : MonoBehaviour
    {
        public static TransistionController instance = null;

        public CanvasGroup canvasController;

        public AudioSource themeSource;

        public float currentTime = 0.0f;
        public float timeToFade = 5.0f;

        private IEnumerator fadeOutEnumerator;
        private IEnumerator fadeInEnumerator;
        public AnimationCurve fadeCurve;

        private bool transistioning = false;

        private IEnumerator transitionOut()
        {
            //Transistion Out Music
            currentTime = 0.0f;
            transistioning = true;
            while (true)
            {
                currentTime += Time.deltaTime;
                currentTime = Mathf.Min(currentTime, timeToFade);
                canvasController.alpha = fadeCurve.Evaluate(currentTime / timeToFade);
                yield return null;
            }
            //Cleanup Scene and call Scene Manager
            transistioning = false;
        }

        private IEnumerator transitionIn()
        {
            themeSource.Play();
            themeSource.loop = true;
            //Start the music maybe while increasing volume?
            CupManager.instance.initiateAGame();
            currentTime = 0.0f;
            transistioning = true;

            while (currentTime < timeToFade)
            {
                currentTime += Time.deltaTime;
                currentTime = Mathf.Min(currentTime, timeToFade);
                canvasController.alpha = fadeCurve.Evaluate(1-(currentTime / timeToFade));
                themeSource.volume = fadeCurve.Evaluate( (currentTime / timeToFade));

                    yield return null;
            }
            transistioning = false;
        }

        // Use this for initialization
        void Start()
        {
            if(instance != null)
            {
                Destroy(this);
            }
            instance = this;
            canvasController = GetComponent<CanvasGroup>();
            fadeOutEnumerator = transitionOut();
            fadeInEnumerator = transitionIn();
            gameStart();
        }

        public void gameOut()
        {
            themeSource.loop = false;
            if(!transistioning)
            {
                StartCoroutine(fadeOutEnumerator);
            }

        }

        public void gameStart()
        {
            if (!transistioning)
            {
                StartCoroutine(fadeInEnumerator);
            }

        }


    }
}