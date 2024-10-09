using UnityEngine;

public class Laser : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;

    public float screenTop;
    public float screenBot;
    public float screenLeft;
    public float screenRight;
    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * speed;
        if (transform.position.y >= screenTop || transform.position.y <= screenBot || transform.position.x >= screenRight || transform.position.x <= screenLeft) Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SendMessage("Hit");
        GameObject.Find("GameManager").SendMessage("IncreaseScore", 100);
        Destroy(gameObject);
    }
}
