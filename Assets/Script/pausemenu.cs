using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausemenu : MonoBehaviour
{   
    public GameObject player,canvas;
    Usermovement2 sc;
    // Start is called before the first frame update
    void Start(){
        sc = player.GetComponent<Usermovement2>();
        Time.timeScale =1f;//set time to normal
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            canvas.SetActive(true); //call pause menu
            pause();//pause the game
        }
    }
    public void pause(){
        Time.timeScale =0f;//pause the game
    }
    public void resume(){
        Time.timeScale = 1f;//continue the game
    }
    public void retur(){
        SceneManager.LoadScene("startmenu");//back to main menu
        Time.timeScale = 1f;
    }
    public void retry(){
        sc.killplayer();// kills the player
        Time.timeScale =1f;
    }
}   
