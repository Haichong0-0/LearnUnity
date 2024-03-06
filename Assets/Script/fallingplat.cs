using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingplat : MonoBehaviour
{   
    float speed = 50f,amount = 0.1f,x,y,t = 5f; //how fast it shakes
    bool fall = false,onshake = false;
    Rigidbody2D rb;
 
    // Start is called before the first frame update
    void Start()
    {
        x= transform.position.x;//set positon x
        y = transform.position.y;//set position y
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        if (fall == true){
            onshake = false;
            rb.bodyType = RigidbodyType2D.Dynamic;//set rigidbody to dynamic for gravity to work
            t  = t - Time.deltaTime;//calculate remaining time
        }
        if (t<=0){
            gameObject.SetActive(false);//kill platform
        }
        if (onshake ==true){
            shake();//vibrate
        }
    }
    void OnCollisionEnter2D(Collision2D collision){
        collision.transform.SetParent(transform); //make object child of the platform
        onshake  =true;
        StartCoroutine(startfall());//set the platform ready to fall after delay
    }

    void OnCollisionExit2D(Collision2D collision){
        collision.transform.SetParent(null);//isolate the object form the player
    }
    IEnumerator startfall(){
        yield return new WaitForSeconds(0.5F);
        fall= true;//start falling
    } 
    void shake(){
        //vibration according to sin graph
        transform.position = new Vector2(x+Mathf.Sin(Time.time * speed) * amount,transform.position.y);
    }
}
