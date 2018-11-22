using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCoordinator : MonoBehaviour {

    [SerializeField] int totalSceneCount;

    //Apply Persistance
    public void Awake()
    {
        //"Singleton Pattern for persistance
        int numLevelCoordinators = FindObjectsOfType<LevelCoordinator>().Length;
        if (numLevelCoordinators > 1)
        {
            Destroy(gameObject);
        }
        else { 
            GameObject.DontDestroyOnLoad(this);
        }
    }

    void Start () {
        //int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        //print(totalSceneCount);
    }

    private void Update()
    {
        if (Debug.isDebugBuild)
        {
            RespondToDebug();
        }    
    }

    private void RespondToDebug() //Only Active for Dev Builds
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;
        print(totalSceneCount);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        print(currentSceneIndex);
        int nextSceneIndex = currentSceneIndex + 1;
        print(nextSceneIndex);

        if (nextSceneIndex < totalSceneCount)
        {
            SceneManager.LoadScene(nextSceneIndex);
            print("success");
        }
        else
        {
            LoadFirstLevel();
            print("return");
        }
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    
}