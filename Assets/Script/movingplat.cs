using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingplat : MonoBehaviour
{
    // Start is called before the first frame update
    public float xoffset, yoffset; 
    float distanceToA, distanceToB,tempx,tempy,facing,rotangle;
    Vector2 a1,b1,targetpos,platpos,objpos;
    int direction=0;
    string temp;
    void Start(){
        platpos = transform.position;
        a1 =new Vector2(platpos.x+xoffset,platpos.y+yoffset);//define right target
        b1 = new Vector2(platpos.x-xoffset,platpos.y-yoffset);//define left target
        targetpos = b1;        
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        distanceToA = Vector2.Distance(transform.position, a1);//determin distance
        distanceToB = Vector2.Distance(transform.position, b1);
        if(direction==1 && distanceToA == 0 ){//travel to B
            targetpos = b1;
            direction =0;
        }
        else if (direction == 0 && distanceToB == 0){//Travel to A
            targetpos = a1;
            direction =1;
        }
        movement();//move the plat
    }
    void movement(){
        transform.position =  Vector2.MoveTowards(transform.position, targetpos,0.02f);// transformation of the platform
    }
    void OnCollisionEnter2D(Collision2D collision){
        collision.transform.SetParent(transform); //make object child of the platform
    }

    void OnCollisionExit2D(Collision2D collision){
        collision.transform.SetParent(null);    //isolate the object
    }
}
