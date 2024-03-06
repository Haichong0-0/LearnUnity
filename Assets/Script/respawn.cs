using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour
{   
    public GameObject player;
    Usermovement2 playersc;
    Vector2 resppos;
    bool flag=false;
    string no;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        playersc = player.GetComponent<Usermovement2>();//player script
        no = PlayerPrefs.GetString("loadno");//choosing save
    }

    // Update is called once per frame
    void Update()
    {
        if (playersc.alive ==false&&flag == false){
            flag = true;
            StartCoroutine(retu());//respawnb after delay
        }
    }
    public void enterroom(){
        StartCoroutine(record());//recod the player position after delay
    }
    IEnumerator retu(){
        yield return new WaitForSeconds(0.5F);
        player.transform.position = resppos;//send back to recorded positon
        player.GetComponent<Renderer>().enabled = true;//display player
        adddeath();//death count+1
        flag = false;
        playersc.alive = true;//set player death
    } 
    IEnumerator record(){
        yield return new WaitForSeconds(0.2F);
        resppos = player.transform.position;
        PlayerPrefs.SetFloat("lastposx"+no,player.transform.position.x);//save location
        PlayerPrefs.SetFloat("lastposy"+no,player.transform.position.y);
    }
    void adddeath(){
        count = PlayerPrefs.GetInt("death"+no);//get data
        PlayerPrefs.SetInt("death"+no,count+1);//save data
    }
}
