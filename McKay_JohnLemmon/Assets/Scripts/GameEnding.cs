using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    public float fadeDuration = 1f;
    public float displayDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;

    //Checks to see if the player reaches the end
    void OnTriggerEnter(Collider other){
        if(other.gameObject == player){
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer(){
        m_IsPlayerCaught = true;
    }

    void Update(){
        if(m_IsPlayerAtExit){
            EndLevel(exitBackgroundImageCanvasGroup, false);
        }
        else if(m_IsPlayerCaught){
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }
    }

    //Puts up the end screen for a time when the player reaches the end and quits the program
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart){
        m_Timer += Time.deltaTime;

        exitBackgroundImageCanvasGroup.alpha = m_Timer / fadeDuration;

        if(m_Timer > fadeDuration + displayDuration){

            if(doRestart){
                SceneManager.LoadScene(0);
            }
            else{
                Application.Quit();
            }
        }
    }
}
