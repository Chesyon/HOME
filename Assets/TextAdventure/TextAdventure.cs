﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextAdventure : MonoBehaviour
{
    public GameState[] States = new GameState[] {
        new GameState { ScenarioText = "Start", OptionTexts = new string[] { "Explode", "be winner", "uhhhhhhh" }, OptionIds = new int[] { 1, 2, 3 }, newBgID = 2},
        new GameState { ScenarioText = "You explode! womp!", OptionTexts = new string[] { "go to start" }, OptionIds = new int[] {0}, newBgID = 0 },
        new GameState { ScenarioText = "You are winner! Wow!", OptionTexts = new string[] { "go to start" }, OptionIds = new int[] {0}, newBgID = 1 },
        new GameState { ScenarioText = "uhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh", OptionTexts = new string[] { "go to start" }, OptionIds = new int[] {0}, newBgID = 4, textColor = new Color(0,1,0) }
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
        spawner.TextColor = currentState.textColor;
        spawner.SpawnText(currentState.OptionTexts, currentState.OptionIds);
        scenarioTextObject.color = currentState.textColor;
        scenarioTextObject.text = currentState.ScenarioText;
        SpecialProcess(currentState.SpecialProcessID);
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

        // this logic took forever to figure out, the conditionals could absolutely be optimized but if it works it works ¯\_(ツ)_/¯
        float scale;
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
    // TEXT
    public string ScenarioText { get; set; }
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