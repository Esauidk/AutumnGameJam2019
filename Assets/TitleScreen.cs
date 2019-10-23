using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleScreen : MonoBehaviour
{
    RectTransform titlebar;
    RectTransform instructions;
    RectTransform StartBut;
    bool start;
    int selected = 1;
    bool selectedBut;
    Image fade;
    
    // Start is called before the first frame update
    void Start()
    {
        titlebar = GameObject.FindWithTag("TitleBar").GetComponent<RectTransform>();
        instructions = GameObject.FindWithTag("Instructions").GetComponent<RectTransform>();
        StartBut = GameObject.FindWithTag("StartButton").GetComponent<RectTransform>();
        fade = GameObject.FindWithTag("ScreenFade").GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start == false){
            titlebar.position = new Vector3(titlebar.position.x,Mathf.Lerp(titlebar.position.y,550,.05f), titlebar.position.z);
            instructions.position = new Vector3(instructions.position.x,Mathf.Lerp(instructions.position.y,400,.02f), instructions.position.z);
            StartBut.position = new Vector3(StartBut.position.x,Mathf.Lerp(StartBut.position.y,400,.02f), StartBut.position.z);
        }else if(start == true){
            if(selected == 1){
                StartBut.localScale = Vector3.Lerp(StartBut.localScale,new Vector3(1.1972f,1.1972f,1.1972f), .05f);
                if(instructions.localScale != new Vector3(1,1,1)){
                    instructions.localScale = Vector3.Lerp(instructions.localScale,new Vector3(1,1,1),.05f);
                }
            }else if(selected == 2){
                instructions.localScale = Vector3.Lerp(instructions.localScale, new Vector3(1.1972f,1.1972f,1.1972f),.05f);
                if(StartBut.localScale != new Vector3(1,1,1)){
                    StartBut.localScale = Vector3.Lerp(StartBut.localScale,new Vector3(1,1,1),.05f);
                }
            }
        }
        if(StartBut.position.y > 90){
            start = true;
        }
        if(Input.GetAxis("Horizontal") > 0 && selected == 1){
            selected = 2;
        }else if(Input.GetAxis("Horizontal") < 0 && selected == 2){
            selected = 1;
        }
        if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2")){
            selectedBut = true;
        }
        if(selected == 1 && selectedBut == true){
            titlebar.position = new Vector3(titlebar.position.x,Mathf.Lerp(titlebar.position.y,1660,.05f), titlebar.position.z);
            if(titlebar.position.y > 500){
                instructions.position = new Vector3(instructions.position.x,Mathf.Lerp(instructions.position.y,1660,.02f), instructions.position.z);
            }
            if(instructions.position.y> 500){
                StartBut.position = new Vector3(StartBut.position.x,Mathf.Lerp(StartBut.position.y,1660,.02f), StartBut.position.z);
            }
            if(StartBut.position.y > 500){
                fade.color = Color.Lerp(fade.color, new Color(0,0,0,1),.05f);
            }
            
            
        }
        if(fade.color.a > .98f){
            SceneManager.LoadScene("SampleScene");
            Debug.Log("Load!");
        }
        
        

    }
}
