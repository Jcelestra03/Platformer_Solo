using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D myRB; 
    private Vector2 velocity; 
    private GameObject playerTarget; 
    public float movementSpeed = 2;

    public float health = 1;


    public bool weak;
    public bool colorb;
    //public bool invoke;
    private float timer;

    public float cooldown = 2;
  
 
  
    public bool isFollowing = false;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>(); 
        playerTarget = GameObject.Find("player");
        health = 1;
      
        colorb = false;
        //invoke = false;
        timer = 2.0f;
   
        weak = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject.Find("Manager").GetComponent<GameManager>().addtime = true;
        }
        Vector3 lookPos = playerTarget.transform.position - transform.position;

        lookPos.Normalize();

        velocity = myRB.velocity;

        if (!isFollowing) 
        { 
            velocity.x = 0; 
        }

        if (isFollowing)
        {
            velocity.x = lookPos.x * movementSpeed;
            if (velocity.x > 0)                
                GetComponent<SpriteRenderer>().flipX = false;            
            else if (velocity.x < 0)                
                GetComponent<SpriteRenderer>().flipX = true;        
        }

        myRB.velocity = velocity;

        //if (isFollowing == true)
        //    invoke = true;
        //else if (isFollowing == false)
        //    invoke = false;

        //if (invoke == true)
        //    InvokeRepeating("shield", 1, 1.0f);
        //else if (invoke == false)
        //    CancelInvoke();

        if (isFollowing == true && weak == true && !colorb)
        {
            timer += Time.deltaTime;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            if (timer >= cooldown)
            {
                weak = false;
                timer = 0;
            }
        }
        else if (weak == false && isFollowing == true && !colorb)
        {
            timer += Time.deltaTime;
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            if (timer >= cooldown)
            {
                weak = true;
                timer = 0;   
            }
        }

        if (colorb == true)
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;

        //if (weak == true)
        //    GameObject.Find("Enemy").GetComponent<SpriteRenderer>().color = Color.green;
        //
        //else if (weak == false)
        //    GameObject.Find("Enemy").GetComponent<SpriteRenderer>().color = Color.red;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isFollowing && (collision.gameObject.name == "player"))
        {
            isFollowing = true;
            timer = Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("player") && GameObject.Find("player").GetComponent<PlayerController>().groundpound == true)
        {
            if (colorb == true)
                health--;
        }
        if (collision.gameObject.name.Contains("player") && GameObject.Find("player").GetComponent<PlayerController>().groundpound == true && weak == true)
        {
            colorb = true;
            //invoke = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    { 
        if (isFollowing && (collision.gameObject.name == "player")) 
            isFollowing = false; 
    }

    //void shield()
    //{
    //    if (weak == false)
    //        weak = true;

    //    else if (weak == true)
    //        weak = false;
    //}

}
