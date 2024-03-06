using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StaminaBar : MonoBehaviour
{
    Image StaminaBa;  
    public void setbar(int stamina){
        StaminaBa.fillAmount = (stamina/100);//fills the bar at right percentage 
    }
    void  Start()
    {   

        StaminaBa = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
