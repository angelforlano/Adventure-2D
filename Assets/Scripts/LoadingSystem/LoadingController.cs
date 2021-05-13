using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{
    public Image loadingImage;

    void Update()
    {
        if (loadingImage.fillAmount >= 1) return;

        loadingImage.fillAmount += Random.Range(0.2f, 1f) * Time.deltaTime;

        if (loadingImage.fillAmount >= 1)
            SceneManager.LoadScene(LoadingManager.Instance.sceneToLoad);
    }
}