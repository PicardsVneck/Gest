using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestCube : MonoBehaviour{

    MeshRenderer rend;
    public GestRecognizer gestureToRecognize;
    private void Awake(){
        GestEvents gestEvents = new GestEvents();
        rend = GetComponent<MeshRenderer>();
    }

    void Update(){
        if(GestSystem.Recognize(gestureToRecognize, GestSystem.RecognizeMode.RIGHT)){
            rend.material.color = Color.red;
        } else{
            rend.material.color = Color.white;
        }
    }

}
