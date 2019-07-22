using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UI_Image_indicator : MonoBehaviour {
    private bool status;
    public Image img;

    public void toggle_status()
    {
        status = !status;
    }
    void Update()
    {
        if (status)
        {
            img.GetComponent<Image>().color = new Color32(0, 255, 0, 100);
        }
        else
        {
            img.GetComponent<Image>().color = new Color32(255, 0, 0, 100);
        }
    }
}
