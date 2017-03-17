using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
    public void ReloadGame()
    {
        Application.LoadLevel("Game_Play");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
