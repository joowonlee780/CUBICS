using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFlame : MonoBehaviour
{
    
    private bool musicStart = false;

    public void ResetMusic()
    {
        musicStart = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!musicStart)
        {
            if (collision.CompareTag("Note"))
            {
                AudioManager.instance.PlayBGM("BGM0");
                musicStart = true;
            }
        }
    }
}
