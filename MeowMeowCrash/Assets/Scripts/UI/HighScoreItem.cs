using TMPro;
using UnityEngine;

namespace RobotCat.UI
{
    public class HighScoreItem : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private TextMeshProUGUI indexText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private TextMeshProUGUI scoreText;
        #pragma warning restore 649

        private int score;
        
        private enum States
        {
            Readonly, Editable
        }
        private States state;

        private void Update()
        {
            if (state == States.Readonly) return;

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SubmitIfValidName();
            }
        }

        public void NameChanged()
        {
            if (nameInput.text.Length > 30)
            {
                nameInput.text = nameInput.text.Substring(0, 30);
            }
        }

        public void Populate(int index, string name, int score)
        {
            this.score = score;

            indexText.text = index.ToString() + ".";
            nameText.text = name;
            scoreText.text = score.ToString();
            SetNameReadonly();
        }

        public void SetNameEditable()
        {
            state = States.Editable;
            nameInput.gameObject.SetActive(true);
            nameText.gameObject.SetActive(false);
            nameInput.text = nameText.text;
            // TODO focus
        }

        public void SetNameReadonly()
        {
            state = States.Readonly;
            nameInput.gameObject.SetActive(false);
            nameText.gameObject.SetActive(true);
        }

        public bool IsEditable { get { return state == States.Editable; } }

        public void SubmitIfValidName()
        {
            if (string.IsNullOrEmpty(nameInput.text)) return;

            RCStatics.Data.PostNewScore(nameInput.text, score);
            nameText.text = nameInput.text;
            SetNameReadonly();
            RCStatics.GameManager.NewScoreSubmitted();
            GetComponent<Animator>().Play("NewScoreSubmitted", 0, 0f);
        }
    }
}
