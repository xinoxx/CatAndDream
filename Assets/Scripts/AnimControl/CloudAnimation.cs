using UnityEngine;

/// <summary>
/// Cloud movement animation
/// </summary>
public class CloudAnimation : MonoBehaviour
{
    [SerializeField] private float speed = 0.02f;

    private MeshRenderer mr;
    private Vector2 movement;

    void Start()
    {
        // Change rendering order.
        mr = GetComponent<MeshRenderer>();
        mr.sortingLayerName = SortingLayer.Foreground.ToString();
        mr.sortingOrder = 0;
    }

    void FixedUpdate()
    {
        movement.x += speed * Time.deltaTime;
        mr.material.mainTextureOffset = movement;
    }
}
