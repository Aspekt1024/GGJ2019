using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RobotCat.UI
{
    public class ExcitementBar : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private Image barImage;
        [SerializeField] private TextMeshProUGUI excitementText;
        #pragma warning restore 649

        private CanvasGroup textCanvasGroup;

        private Vector3 originalTextScale;
        private Coroutine textRoutine;

        private enum States
        {
            None, FirstExcitement, Bored, Sleepy, Lively
        }
        private States state;

        private void Start()
        {
            textCanvasGroup = excitementText.GetComponent<CanvasGroup>();
            originalTextScale = excitementText.transform.localScale;
            textCanvasGroup.alpha = 0f;

            state = States.None;
        }

        public void SetValue(float value, Color color)
        {
            color.a = barImage.color.a;
            barImage.fillAmount = value;
            
            //SetText(value);
        }

        public void SetFirstExcitement()
        {
            state = States.FirstExcitement;
            excitementText.text = "meow!";
        }

        private void SetText(float value)
        {
            if (value > 0.9f && state != States.Lively)
            {
                state = States.Lively;
                excitementText.text = "MEOW!";
                StartCoroutine(FadeTextRoutine());
                StartCoroutine(ResizeTextRoutine(1.3f));
            }
            else if (value < 0.5f && state != States.Bored && state != States.Sleepy)
            {
                state = States.Bored;
                excitementText.text = "meow...";
                StartCoroutine(FadeTextRoutine());
            }

            if (value < 0.2f && state != States.Sleepy)
            {
                state = States.Sleepy;
                excitementText.text = "mmmowr...";
                StartCoroutine(FadeTextRoutine());
            }
        }

        private IEnumerator FadeTextRoutine()
        {
            yield return new WaitForSeconds(2f);

            float timer = 0f;
            const float FADE_DURATION = 1f;
            while (timer < FADE_DURATION)
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / FADE_DURATION);
                textCanvasGroup.alpha = alpha;
                yield return null;
            }
            textCanvasGroup.alpha = 0f;
            state = States.None;
        }

        private IEnumerator ResizeTextRoutine(float scale)
        {
            excitementText.transform.localScale = originalTextScale * scale;
            yield return new WaitForSeconds(0.3f);

            const float SCALE_MOD_DURATION = 0.2f;

            float timer = 0f;
            while (timer < SCALE_MOD_DURATION)
            {
                timer += Time.deltaTime;
                float scaleFactor = Mathf.Lerp(scale, 1f, timer / SCALE_MOD_DURATION);
                excitementText.transform.localScale = originalTextScale * scaleFactor;
                yield return null;
            }
            excitementText.transform.localScale = originalTextScale;
        }
    }
}
