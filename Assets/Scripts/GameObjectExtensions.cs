using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions {
    public static Vector3 WorldToLocal(this Transform transform, Vector3 worldPosition) {
        return transform.worldToLocalMatrix.MultiplyVector(worldPosition);
    }

    public static Quaternion WorldToLocal(this Transform transform, Quaternion worldRotation) {
        return Quaternion.Inverse(transform.rotation) * worldRotation;
    }
}
