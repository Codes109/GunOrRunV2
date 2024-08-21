using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScoresButton : MonoBehaviour
{
    public GameObject panel;

    public void ShowScores()
    {
        panel.SetActive(true);
        
    }

    public void HideScores()
    {
        panel.SetActive(false);
    }
}
