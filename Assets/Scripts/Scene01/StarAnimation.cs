using UnityEngine;

/// <summary>
/// Star flashing animation
/// </summary>
public class StarAnimation : MonoBehaviour
{
    [SerializeField] private float vanishSpeed = 1.0f;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        starFlashing();
    }

    private void starFlashing()
    {
        if (sr.color.a >= 1)
        {
            vanishSpeed *= -1;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1 + vanishSpeed * Time.deltaTime);
        }
        else if (sr.color.a <= 0)
        {
            sr.flipX = !sr.flipX;
            sr.flipY = !sr.flipY;
            vanishSpeed *= -1;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0 + vanishSpeed * Time.deltaTime);
        }
        else
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + (vanishSpeed * Time.deltaTime));
        }
    }
}
