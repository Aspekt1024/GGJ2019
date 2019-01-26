using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuScript : MonoBehaviour {
    public Canvas creditsCranvas;

    public void ToggleCredits()
    {
        if (creditsCranvas.gameObject.active)
        {
            creditsCranvas.gameObject.SetActive(false);
        }
        else
        {
            creditsCranvas.gameObject.SetActive(true);
        }
    }
}
