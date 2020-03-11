using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    ColorGrading colorGradingLayer;

    public GameObject projectilePrefab;
    public Transform shotSpawn;
    public float shotSpeed = 10f;
    public bool shrineEffect = false;
    float shrineCooldown;
    public Text tutorialText;
    public Text tutorialTextTwo;
    public Text ShrineText;
    

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other){
        if(!shrineEffect && shrineCooldown <= 0f){
            if(other.CompareTag("Shrine")){
                shrineEffect = true;
                shrineCooldown = 15f;
               // colorGradingLayer.hueShift.value = 180;
            }
        }

        //If the player is in the spawn area then text appears showing the controls
        if(other.gameObject.CompareTag("Tutorial")){
            tutorialText.text = "WASD or arrow keys to move, SPACE to shoot laser eyes";
            tutorialTextTwo.text = "Pray at the shrines to be able to banish the red ghosts";

        }

        //When the player leaves the spawn spot then the tutorial text disapears
        if(other.gameObject.CompareTag("EndTut")){
            tutorialText.text = "";
            tutorialTextTwo.text = "";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Enables input for movement of player character
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        //Set up logic gate to see if character is moving
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        //Starts the walking animation if logic is right
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("isWalking", isWalking);

        //Shoot the laser from the player's face
        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject projectile = Instantiate(projectilePrefab, shotSpawn.transform.position, projectilePrefab.transform.rotation);
            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
            projectileRB.velocity = transform.forward * shotSpeed;
        }

        ShrineText.text = "Shrine Time Left: " + shrineCooldown;

        //Allow player to kill red ghosts
        if(shrineEffect){
            shrineCooldown -= Time.deltaTime;
            print("ShrineCooldown: " + shrineCooldown);
            if(shrineCooldown <= 0f){
                shrineEffect = false;
            }
        }

        //Plays footsteps
        if(isWalking){
            if(!m_AudioSource.isPlaying){
                m_AudioSource.Play();
            }
        }
        else{
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        
    }

    //Move the rigidbody when the model moves
    void OnAnimatorMove(){
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
