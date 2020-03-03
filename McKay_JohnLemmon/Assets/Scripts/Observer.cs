﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;
    private int HP;

    //Checks to see if player is run into
    void OnTriggerEnter(Collider other){
        if(other.transform == player){
            if(other.transform == player){
                m_IsPlayerInRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.transform == player){
            m_IsPlayerInRange = false;
          }
    }
    //Checks to see if player is in line of sight
    void Update(){
        if(m_IsPlayerInRange){
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit)){
                if(raycastHit.collider.transform == player){
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}
