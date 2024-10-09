using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextSpawner : MonoBehaviour
{
    public RectTransform rotatee;
    public List<GameObject> textObjects;
    public GameObject textPrefab;
    public Transform canvas;
    public void SpawnText(string[] texts)
    {
        ClearExistingText();
        float angle = 360 / texts.Length;
        for (int i = 0; i < texts.Length; i++)
        {
            GameObject textObject = Instantiate(textPrefab, rotatee.position, Quaternion.identity, canvas);
            textObject.GetComponent<TextMeshProUGUI>().text = texts[i];
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