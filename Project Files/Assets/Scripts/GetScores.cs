using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetScores : MonoBehaviour
{
    public GameObject scoresContainer;
    Dictionary<string, int> highScoresDict = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        scoresContainer = GameObject.FindGameObjectWithTag("ScoresContainer");
        highScoresDict = scoresContainer.GetComponent<HighScoresDictionary>().scores;
    }

    // Update is called once per frame
    void Update()
    {
        string scoreReel = "";
        foreach (KeyValuePair<string, int> kvp in highScoresDict)
        {
            scoreReel += kvp.Key + " - " + kvp.Value + "\n";
        }
        gameObject.GetComponent<TMP_Text>().text = scoreReel;
    }
}
