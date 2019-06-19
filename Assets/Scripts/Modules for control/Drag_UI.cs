using UnityEngine;
using System.Collections;


//Referenced https://www.youtube.com/watch?v=hzuxb8CPGyQ
public class Drag_UI : MonoBehaviour
{
    private float offsetX;
    private float offsetY;

    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
    }

    public void OnDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }
}