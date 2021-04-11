using UnityEngine;

/// <summary>
/// Control the movement of the cat(player)
/// </summary>
public class CatMovementByMouse : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4.0f;
    [SerializeField] private Animator anim;

    private Vector2 cursorPos;
    private bool isMoving = false;

    private int horizontalMoveId;
    private int idleId;

    void Start()
    {
        anim = GetComponent<Animator>();
        horizontalMoveId = Animator.StringToHash("horizontalMoving");
        idleId = Animator.StringToHash("idle");
    }

    void Update()
    {
        Movement();
        SwitchAnim();
    }

    // Move with mouse click.
    private void Movement()
    {
        if (Input.GetMouseButton(0))
        {
            // Convert screen coordinates to world coordinates.
            cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }
        if (isMoving)
        {
            // Move to target position.
            transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, cursorPos) < 0.1f)
            {
                isMoving = false;
            }
        }
    }

    // GameObject stops moving when it collides with other collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (string.Compare(collision.gameObject.tag, TagContants.COLLIDER) == 0)
        {
            isMoving = false;
        }*/
    }

    // Switch animation of cat in different status.
    // Change the direction of cat.
    private void SwitchAnim()
    {
        float scaleX = transform.localScale.x;
        if (isMoving)
        {
            anim.SetBool(horizontalMoveId, true);
            anim.SetBool(idleId, false);
            if (cursorPos.x < transform.position.x && scaleX > 0)
            {
                transform.localScale = new Vector3(scaleX * -1, 1, 1);
            }
            else if (cursorPos.x >= transform.position.x && scaleX < 0)
            {
                transform.localScale = new Vector3(scaleX * -1, 1, 1);
            }
        }
        else
        {
            anim.SetBool(idleId, true);
            anim.SetBool(horizontalMoveId, false);
        }
    }

}
