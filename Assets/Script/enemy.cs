using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{   
    GameObject player;
    Usermovement2 sc;
    void OnTriggerEnter2D(Collider2D collider){
        player = collider.gameObject;
        Debug.Log(player.name);
        if (player.tag == "Player"){//detec collision item
            sc = player.GetComponent<Usermovement2>();
            sc.killplayer();
        }
    }
}
