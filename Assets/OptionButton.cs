using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public TextAdventure ta;
    public int OptionID;
    public void OptionSelected()
    {
        ta.UpdateState(OptionID);
    }
}
