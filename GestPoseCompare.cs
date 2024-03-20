using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GestPoseCompare
{
    public static float Compare(GestPose pose, GestPose otherPose){
        
        float diff = 0f;

        for(int i = 0; i < pose.jointAngles.Length; i++){
            diff += Mathf.Pow(Mathf.Abs(pose.jointAngles[i] - otherPose.jointAngles[i]), 1.5f);
        }

        return diff;
    }
}
