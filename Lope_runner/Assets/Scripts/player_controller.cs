using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_controller : MonoBehaviour
{
    private Rigidbody playerRB;

    public float jumpForce; // the force the player jumps
    public float gravityModifier; // to modify the gravity, to earth one to a lunar one!

    public bool IsOnGround = true; //is on the ground
    public bool isGameOver = false;
    public bool restart = false;

    public ParticleSystem explosion;
    public ParticleSystem dirt;

    //Audio
    private AudioSource playerAudio;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public int hp;

    private SpriteRenderer hp1;
    private SpriteRenderer hp2;
    private SpriteRenderer hp3;


    //private SpriteRenderer Enemy1;


    private Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); //Get the Rigidbody component

        Physics.gravity *= gravityModifier; //Modify the default Unity gravity to your gravity!

        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        GameObject canvas = GameObject.Find("Canvas");
        hp1 = canvas.transform.GetChild(0).GetComponent<SpriteRenderer>();
        hp2 = canvas.transform.GetChild(1).GetComponent<SpriteRenderer>();
        hp3 = canvas.transform.GetChild(2).GetComponent<SpriteRenderer>();

        //Enemy1 = canvas.transform.GetComponent<SpriteRenderer>();
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
        else if (isGameOver && Input.GetKeyDown(KeyCode.Space))
        {        
            isGameOver = false;
            hp = 4;
            LoseHp();
            playerAnim.SetBool("Death_b", false);
            restart = true;
        }
    }
    void LoseHp()
    {
        if (hp>= 0)
        {
            hp--;
            switch (hp)
            {
                case 2: hp3.gameObject.SetActive(false);
                    break;
                case 1: hp2.gameObject.SetActive(false);
                    break;
                case 0: hp1.gameObject.SetActive(false);
                    isGameOver = true;
                    // Animations
                    playerAnim.SetBool("Death_b", true);
                    playerAnim.SetInteger("DeathType_int", 1);
                    // particles
                    explosion.Play();
                    dirt.Stop();
                    //sounds
                    playerAudio.PlayOneShot(crashSound);

                    break;
                default: hp3.gameObject.SetActive(true);
                    hp2.gameObject.SetActive(true);
                    hp1.gameObject.SetActive(true);
                    break;
            }
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
            LoseHp();
            Destroy(collision.gameObject); 
        }
           
        
    }
}

    
