using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class GestRecorder : MonoBehaviour{
    
    public OVRSkeleton skeleton;
    public GestPose gestPose;
    public GestRecognizer saveRecognizer;

    public float matchFit;

    void Awake(){
        gestPose = new GestPose();
    }

    void Update(){

        if(skeleton.IsInitialized){
            gestPose.SetHandModel(skeleton);
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            saveRecognizer = ScriptableObject.CreateInstance<GestRecognizer>();
            saveRecognizer.pose = new GestPose();
            saveRecognizer.pose.SetHandModel(skeleton);
            AssetDatabase.CreateAsset(saveRecognizer, "Assets/Gest/GestRecognizers/" + "recognizer" + Random.Range(0,10000) + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Saved Recognizer with Pose:");
                        Debug.Log(saveRecognizer.pose.ToString());
        }
    }
}
