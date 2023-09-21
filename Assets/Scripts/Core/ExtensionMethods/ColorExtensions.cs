using UnityEngine;

namespace MyUnityHelpers {

    public static class ColorHelpers {
        public static Color GetRandom() {
            var vek = new Vector3(
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f),
                Random.Range(0.0f, 1.0f));
            vek = Vector3.ClampMagnitude(vek, 1);
            return new Color(
                vek.x,
                vek.y,
                vek.z,
                1
            );
        }
    }

}