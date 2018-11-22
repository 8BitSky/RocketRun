using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] float musicVolume = 0.2f;
  
    // Use this for initialization
    private void Awake()
    {
        //Singleton Pattern for persistance
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      UpdateMusicAudio();
    }

    private void UpdateMusicAudio()
    {
        audioSource.volume = musicVolume;
    }

    public void AdjustMusicVloume(float volume)
    {
        musicVolume = volume;
    }

}
