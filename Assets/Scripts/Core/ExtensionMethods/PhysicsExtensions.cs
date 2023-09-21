using UnityEngine;

public static class PhysicsHelpers {
    public static bool MouseRayFromCamera(Camera camera, out RaycastHit hit) {
        var ray = camera.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hit, 1000);
    }

}