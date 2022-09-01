using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusManager : MonoBehaviour
{
    [SerializeField] float blickSpeed = 0.1f;
    [SerializeField] int blinkCount = 10;
    int currentBlinkCount = 0;
    private bool isblink = false;
    
    
    bool isDead = false;
    
    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;

    [SerializeField] Image[] hpImage = null;
    [SerializeField] Image[] shieldImage = null;

    [SerializeField] int shieldIncreaseCombo = 5;
    private int currentShieldCombo = 0;
    [SerializeField] Image shieldGauge = null;
    
    
    Result theResult;
    NoteManager theNote;
    [SerializeField] MeshRenderer playerMesh = null;
    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
    }

    public void CheckShield()
    {
        currentShieldCombo++;
        if (currentShieldCombo >= shieldIncreaseCombo)
        {
            currentShieldCombo = 0;
            IncreaseShield();
            
        }
        shieldGauge.fillAmount = (float) currentShieldCombo / shieldIncreaseCombo;
    }

    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float) currentShieldCombo / shieldIncreaseCombo;
    }
    
    public void IncreaseShield()
    {
        currentShield++;

        if (currentShield >= maxShield)
            currentShield = maxShield;
        SettingShieldImage();
        
    }

    public void DecreaseShield(int p_num)
    {
        currentShield -= p_num;
        if (currentShield <= 0)
            currentShield = 0;
        SettingShieldImage();
    }

    public void IncreaseHp(int p_num)
    {
        currentHp += p_num;
        if (currentHp >= maxHp)
            currentHp = maxHp;
        SettingHPImage();
        
    }
    public void DecreaseHp(int p_num)
    {
        if (!isblink)
        {
            
            if(currentShield>0)
                DecreaseShield(p_num);
            else
            {
                currentHp -= p_num;

                if (currentHp <= 0)
                {
                    theResult.ShowResult();
                    theNote.RemoveNote();
                }
                else
                {
                    StartCoroutine(BlinkCo());

                }
                SettingHPImage();
            }
            
        }
       
    }

    void SettingHPImage()
    {
        for (int i = 0; i < hpImage.Length; i++)
        {
            if (i < currentHp)
                hpImage[i].gameObject.SetActive(true);
            else
                hpImage[i].gameObject.SetActive(false);

        }
            
    }
    void SettingShieldImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
                shieldImage[i].gameObject.SetActive(true);
            else
                shieldImage[i].gameObject.SetActive(false);

        }
            
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlinkCo()
    {
        isblink = true;
        while (currentBlinkCount<=blinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(blickSpeed);

            currentBlinkCount++;
        }
        playerMesh.enabled = true;
        currentBlinkCount = 0;
        isblink = false;
    }
}
