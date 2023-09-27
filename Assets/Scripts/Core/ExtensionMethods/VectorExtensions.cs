using UnityEngine;

namespace MyUnityHelpers
{

    public static class VectorExtensionMethods
    {

        public static Vector3 Randomize(this Vector3 t, float length)
        {
            var r = Quaternion.Euler(Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)) * Vector3.up * length;
            return r;
        }

        public static Vector3 ZeroY(this Vector3 v)
        {
            v.y = 0;
            return v;
        }

        public static Vector2 Xy(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }
        public static Vector2 Xz(this Vector3 v)
        {
            return new Vector2(v.x, v.z);
        }
        public static Vector3 Vector2FlatToX0Z(this Vector2 v)
        {
            return new Vector3(v.x, 0, v.y);
        }
        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static Vector2 WithX(this Vector2 v, float x)
        {
            return new Vector2(x, v.y);
        }

        public static Vector2 WithY(this Vector2 v, float y)
        {
            return new Vector2(v.x, y);
        }

        public static Vector3 WithZ(this Vector2 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

        public static bool ApproximateEqual(this Vector3 one, Vector3 two)
        {
            return
                Mathf.Approximately(one.x, two.x) &&
                Mathf.Approximately(one.y, two.y) &&
                Mathf.Approximately(one.z, two.z);
        }

        // axisDirection - unit vector in direction of an axis (eg, defines a line that passes through zero)
        // point - the point to find nearest on line for
        public static Vector3 NearestPointOnAxis(this Vector3 axisDirection, Vector3 point, bool isNormalized = false)
        {
            if (!isNormalized) axisDirection.Normalize();
            var d = Vector3.Dot(point, axisDirection);
            return axisDirection * d;
        }

        // lineDirection - unit vector in direction of line
        // pointOnLine - a point on the line (allowing us to define an actual line in space)
        // point - the point to find nearest on line for
        public static Vector3 NearestPointOnLine(
            this Vector3 lineDirection, Vector3 point, Vector3 pointOnLine, bool isNormalized = false)
        {
            if (!isNormalized) lineDirection.Normalize();
            var d = Vector3.Dot(point - pointOnLine, lineDirection);
            return pointOnLine + (lineDirection * d);
        }

        public static Vector3 RandomPointInBounds(this Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );
        }
    }
}