using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class NeededVariables
{
    public static float BORDERDISTANCE = 40f;//{ get; set; }
    //public static float MUTATESPEED = 3.0f;
    public static float RESTTIME = 2.0f;
    public static float ARRIVALRANGE = 2f;
    public static float SAFEARRIVALRANGE = 5f;
    public static float SHOOTRANGE = 50f;
    public static float SAFESHOOTRANGE = 10f;
    public static float WAITTIME = 0.3f;
    public static float SHOOTTIMER = 3f;
    public static float TURRETROTATION = 300f;
    public static float TURRETAIMSPEED = 500f;
    public static float AIMLEEWAY = 10f;
    public static float MAXHEALTH = 100f;

    public static float RUNAWAYSPEED = 4.5f;
    public static float WANDERSPEED = 3.5f;
    public static float RUNAWAYRADIUS;

    public static float RemainingDistance(Vector3[] points)
    {
        if (points.Length < 2) return 0;
        float distance = 0;
        for (int i = 0; i < points.Length - 1; i++)
            distance += Vector3.Distance(points[i], points[i + 1]);
        return distance;
    }

    /// <summary>
    /// Function to unwrap each axis at the same time, without needing to call the function for each individual axis
    /// </summary>
    /// <param name="vec3"></param>
    /// <returns></returns>
    public static Vector3 UnwrapAngles(Vector3 vec3)
    {
        return new Vector3(UnwrapAngle(vec3.x), UnwrapAngle(vec3.y), UnwrapAngle(vec3.z));
    }

    /// <summary>
    /// This function sets any angle which is negative to its positive alternate angle. I.E. -90 will become 270
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    public static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }

    //public static float INFECTIONDISTANCE = 2.5f;

    //public static float HUMANSPEED = 10f;
    //public static float ZOMBIESPEED = 12f;
}
