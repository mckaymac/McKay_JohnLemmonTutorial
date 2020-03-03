using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   public int health = 3;
   public Transform player;
   public bool canHit;

    //Damage enemy or delete it if hp = 0
   public void TakeDamage(int damageAmount){
       if(canHit){
            health -= damageAmount;
            if(health <= 0){
                Destroy(gameObject);
            }
       }
       else if(!canHit){
           PlayerMovement shrine = player.GetComponent<PlayerMovement>();
           if(shrine.shrineEffect){
               canHit = true;
           }
       }
   }
}
