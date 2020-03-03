using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
   public int health = 3;

    //Damage enemy or delete it if hp = 0
   public void TakeDamage(int damageAmount){
       health -= damageAmount;
       if(health <= 0){
           Destroy(gameObject);
       }
   }
}
