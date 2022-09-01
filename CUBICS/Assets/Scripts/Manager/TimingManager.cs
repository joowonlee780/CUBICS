using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    int[] judgementRecord = new int[5];
    
    [SerializeField] Transform Center = null;
    [SerializeField] RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    EffectManager theEffect;
    ScoreManager theScoreManager;
    ComboManager theComboManager;
    StageManager theStageManager;
    PlayerController thePlayer;
    StatusManager theStatus;

    AudioManager theAudioManager;
    
    
    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theStageManager = FindObjectOfType<StageManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStatus = FindObjectOfType<StatusManager>();
        theAudioManager = AudioManager.instance;
        
        
        
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;
            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    
                    boxNoteList.RemoveAt(i);
                    
                    
                    if(x < timingBoxs.Length - 1)
                        theEffect.NoteHitEffect();
                    
                    
                    
                    
                    

                    if (CheckCanNextPlate())
                    {
                        theScoreManager.IncreaseScore(x);
                        theStageManager.ShowNextPlate();
                        theEffect.JudgementEffect(x);
                        judgementRecord[x]++;
                        theStatus.CheckShield();
                    }

                    else
                    {
                        theEffect.JudgementEffect(5);
                    }
                    
                    theAudioManager.PlaySFX("Clap");
                    return true;
                }
            }
        }
        theComboManager.ResetCombo();
        theEffect.JudgementEffect(timingBoxs.Length);
        MissRecord();
        return false;
    }
    
    bool CheckCanNextPlate()
    {
        if (Physics.Raycast(thePlayer.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f))
        {
            if (t_hitInfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_Plate = t_hitInfo.transform.GetComponent<BasicPlate>();
                if (t_Plate.flag)
                {
                    t_Plate.flag = false;
                    return true;
                }
            }
        }

        return false;
    }

    public int[] GetJudgementRecord()
    {
        return judgementRecord;
    }

    public void MissRecord()
    {
        judgementRecord[4]++;
        theStatus.ResetShieldCombo();
    }
    
    public void Initialized()
    {
        for(int i= 0 ;i<judgementRecord.Length;i++)
        {
            judgementRecord[i] = 0;
        }
        
    }
}
