using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas = null;
    private RectTransform rectTrans;
    private CanvasGroup canvasGroup;
    public Item thisItem;

    void Start()
    {
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
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
    }
}
