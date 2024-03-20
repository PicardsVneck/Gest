using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Locomotion;
using UnityEngine;

public class GestureLamp : GestureInteractableObject
{
    public Light bulbLight;
    public MeshRenderer bulbRenderer;
    public Material onMaterial;
    public Material offMaterial;
    public GestRecognizer onRecognizer;
    public GestRecognizer offRecognizer;
    public bool isOn;

    void Awake(){
        Register(this);
    }
    void Update(){
        UpdateLight();
    }

    public override void ProcessGestures(){
        if(GestSystem.Recognize(onRecognizer, GestSystem.RecognizeMode.RIGHT)){
            isOn = true;
        }

        if(GestSystem.Recognize(offRecognizer, GestSystem.RecognizeMode.RIGHT)){
            isOn = false;
        }
        UpdateLight();
    }

    public void UpdateLight(){
        if(isOn){
            bulbLight.enabled = true;
            bulbRenderer.material = onMaterial;
        } else{
            bulbLight.enabled = false;
            bulbRenderer.material = offMaterial;
        }
    }
}
