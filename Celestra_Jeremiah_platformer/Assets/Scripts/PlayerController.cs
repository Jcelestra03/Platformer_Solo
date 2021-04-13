using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D myRB;
    public Vector2 velocity;
    private Vector2 respawnPos;
    private Vector2 groundDetection;
    public float groundDetectDistance = .1f;
    private Quaternion zero;
    public float feetOffset;


    public float Movementspeed = 5;
    public float Jumpheight = 11;
    
   
    public int JumpNumber = 2;

    public int Health = 5;

    public bool isFalling;
    public bool canGlide;

    public float glideSpeed = -1;
    public float thrust = -15.0f;
    public bool groundpound;
    

    public float timer;
    public float timedifference;

    


    //portal gun
    //grapple hook
    //gun 
    //wonky slush 
  


    // Start is called before the first frame update
    void Start()
    {
        JumpNumber = 2;
        myRB = GetComponent<Rigidbody2D>();
        Jumpheight = 11;
        
        respawnPos = new Vector2(-7,0);
            zero = new Quaternion();
        groundpound = false;
        timedifference = 1.2f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Health <= 0)
        {
            transform.SetPositionAndRotation(respawnPos, zero);
            Health = 5;
          
        }

        groundDetection.x = transform.position.x + feetOffset;
        groundDetection.y = transform.position.y - .51f;

        if (this.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            isFalling = true;
        }
        
        else if (this.gameObject.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            isFalling = false;
        }

        if (isFalling == true)
        {
            canGlide = true;
        }

        else if (isFalling == false)
        {
            canGlide = false;
        }
    


        velocity = myRB.velocity;

        velocity.x = Input.GetAxisRaw("Horizontal") * Movementspeed;

        Debug.DrawRay(groundDetection, Vector2.down, UnityEngine.Color.white);

        if (Input.GetKeyDown(KeyCode.Space) && Physics2D.Raycast(groundDetection, Vector2.down, groundDetectDistance))
        {
            velocity.y = Jumpheight;
        }
        

        if (Input.GetKey(KeyCode.E) && canGlide == true)
        {
            velocity.y = glideSpeed;
        }
        
        if (Input.GetKey(KeyCode.C) && isFalling == true)
        {
            velocity.y = thrust;
            groundpound = true;
        }
        myRB.velocity = velocity;

        //if (GameObject.Find("Enemy").GetComponent<EnemyController>().groundoff == false && isFalling == false)
        //groundpound = false;
        if (groundpound == true)
        {
            timer += Time.deltaTime;
            if (timer >= timedifference)
            {
                groundpound = false;
                timer = 0;
            }
            Movementspeed = 2;
        }

        else if (groundpound == false)
            Movementspeed = 5;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Wonky") || collision.gameObject.name.Contains("Slush"))
            Health--;

        if (collision.gameObject.name.Contains("Enemy"))
            Health--;


        if (collision.gameObject.name.Contains("Door"))
            SceneManager.LoadScene("Second");
        if (collision.gameObject.name.Contains("Door2"))
        {
            SceneManager.LoadScene("Win");
        }
    }

}
