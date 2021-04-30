using UnityEngine;

/// <summary>
/// Puzzle gameObject can be triggered and mouse texture changed.
/// </summary>
public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture = null;
    [SerializeField] private GameObject puzzleGameObject = null;

    private void Update()
    {
        if (puzzleGameObject.transform.localScale.x != 0)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                // Conceal the puzzle gameObject.
                puzzleGameObject.transform.localScale = Vector3.zero;
                // Enable collider of this gameObject.
                this.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }

    private void OnMouseOver()
    {
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseEnter()
    {
            transform.localScale += Vector3.one * 0.1f;
    }

    private void OnMouseExit()
    {
            transform.localScale -= Vector3.one * 0.1f;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseUpAsButton()
    {
        puzzleGameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        puzzleGameObject.transform.position = new Vector3(Camera.main.transform.position.x,
                                                          Camera.main.transform.position.y + 0.7f,
                                                          puzzleGameObject.transform.position.z);
        // This gameObject can not been click again when it has shown in screen.
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
