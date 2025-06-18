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
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * appliedJumpForce, ForceMode2D.Impulse);

            jumpCount++;
            animator.SetBool("UserJump", true);
            animator.SetBool("walk", false);

            jumpPressed = false;
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
