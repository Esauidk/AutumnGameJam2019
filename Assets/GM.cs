﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static int winner;
    public Camera Left;
    public Camera Right;
    public Camera Main;
    public static Camera mainref;
    public static bool changeMain;
    bool destroyed;
    public static float timer = 4;
    public Text text1;
    public Text text2;
    bool start;
    public Transform particleSparks;
    bool finish;
    bool transitionComplete;
    GameObject[] screenfade;
    GameObject[] fireKeys;
    bool restart;
    bool grow;
    bool keyalive = true;

    // Start is called before the first frame update
    void Start()
    {
        mainref = Main;
        screenfade = GameObject.FindGameObjectsWithTag("ScreenFade");
        fireKeys = GameObject.FindGameObjectsWithTag("FireKey");
    }

    // Update is called once per frame
    void Update()
    {
        if(transitionComplete == false){
            foreach(GameObject screen in screenfade){
                if(screen.GetComponent<Image>().color.a > .01f){
                    screen.GetComponent<Image>().color = Color.Lerp(screen.GetComponent<Image>().color, new Color(0,0,0,0),.05f);
                }else{
                    transitionComplete = true;
                }
            }
        }
        if(transitionComplete == true && keyalive == true){
            foreach(GameObject key in fireKeys){
                if(key.GetComponent<Image>().color.a < .98f){
                    key.GetComponent<Image>().color = Color.Lerp(key.GetComponent<Image>().color, new Color(1,1,1,1),.05f);
                }else{
                    if(key.GetComponent<RectTransform>().localScale.x < 1.05f && grow == false){
                        grow = true;
                    }
                    if(key.GetComponent<RectTransform>().localScale.x > 1.2f && grow == true){
                        grow = false;
                    }
                    if(grow == true){
                        key.GetComponent<RectTransform>().localScale = Vector3.Lerp(key.GetComponent<RectTransform>().localScale, new Vector3(1.217f,1.217f,1.217f),.05f);
                    }else{
                        key.GetComponent<RectTransform>().localScale = Vector3.Lerp(key.GetComponent<RectTransform>().localScale, new Vector3(1f,1f,1f),.05f);
                    }
                }
            }
        }
        
        
        if(timer > 0 && transitionComplete == true){
            timer -= .01f;
        }else if(timer <= 0 && start == false){
            Destroy(text1.transform.parent.gameObject);
            Destroy(text2.transform.parent.gameObject);
            start = true;
        }
        text1.text = (int)timer + "";
        text2.text = (int)timer + "";
        if(changeMain == true && destroyed == false){
            Left.rect = Rect.zero;
            Right.rect = Rect.zero;
            Main.rect = new Rect(0,0,1,1);
        }
        if(winner != 0 && changeMain == true && destroyed == false){
            if(particleSparks.gameObject.GetComponent<ParticleSystem>().isPlaying == true){
                particleSparks.gameObject.GetComponent<ParticleSystem>().Stop();
            }
            destroyed = true;
        }
        if(changeMain == true && winner == 0){
            particleSparks.position = LeftProjectileControl.hit.transform.position;
            if(particleSparks.gameObject.GetComponent<ParticleSystem>().isPlaying == false){
                particleSparks.gameObject.GetComponent<ParticleSystem>().Play();
            }
        }
        if(winner!=0 && finish == false){
            Destroy(Left.gameObject);
            Destroy(Right.gameObject);
            foreach(GameObject key in fireKeys){
                Destroy(key);
            }
            keyalive = false;
            Destroy(Main.GetComponent<Camera_Follow>());
            finish = true;
        }else if(winner!=0 && (Input.GetButtonDown("Fire1")||Input.GetButtonDown("Fire2")) && DestroyTower.ready == true){
            
            restart = true;
            
        }
        if(GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Image>().color.a < .89f && restart == true){
                GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Image>().color = Color.Lerp(GameObject.FindGameObjectWithTag("ScreenFade").GetComponent<Image>().color, new Color(0,0,0,1),.05f);
        }else if(restart == true){
                winner = 0;
                restart = false;
                changeMain = false;
                timer = 4;
                DestroyTower.ready = false;
                SceneManager.LoadScene("SampleScene");
        }
        if(Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }

    }
}
