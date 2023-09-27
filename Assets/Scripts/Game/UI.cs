using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject GameOverScreen;

    public void SetGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }
}