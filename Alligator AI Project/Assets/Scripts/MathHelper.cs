using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathHelper {

    public static bool IsVectorWithinRange(Vector3 range1, Vector3 range2, Vector3 vectorToCheck)
    {
        Vector3 min = new Vector3(Mathf.Min(range1.x, range2.x), Mathf.Min(range1.y, range2.y), Mathf.Min(range1.z, range2.z));
        Vector3 max = new Vector3(Mathf.Max(range1.x, range2.x), Mathf.Max(range1.y, range2.y), Mathf.Max(range1.z, range2.z));

        if(vectorToCheck.x >= min.x && vectorToCheck.x <= max.x &&
           vectorToCheck.y >= min.y && vectorToCheck.y <= max.y &&
           vectorToCheck.z >= min.z && vectorToCheck.z <= max.z)
        {
            return true;
        }

        return false;
    }

}
