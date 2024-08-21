using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadDestroyCopy : MonoBehaviour
{
    private static GameObject sampleInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (sampleInstance == null)
        {
            sampleInstance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
