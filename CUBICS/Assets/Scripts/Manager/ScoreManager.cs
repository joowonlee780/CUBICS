using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null;
    [SerializeField] int increaseScore = 10;
    int currentScore = 0;
    [SerializeField] float[] weight = null;
    [SerializeField] private int comboBonusScore = 10;
    
    private Animator myAnim;
    private string animScoreUp = "ScoreUp";

    private ComboManager theCombo;
    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        currentScore = 0;
        txtScore.text = "0";
    }

    public void Initialized()
    {
        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        theCombo.IncreaseCombo();

        int t_currentCombo = theCombo.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;
        
        int t_increaseScore = increaseScore + t_bonusComboScore;

        t_increaseScore = (int) (t_increaseScore * weight[p_JudgementState]);

        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);
        
        myAnim.SetTrigger(animScoreUp);
    }


    public int GetCurrentScore()
    {
        return currentScore;
    }
}
