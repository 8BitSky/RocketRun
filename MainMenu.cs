using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField] LevelCoordinator levelCoordinator;
    [SerializeField] MusicPlayer musicPlayer;

    public void StartGame()
    {
        levelCoordinator.LoadNextLevel();
    }

    public void AdjustBGMusic(float volume)
    {
        musicPlayer.AdjustMusicVloume(volume);
    }

    //public void SettingsMenu()
    //{
    //Settings functions will go here
    //}
}
