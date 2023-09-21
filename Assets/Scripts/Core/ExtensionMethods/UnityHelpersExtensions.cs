using UnityEngine;

public static class UnityHelpers {
    public static void DestroyChildren(this Transform transform) {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}