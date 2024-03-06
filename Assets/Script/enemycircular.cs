using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycircular : MonoBehaviour
{   
    public GameObject centre;
    GameObject player;
    Usermovement2 sc;
    Vector3 centreposition;
    [SerializeField]
    public int clockwise=1;//direction of rotation
    
    // Start is called before the first frame update
    void Start()
    {   
        //set center of orbit
        centreposition = transform.position+ centre.transform.localPosition;
    }
    // Update is called once per frame
    void FixedUpdate()
    {   
        //rotate around center
        transform.RotateAround(centreposition, new Vector3(0,0,clockwise), -2);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //line towards the center from the enemy object
        Gizmos.DrawLine(transform.position, centreposition);
    }
    void OnTriggerEnter2D(Collider2D collider){
        player = collider.gameObject;
        if (player.tag == "Player"){
            sc = player.GetComponent<Usermovement2>();
            sc.killplayer();
        }
    }

}
