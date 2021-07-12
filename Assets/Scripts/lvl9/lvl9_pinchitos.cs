using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl9_pinchitos : MonoBehaviour
{
    private Rigidbody2D rb;

    public Transform respawn;
    public float speed;

    public enum Direction { None, Left, Right, Up, Down }
    public Direction direction;

    public bool TriggerExit;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        switch (direction)
        {
            case Direction.None:
                rb.velocity = new Vector2(0, 0);
                break;
            case Direction.Left:
                rb.velocity = new Vector2(speed, 0);
                break;
            case Direction.Right:
                rb.velocity = new Vector2(-speed, 0);
                break;
            case Direction.Up:
                rb.velocity = new Vector2(0, speed);
                break;
            case Direction.Down:
                rb.velocity = new Vector2(0, -speed);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!TriggerExit && other.gameObject.tag == "despawn")
        {
            gameObject.transform.position = respawn.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (TriggerExit && other.gameObject.tag == "despawn")
        {
            gameObject.transform.position = respawn.position;
        }
    }
}
