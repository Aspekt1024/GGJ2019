using UnityEngine;

public class CreditsMenuScript : MonoBehaviour {
    public Canvas creditsCranvas;

    public void Hide()
    {
        creditsCranvas.gameObject.SetActive(false);
    }

    public void Show()
    {
        creditsCranvas.gameObject.SetActive(true);
    }

    public void ToggleCredits()
    {
        if (creditsCranvas.gameObject.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
