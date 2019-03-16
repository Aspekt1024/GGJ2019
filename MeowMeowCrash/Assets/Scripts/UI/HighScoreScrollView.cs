using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RobotCat.UI
{
    public class HighScoreScrollView : MonoBehaviour
    {
        public GameObject ScoreItemPrefab;
        public Transform ContentParent;

        private RectTransform parentRect;
        private ScrollRect scrollRect;

        private List<HighScoreItem> highScoreItems = new List<HighScoreItem>();

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            parentRect = ContentParent.GetComponent<RectTransform>();
        }

        public void Reset()
        {
            for (int i = highScoreItems.Count - 1; i >= 0; i--)
            {
                Destroy(highScoreItems[i].gameObject);
            }
            highScoreItems.Clear();
        }

        public void AddScore(int index, string name, int score)
        {
            var scoreItem = NewScoreItem(index, name, score);
        }

        public void AddScoreWithNameInput(int index, int score)
        {
            var scoreItem = NewScoreItem(index, "", score);
            scoreItem.SetNameEditable();
        }

        public void HandleSubmit()
        {
            foreach (var item in highScoreItems)
            {
                if (item.IsEditable)
                {
                    item.SubmitIfValidName();
                }
            }
        }

        public void SetContentSize(int numElements)
        {
            float CONTENT_SIZE = 40f;
            parentRect.sizeDelta = new Vector2(parentRect.sizeDelta.x, numElements * CONTENT_SIZE);
            scrollRect.verticalScrollbar.value = 1;
        }

        private HighScoreItem NewScoreItem(int index, string name, int score)
        {
            GameObject newItem = Instantiate(ScoreItemPrefab, ContentParent);
            HighScoreItem scoreItem = newItem.GetComponent<HighScoreItem>();
            scoreItem.Populate(index, name, score);
            highScoreItems.Add(scoreItem);

            PositionScoreItem(newItem.GetComponent<RectTransform>(), index);

            return scoreItem;
        }

        private void PositionScoreItem(RectTransform rect, int index)
        {
            rect.position = parentRect.position;
            rect.position -= rect.rect.height * (index - 0.5f) * Vector3.up;
            rect.position += rect.rect.width / 2f * Vector3.right;
        }
    }
}
