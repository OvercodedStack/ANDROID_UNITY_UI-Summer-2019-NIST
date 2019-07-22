using UnityEngine;

public class MenuAppearScript : MonoBehaviour
{

    public GameObject menu; // Assign in inspector
    private bool isShowing;

    void hide_or_show()
    {
        isShowing = !isShowing;
        menu.SetActive(isShowing);
    }
}
