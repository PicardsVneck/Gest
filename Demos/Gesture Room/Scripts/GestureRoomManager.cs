using System.Collections.Generic;
using UnityEngine;

public class GestureRoomManager : GestStateMachine
{
    private static List<GestureInteractableObject> gestureInteractables;
    public static bool locked;

    private void Awake()
    {
        transitions = new List<IGestState>();
        //transitions.Add();
    }

    private void Update()
    {
        if (locked)
            return;

        GestureInteractableObject selectedObject = GetClosestInteractableObject();

        foreach (GestureInteractableObject obj in gestureInteractables)
        {
            obj.selected = (obj == selectedObject);
            if (obj.selected)
            {
                obj.ProcessGestures();
            }
        }
    }

    private GestureInteractableObject GetClosestInteractableObject()
    {
        float smallestAngle = 180f;
        Vector3 cameraPosition = GestSystem.GetHeadsetPosition();
        Vector3 cameraDirection = GestSystem.GetHeadsetDirection();
        GestureInteractableObject selectedObject = null;

        foreach (GestureInteractableObject obj in gestureInteractables)
        {
            Vector3 directionToInteractable = obj.transform.position - cameraPosition;
            float angleToObject = Vector3.Angle(cameraDirection, directionToInteractable);
            if (angleToObject < 30 && angleToObject < smallestAngle)
            {
                selectedObject = obj;
                smallestAngle = angleToObject;
            }
            obj.selected = false;
        }

        return selectedObject;
    }

    public static void RegisterInteractable(GestureInteractableObject gestureInteractable)
    {
        if (gestureInteractables == null)
            gestureInteractables = new List<GestureInteractableObject>();
        gestureInteractables.Add(gestureInteractable);
    }
}
