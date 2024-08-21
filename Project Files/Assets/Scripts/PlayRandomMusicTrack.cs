using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayRandomMusicTrack : MonoBehaviour
{
    public MusicTrack[] playlist;
    AudioClip track;
    int trackNumber = 1;
    public int seed;

    public bool shuffle;
    public bool playOnAwake;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        seed = System.DateTime.Now.Millisecond;
        Random.InitState(seed);
    }

    // Update is called once per frame
    void Update()
    {
        if (playOnAwake)
        {
            if (shuffle)
            {
                PlayRandomTrack();
            }
            else
            {
                if (!audioSource.isPlaying)
                {
                    PlayTrack(trackNumber);
                }
            }
        }
    }

    [System.Serializable]
    public struct MusicTrack
    {
        public string name;
        public AudioClip track;
    }

    public void PlayTrack(int trackNumber)
    {
        audioSource.clip = playlist[trackNumber - 1].track;
        audioSource.Play();
    }

    public void PlayTrack(string trackName)
    {
        for(int i = 0; i < playlist.Length; i++)
        {
            if (playlist[i].name == trackName)
            {
                trackNumber = i + 1;
            }
        }
        audioSource.clip = playlist[trackNumber - 1].track;
        audioSource.Play();
    }

    public void PlayRandomTrack()
    {
       
        trackNumber = Random.Range(1, playlist.Length);
        if (!audioSource.isPlaying)
        {
            PlayTrack(trackNumber);
        }
    }

}
