using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextAdventure ta; // needed so we can spawn the options buttons AFTER text is done reading
    public TextMeshProUGUI DialogueText;
    public TextMeshProUGUI NameText;

    //public Animator animator;
    
    public GameObject DialogueBox;
    public Queue<string> sentences = new Queue<string>();
    public Queue<string> names = new Queue<string>();
    string sentence;
    const float DefaultTypeSpeed = 0.02f;
    bool typing;
    float TypeDelay()
    {
        if (typing) return DefaultTypeSpeed;
        else return 0f;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (typing) // no need to stop typing again if typing is already done.
            {
                typing = false;
            }
            else if (sentences.Count != 0)
            {
                DisplayNextSentence();
            }
            else if (ta.currentState.OptionIds.Length == 1)
            {
                ta.UpdateState(ta.currentState.OptionIds[0]);
            }
        }
    }
    public void StartDialogue (Dialogue Dialogue)
    {
        //animator.SetBool("IsOpen", true);
        sentences.Clear();

        foreach (string sentence in Dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }
    IEnumerator TypeSentence ()
    {
        typing = true;
        bool textTagMode = false;
        string currentTextTag = "";
        DialogueText.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in sentence.ToCharArray())
        {
            if(letter.ToString() == "[") // START TEXT TAG MODE
            {
                textTagMode = true;
            }
            if(textTagMode)
            {
                switch (letter.ToString())
                {
                    case "[":
                        break;
                    case "]":
                        textTagMode = false;
                        ta.ExecuteTextTag(currentTextTag);
                        currentTextTag = "";
                        break;
                    default:
                        currentTextTag += letter;
                        break;
                }
            }
            else
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(TypeDelay());
            }
        }
        typing = false;
        if (sentences.Count == 0 && ta.currentState.OptionIds.Length > 1) ta.ShowOptions(); // sentence writing is done! show the buttons
    }
    void EndDialogue ()
    {
        //animator.SetBool("IsOpen", false);
    }
}
