using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMath : MonoBehaviour
{
    public static float GetDistanceWOSqrt(Vector3 startPoint, Vector3 endPoint)
    {
        return ((endPoint.x - startPoint.x) * (endPoint.x - startPoint.x) - (endPoint.y - startPoint.y) * (endPoint.y - startPoint.y) - (endPoint.z - startPoint.z) * (endPoint.z - startPoint.z));
    }

    public static float GetDistanceWOSqrt(Vector2 startPoint, Vector2 endPoint)
    {
        return ((endPoint.x - startPoint.x) * (endPoint.x - startPoint.x) - (endPoint.y - startPoint.y) * (endPoint.y - startPoint.y));
    }
}
