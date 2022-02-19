using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GoalValue : MonoBehaviour
{
    public int value;   // earned score after capture this goal
    //public AudioClip captureGoalAudio;
    private AudioSource audio;
    public bool isProps = false;
    public bool isBlackHole =false;
    [HideInInspector]public bool isCaptured;
    private Gloable.PropsType type;

    // Start is called before the first frame update
    void Start()
    {
        isCaptured = false;
        if (isProps)
        {
            RandomSetProp();
        }
    }

    public void CapturedEffect()
    {
        if (PlayerPrefs.GetInt("sound") == 1)
        {
            GameObject goalAudio = GameObject.Find("goalAudio");
            goalAudio.GetComponent<AudioSource>().Play();
        }
            //AudioSource.PlayClipAtPoint(captureGoalAudio, Camera.main.transform.position);
        if (GameManager.gm != null)
        {
            if(!isProps)
            {
                GameManager.gm.AddScore(value);
                GameManager.gm.currentGoal = null;
            }
            else
            {
                // work immediately (time extension & score increase)
                if (type == Gloable.PropsType.TIME_INCREASE)
                {
                    PropsManager.manager.IncreaseTime(true);
                }
                else if (type == Gloable.PropsType.SCORE_INCREASE)
                {
                    PropsManager.manager.IncreaseScore(true);
                }
                else
                {
                    PropsManager.manager.UpdatePropsCounter(type);
                }
            }
        }
        
        // for tutorial level
        if (SimpleGameManager.gm != null)
        {
            if(!isProps)
            {
                SimpleGameManager.gm.AddScore();
                SimpleGameManager.gm.currentGoal = null;
            }
            else
            {
                // work immediately (time extension & score increase)
                if (type == Gloable.PropsType.TIME_INCREASE)
                {
                    SimpleGameManager.gm.AddRemainingTime();
                }
                else if (type == Gloable.PropsType.SCORE_INCREASE)
                {
                    SimpleGameManager.gm.AddScore();
                }
            }
        }
    }
    
    private void RandomSetProp()
    {
        List<Gloable.PropsType> propsList =
            Enum.GetValues(typeof(Gloable.PropsType)).Cast<Gloable.PropsType>().ToList();
        type = propsList[Random.Range(0, propsList.Count)];
    }
}
