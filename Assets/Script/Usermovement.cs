using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usermovement : MonoBehaviour
{   
    Vector2 Player_position;
    float speed =150f, jump_speed=200f;
    float ground = 0.6f, maxHeight = 100f;
    Rigidbody2D rb;
    bool onground, onwall,canjump;
    LayerMask Enviroment = 64, Player =8;
    Transform GroundCheck1;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		Player_position =rb.position;

    }
    // Update is called once per frame
    void Update()
    {   
        Player_position =rb.position;
        RaycastHit2D hit = Physics2D.Raycast(Player_position, -transform.up,Mathf.Infinity,Enviroment);//ray down
        Debug.DrawRay(Player_position, 0.6f*-transform.up, Color.red);
        Debug.Log(canjump);
        if (hit.collider != null){
            if (hit.distance <= ground){//ground detection
                onground = true;
                canjump = true;//allow jump 
            }
            else if(hit.distance>= maxHeight){
                canjump = false;
            }
            else{
                onground = false;
                
            }
        }
        if (Input.GetKeyUp(KeyCode.K)){
            canjump = false;//can not jump again
        }
        
        if (onground ==true){
            Printaftertime(1,"Ground");
            if (Input.GetKey(KeyCode.D)) {
                rb.AddForce(transform.right * speed);//move right
            }
           else if (Input.GetKey(KeyCode.A)) {
                rb.AddForce(transform.right * -speed);//move left
            }
            if (canjump == true){
                if (Input.GetKey(KeyCode.K)){
                    rb.AddForce(transform.up * jump_speed);//jump up
                }
            }
        }
        else if(onwall == true){
            
        }
        else{
            Printaftertime(1,"air");
            if (Input.GetKey(KeyCode.D)) {
                rb.AddForce(transform.right * speed * 0.5f);//move right
            }
            else if (Input.GetKey(KeyCode.A)) {
                rb.AddForce(transform.right * -speed*0.5f);//move left
            }
            if (canjump == true){
                if (Input.GetKey(KeyCode.K)){
                    rb.AddForce(transform.up * jump_speed);//jump up
                }
            }
        }
        

    }
    IEnumerator Printaftertime(float time, string stat){
     yield return new WaitForSeconds(time);
    Debug.Log(stat);
     
    }

    

}
