using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCam,resp;
    respawn res;
    // Start is called before the first frame update
    void Start(){
        res = resp.GetComponent<respawn>();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){//if player enter room
            virtualCam.SetActive(true);//turn camera on
            res.enterroom();//record position 
        }

    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){//if playe leave room
            virtualCam.SetActive(false);//turn camera off
        }
    }
}
