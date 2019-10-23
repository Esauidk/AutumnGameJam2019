using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftProjectileControl : MonoBehaviour
{
    Rigidbody2D rb;
    bool fired = false;
    float precentage = 0;
    public Text power;
    public static Collision2D hit;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.timer <=0){
            if(fired == false){
                power.text = (int)(precentage*100) + "%";
                precentage = Bullets.Precentage(precentage);
                precentage = Mathf.Clamp(precentage,0,1);
            }
            if(fired == true && power != null){
                Destroy(power.transform.parent.gameObject);
            }
        }
        
        
        
        
    }
    void FixedUpdate(){
        if(GM.timer <=0){
            if(Input.GetButtonDown("Fire1")&& fired == true){
                rb.AddForce(new Vector2(500*precentage,0));
            }else if( Input.GetButtonDown("Fire1") && fired == false){
                rb.AddForce(new Vector2(3000*precentage,0));
                GameObject.FindWithTag("SmokeL").GetComponent<ParticleSystem>().Play();
                fired = true;
            }
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "BulletR"){
            hit = other;
            GM.changeMain = true;
        }
    }
}
