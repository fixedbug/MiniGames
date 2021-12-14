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
        // 按照加速度给鸟做一个抬头低头效果
        transform.rotation = new Quaternion(0,0, rb.velocity.y/25, 1.0f);

        // 在用户按下给定鼠标按钮的帧期间返回 true
        // Input.GetMouseButton(0):表示鼠标左键按下，Input.GetMouseButton(1):表示鼠标右键按下，Input.GetMouseButton(2):表示鼠标中键按下
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            // 跳跃
            rb.velocity = Vector2.up * velocity;
        }

        if(transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager.GameOver();
    }
}
