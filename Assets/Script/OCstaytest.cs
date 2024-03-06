using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCstaytest : MonoBehaviour
{   
    bool onground,onwall;

    Rigidbody2D rb;
    CircleCollider2D col;
    Vector2 contactpoint, rb_position, xboxSize, yboxSize,poz, scaleChange;
    
    GameObject colItem,colItemy;
    [SerializeField] LayerMask enviroment ;
    [SerializeField] Transform xboxCorner;
    [SerializeField] Transform yboxCorner;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        scaleChange = rb.transform.localScale;
        xboxSize =2*xboxCorner.localPosition*scaleChange;
        yboxSize =2*yboxCorner.localPosition*scaleChange* new Vector2(1,-1); 

    }
    void Update(){
        
    }
    void FixedUpdate(){
        
        onwall = false;
        onground = false;
        Collider2D[] colx = Physics2D.OverlapBoxAll(rb.position,xboxSize,0,enviroment);
        foreach (Collider2D walx in colx){
            colItem =  walx.gameObject;
            if (colItem.tag =="Wall"){
                onwall = true;
            }
        }
        Collider2D[] coly = Physics2D.OverlapBoxAll(rb.position,yboxSize,0,enviroment);
        foreach (Collider2D waly in coly){  
            colItemy =  waly.gameObject;
            if (colItemy.tag =="Wall"){
                onground = true;
            }
        }
        poz = rb.position;
        if (onwall == true){
            Debug.Log("the item is onwall");
        }
        if (onground == true){
            Debug.Log("the item is onground");
        }
    } 

    void OnDrawGizmos()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(poz, xboxSize);
        Gizmos.DrawCube(poz, yboxSize); 
    }

}
