using UnityEngine;
using System.Collections;

public class ShipCtrl : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float turnSpeed;
    public GameObject laser;
    public float LaserOffset;
    bool Alive = true;
    IEnumerator coro;
    public GameObject explosion;
    public SpriteRenderer sr;
    public Collider2D collide;
    float aliveTimer = 999;
    public float IFrameTime;
    public int lives = 3;
    public GameObject LiveIndicator1;
    public GameObject LiveIndicator2;
    // Update is called once per frame
    void Update()
    {
        if (Alive)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                rb.velocity = transform.up * speed;
            }
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject newLaser = Instantiate(laser);
                newLaser.transform.up = transform.up;
                newLaser.transform.position = transform.position + (transform.up * LaserOffset);
            }
            transform.eulerAngles += new Vector3(0, 0, -Input.GetAxis("Horizontal") * turnSpeed);
            aliveTimer += Time.deltaTime;
            if(aliveTimer <= IFrameTime)
            {
                if (sr.color.a == 0) sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
                else sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            }
            else
            {
                collide.isTrigger = false;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
        }
    }

    void OnCollisionEnter2D()
    {
        coro = Explode();
        StartCoroutine(coro);
    }

    private IEnumerator Explode()
    {
        Alive = false;
        collide.isTrigger = true;
        sr.color = new Color(0, 0, 0, 0);
        Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        lives -= 1;
        switch (lives)
        {
            case 2:
                LiveIndicator2.SetActive(false);
                break;
            case 1:
                LiveIndicator1.SetActive(false);
                break;
        }
        if (lives != 0)
        {
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            rb.angularDrag = 0;
            sr.color = new Color(1, 1, 1, 1);
            Alive = true;
            aliveTimer = 0;
        }
        else Time.timeScale = 0;
    }
}
