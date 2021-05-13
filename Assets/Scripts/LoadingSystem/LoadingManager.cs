using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public string sceneToLoad;
    
    public static LoadingManager Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void LoadTo(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene("Loading");
    }
}