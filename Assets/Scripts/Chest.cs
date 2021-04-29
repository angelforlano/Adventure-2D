using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator chestAnimator;
    public Animator startAnimator;
    private bool open;

    public void Open()
    {
        if (open) return;
        
        open = true;
        chestAnimator.SetTrigger("Open");
        startAnimator.SetTrigger("Open");
    }
}
