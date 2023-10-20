using UnityEngine;

[CreateAssetMenu(fileName = "PrintAction", menuName = "Core/PrintAction", order = 0)]
public class PrintAction : ScriptableObject {
    public string Message;

    public void Execute() {
        Debug.Log(Message);
    }
}