using PainfulSmile.Runtime.Utilities;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerTakeHit;

    private Animator animator;
    private Rigidbody2D playerRb;
    private DBPersonagens dbChar;

    public IsVisible onVisible;
    public Status lifeControl;

    private int idAnim;
    private float speedY;

    private bool isGrounded;
    private bool isLeft;
    private bool isFall;
    private bool isDoubleJump;
    public bool isHit;
    public bool isMayHaveDoubleJump;

    private GameManager gameManager;

    public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jForce = 150;
    [SerializeField][Range(0.5f, 0.9f)] private float percDoubleJumpForce = 0.7f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask groundMask;

    public float horizontal;
    public Transform colectPosition;

    private void Start()
    {
        gameManager = GameManager.Instance;
        animator = GetComponentInChildren<Animator>();
        onVisible = GetComponentInChildren<IsVisible>();
        lifeControl = GetComponentInChildren<Status>();
        playerRb = GetComponent<Rigidbody2D>();
        dbChar = FindObjectOfType<DBPersonagens>();

        if (dbChar != null)
        {
            int idAtual = dbChar.idPersonagemAtual;
            speed = dbChar.velocidadeMovimento[idAtual];
            jumpForce = dbChar.forcaPulo[idAtual] * jForce;
            isMayHaveDoubleJump = dbChar.puloDuplo[idAtual];
            lifeControl.maxLife = dbChar.pontosDeVida[idAtual];
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundMask);

        if (isGrounded)
        {
            isFall = false;
            isDoubleJump = true;
        }
    }

    private void Update()
    {
        UpdateAnimator();
        Jump();
        Move();
        CheckFall();
    }

    private void CheckFall()
    {
        if (!isHit)
        {
            if (transform.position.y < -5f)
            {
                OnPlayerTakeHit?.Invoke();
                lifeControl.HealthChange(1);
                isHit = true;
            }
        }
    }

    private void Move()
    {
        if (!gameManager.isEndless)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }

        playerRb.velocity = new(horizontal * speed, speedY);

        if (horizontal != 0)
        {
            idAnim = 2;
        }
        else
        {
            idAnim = 0;
        }

        if (isLeft && horizontal > 0)
        {
            Flip();
        }

        if (!isLeft && horizontal < 0)
        {
            Flip();
        }

    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            playerRb.AddForce(new Vector2(0, jumpForce));
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && isDoubleJump && isMayHaveDoubleJump)
        {
            playerRb.velocity = new Vector2(0, 0);
            playerRb.AddForce(new Vector2(0, jumpForce * percDoubleJumpForce));
            isDoubleJump = false;
        }

        if (Input.GetButtonUp("Jump") && !isFall)
        {
            playerRb.velocity = new Vector2(0, playerRb.velocity.y / 2);
            isFall = true;
        }

        speedY = playerRb.velocity.y;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("speedY", speedY);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetInteger("idAnim", idAnim);
    }


    private void Flip()
    {
        isLeft = !isLeft;
        float scaleX = transform.localScale.x;
        scaleX *= -1f;
        transform.localScale = new(scaleX, transform.localScale.y, transform.localScale.z);

        gameManager.offsetX *= -1;

    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position, boxSize);
    }
#endif
}
