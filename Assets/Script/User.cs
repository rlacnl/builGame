using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    public float jumpForce = 5f;
    public float doubleJumpForce = 6f;
    public float moveSpeed = 5f;
    public float smoothTime = 0.1f;

    private Rigidbody2D rb;
    private Vector2 startPosition;
    private float startY;
    public float groundTolerance = 0.05f;

    private int jumpCount = 0;
    private const int maxJumpCount = 3;

    private Animator animator;
    private float velocityX = 0f;

    private bool jumpPressed = false;
    public Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        startY = transform.position.y;
        startPosition = transform.position;
    }

    void Update()
    {
        if (StopGameScript.IsPaused)
        {
            // 정지 중이면 애니메이션도 비활성화 (선택사항)
            animator.SetBool("UserJump", false);
            animator.SetBool("walk", false);
            return;
        }

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
            jumpPressed = true;
        }

        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    void FixedUpdate()
    {
        if (StopGameScript.IsPaused) return;

        if (jumpPressed && jumpCount < maxJumpCount)
        {
            float appliedJumpForce = (jumpCount == 0) ? jumpForce : doubleJumpForce;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * appliedJumpForce, ForceMode2D.Impulse);

            jumpCount++;
            animator.SetBool("UserJump", true);
            animator.SetBool("walk", false);
        }

        jumpPressed = false;

        // 바닥보다 아래로 떨어지면 위치 보정
        if (transform.position.y < startY)
        {
            rb.position = new Vector2(rb.position.x, startY);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, 0));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("obstacle"))
        {
            int score = ParseScoreFromText();
            ScoreManager.FinalScore = score;
            SceneManager.LoadScene("ClearScene");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("EventSystem"))
        {
            rb.velocity = Vector2.zero;
            transform.position = startPosition;
        }
    }

    int ParseScoreFromText()
    {
        if (scoreText == null)
            return 0;

        string text = scoreText.text.Trim();
        if (text.Contains(":"))
        {
            string[] parts = text.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int parsedScore))
                return parsedScore;
        }
        else if (int.TryParse(text, out int directScore))
        {
            return directScore;
        }

        Debug.LogWarning($"[User] 점수 파싱 실패: '{text}'");
        return 0;
    }
}
