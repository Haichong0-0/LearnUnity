using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class read : MonoBehaviour
{
    // Start is called before the first frame update
    string input,no;
    string loadname;
    public GameObject inputField;
    void OnEnable()
    {   
        no = PlayerPrefs.GetString("loadno");
        loadname = PlayerPrefs.GetString("loadname"+no);//pick the wanting save
        Debug.Log("loadname"+no+ " is " +loadname);
        if (loadname != "KE7U0IUKmrtfbh6XZYN2"){//detect data
            SceneManager.LoadScene("Map1");//if data is not empty load the map
        }
    }


    public void saveinput(){
        input = inputField.GetComponent<Text>().text;//sanitize input
        if (input != ""){
            PlayerPrefs.SetString("loadname"+no, input); //store data locally
        }
    }
}
