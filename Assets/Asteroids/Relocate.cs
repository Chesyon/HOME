using UnityEngine;

public class Relocate : MonoBehaviour
{
    public Rigidbody2D rb;
    ScreenBounds sb;

    void Awake()
    {
        sb = GameObject.Find("GameManager").GetComponent<ScreenBounds>();
    }
    void Update()
    {
        if (transform.position.y > sb.top && rb.velocity.y > 0) transform.position = new Vector2(transform.position.x, sb.bot);
        if (transform.position.y < sb.bot && rb.velocity.y < 0) transform.position = new Vector2(transform.position.x, sb.top);
        if (transform.position.x > sb.right && rb.velocity.x > 0) transform.position = new Vector2(sb.left, transform.position.y);
        if (transform.position.x < sb.left && rb.velocity.x < 0) transform.position = new Vector2(sb.right, transform.position.y);
    }
}
