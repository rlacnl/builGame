using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    private float startY;                  // 처음 Y 좌표 저장
    public float groundTolerance = 0.05f;  // 바닥 감지 허용 오차
    private int jumpCount = 0;             // 현재 점프 횟수
    private const int maxJumpCount = 1;    // 최대 점프 허용 수

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("[User] Rigidbody2D가 필요합니다!");
        }

        startY = transform.position.y;
    }

    void Update()
    {
        bool isGrounded = Mathf.Abs(transform.position.y - startY) <= groundTolerance;

        // 바닥에 도달했으면 점프 횟수 초기화
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // 점프 입력 처리 (최대 2번까지)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
}
