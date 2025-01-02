using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextAdventure : MonoBehaviour
{
    public GameState[] States;
    public AudioClip[] sfxLibrary;
    public AudioClip[] bgmLibrary;
    public Sprite[] frame1Library;
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
    public Animator bgAnimator;

    const char TAG_SEPARATOR = ':';
    const char PARAM_SEPARATOR = ',';
    const string ANIMATOR_PARAMETER = "bgAnimId";

    // for debug mode
    public DebugMode debug;

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
        // setup debugmode
        debug = GameObject.FindGameObjectWithTag("Debug").GetComponent<DebugMode>();
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
        debug.UpdateGameStateIndicator(newId.ToString());
        dialogueText.text = "";
        dialogueText.color = currentState.textColor;
        dialogueManager.StartDialogue(currentState.DialogueText);
        SpecialProcess(currentState.SpecialProcessID);
        UpdateBackground(currentState.BgAnimID);
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
    public void UpdateBackground(int id)
    {
        bgAnimator.SetInteger(ANIMATOR_PARAMETER, id);
        ScaleBackground(frame1Library[id]);
    }
    public void PlaySfx(int id)
    {
        sfxSource.Stop();
        sfxSource.clip = sfxLibrary[id];
        sfxSource.Play();
    }
    public void PlayBgm(int id)
    {
        bgmSource.Stop();
        bgmSource.clip = bgmLibrary[id];
        bgmSource.Play();
    }
    public void ExecuteTextTag(string textTag)
    {
        string instruction = textTag.Split(TAG_SEPARATOR)[0];
        string[] parameters = textTag.Split(TAG_SEPARATOR)[1].Split(PARAM_SEPARATOR);
        switch (instruction)
        {
            case "name":
                dialogueManager.NameText.text = parameters[0];
                break;
            case "face":
                face.sprite = faceLibrary[int.Parse(parameters[0])];
                break;
            case "bgm":
                PlayBgm(int.Parse(parameters[0]));
                break;
            case "sfx":
                PlaySfx(int.Parse(parameters[0]));
                break;
            case "ground":
                UpdateBackground(int.Parse(parameters[0]));
                break;
            case "menu":
                GameObject.Find("EndingTracker").GetComponent<EndingTracker>().AddNewEnding(int.Parse(parameters[0]));
                SceneManager.LoadScene(0);
                break;
            default:
                Debug.LogError($"Unexpected text tag { instruction }");
                break;
        }
    }
}