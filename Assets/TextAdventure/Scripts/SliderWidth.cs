using UnityEngine;

public class SliderWidth : MonoBehaviour
{
    public RectTransform rect;
    // JAAAAAANK city :)
    // rect transforms my abhorred
    void Start()
    {
        rect.sizeDelta = new Vector2(Screen.width / 32, rect.sizeDelta.y);
    }
}
