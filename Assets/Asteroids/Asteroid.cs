using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector3 dir;
    float speed;
    float initialAngle;
    public float speedMin;
    public float speedMax;
    float rot;
    public float rotMax;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Sprite[] sprites;
    public int splitAsteroidCount;
    // Start is called before the first frame update
    void Start()
    {
        sr.sprite = sprites[Random.Range(0, sprites.Length - 1)];
        rot = Random.Range(-rotMax, rotMax);
        if (transform.localScale == Vector3.one)
        {
            transform.eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
        }
        initialAngle = transform.eulerAngles.z;
        speed = Random.Range(speedMin, speedMax);
        dir = transform.up * speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, 0, rot * Time.deltaTime);
        rb.velocity = dir;
    }

    public void Hit()
    {
        if (transform.localScale != new Vector3(0.125f, 0.125f, 0.125f))
        {
            for (int i = 1; i <= splitAsteroidCount; i++)
            {
                GameObject newAsteroid = Instantiate(gameObject);
                newAsteroid.transform.localScale = transform.localScale * 0.5f;
                newAsteroid.transform.position = transform.position;
                if (i == 1) newAsteroid.transform.eulerAngles = new Vector3(0, 0, initialAngle + 90);
                else newAsteroid.transform.eulerAngles = new Vector3(0, 0, initialAngle - 90);
            }
        }
        Destroy(gameObject);
    }
}