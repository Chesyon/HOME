using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public ScreenBounds sb;
    public float boundsOffset = 1;
    float timer;
    public float spawnTime;
    public GameObject asteroid;

    void Awake()
    {
        timer = spawnTime - 3;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            timer = 0;
            SpawnAsteroid();
        }
    }

    void SpawnAsteroid()
    {
        switch (Random.Range(1, 5))
        {
            case 1: //left
                transform.position = new Vector2(sb.left - boundsOffset, Random.Range(sb.bot - boundsOffset, sb.top + boundsOffset));
                break;
            case 2: //right
                transform.position = new Vector2(sb.right + boundsOffset, Random.Range(sb.bot - boundsOffset, sb.top + boundsOffset));
                break;
            case 3: //top
                transform.position = new Vector2(Random.Range(sb.left - boundsOffset, sb.right + boundsOffset), sb.top + boundsOffset);
                break;
            case 4: //bot
                transform.position = new Vector2(Random.Range(sb.left - boundsOffset, sb.right + boundsOffset), sb.bot - boundsOffset);
                break;
        }
        Instantiate(asteroid, transform.position, Quaternion.identity);
    }
}