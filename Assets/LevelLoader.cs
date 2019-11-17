using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance {get;set;}

    public string[] levels;
    public GameObject canvas;
    public AudioListener menuListener;

    private string currentLevelname;

    void Awake()
    {
        Instance = this;
    }

    public void LoadLevel(int levelNum) {
        currentLevelname = levels[levelNum];
        Debug.Log("LOAD " + currentLevelname);
        canvas.gameObject.SetActive(false);
        menuListener.gameObject.SetActive(false);
        SceneManager.LoadSceneAsync(currentLevelname, LoadSceneMode.Additive);
    }

    public void OnFail() {
        Debug.Log("ON FAIL");
        Time.timeScale = 0;
        
        /*
        SceneManager.UnloadSceneAsync(currentLevelname);

        SceneManager.LoadSceneAsync(currentLevelname, LoadSceneMode.Additive);
        */
    }

    public void OnSuccess() {
        Debug.Log("ON SUCCESS");
        SceneManager.UnloadSceneAsync(currentLevelname);
        SceneManager.LoadSceneAsync(levels[1]);
    }

}
