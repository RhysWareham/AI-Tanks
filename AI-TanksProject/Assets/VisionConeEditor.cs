//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor (typeof (VisionCone))]
//public class VisionConeEditor : Editor
//{
//    private void OnSceneGUI()
//    {
//        VisionCone visionCone = (VisionCone)target;
//        Handles.color = Color.white;
//        Handles.DrawWireArc(visionCone.transform.position, Vector3.up, Vector3.forward, 360, visionCone.viewingRadius);
//        Vector3 viewAngleA = visionCone.DirectionFromAngle(-visionCone.viewingAngle / 2, false); //Not a global angle
//        Vector3 viewAngleB = visionCone.DirectionFromAngle(visionCone.viewingAngle / 2, false); //Not a global angle

//        Handles.DrawLine(visionCone.transform.position, visionCone.transform.position + viewAngleA * visionCone.viewingRadius);
//        Handles.DrawLine(visionCone.transform.position, visionCone.transform.position + viewAngleB * visionCone.viewingRadius);

//        Handles.color = Color.red;
//        foreach(Transform visibleTarget in visionCone.visibleTargets)
//        {
//            Handles.DrawLine(visionCone.transform.position, visibleTarget.position);
//        }
//    }
//}
