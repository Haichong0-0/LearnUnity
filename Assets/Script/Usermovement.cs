using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Usermovement : MonoBehaviour
{   
    Vector2 Player_position, position_jump;
    Vector2 wd,Dash_direction;
    
    float ground = 0.55f, maxHeight = 4f,distance;
    float wall = 0.3f;
    float nextdash=0,dash_cd = 0.5f;
    int X_direction;//-1:left,1:right
    int Y_direction;//-1: down, 1: up
    int stamina;
    Rigidbody2D rb;
    bool onground, onwall,canjump,canchange,jumping;
    LayerMask Enviroment = 64, Player =8;
    
    [SerializeField] float speed =80f, jump_speed=200f, dash_force = 3500f;
    [SerializeField] int gravity=21;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		Player_position =rb.position;
        rb.gravityScale = gravity;
        wd= new Vector2(1,1);
        


    }
    // Update is called once per frame
    void Update()
    {   
        Player_position =rb.position;
        distance = Player_position.y - position_jump.y; 
        RaycastHit2D downray = Physics2D.Raycast(Player_position, -transform.up,10f,Enviroment);//ray down
        RaycastHit2D leftray = Physics2D.Raycast(Player_position, -transform.right,10f,Enviroment);//ray left
        RaycastHit2D rightray = Physics2D.Raycast(Player_position, transform.right ,10f,Enviroment);//ray right
        Debug.DrawRay(Player_position, 10f*-transform.up, Color.red);
        if (leftray.distance <= wall || rightray.distance <= wall){
            onwall = true;
        }
        if (downray.collider != null){
            if (downray.distance <= ground){//ground detection 
                onground = true;
                canjump = true;//allow jump 
                canchange = true;
            }
            else if(distance>= maxHeight){
                canjump = false;
                onground = false;
            }
            else{
                onground = false;
            }
        }
        else{
            onground = false;
            
        }
        Debug.Log(onwall);

        


        if (onground == false){
            if (canchange == true){
                position_jump = rb.position;//Pre-jump location
                canchange = false;
            }
        }
        
        if (Input.GetKeyUp(KeyCode.K)){
            canjump = false;//can not jump again
            jumping = false;
        }
        


        if (onground == true){
            stamina = 100;
            if (Input.GetKey(KeyCode.D)) {
                rb.AddForce(transform.right * speed);//move right
                X_direction = 1;
            }
            else if (Input.GetKey(KeyCode.A)) {
                rb.AddForce(transform.right * -speed);//move left
                X_direction = -1;
            }

            

            if (Input.GetKey(KeyCode.K)){
                if (onground == true){
                    rb.AddForce(transform.up * jump_speed);//jump up
                    jumping = true;
                }
            }
        }
        else if(onwall ==true){
            if (Input.GetKey(KeyCode.LeftShift)){
                rb.gravityScale = 0;
                stamina -= 1;
                if (stamina <= 0 ){
                    rb.gravityScale = 4;
                }
                if (Input.GetKey(KeyCode.D)) {
                    rb.AddForce(transform.right * speed);//move right
                    X_direction = 1;
                }
                else if (Input.GetKey(KeyCode.A)) {
                    rb.AddForce(transform.right * -speed);//move left
                    X_direction = -1;
                }

            }
            if (Input.GetKeyUp(KeyCode.LeftShift)){
                rb.gravityScale = gravity;
            }
            
        }
        else{
            
            if (Input.GetKey(KeyCode.D)) {
                rb.AddForce(transform.right * speed*0.5f);//move right
                X_direction = 1;
            }
            else if (Input.GetKey(KeyCode.A)) {
                rb.AddForce(transform.right * -speed*0.5f);//move left
                X_direction = -1;
            }
            else{
                X_direction = 0;
            }
            
            if (Input.GetKey(KeyCode.K)){
                if (canjump == true){
                    if (jumping == true){
                        rb.AddForce(transform.up * jump_speed);//jump up
                    }
                }
            }
        }
        if (Input.GetKey(KeyCode.L) && Time.time > nextdash){  //dash
            if (Input.GetKey(KeyCode.W)){
                    Y_direction = 1;
                }
            else if (Input.GetKey(KeyCode.S)){
                    Y_direction = -1;
                }
            else{
                    Y_direction =0;
                }
            nextdash = Time.time + dash_cd;
            //Dash_direction = new Vector2(X_direction,Y_direction);
            Debug.Log(Dash_direction);
            rb.gravityScale =0;
            rb.AddForce(transform.right* X_direction*dash_force);
            rb.AddForce(transform.up*Y_direction*dash_force);
            rb.gravityScale = gravity;
        }






        if (Time.time < nextdash){
            //colour change
        }
        if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.R)){
            SceneManager.LoadScene("SampleScene");
        }

        
    }
    IEnumerator Printaftertime(float time, string stat){
        yield return new WaitForSeconds(time);
        Debug.Log(stat);
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(position_jump, 0.1f);
    }

    

}
