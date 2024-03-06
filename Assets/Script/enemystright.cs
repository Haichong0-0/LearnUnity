using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemystright : MonoBehaviour
{
    // Start is called before the first frame update
    public float xoffset, yoffset;
    float distanceToA, distanceToB,tempx,tempy,facing,rotangle,nowface,changeamount,i,angularspeed=100;
    bool isturning= true;
    Vector2 a1,b1,targetpos,platpos,objpos;
    int direction=0;
    string temp;
    GameObject player;
    Usermovement2 sc;
    void Start(){
        platpos = transform.position;
        a1 =new Vector2(platpos.x+xoffset,platpos.y+yoffset);//define right target
        b1 = new Vector2(platpos.x-xoffset,platpos.y-yoffset);//define left target
        targetpos = b1;
        objpos = transform.position;
        engageturn();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        objpos = transform.position;
        distanceToA = Vector2.Distance(objpos, a1);
        distanceToB = Vector2.Distance(objpos, b1);//determin position to two points
        if (isturning ==true){
            transform.eulerAngles= new Vector3(0,0,i);//rotating object
            i=i+changeamount;
        }
        else if(isturning ==false){
            movement();
        }
        if (i>=facing&& direction ==0){
            isturning =false; // set object back to movement
        }
        else if(i<=facing&& direction ==1){
            isturning =false; 
        }
            
        

        if(direction==1 && distanceToA == 0 ){//travel to B
            targetpos = b1;
            direction =0;
            engageturn();
            
        }
        else if (direction == 0 && distanceToB == 0){//Travel to A
            targetpos = a1;
            direction =1;
            engageturn();
        }
        
        
    }
    void movement(){
        transform.position =  Vector2.MoveTowards(objpos, targetpos,0.04f);//transformatioon of the platform
    }

    float redirect(){//return angle of facing
        tempx = targetpos.x - objpos.x;//horizontal change
        tempy = targetpos.y - objpos.y;//vertical change
        if(tempx ==0){ 
            //determin change angle
            if (tempy>0){//if angles changes to the left
                return -90f;
            }
            else if (tempy<0){// if angle changes to the right
                return 90f;
            }
        }
        else if(tempy ==0){
            if (tempx>0){
                return 180f;
            }
            else if (tempy<0){
                return 0f;
            }
        }
        //calculate the angle rotated from rad to deg
        rotangle = (Mathf.Atan(-tempx/tempy))*180/Mathf.PI;
        return rotangle;
    }
    

    
    void engageturn(){
        facing =redirect();//face the next direction
        isturning = true;//start turn
        changeamount = (facing-i)/angularspeed;//speed of turning
    }

    void OnTriggerEnter2D(Collider2D collider){
        player = collider.gameObject;
        Debug.Log(player.name);
        if (player.tag == "Player"){//detec player
            sc = player.GetComponent<Usermovement2>();
            sc.killplayer();
        }
        
    }

}
