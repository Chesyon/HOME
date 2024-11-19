﻿using UnityEngine;
using System;
using UnityEngine.UI;

public class EndingTracker : MonoBehaviour
{
    public bool[] endings;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadEndings();
    }
    
    void LoadEndings()
    {
        int endingPrefs = PlayerPrefs.GetInt("endings"); // load the number from PlayerPrefs
        char[] binary = Convert.ToString(endingPrefs, 2).ToCharArray(); // convert it back into a binary number (reminder that each bit represents an ending)
        Array.Reverse(binary); // array needs to be reversed because we want to start at the lowest digit, but the string has highest digit first
        for (int i = 0; i < binary.Length; i++)
        {
            endings[i] = binary[i] == '1';
            i++;
        }
    }
    // there are a million ways i could have saved a bool[] to PlayerPrefs. this isn't super good in terms of readability, but it converts the bool[] to a binary number, with each bit representing one ending. the main benefit of this is that it's super good for file sizes, only taking up 14 bits.
    void SaveEndings()
    {
        int i = 1;
        int endingPrefs = 0;
        foreach (bool ending in endings)
        {
            if (ending) endingPrefs += i;
            i *= 2;
        }
        PlayerPrefs.SetInt("endings", endingPrefs);
    }

    // TESTING
    public Toggle[] toggles;
    public void EndingsToButtons()
    {
        LoadEndings();
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = endings[i];
        }
    }
    public void ButtonsToEndings()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            endings[i] = toggles[i].isOn;
        }
        SaveEndings();
    }
}
