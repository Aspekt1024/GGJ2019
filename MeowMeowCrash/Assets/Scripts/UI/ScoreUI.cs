using System.Collections;
using TMPro;
using UnityEngine;

namespace RobotCat.UI
{
    public class ScoreUI : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private TextMeshProUGUI scoreText;
        #pragma warning restore 649

        private int currentScore;
        private Vector3 originalScale;
        private Coroutine scaleRoutine;

        const float SCALE_MODIFIER = 1.2f;
        const float ENLARGE_DURATION = 0.3f;
        const float SHRINK_DURATION = 0.1f;

        private void Awake()
        {
            originalScale = scoreText.transform.localScale;
        }

        public void SetScore(int score)
        {
            int scoreDifference = score - currentScore;
            if (scoreDifference > 24)
            {
                if (scaleRoutine != null)
                {
                    StopCoroutine(scaleRoutine);
                }
                scaleRoutine = StartCoroutine(AnimateScoreRoutine());
            }
            currentScore = score;
            scoreText.text = score.ToString();
        }

        private IEnumerator AnimateScoreRoutine()
        {
            scoreText.transform.localScale = originalScale * SCALE_MODIFIER;
            yield return new WaitForSeconds(ENLARGE_DURATION);

            float timer = 0f;
            while (timer < SHRINK_DURATION)
            {
                timer += Time.deltaTime;
                float scaleFactor = Mathf.Lerp(SCALE_MODIFIER, 1f, timer / SHRINK_DURATION);
                scoreText.transform.localScale = originalScale * scaleFactor;
                yield return null;
            }
        }
    }
}
