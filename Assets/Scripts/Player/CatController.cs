using UnityEngine;

/// <summary>
/// Control cat(player)
/// </summary>
public class CatController : MonoBehaviour
{
    // Singleton mode
    public static CatController instance;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector2 initPos = Vector2.zero; // The initial position when game starts.

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 movement;

    // Animator parameters id
    private int horizontalMovingId;
    private int idleId;
    private int moveUpId;
    private int moveDownId;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            if (instance != this) Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        transform.position = initPos;

        horizontalMovingId = Animator.StringToHash("horizontalMoving");
        idleId = Animator.StringToHash("idle");
        moveUpId = Animator.StringToHash("movingUp");
        moveDownId = Animator.StringToHash("movingDown");
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        float scaleZ = transform.localScale.z;
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
            anim.SetBool(moveUpId, true);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
            anim.SetBool(moveDownId, true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
            anim.SetBool(horizontalMovingId, true);
            if (scaleX > 0)
                transform.localScale = new Vector3(scaleX * -1, scaleY, scaleZ);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
            anim.SetBool(horizontalMovingId, true);
            if (scaleX <= 0)
                transform.localScale = new Vector3(scaleX * -1, scaleY, scaleZ);
        }
        else
        {
            movement = Vector2.zero;
            anim.SetBool("taking", false);
            anim.SetBool(moveUpId, false);
            anim.SetBool(moveDownId, false);
            anim.SetBool(horizontalMovingId, false);
            anim.SetBool(idleId, true);
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
