using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    [SerializeField] LevelCoordinator levelCoordinator;
    [SerializeField] MusicPlayer musicPlayer;

    public static bool GameIsPaused = false;

    public void Awake()
    {
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

        public void ReturnToMainMenu()
    {
        levelCoordinator.LoadFirstLevel();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void AdjustBGMusic(float volume)
    {
        musicPlayer.AdjustMusicVloume(volume);
    }
}
