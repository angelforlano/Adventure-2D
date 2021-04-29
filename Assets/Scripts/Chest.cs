using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public AudioSource starSfx;
    public Animator chestAnimator;
    public Animator starAnimator;
    private bool open;

    public void Open(Player player)
    {
        if (open) return;
        
        open = true;
        player.stars++;
        
        starSfx.Play();
        chestAnimator.SetTrigger("Open");
        starAnimator.SetTrigger("Open");
    
        Destroy(starAnimator.gameObject, 1f);
    }
}
