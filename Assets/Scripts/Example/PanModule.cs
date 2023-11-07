using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanModule : MonoBehaviour
{
    public Vector3 targetDirection = Vector3.one;
    public float speed = 1f;

    Vector3 target = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position + targetDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.Round(1) == target.Round(1))
        {
            targetDirection *= -1f;
            target = transform.position + targetDirection;
        }

        transform.position = Vector3.Lerp(transform.position, target, speed*Time.deltaTime);
    }
}

static class Extension
{
    /// <summary>
    /// Rounds Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

    /// <summary>
    /// Compares two rotations.
    /// </summary>
    /// <param name="quatA">The value of this Quaternion</param>
    /// <param name="value">Target to compare to</param>
    /// <param name="acceptableRange">Acceptable Range for the comparision. For example: 0.0000004f for 1 degree</param>
    /// <returns>Returns true if both angles are within the acceptable range</returns>
    public static bool Approximately(this Quaternion quatA, Quaternion value, float acceptableRange = 0.0000004f)
    {
        return 1 - Mathf.Abs(Quaternion.Dot(quatA, value)) < acceptableRange;
    }

    /// <summary>
    /// Compares two rotations
    /// </summary>
    /// <param name="quatA">This rotation</param>
    /// <param name="target">The target of this comparison</param>
    /// <param name="acceptableRange">The range in which the difference between the two rotations may lie. 1f = 1 degree</param>
    /// <returns>True if the difference bewteen both rotations is smaller then the range</returns>
    public static bool Compare(this Quaternion quatA, Quaternion target, float acceptableRange = 1f)
    {
        // Quaternion.Angle() returns the angle between two rotations in degree.
        return Quaternion.Angle(quatA, target) < acceptableRange;
    }
}
