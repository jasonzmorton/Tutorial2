using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text LivesText; 
    public Text winText;
    public Text loseText;
    private int scoreValue = 0; 
    private int lives = 3;

    private bool facingRight = true;
    
    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    Animator anim;

    

    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        loseText.text = "";
        LivesText.text = lives.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

    }

    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if(collision.collider.tag == "Coin")
            {
                scoreValue += 1;
                score.text = scoreValue.ToString();
                Destroy(collision.collider.gameObject);
            
            if (scoreValue == 4)
            {
               lives = 3;
               LivesText.text = lives.ToString(); 
               transform.position = new Vector3(79.0f, 1.0f, 0.0f);
                }          
            if (scoreValue == 8)
            {
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = false;
                winText.text = "You Win! Game Created By Jason Morton";
                }
            
           
            }
            else if (collision.collider.tag =="Enemy")
            {
                lives -= 1;
                LivesText.text = lives.ToString();
                Destroy(collision.collider.gameObject);
            
            if (lives <= 0)
            {
                loseText.text = "You Lose";
                Destroy(gameObject);
            }
        }            
    }

    
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
            if(Input.GetKeyDown("escape"))
            {
                Application.Quit();
            }
        }
    }
}
