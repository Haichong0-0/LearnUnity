using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject nametext, deathtext,berrytext;
    public string load;
    string loadname;
    int death, berry;
    void OnEnable()//every time the page is loaded
    {   
        loadname = PlayerPrefs.GetString("loadname"+load);
        death = PlayerPrefs.GetInt("death"+load);
        berry = PlayerPrefs.GetInt("berry"+load);
        Debug.Log("loadname"+load+ " is " +loadname);
        if (loadname == "KE7U0IUKmrtfbh6XZYN2"){
            //detect exsisting file
            //file does not exist display 0
            nametext.GetComponent<TMP_Text>().text = "New load";
            deathtext.GetComponent<TMP_Text>().text = "X 0";
            berrytext.GetComponent<TMP_Text>().text = "X 0";
        }
        else{
            //file exist display data
            nametext.GetComponent<TMP_Text>().text = loadname;
            deathtext.GetComponent<TMP_Text>().text = "X "+ death.ToString();
            berrytext.GetComponent<TMP_Text>().text = "X " + berry.ToString();
        }
    }
    public void reset(){//set all data do defalt
        PlayerPrefs.SetInt("death"+load,0);
        PlayerPrefs.SetFloat("lastposx"+load,-28.49f);
        PlayerPrefs.SetFloat("lastposy"+load,2.03f);
        PlayerPrefs.SetInt("berry"+load,0);
        PlayerPrefs.SetString("loadname"+load, "KE7U0IUKmrtfbh6XZYN2"); 
        gameObject.SetActive(true);
        nametext.GetComponent<TMP_Text>().text = "New load";
        deathtext.GetComponent<TMP_Text>().text = "X 0";
        berrytext.GetComponent<TMP_Text>().text = "X 0";
        Debug.Log(load);
    }
    public void setload(){
        //choose current save
        //so update correct save
        PlayerPrefs.SetString("loadno",load);
        Debug.Log(load);
    }
}
