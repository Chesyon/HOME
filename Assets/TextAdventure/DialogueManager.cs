using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextAdventure ta; // needed so we can spawn the options buttons AFTER text is done reading

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI DialogueText;

    //public Animator animator;
    
    public GameObject DialogueBox;
    public Queue<string> sentences;
    public Queue<string> names;
    string sentence;
    bool typing;
    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (typing) // no need to stop typing again if typing is already done.
            {
                typing = false;
                DialogueText.text = sentence;
            }
            else if (sentences.Count != 0)
            {
                DisplayNextSentence();
            }
        }
    }
    public void StartDialogue (Dialogue Dialogue)
    {
        //animator.SetBool("IsOpen", true);
        names.Clear();
        sentences.Clear();

        foreach (string name in Dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in Dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        /*if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }*/
        nameText.text = names.Dequeue();
        sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence());
    }
    IEnumerator TypeSentence ()
    {
        typing = true;
        DialogueText.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in sentence.ToCharArray())
        {
            if (typing)
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
        }
        typing = false;
        if(sentences.Count == 0) ta.ShowOptions(); // sentence writing is done! show the buttons
    }
    void EndDialogue ()
    {
        //animator.SetBool("IsOpen", false);
    }

}
