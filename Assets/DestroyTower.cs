using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyTower : MonoBehaviour
{
    bool destroyed;
    public Image WinnerAcounce;
    public Image Again;
    public static bool ready;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GM.winner != 0 && destroyed == false){

            Destroy(GameObject.FindWithTag("BulletR"));
            Destroy(GameObject.FindWithTag("BulletL"));
            destroyed = true;
        }
        if(GM.winner == 1){
            WinnerAcounce.GetComponent<RectTransform>().position = new Vector2(WinnerAcounce.GetComponent<RectTransform>().position.x, Mathf.Lerp(WinnerAcounce.GetComponent<RectTransform>().position.y,5,.005f));
            WinnerAcounce.GetComponentInChildren<Text>().text = "Player 1 Wins";
            Again.GetComponent<RectTransform>().position = new Vector2(Again.GetComponent<RectTransform>().position.x, Mathf.Lerp(Again.GetComponent<RectTransform>().position.y,-5,.005f));
            GameObject tower = GameObject.FindWithTag("TowerR");
            StartCoroutine(ShakeFallTower(tower));
            tower.transform.position = new Vector2(tower.transform.position.x,Mathf.Lerp(tower.transform.position.y, -21.1f,.005f)); 
            if(Again.GetComponent<RectTransform>().position.y < -2){
                ready = true;
            }
            
        }else if(GM.winner == 2){
            WinnerAcounce.GetComponent<RectTransform>().position = new Vector2(WinnerAcounce.GetComponent<RectTransform>().position.x, Mathf.Lerp(WinnerAcounce.GetComponent<RectTransform>().position.y,5,.005f));
            WinnerAcounce.GetComponentInChildren<Text>().text = "Player 2 Wins";
            Again.GetComponent<RectTransform>().position = new Vector2(Again.GetComponent<RectTransform>().position.x, Mathf.Lerp(Again.GetComponent<RectTransform>().position.y,-5,.005f));
            GameObject tower = GameObject.FindWithTag("TowerL");
            StartCoroutine(ShakeFallTower(tower));
            tower.transform.position = new Vector2(tower.transform.position.x,Mathf.Lerp(tower.transform.position.y, -21.1f,.005f));
            if(Again.GetComponent<RectTransform>().position.y < -2){
                ready = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(this.gameObject.tag == "TowerL" && other.gameObject.tag == "BulletR"){
            GM.changeMain = true;
            GM.winner = 2;
            GM.mainref.transform.position = this.gameObject.transform.position + new Vector3(0,0,-10);
        }else if(this.gameObject.tag == "TowerR" && other.gameObject.tag == "BulletL"){
            GM.changeMain = true;
            GM.winner = 1;
            GM.mainref.transform.position = this.gameObject.transform.position + new Vector3(0,0,-10);
        }
    }
    IEnumerator ShakeFallTower(GameObject tower){
        tower.transform.position = new Vector3(tower.transform.position.x + .02f ,tower.transform.position.y,tower.transform.position.z);
        yield return new WaitForSeconds(.2f);
        tower.transform.position = new Vector3(tower.transform.position.x - .04f,tower.transform.position.y,tower.transform.position.z);
        yield return new WaitForSeconds(.2f);
        tower.transform.position = new Vector3(tower.transform.position.x + .04f,tower.transform.position.y,tower.transform.position.z);
        yield return new WaitForSeconds(.2f);
        tower.transform.position = new Vector3(tower.transform.position.x -.02f ,tower.transform.position.y,tower.transform.position.z);
        
    }
    
}
