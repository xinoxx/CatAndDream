using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer catSprite;
    private CameraController cameraController;
    private GameObject lockTrigger;
    private GameObject lockPuzzle;
    private Vector2 initPos = Vector2.zero;
    private bool isShielded = false;
    private Vector2 offset = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        catSprite = GetComponent<SpriteRenderer>();
        cameraController = GameObject.FindGameObjectWithTag(TagContants.MAIN_CAMERA).GetComponent<CameraController>();
        lockTrigger = GameObject.FindGameObjectWithTag(TagContants.LOCK);
        lockPuzzle = GameObject.FindGameObjectWithTag(TagContants.PUZZLE);
    }

    // FIXME: If the speed of mouse is too fast, the picture can't keep with the mouse movement.

    private void OnMouseEnter()
    {
        // Check if cat is shielded by large lock picture.
        if (lockPuzzle.transform.localScale.x != 0 && transform.position.x > -3.5f && transform.position.x < 3.1f)
            isShielded = true;
        else
            isShielded = false;
        if (!isShielded)
        {
            // Custom mouse texture.
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            // Enlarge cat picture.
            // The localScale x may be negative because of the direction, so 'transform.localScale += Vector3.one * 2.0f' has bug sometimes.
            Vector3 initScale = transform.localScale;
            transform.localScale = new Vector3(initScale.x * 2.0f, initScale.y * 2.0f, initScale.z * 2.0f);
        }
    }

    private void OnMouseExit()
    {
        if (!isShielded)
        {
            // Change the mouse texture to default.
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            // Recover the size of cat picture.
            Vector3 initScale = transform.localScale;
            transform.localScale = new Vector3(initScale.x / 2.0f, initScale.y / 2.0f, initScale.z / 2.0f);
        }
    }

    private void OnMouseDrag()
    {
        if (!isShielded)
        {
            // Control cat to follow the mouse movement.
            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                             Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + offset;
            // Neglect the collision.
            // When cat collider collids with other collider, the display of cat is not stable.
            rb.bodyType = RigidbodyType2D.Kinematic;
            // The cat collider will be blocked by small lock collider because of the raycast check of mouse. 
            // Cancel the lock collider.
            lockTrigger.GetComponent<BoxCollider2D>().enabled = false;
            // Do not follow.
            cameraController.isFollowing = false;
            // Change sorting layer and order.
            catSprite.sortingLayerName = SortingLayer.Front.ToString();
            catSprite.sortingOrder = 0;
        }
    }

    private void OnMouseDown()
    {
        // Initialize cat startPos before draging.
        initPos = transform.position;
        // Calculate the interpolation between mouse position and cat position.
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // Pause the animation of cat.
        anim.speed = 0;
    }

    private void OnMouseUp()
    {
        // Get current cat animation frame.
        AnimatorStateInfo animInfo = anim.GetCurrentAnimatorStateInfo(0);
        float normalizedTime = animInfo.normalizedTime;
        int frameNum = (int)Mathf.Floor((normalizedTime - Mathf.Floor(normalizedTime)) * 5);

        // Get original position.
        Vector2 correctPos = lockPuzzle.transform.GetChild(1).position;

        // The picture matches successfully.
        // Otherwise go back to initial position.
        if (Mathf.Abs(transform.position.x - correctPos.x) <= 0.3f &&
            Mathf.Abs(transform.position.y - correctPos.y) <= 0.3f &&
            (frameNum == 0 || frameNum == 4) && transform.localScale.x > 0)
        {
            // In case the coordinates of cat can not match with the blank exactly.
            // Set localScale of cat to zero.
            transform.localScale = Vector3.zero;
            // Display the suitable cat picture.
            lockPuzzle.transform.GetChild(6).localScale = Vector3.one;
        }
        else
        {
            transform.position = initPos;
            // Continue follow cat movement.
            cameraController.isFollowing = true;
            // Change sorting layer and order to original.
            catSprite.sortingLayerName = SortingLayer.Character.ToString();
            catSprite.sortingOrder = 0;
            // Continue to play animation of cat.
            anim.speed = 1;

            // If the small lock collider is enable when the large lock exists, moving the cat through the small lock will cause new bug.
            if (lockPuzzle.transform.localScale.x == 0)
            {
                // Recover the small lock collider.
                lockTrigger.GetComponent<BoxCollider2D>().enabled = true;
            }
        }

        // Recover the collision.
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 0;
    }
}
