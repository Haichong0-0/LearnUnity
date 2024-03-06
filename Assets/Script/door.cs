using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    GameObject colider,rb;
    float respcd = 1.5f, resptime;
    public GameObject player;
    bool flag=true;
    public bool open = false;
    Transform tr;
    Usermovement2 sc;
    void Start()
    {
        rb = gameObject;
        sc = player.GetComponent<Usermovement2>();
    }
    void OnTriggerEnter2D(Collider2D col){
        colider = col.gameObject;
        if (flag == true){
            if (colider.tag == "Player"){
                if (sc.havekey == true){
                    sc.GetKey(false);//finish using the key
                    open = true;//door open
                }
            }
        }   
    }
    void dooropen(){
        rb.SetActive(false);//door disappear
    }
}
