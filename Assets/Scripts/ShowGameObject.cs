using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGameObject : MonoBehaviour {

    public GameObject item; // Assign in inspector
    public bool isShowing = false;

    public void hide_or_show()
    {
        isShowing = !isShowing;
        item.SetActive(isShowing);
    }
}
