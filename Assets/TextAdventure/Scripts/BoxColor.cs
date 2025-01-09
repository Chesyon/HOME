using UnityEngine;
using UnityEngine.UI;

public class BoxColor : MonoBehaviour
{
    public Image bg;
    public Gradient colorRange;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        bg.color = colorRange.Evaluate((Mathf.Sin(Time.time * speed) + 1) / 2);
    }
}
