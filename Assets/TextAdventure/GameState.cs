using UnityEngine;

public class GameState : MonoBehaviour
{
    // TEXT
    public Dialogue DialogueText;
    public string[] OptionTexts;
    // CHOICE LOGIC
    public int[] OptionIds;
    // SOUND
    public int BgAnimID = 0;
    public int BgmID = -1;
    public int SfxID = -1;
    // COLOR
    public Color textColor = new Color(0, 0, 0, 1);
    // SP
    public int SpecialProcessID = -1;
}
