using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject bird;
    public float speedUpScale;


    private void Start()
    {
        Time.timeScale = 0;
    }


    public void GameStart()
    {
        Debug.Log("GameStart");
        Time.timeScale = 1f;

        // 鸟归位，动力清空
        bird.transform.position = new Vector3(0, 1f, 0);
        bird.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        bird.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        bird.transform.rotation = Quaternion.identity;
        // 管道清空
        foreach (var item in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            DestroyImmediate(item);
        }

        
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Update()
    {
        Time.timeScale += Time.deltaTime * speedUpScale;
    }
}
