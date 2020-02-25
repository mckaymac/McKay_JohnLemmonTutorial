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
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    //Checks to see if the player reaches the end
    void OnTriggerEnter(Collider other){
        if(other.gameObject == player){
            m_IsPlayerAtExit = true;
        }
    }

    //Logic gate for getting caught
    public void CaughtPlayer(){
        m_IsPlayerCaught = true;
        m_HasAudioPlayed = true;
    }

    //Checks to see if the player is caught or exits and does appropriate action
    void Update(){
        if(m_IsPlayerAtExit){
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if(m_IsPlayerCaught){
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    //Puts up the end screen for a time when the player reaches the end and quits the program or restarts if caught
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource){

        if(!m_HasAudioPlayed){
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

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
