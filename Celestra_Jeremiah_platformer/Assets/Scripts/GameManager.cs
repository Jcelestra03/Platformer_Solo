using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text hUI;
    public Text cUI;
    public Text rUI;
    public int UIhealth = 5;




    //public float Timer = 0.0f;
    public float Timer = 90.0f;
    private bool stop = false;
    public bool pause;
    public bool addtime;


    // Start is called before the first frame update
    void Start()
    {
        UIhealth = 5;
        pause = false;
        addtime = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Timer <= 0)
        {
            SceneManager.LoadScene("Menu");
        }

        //Timer += Time.deltaTime;

        if(!stop)
        {
            Timer -= Time.deltaTime; 
        }
        if (Timer <= 0)
        {
            stop = true;
        }
        if (addtime == true)
        {
            Timer = Timer + 5.0f;
            addtime = false;
        }
        


        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.timeScale > 0)
            {
                Time.timeScale = 0;
                stop = true;
                GameObject.Find("Space").GetComponent<Image>().enabled = true;
                GameObject.Find("Text").GetComponent<Text>().enabled = true;
            }

            else
            {
                Time.timeScale = 1;
                stop = false;
                GameObject.Find("Space").GetComponent<Image>().enabled = false;
                GameObject.Find("Text").GetComponent<Text>().enabled = false;
            }
                
        }



        cUI.text = "Timer: " + Timer; 
        hUI.text = "Current Health: " + GameObject.Find("player").GetComponent<PlayerController>().Health; 
    }
}
