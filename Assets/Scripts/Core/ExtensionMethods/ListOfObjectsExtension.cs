using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyUnityHelpers {
    public static class ListsExtensionMethods {
        public static void Test(this List<MonoBehaviour> test) {

        }

        public static T GetRandom<T>(this IList<T> list) {
            var index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        public static void DestroyAll(this IEnumerable<MonoBehaviour> objects) {
            foreach (var obj in objects) {
                GameObject.Destroy(obj.gameObject);
            }
        }

        public static void RotateAroundAxis(this IList<GameObject> objects, Vector3 axis, float offset) {
            var angle = 360 / objects.Count;
            for (int i = 0; i < objects.Count; i++) {
                GameObject obj = objects[i];

                var rotation = Quaternion.AngleAxis(angle * i, axis);
                var pos = rotation * Vector3.up * offset;
                obj.transform.localPosition = pos;
            }
        }

        public static void GizmoDrawPoints(this IEnumerable<Vector3> source, float radius) {
            if (source == null) {
                return;
            }
            Vector3? prev = null;
            foreach (var point in source) {
                if (prev != null) {
                    Gizmos.DrawLine(prev.Value, point);
                }
                Gizmos.DrawWireSphere(point, radius);
                prev = point;
            }

        }

        public static int GetClosest(this IList<Vector3> source, Vector3 point) {
            if (source.Count == 0) {
                return -1;
            }
            float minDist = float.MaxValue;
            int index = 0;
            for (int i = 0; i < source.Count; i++) {
                var otherPoint = source[i];
                var distance = otherPoint - point;
                if (distance.sqrMagnitude < minDist * minDist) {
                    minDist = distance.magnitude;
                    index = i;
                }
            }
            return index;
        }

        public static T GetClosest<T>(this IList<T> source, Vector3 position) where T : MonoBehaviour {
            if (source.Count == 0) {
                return null;
            }
            float minDist = float.MaxValue;
            int index = 0;
            for (int i = 0; i < source.Count; i++) {
                var obj = source[i];
                var distance = obj.transform.position - position;
                if (distance.sqrMagnitude < minDist * minDist) {
                    minDist = distance.magnitude;
                    index = i;
                }
            }
            return source[index];
        }
    }

}