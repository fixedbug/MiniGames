using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    public GameManager gameManager;
    public float velocity = 1;// 速度

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 在用户按下给定鼠标按钮的帧期间返回 true
        // Input.GetMouseButton(0):表示鼠标左键按下，Input.GetMouseButton(1):表示鼠标右键按下，Input.GetMouseButton(2):表示鼠标中键按下
        if (Input.GetMouseButtonDown(0))
        {
            // 跳跃
            rb.velocity = Vector2.up * velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GameOver();
    }
}
