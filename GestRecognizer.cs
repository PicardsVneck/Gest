
using UnityEngine;

[CreateAssetMenu(fileName = "GestRecognizer", menuName = "Gest/GestRecognizer", order = 2)]
public class GestRecognizer : ScriptableObject {

    public string recognizerName;
    [SerializeField] public GestPose pose;
    public float matchFit;
    public float[] jointWeights = new float[15];

    void Awake(){

        //Initilaize values
        matchFit = 1f;
        for(int i = 0; i < jointWeights.Length; i++){
            jointWeights[i] = 1;
        }
    }
}
