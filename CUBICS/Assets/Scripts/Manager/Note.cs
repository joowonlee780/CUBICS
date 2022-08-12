using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float noteSpeed = 400;

    private UnityEngine.UI.Image noteImage;
    private void OnEnable()
    {
        if(noteImage==null)
            noteImage = GetComponent<UnityEngine.UI.Image>();

        noteImage.enabled = true;
    }

    public void HideNote()
    {
        noteImage.enabled = false;
    }
    
    private void Update()
    {
        transform.localPosition += Vector3.right * noteSpeed * Time.deltaTime;
    }

    public bool GetNoteFlag()
    {
        return noteImage.enabled;
    }
}
