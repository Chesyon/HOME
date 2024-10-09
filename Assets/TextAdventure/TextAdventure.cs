using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAdventure : MonoBehaviour
{
    public GameState[] States = new GameState[] {
        new GameState { ScenarioText = "wow it's state 0!", OptionTexts = new string[] { "Press A to Explode", "Press B to be winner" }, OptionKeys = new KeyCode[] { KeyCode.A, KeyCode.B }, OptionIds = new int[] { 1, 2 }, newBgID = 0},
        new GameState { ScenarioText = "wow it's state 1!", OptionTexts = new string[] { "Press Enter to go to start" }, OptionKeys = new KeyCode[] { KeyCode.Return }, OptionIds = new int[] {0} },
        new GameState { ScenarioText = "wow it's state 2!", OptionTexts = new string[] { "Press Enter to go to start" }, OptionKeys = new KeyCode[] { KeyCode.Return }, OptionIds = new int[] {0} }
    };
    public AudioClip[] sfxLibrary;
    public AudioClip[] bgmLibrary;
    public Sprite[] bgLibrary;

    public GameState currentState;
    public TextSpawner spawner;
    public TextMeshProUGUI scenarioTextObject;
    public AudioSource sfxSource;
    public AudioSource bgmSource;
    public Image bgObject;

    void Awake()
    {
        UpdateState(0);
    }

    void Update()
    {
        for (int i = 0; i < currentState.OptionKeys.Length; i++)
        {
            if (Input.GetKeyDown(currentState.OptionKeys[i])) UpdateState(currentState.OptionIds[i]);
        }
    }

    void UpdateState(int newId)
    {
        currentState = States[newId];
        spawner.SpawnText(currentState.OptionTexts);
        scenarioTextObject.text = currentState.ScenarioText;
        if (currentState.newBgID != -1) { bgObject.sprite = bgLibrary[currentState.newBgID]; ScaleBackground(bgLibrary[currentState.newBgID]); }
        if (currentState.SfxID != -1) { sfxSource.Stop(); sfxSource.clip = sfxLibrary[currentState.SfxID]; sfxSource.Play(); }
        if (currentState.BgmID != -1) { bgmSource.Stop(); bgmSource.clip = bgmLibrary[currentState.BgmID]; bgmSource.Play(); }
    }

    public void ScaleBackground(Sprite bgSprite)
    {
        // calculate screen aspect ratio
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // calculate sprite aspect ratio
        float spriteWidth = bgSprite.rect.width;
        float spriteHeight = bgSprite.rect.height;
        float scale;

        // this logic took forever to figure out, the conditionals could absolutely be optimized but if it works it works ¯\_(ツ)_/¯
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
        else
        {
            if (spriteHeight >= spriteWidth)
            {
                scale = screenHeight / screenWidth * spriteWidth / spriteHeight;
            }
            else
            {
                scale = screenWidth / screenHeight * spriteHeight / spriteWidth;
            }
        }

        // update object with new scale
        bgObject.transform.localScale = new Vector3(scale, scale, 1);
    }
}

public class GameState
{
    public string ScenarioText { get; set;}
    public string[] OptionTexts { get; set; }
    public KeyCode[] OptionKeys { get; set; }
    public int[] OptionIds { get; set; }
    public int newBgID { get; set; } = -1;
    public int BgmID { get; set; } = -1;
    public int SfxID { get; set; } = -1;
}