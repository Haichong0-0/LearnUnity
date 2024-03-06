using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spring : MonoBehaviour
{   
    GameObject colider,rb;
    float  resptime;
    [SerializeField] float force;

    public GameObject player;

    Usermovement2 sc;
    void Start()
    {
        rb = gameObject;
        sc = player.GetComponent<Usermovement2>();
    }
 
    void OnCollisionEnter2D(Collision2D col){
        colider = col.gameObject;
            if (colider.tag == "Player"){//detect player
                colider.GetComponent<Rigidbody2D>().AddForce(force * transform.up);//bounce the player up
            }
            sc.ResetStamina();//set to full stamina and allow jump
        }
            
    
}
