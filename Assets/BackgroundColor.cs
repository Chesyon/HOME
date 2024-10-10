using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    public Gradient colorRange;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        Camera.main.backgroundColor = colorRange.Evaluate((Mathf.Sin(Time.time * speed) + 1 ) /2);
    }
}
