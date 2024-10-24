using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    public Text nameText;
    public Text DialougeText;

    public Animator animator;

    public GameObject raycastingObject;
    public Raycasting raycastingScript;
    public GameObject DialougeBox;
    public Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        raycastingScript = raycastingObject.GetComponent<Raycasting>();
        sentences = new Queue<string>();
    }
    public void StartDialouge (Dialouge dialouge)
    {
        animator.SetBool("IsOpen", true);
        Debug.Log("Starting conversation with " + dialouge.name);

        nameText.text = dialouge.name;

        sentences.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence (string sentence)
    {
        DialougeText.text = "";
        yield return new WaitForSeconds(0.5f);
        foreach (char letter in sentence.ToCharArray())
        {
            DialougeText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }
    void EndDialouge ()
    {
        animator.SetBool("IsOpen", false);
        raycastingScript.InDialouge = false;
        raycastingScript.HitObject = null;
    }

}
