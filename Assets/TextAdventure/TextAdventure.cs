using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAdventure : MonoBehaviour
{
    public GameState[] States;
    public AudioClip[] sfxLibrary;
    public AudioClip[] bgmLibrary;
    public Sprite[] bgLibrary;
    public Sprite[] faceLibrary;

    public GameState currentState;
    public OvalButtonSpawner spawner;
    public DialogueManager dialogueManager;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public AudioSource sfxSource;
    public AudioSource bgmSource;
    public Image bgObject;
    public Image face;

    const char tagSeparator = ':';

    void Start()
    {
        // sanity checks
        for (int i = 0; i <= States.Length - 1; i++)
        {
            if (States[i].OptionTexts.Length != States[i].OptionIds.Length) Debug.LogError($"Length of option texts and IDs are not equal for state { i.ToString() }. Texts length: { States[i].OptionTexts.Length.ToString() }. IDs length: { States[i].OptionIds.Length.ToString() }.");
            foreach (int ID in States[i].OptionIds)
            {
                if (ID < 0 || ID > States.Length - 1) Debug.LogError($"State { i.ToString() } contains an option leading to ID { ID.ToString() }, which does not exist.");
            }
        }
        // start the gaem :)
        UpdateState(0);
    }

    void SpecialProcess(int ID) // Special Processes are used for rare scenarios in code that don't need to be included in the class.
    {
        switch (ID)
        {
            default:
                break;
        }
    }

    public void UpdateState(int newId)
    {
        currentState = States[newId];
        spawner.ClearExistingText();
        spawner.TextColor = currentState.textColor;
        nameText.color = currentState.textColor;
        dialogueText.text = "";
        dialogueText.color = currentState.textColor;
        dialogueManager.StartDialogue(currentState.DialogueText);
        SpecialProcess(currentState.SpecialProcessID);
        if (currentState.newBgID != -1) { bgObject.sprite = bgLibrary[currentState.newBgID]; ScaleBackground(bgLibrary[currentState.newBgID]); }
        if (currentState.SfxID != -1) { sfxSource.Stop(); sfxSource.clip = sfxLibrary[currentState.SfxID]; sfxSource.Play(); }
        if (currentState.BgmID != -1) { bgmSource.Stop(); bgmSource.clip = bgmLibrary[currentState.BgmID]; bgmSource.Play(); }
    }

    public void ShowOptions() // called by DialogueManager when dialogue is done showing.
    {
        spawner.SpawnButtons(currentState.OptionTexts, currentState.OptionIds);
    }

    public void ScaleBackground(Sprite bgSprite)
    {
        // calculate screen aspect ratio
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // calculate sprite aspect ratio
        float spriteWidth = bgSprite.rect.width;
        float spriteHeight = bgSprite.rect.height;

        // this logic took forever to figure out, the conditionals could absolutely be optimized but if it works it works ¯\_(ツ)_/¯
        float scale = 0;
        if (screenWidth >= screenHeight)
        {
            if (spriteWidth > spriteHeight)
            {
                scale = screenHeight / screenWidth * spriteWidth / spriteHeight;
            }
            else
            {
                scale = screenWidth / screenHeight * spriteHeight / spriteWidth;
            }
        }
        /*else // currently commented out because screenheight will never be greater than screenwidth with a 16:9 aspect ratio
        {
            if (spriteHeight >= spriteWidth)
            {
                scale = screenHeight / screenWidth * spriteWidth / spriteHeight;
            }
            else
            {
                scale = screenWidth / screenHeight * spriteHeight / spriteWidth;
            }
        }*/

        // update object with new scale
        bgObject.transform.localScale = new Vector3(scale, scale, 1);
    }

    public void ExecuteTextTag(string textTag)
    {
        string instruction = textTag.Split(tagSeparator)[0];
        string parameter = textTag.Split(tagSeparator)[1];
        switch (instruction)
        {
            case "name":
                dialogueManager.NameText.text = parameter;
                break;
            case "face":
                face.sprite = faceLibrary[int.Parse(parameter)];
                break;
            default:
                Debug.LogError($"Unexpected text tag { instruction }");
                break;
        }
    }
}