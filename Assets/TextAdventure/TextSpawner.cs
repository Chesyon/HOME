using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextSpawner : MonoBehaviour
{
    public TextAdventure ta; // This is assigned here so it can be assigned to OptionButtons when generated.
    public RectTransform rotatee;
    public List<GameObject> buttonObjects;
    public GameObject buttonPrefab;
    public Transform canvas;

    public Color TextColor;
    public void SpawnText(string[] texts, int[] OptionIds)
    {
        ClearExistingText();
        float angle = 360 / texts.Length;
        for (int i = 0; i < texts.Length; i++)
        {
            GameObject buttonObject = Instantiate(buttonPrefab, rotatee.position, Quaternion.identity, canvas);
            TextMeshProUGUI text = buttonObject.GetComponentInChildren<TextMeshProUGUI>();
            OptionButton ob = buttonObject.GetComponent<OptionButton>(); // still not really efficient but uhhh womp womp. the alternative would be having GameObject.Find() in OptionButton which i think is less efficient. can't really think of a more optimized way to do this
            ob.ta = ta;
            ob.OptionID = OptionIds[i];
            text.text = texts[i];
            text.color = TextColor;
            buttonObjects.Add(buttonObject);
            transform.eulerAngles += new Vector3(0, 0, angle);
        } 
    }
    public void ClearExistingText()
    {
        foreach (GameObject go in buttonObjects) Destroy(go);
        buttonObjects = new List<GameObject>();
    }
}