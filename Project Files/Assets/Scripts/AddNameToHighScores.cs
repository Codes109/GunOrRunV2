using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddNameToHighScores : MonoBehaviour
{
    public string playerName;
    GameObject scoresContainer;
    public void AddName()
    {
        playerName = gameObject.GetComponent<TMP_InputField>().text;
        scoresContainer = GameObject.FindGameObjectWithTag("ScoresContainer");
        scoresContainer.GetComponent<HighScoresDictionary>().scores.Add(playerName, 0);
    }
}
