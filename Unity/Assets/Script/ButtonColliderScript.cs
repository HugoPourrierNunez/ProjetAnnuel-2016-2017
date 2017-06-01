using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonColliderScript : MonoBehaviour
{
    [SerializeField]
    ButtonBackdropColliderScript buttonBackdropColliderScript;

    public ButtonBackdropColliderScript getButtonBackdropColliderScript()
    {
        return buttonBackdropColliderScript;
    }
}
