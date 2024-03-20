using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class GestureInteractableObject : MonoBehaviour
{
    public bool selected;
    public abstract void ProcessGestures();

    public void Register(GestureInteractableObject obj){
        GestureRoomManager.RegisterInteractable(obj);
    }
}
