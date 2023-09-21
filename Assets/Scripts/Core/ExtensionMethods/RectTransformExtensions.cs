using UnityEngine;

public static class RectTransformExtensions {
    public static bool IsPointInsideRect(this RectTransform rect, Vector2 point) {
        var localPoint = rect.InverseTransformPoint(point);
        return rect.rect.Contains(localPoint);
    }
}