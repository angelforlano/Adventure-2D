using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void PlayBtn()
    {
        LoadingManager.Instance.LoadTo("Level_1");
    }
}