using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions {
    public static Vector3 RelativePosition(this GameObject gameObject, GameObject other) {
        return gameObject.transform.position - other.transform.position;
    }

    public static Vector3 RelativeRotation(this GameObject gameObject, )
}
