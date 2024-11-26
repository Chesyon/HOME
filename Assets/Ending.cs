using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    public TextMeshProUGUI endingNameObject;
    public TextMeshProUGUI endingDescriptionObject;
    public Image endingPictureObject;
    
    public string endingName;
    public string endingDescription;
    public Sprite endingPicture;

    const string hiddenName = "???";
    const string hiddenDescription = "This ending is undiscovered.";

    public void SetUnlocked(bool unlocked)
    {
        if (unlocked)
        {
            endingNameObject.text = endingName;
            endingDescriptionObject.text = endingDescription;
            endingPictureObject.color = Color.white;
            endingPictureObject.sprite = endingPicture;
        }
        else
        {
            endingNameObject.text = hiddenName;
            endingDescriptionObject.text = hiddenDescription;
        }
    }
}
