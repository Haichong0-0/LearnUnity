using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strawberry : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject colider,rb;
    float d;
    int count;
    public GameObject player;
    bool flag= false,dt=true;
    Usermovement2 sc;
    Vector2 targetpos;
    string no;
    
    //private Transform rb;
    void Start()
    {
        rb = gameObject;
        sc = player.GetComponent<Usermovement2>();
    }
    void FixedUpdate(){
        if (flag == true){//if player has key
            targetpos = sc.keypos();//set tranform positon
            gameObject.transform.position = Vector2.MoveTowards(transform.position, targetpos,6f*Time.deltaTime);// fly to player
            if (sc.haveberry == false){
                if(dt == false){
                    d = Vector2.Distance(transform.position, targetpos);  
                }
                if (d==0){//if arrive at target position
                    dt = true;
                    addberry();//berry count +1
                    StartCoroutine(dieafter());//kill berry after delay
                    d=1;
                } 

            }
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        colider = col.gameObject;
            if (colider.tag == "Player"){//if it is player
                flag = true;
            }
    }
    IEnumerator dieafter(){
        yield return new WaitForSeconds(0.2F);
        gameObject.SetActive(false);//die
    }  

    void addberry(){
        no = PlayerPrefs.GetString("loadno");//choose save
        count = PlayerPrefs.GetInt("berry"+no);//get berry no
        PlayerPrefs.SetInt("berry"+no,count+1);//set berry no to +1
        Debug.Log(no);
        Debug.Log(count);
    }
}
