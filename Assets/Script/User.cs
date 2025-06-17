using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public float jumpForce = 5f;
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f; // �ε巯�� ����/���� �ð�

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float startY;
    public float groundTolerance = 0.05f;

    private int jumpCount = 0;
    private const int maxJumpCount = 1;

    private Animator animator;
    private float velocityX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("[User] Rigidbody2D�� �ʿ��մϴ�!");
        if (animator == null)
            Debug.LogError("[User] Animator�� �ʿ��մϴ�!");

        startY = transform.position.y;
        startPosition = transform.position; // ���� ��ġ ����
    }

    void Update()
    {
        float targetVelocityX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float newVelocityX = Mathf.SmoothDamp(rb.velocity.x, targetVelocityX, ref velocityX, smoothTime);

        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        if (targetVelocityX > 0.1f)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (targetVelocityX < -0.1f)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        bool isGrounded = Mathf.Abs(transform.position.y - startY) <= groundTolerance;

        if (isGrounded)
        {
            jumpCount = 0;
            animator.SetBool("UserJump", false);
            animator.SetBool("walk", Mathf.Abs(newVelocityX) > 0.1f);
        }
        else
        {
            animator.SetBool("UserJump", true);
            animator.SetBool("walk", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            animator.SetBool("UserJump", true);
            animator.SetBool("walk", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ��� 1: �±׷� üũ
        if (collision.CompareTag("EventSystem"))
        {
            ResetToStart();
        }

        // ��� 2: �̸����� üũ
        if (collision.gameObject.name.Contains("EventSystem"))
        {
            ResetToStart();
        }
    }

    void ResetToStart()
    {
        transform.position = startPosition;
        rb.velocity = Vector2.zero;
    }
}
