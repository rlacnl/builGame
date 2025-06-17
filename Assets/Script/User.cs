using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public float jumpForce = 5f;
    public float doubleJumpForce = 6f; // 2�� ���� ���� ���� �� ����
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float startY;
    public float groundTolerance = 0.05f;

    private int jumpCount = 0;
    private const int maxJumpCount = 2;  // 2�� ����

    private Animator animator;
    private float velocityX = 0f;

    private bool jumpPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("[User] Rigidbody2D�� �ʿ��մϴ�!");
        if (animator == null)
            Debug.LogError("[User] Animator�� �ʿ��մϴ�!");

        startY = transform.position.y;
        startPosition = transform.position;
        StartGame startGameScript = gameObject.GetComponent<StartGame>();
        if (startGameScript != null)
        {
            startGameScript.enabled = true; // �̰� MonoBehaviour�� �����ϴ� �Ӽ���
        }
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

        // ���� �Է� ���� (��ư ������ ����ص�)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        if (jumpPressed && jumpCount < maxJumpCount)
        {
            float appliedJumpForce = (jumpCount == 0) ? jumpForce : doubleJumpForce;
            rb.velocity = new Vector2(rb.velocity.x, 0); // y�ӵ� �ʱ�ȭ (�߷� ���� ��ø ����)
            rb.AddForce(Vector2.up * appliedJumpForce, ForceMode2D.Impulse);

            jumpCount++;
            animator.SetBool("UserJump", true);
            animator.SetBool("walk", false);

            jumpPressed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EventSystem") || collision.gameObject.name.Contains("EventSystem"))
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

