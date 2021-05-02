using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Canvas canvas;
    public Item thisItem;

    private RectTransform rectTrans;
    private Vector2 originalPos;
    private CanvasGroup canvasGroup;

    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPos = rectTrans.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Turn off its raycast in case the system fails to detect the target(player bag).
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Set the position of this.gameObject.
        // Since the canvas scale is not 1, should to adjust the move distance.
        rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Recover the raycast of gameObject.
        canvasGroup.blocksRaycasts = true;
        IsInBagScale();
    }

    private void IsInBagScale()
    {
        Vector2 minPos, maxPos;
        minPos = new Vector2(384.0f, 268.0f);
        maxPos = new Vector2(572.0f, 321.0f);

        // If it is not draged to the range of bag.
        if (!(rectTrans.position.x >= minPos.x && rectTrans.position.y >= minPos.y &&
              rectTrans.position.x <= maxPos.x && rectTrans.position.y <= maxPos.y))
        {
            // Back to the original position.
            rectTrans.position = originalPos;
        }
    }
}
