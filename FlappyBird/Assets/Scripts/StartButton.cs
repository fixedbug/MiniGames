using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameManager go;

    public void OnStartGame()
    {
        go.GameStart();
    }

}
