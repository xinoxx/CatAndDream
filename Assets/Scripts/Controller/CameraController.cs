using UnityEngine;

/// <summary>
/// The camera follows cat(player).
/// </summary>
public class CameraController : MonoBehaviour
{
    [HideInInspector] public bool isFollowing = true;
    [SerializeField] private float smoothSpeed = 3.0f;
    [SerializeField] private float minX = 0f, minY = 0f, maxX = 0f, maxY = 0f;

    private Transform target;

    private void Start()
    {
        // target = GameObject.FindGameObjectWithTag(TagContants.PLAYER).GetComponent<Transform>();
        target = CatController.instance.transform;
    }

    private void LateUpdate()
    {
        if (isFollowing)
        {
            /*
             * Method 1: Trandition method of move camera
             * transform.position.z can not instead of target.positon.z.
             * Otherwise the camera will be on the same plane as the player.
             */
            // transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            /* Method 2: Smoothly move camera
             * first parameter is the current position
             * second parameter is target position
             * third parameter is speed
             */
            transform.position = Vector3.Lerp(transform.position,
               new Vector3(target.position.x, target.position.y, transform.position.z), smoothSpeed * Time.deltaTime);

            // Limit the range that the camera can move.
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
                                             Mathf.Clamp(transform.position.y, minY, maxY),
                                             transform.position.z);
        }
    }
}
