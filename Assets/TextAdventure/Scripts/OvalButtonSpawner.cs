using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class OvalButtonSpawner : MonoBehaviour
{
    public TextAdventure ta;
    public GameObject buttonPrefab;
    float width; // Width of the oval
    float height; // Height of the oval
    public const float angleOffset = 90;

    public Color TextColor;
    List<GameObject> buttonObjects;

    public Vector2 scale()
    {
        RectTransform rect = GetComponent<RectTransform>();
        return new Vector2(rect.anchorMax.x - rect.anchorMin.x, rect.anchorMax.y - rect.anchorMin.y);
    }

    void Awake()
    {
        buttonObjects = new List<GameObject>();
        width = Screen.width * scale().x;
        height = Screen.height * scale().y;
    }

    public void SpawnButtons(string[] texts, int[] OptionIds)
    {
        for (int i = 0; i < texts.Length; i++)
        {
            float angle = (360f / texts.Length) * i + angleOffset; // Calculate the angle for each button
            Vector2 position = GetOvalEdgePoint(width, height, angle); // Get the position on the oval edge
            GameObject button = Instantiate(buttonPrefab, transform); // Instantiate the button
            RectTransform rectTransform = button.GetComponent<RectTransform>(); // Set the button's position
            rectTransform.anchoredPosition = position;
            // yoinked right from the old code
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            OptionButton ob = button.GetComponent<OptionButton>(); // still not really efficient but uhhh womp womp. the alternative would be having GameObject.Find() in OptionButton which i think is less efficient. can't really think of a more optimized way to do this
            ob.ta = ta;
            ob.OptionID = OptionIds[i];
            text.text = texts[i];
            text.color = TextColor;
            buttonObjects.Add(button);
        }
    }

    Vector2 GetOvalEdgePoint(float width, float height, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float x = (width / 2) * Mathf.Cos(angleRadians);
        float y = (height / 2) * Mathf.Sin(angleRadians);
        return new Vector2(x, y);
    }

    public void ClearExistingText()
    {
        foreach (GameObject go in buttonObjects) Destroy(go);
        buttonObjects = new List<GameObject>();
    }
}