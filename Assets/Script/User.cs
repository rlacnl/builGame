using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    private float startY;                  // ó�� Y ��ǥ ����
    public float groundTolerance = 0.05f;  // �ٴ� ���� ��� ����
    private int jumpCount = 0;             // ���� ���� Ƚ��
    private const int maxJumpCount = 1;    // �ִ� ���� ��� ��

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("[User] Rigidbody2D�� �ʿ��մϴ�!");
        }

        startY = transform.position.y;
    }

    void Update()
    {
        bool isGrounded = Mathf.Abs(transform.position.y - startY) <= groundTolerance;

        // �ٴڿ� ���������� ���� Ƚ�� �ʱ�ȭ
        if (isGrounded)
        {
            jumpCount = 0;
        }

        // ���� �Է� ó�� (�ִ� 2������)
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
}
