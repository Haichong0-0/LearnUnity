using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject colider;
    bool flag=false;
    public GameObject player, doorobject;
    Usermovement2 sc;
    door dsc;
    Vector2 targetpos,position,doorpos;
    float d;
    void Start()
    {
        sc = player.GetComponent<Usermovement2>();
        position = gameObject.transform.position;
        dsc = doorobject.GetComponent<door>();
        doorpos = dsc.transform.position;
    }
    
    void Update()
    {   
        position = gameObject.transform.position;
        if(dsc.open == true){
            gameObject.transform.position = Vector2.MoveTowards(transform.position, doorpos,6f*Time.deltaTime); // fly to door
            d = Vector2.Distance(transform.position, doorpos);
            if (d==0){
                StartCoroutine(dieafter()); //key disappear after delay
            }
        }
        else if (flag == true){
            targetpos = sc.keypos();//set key target position
            gameObject.transform.position = Vector2.MoveTowards(transform.position, targetpos,6f*Time.deltaTime);// fly to player
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        colider = col.gameObject;
            if (colider.tag == "Player"){
                flag =true;
                sc.GetKey(true);
        }
            
    }
    IEnumerator dieafter(){
        yield return new WaitForSeconds(0.4F);
        doorobject.SetActive(false);//kill door
        gameObject.SetActive(false);//kill key
    }  
    
}
