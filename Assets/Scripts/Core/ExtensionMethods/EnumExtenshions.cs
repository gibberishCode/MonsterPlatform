using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace MyUnityHelpers {


    public class Enum<T> where T : System.Enum {
        public static IEnumerable<T> GetValues {
            get {
                if (!typeof(T).IsEnum) {
                    Debug.LogError("Not enum");
                    return null;
                }

                return Enum.GetValues(typeof(T)).Cast<T>();
            }
        }


        public static T GetRandom() {
            return (T)Enum.ToObject(typeof(T), UnityEngine.Random.Range(0, Length));
        }

        public static int Length {
            get {
                if (!typeof(T).IsEnum) {
                    Debug.LogError("Not enum");
                    return -1;
                }

                return Enum.GetValues(typeof(T)).Length;
            }
        }
    }
}