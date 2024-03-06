using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinkingplat : MonoBehaviour
{
    // Start is called before the first frame update
    bool fall =false;
    Vector2 initialpos;
    void Start()
    {
        initialpos = transform.position;//record position
    }

    void FixedUpdate()
    {
        if (fall ==false){
            //return the platform to original position
            transform.position = Vector2.MoveTowards(transform.position,initialpos,2f*Time.deltaTime);
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        collision.transform.SetParent(transform); //make object child of the platform
        StartCoroutine(startfall());//set fall to true after delay
    }
    void OnCollisionStay2D(Collision2D collision){
        if (fall == true){
            //platform falls
            transform.position = transform.position + new Vector3(0,-1f*Time.deltaTime ,0);
        }
    }
    void OnCollisionExit2D(Collision2D collision){
        collision.transform.SetParent(null);    //isolate the object
        StartCoroutine(startfall());//set fall to false
    }
    IEnumerator startfall(){
        yield return new WaitForSeconds(0.5F);
        if (fall ==true){
            fall = false;
        }
        else{
            fall =true;
        }
    }  
}

