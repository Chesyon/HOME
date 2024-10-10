using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextSpawner : MonoBehaviour
{
    public RectTransform rotatee;
    public List<GameObject> textObjects;
    public GameObject textPrefab;
    public Transform canvas;

    public Color TextColor;
    public void SpawnText(string[] texts)
    {
        ClearExistingText();
        float angle = 360 / texts.Length;
        for (int i = 0; i < texts.Length; i++)
        {
            GameObject textObject = Instantiate(textPrefab, rotatee.position, Quaternion.identity, canvas);
            TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
            text.text = texts[i];
            text.color = TextColor;
            textObjects.Add(textObject);
            transform.eulerAngles += new Vector3(0, 0, angle);
        } 
    }
    public void ClearExistingText()
    {
        foreach (GameObject go in textObjects) Destroy(go);
        textObjects = new List<GameObject>();
    }
}