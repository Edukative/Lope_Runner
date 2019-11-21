using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    private Rigidbody playerRB;

    public float jumpForce; // the force the player jumps
    public float gravityModifier; // to modify the gravity, to earth one to a lunar one!

    public bool IsOnGround = true; //is on the ground
    public bool isGameOver = false;

    public ParticleSystem explosion;
    public ParticleSystem dirt;

    //Audio
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    private Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); //Get the Rigidbody component

        Physics.gravity *= gravityModifier; //Modify the default Unity gravity to your gravity!

        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround && !isGameOver)  // if you press space and is touching the ground
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //Apply a impulse force, to make it up
            IsOnGround = false; // no longer touches the ground

            //Animations
            playerAnim.SetTrigger("Jump_trig");

            //Particles
            dirt.Stop();

            //audio
            playerAudio.PlayOneShot(jumpSound);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnGround = true;
            dirt.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            explosion.Play();
            isGameOver = true;
            Debug.Log("Game Over you noob");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound);
        }
    }
}

    
