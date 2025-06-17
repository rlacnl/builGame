using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody2D rb;

    private float startY;
    public float groundTolerance = 0.05f;

    private int jumpCount = 0;
    private const int maxJumpCount = 1;  // 2�� ���� ���

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("[User] Rigidbody2D�� �ʿ��մϴ�!");

        if (animator == null)
            Debug.LogError("[User] Animator�� �ʿ��մϴ�!");

        startY = transform.position.y;
    }

    void Update()
    {
        bool isGrounded = Mathf.Abs(transform.position.y - startY) <= groundTolerance;

        if (isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;

            if (animator != null)
            {
                animator.ResetTrigger("Jump"); // ���Է��� ���� ���� ����
                animator.SetTrigger("Jump");   // Ʈ���� �۵�
            }
        }
    }
}