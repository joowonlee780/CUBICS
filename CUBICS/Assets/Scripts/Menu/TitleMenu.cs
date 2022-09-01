using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{

    [SerializeField] private GameObject goStageUI = null;

    public void BtnPlay()
    {
        goStageUI.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
}
