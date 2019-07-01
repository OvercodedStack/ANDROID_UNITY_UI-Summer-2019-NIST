using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_enable_Menu : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing = false;

    private void Start()
    {
        menu.SetActive(isShowing);
    }


    public void hide_or_show()
    {
        isShowing = !isShowing;
        menu.SetActive(isShowing);
    }
}
