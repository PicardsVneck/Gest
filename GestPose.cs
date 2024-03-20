using System;
using UnityEngine;

[Serializable]
public class GestPose
{
    public float[] jointAngles = new float[15];
    public Vector3 crossHand { get; private set; }
    public Vector3 frontHand { get; private set; }
    public Vector3 topHand { get; private set; }
    public Vector3 wristPosition { get; private set; }

    public void SetHandModel(OVRSkeleton skeleton)
    {
        crossHand = (skeleton.Bones[6].Transform.position - skeleton.Bones[16].Transform.position).normalized;
        frontHand = (skeleton.Bones[9].Transform.position - skeleton.Bones[0].Transform.position).normalized;
        topHand = Vector3.Cross(crossHand, frontHand).normalized;
        wristPosition = skeleton.Bones[0].Transform.position;

        for (int i = 0; i < 5; i++)
        {
            CreateFingerModel(i, skeleton);
        }
    }

    public override string ToString()
    {
        string s = "pose: {";
        for (int i = 0; i < jointAngles.Length; i++)
        {
            if (i != jointAngles.Length - 1)
            {
                s += jointAngles[i] + ", ";
            }
            else
            {
                s += jointAngles[i];
            }
        }
        s += "}\ncrossHand: " + crossHand;
        s += "\nfrontHand: " + frontHand;
        s += "\noutHand: " + topHand;
        s += "\nwristPosition: " + wristPosition;
        return s;
    }

    private void CreateFingerModel(int finger, OVRSkeleton skeleton)
    {
        int handMax = 19;
        int fingerStart = 3;
        int finger0 = 0;

        if (finger == 4)
        {
            fingerStart++;
        }

        int finger1 = fingerStart + finger * 3;
        int finger2 = fingerStart + finger * 3 + 1;
        int finger3 = fingerStart + finger * 3 + 2;
        int finger4 = handMax + finger;

        Vector3 Segment1 = skeleton.Bones[finger1].Transform.position - skeleton.Bones[finger0].Transform.position;
        Vector3 Segment2 = skeleton.Bones[finger2].Transform.position - skeleton.Bones[finger1].Transform.position;
        Vector3 Segment3 = skeleton.Bones[finger3].Transform.position - skeleton.Bones[finger2].Transform.position;
        Vector3 Segment4 = skeleton.Bones[finger4].Transform.position - skeleton.Bones[finger3].Transform.position;

        // store deflection of knuckle
        float angleFirstJoint = (Vector3.SignedAngle(Vector3.ProjectOnPlane(Segment1, crossHand), Vector3.ProjectOnPlane(Segment2, crossHand), crossHand) + 90) / 180;
        jointAngles[finger * 3] = angleFirstJoint;

        // store lateral deflection of knuckle
        float lateralAngleFirstJoint = (Vector3.SignedAngle(Vector3.ProjectOnPlane(Segment1, topHand), Vector3.ProjectOnPlane(Segment2, topHand), topHand) + 45) / 180;
        jointAngles[finger * 3 + 1] = lateralAngleFirstJoint;

        // store bend in finger
        float angleSecondJoint = Vector3.Angle(Segment2, Segment3) / 180;
        float angleThirdJoint = Vector3.Angle(Segment3, Segment4) / 180;
        jointAngles[finger * 3 + 2] = angleThirdJoint + angleSecondJoint;
    }
}
