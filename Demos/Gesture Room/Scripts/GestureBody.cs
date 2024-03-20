using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureBody : GestureInteractableObject
{
    public Material defaultMaterial;
    public Material selectedMaterial;
    public Material lockedMaterial;
    public GestRecognizer openHandRecognizer;
    public GestRecognizer closedHandRecognizer;
    public Vector3 target;
    public float distanceToTarget;
    void Awake(){
        Register(this);
    }

    void Update(){
        if(selected && GestureRoomManager.locked){
            GetComponent<MeshRenderer>().material = lockedMaterial;
        }
        else if(selected){
            GetComponent<MeshRenderer>().material = selectedMaterial;
        } else{
            GetComponent<MeshRenderer>().material = defaultMaterial;
        }
    }

    public override void ProcessGestures(){
        if(GestSystem.Recognize(openHandRecognizer, GestSystem.RecognizeMode.RIGHT)){

            GestPose pose = GestSystem.getCurrentPose(GestSystem.RecognizeMode.RIGHT);

            Vector3 vectorToObject = transform.position - pose.wristPosition;
            Vector3 vectorFromHand = 2 * pose.frontHand + -1 * pose.topHand;
            float angleBetweenVectors = Vector3.Angle(vectorFromHand, vectorToObject);
            Debug.Log("Angle Between Vectors: " + angleBetweenVectors);

            if(angleBetweenVectors < 20){
                GestureRoomManager.locked = true;
                Debug.Log("Locked On Object");
                GetComponent<Rigidbody>().useGravity = false;
                distanceToTarget = vectorToObject.magnitude;
            }
        }
        if(GestSystem.Recognize(closedHandRecognizer, GestSystem.RecognizeMode.RIGHT)){
             GestureRoomManager.locked = false;
                Debug.Log("Unlocked On Object");
                GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
