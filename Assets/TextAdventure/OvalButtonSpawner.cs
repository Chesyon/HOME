using UnityEngine;
using UnityEngine.UI;

public class OvalButtonSpawner : MonoBehaviour
{
    public Canvas canvas;
    public GameObject buttonPrefab; // Assign your button prefab in the Inspector
    public Vector2 center = new Vector2(0, 0); // Center of the oval
    public float width = 400f; // Width of the oval
    public float height = 200f; // Height of the oval
    public Vector2 scale;
    public int buttonCount = 12; // Number of buttons to create

    void Start()
    {
        width = Screen.width * scale.x;
        height = Screen.height * scale.y;
        SpawnButtons();
    }

    void SpawnButtons()
    {
        for (int i = 0; i < buttonCount; i++)
        {
            // Calculate the angle for each button
            float angle = (360f / buttonCount) * i;

            // Get the position on the oval edge
            Vector2 position = GetOvalEdgePoint(width, height, angle);

            // Instantiate the button
            GameObject button = Instantiate(buttonPrefab, transform);

            // Set the button's position
            RectTransform rectTransform = button.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = position;

            // Optionally: Set the button text or add any listeners here
            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = $"Button {i + 1}";
        }
    }

    Vector2 GetOvalEdgePoint(float width, float height, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float x = (width / 2) * Mathf.Cos(angleRadians);
        float y = (height / 2) * Mathf.Sin(angleRadians);
        return new Vector2(x, y);
    }
}