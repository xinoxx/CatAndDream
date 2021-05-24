using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicMovement : MonoBehaviour
{
    [SerializeField] private float minX = 0.0f, maxX = 0.0f;
    [SerializeField] private float speed = 1.0f;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 pos = transform.position;
        if (pos.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(pos.x + Time.deltaTime * speed, pos.y);
        }
    }
}
