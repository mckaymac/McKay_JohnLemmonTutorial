using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{

    public float fadeDuration = 1f;
    public float displayDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;

    bool m_IsPlayerAtExit;
    float m_Timer;

    //Checks to see if the player reaches the end
    void OnTriggerEnter(Collider other){
        if(other.gameObject == player){
            m_IsPlayerAtExit = true;
        }
    }

    void Update(){
        if(m_IsPlayerAtExit){
            EndLevel();
        }
    }

    //Puts up the end screen for a time when the player reaches the end and quits the program
    void EndLevel(){
        m_Timer += Time.deltaTime;

        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if(m_Timer > fadeDuration + displayDuration){
            Application.Quit();
        }
    }
}
