using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAdventure : MonoBehaviour
{
    public GameState[] States = new GameState[] {
        new GameState { DialogueText = new Dialogue { names = new string[] { "Narrator" }, sentences = new string[] { "Welcome to the start! h" } }, OptionTexts = new string[] { "Explode", "be winner", "uhhhhhhh" }, OptionIds = new int[] { 1, 2, 3 }, newBgID = 2},
        new GameState { DialogueText = new Dialogue { names = new string[] { "Narrator" }, sentences = new string[] { "You explode! womp!" } }, OptionTexts = new string[] { "go to start" }, OptionIds = new int[] {0}, newBgID = 0 },
        new GameState { DialogueText = new Dialogue { names = new string[] { "Narrator" }, sentences = new string[] { "You are winner! Wow!" } }, OptionTexts = new string[] { "go to start" }, OptionIds = new int[] {0}, newBgID = 1 },
        new GameState { DialogueText = new Dialogue { names = new string[] { "Narrator" }, sentences = new string[] { "uhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh" } }, OptionTexts = new string[] { "go to start" }, OptionIds = new int[] {0}, newBgID = 4, textColor = new Color(0,1,0) }
    };
    public AudioClip[] sfxLibrary;
    public AudioClip[] bgmLibrary;
    public Sprite[] bgLibrary;

    public GameState currentState;
    public TextSpawner spawner;
    public DialogueManager dialogueManager;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public AudioSource sfxSource;
    public AudioSource bgmSource;
    public Image bgObject;

    void Start()
    {
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
        dialogueText.color = currentState.textColor;
        dialogueManager.StartDialogue(currentState.DialogueText);
        SpecialProcess(currentState.SpecialProcessID);
        if (currentState.newBgID != -1) { bgObject.sprite = bgLibrary[currentState.newBgID]; ScaleBackground(bgLibrary[currentState.newBgID]); }
        if (currentState.SfxID != -1) { sfxSource.Stop(); sfxSource.clip = sfxLibrary[currentState.SfxID]; sfxSource.Play(); }
        if (currentState.BgmID != -1) { bgmSource.Stop(); bgmSource.clip = bgmLibrary[currentState.BgmID]; bgmSource.Play(); }
    }

    public void ShowOptions() // called by DialogueManager when dialogue is done showing.
    {
        spawner.SpawnText(currentState.OptionTexts, currentState.OptionIds);
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
}

public class GameState
{
    // TEXT
    public Dialogue DialogueText { get; set; }
    public string[] OptionTexts { get; set; }
    // CHOICE LOGIC
    public int[] OptionIds { get; set; }
    // SOUND
    public int newBgID { get; set; } = -1;
    public int BgmID { get; set; } = -1;
    public int SfxID { get; set; } = -1;
    // COLOR
    public Color textColor { get; set; } = new Color(0, 0, 0, 1);
    // SP
    public int SpecialProcessID { get; set; } = -1;
}