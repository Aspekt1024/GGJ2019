using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace RobotCat
{
    public class MainMenuTransistion : MonoBehaviour
    {
        public static MainMenuTransistion instance = null;

        public CanvasGroup canvasController;

        public float currentTime = 0.0f;
        public float timeToFade = 0.3f;

        private IEnumerator fadeOutEnumerator;
        private IEnumerator fadeInEnumerator;
        public AnimationCurve fadeCurve;
        public Animator catoWoke;

        public bool transistioning = false;

        private IEnumerator transitionOut()
        {
            //Transistion Out Music
            catoWoke.SetBool("Transistioning", true);
            currentTime = 0.0f;
            transistioning = true;
            while (currentTime < timeToFade)
            {
                currentTime += Time.deltaTime;
                currentTime = Mathf.Min(currentTime, timeToFade);
                canvasController.alpha = fadeCurve.Evaluate(currentTime / timeToFade);
                yield return null;
            }
            //Cleanup Scene and call Scene Manager
            transistioning = false;

            SceneManager.LoadScene("MeowMeowCrash", LoadSceneMode.Single);
            
        }

        private IEnumerator transitionIn()
        {
            //Start the music maybe while increasing volume?
            currentTime = 0.0f;
            transistioning = true;
            catoWoke.SetBool("Transistioning", false);

            while (currentTime < timeToFade)
            {
                currentTime += Time.deltaTime;
                currentTime = Mathf.Min(currentTime, timeToFade);
                canvasController.alpha = fadeCurve.Evaluate(1-(currentTime / timeToFade));

                yield return null;
            }
            transistioning = false;
        }

        // Use this for initialization
        void Awake()
        {
            if(instance != null)
            {
                Destroy(this);
            }
            instance = this;
            fadeOutEnumerator = transitionOut();
            fadeInEnumerator = transitionIn();
            gameStart();
        }

        void Start()
        {
            gameStart();
        }

        public void gameOut()
        {
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