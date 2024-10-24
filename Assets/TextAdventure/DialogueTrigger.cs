using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialouge dialouge;
    public void TriggerDialogue()
    {
        FindObjectOfType<DialougeManager>().StartDialouge(dialouge);
    }
}
