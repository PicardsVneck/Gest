using UnityEngine;
public class GestSystem : MonoBehaviour
{
    private static GestSystem instance;
    public OVRSkeleton leftHandSkeleton;
    public OVRSkeleton rightHandSkeleton;
    public Camera centerEyeCamera;
    private GestPose currentPoseRight;
    private GestPose currentPoseLeft;
    public enum RecognizeMode {LEFT, RIGHT}
    void Awake(){
        currentPoseLeft = new GestPose();
        currentPoseRight = new GestPose();
        instance = this;
    }

    public static Vector3 GetHeadsetDirection(){
        return instance.centerEyeCamera.transform.forward;
    }
    public static Vector3 GetHeadsetPosition(){
        return instance.centerEyeCamera.transform.position;
    }

    public static GestPose getCurrentPose(RecognizeMode mode){
        if(mode == RecognizeMode.LEFT){
            instance.currentPoseLeft.SetHandModel(instance.leftHandSkeleton);
            return instance.currentPoseLeft;
        }
        if(mode == RecognizeMode.RIGHT){
            instance.currentPoseRight.SetHandModel(instance.rightHandSkeleton);
            return instance.currentPoseRight;
        }
        return null;
    }

    public static bool Recognize(GestRecognizer recognizer, RecognizeMode mode){
        return instance.RecognizeInternal(recognizer, mode);
    }

    private bool RecognizeInternal(GestRecognizer recognizer, RecognizeMode mode){
        if(recognizer == null){
            return false;
        }

        if(mode == RecognizeMode.LEFT){
            if(leftHandSkeleton.IsInitialized){
                currentPoseLeft.SetHandModel(leftHandSkeleton);
                if(GestPoseCompare.Compare(currentPoseLeft, recognizer.pose) < recognizer.matchFit){
                        return true;
                }
            }
        }

        if(mode == RecognizeMode.RIGHT){
            if(rightHandSkeleton.IsInitialized){
                currentPoseRight.SetHandModel(rightHandSkeleton);
                if(GestPoseCompare.Compare(currentPoseRight, recognizer.pose) < recognizer.matchFit){
                        return true;
                }
            }
        }

        return false;
    }
}
