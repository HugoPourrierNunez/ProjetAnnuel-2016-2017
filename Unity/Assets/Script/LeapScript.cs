using System.Collections;
using UnityEngine;


public class LeapScript : MonoBehaviour
{

    public Color c;
    public static Color selectedColor;
    public bool selectable = false;

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.transform.parent.name.Equals("index"))
        {
            if (this.selectable)
            {
                LeapScript.selectedColor = this.c;
                this.transform.Rotate(Vector3.up, 33);
                return;
            }

            transform.gameObject.GetComponent<Renderer>().material.color = LeapScript.selectedColor;
        }
    }
}