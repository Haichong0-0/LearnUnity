using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gem : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject colider,rb;
    float respcd = 1.5f, resptime;
    public GameObject player;
    bool flag=true;
    Usermovement2 sc;
    void Start()
    {
        rb = gameObject;
        sc = player.GetComponent<Usermovement2>();
    }
    void FixedUpdate(){
        if (Time.time>= resptime){//compare respawn time
            if (flag == false){
                rb.GetComponent<Renderer>().enabled = true ;//respawn
                flag = true;//allow collision
            } 
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        colider = col.gameObject;
        if (flag == true){
            if (colider.tag == "Player"){//comfirm player object
                rb.GetComponent<Renderer>().enabled = false ; //disappear
                resptime = Time.time + respcd; // set respawn time
                flag = false;
                sc.ResetStamina();//reset player stamia and jump
            }
        }
            
    }
}
