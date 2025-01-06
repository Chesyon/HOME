using UnityEngine;
using System;
#if (UNITY_EDITOR) // we don't need UnityEditor outside of editor mode, so we check for UNITY_EDITOR
using UnityEditor;
#endif

public class EndingTracker : MonoBehaviour
{
    public bool[] endings;
    void Awake()
    {
        // i stole this from reflections lol
        GameObject[] objs = GameObject.FindGameObjectsWithTag("EndingTracker");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        // initial endings load
        endings = new bool[endings.Length]; // clear out array (from anything left in editor)
        LoadEndings();
    }
    
    public void LoadEndings()
    {
        int endingPrefs = PlayerPrefs.GetInt("endings"); // load the number from PlayerPrefs
        char[] binary = Convert.ToString(endingPrefs, 2).ToCharArray(); // convert it back into a binary number (reminder that each bit represents an ending)
        Array.Reverse(binary); // array needs to be reversed because we want to start at the lowest digit, but the string has highest digit first
        for (int i = 0; i < binary.Length; i++)
        {
            endings[i] = binary[i] == '1';
            //i++; i put this in at some point but don't remember why, so i removed it. try adding it back in if stuff breaks?
        }
    }
    /* there are a million ways i could have saved a bool[] to PlayerPrefs.
       this isn't super good in terms of readability, but it converts the bool[] to a binary number, with each bit representing one ending.
       the main benefit of this is that it's super good for file sizes, only taking up 14 bits.*/
    public void SaveEndings()
    {
        int i = 1;
        int endingPrefs = 0;
        foreach (bool ending in endings)
        {
            if (ending) endingPrefs += i;
            i = i << 1; // 0b1 becomes 0b10, 0b10 becomes 0b100, etc. preparing for next digit
        }
        PlayerPrefs.SetInt("endings", endingPrefs);
    }

    public void AddNewEnding(int endingNum)
    {
        endings[endingNum] = true;
        SaveEndings();
    }

    public void ResetEndings()
    {
        for (int i = 0; i < endings.Length; i++) endings[i] = false;
        SaveEndings();
    }
}

#if (UNITY_EDITOR) // this prevents the code from compiling in builds, because unity REALLY doesn't like editor stuff in builds
[CustomEditor(typeof(EndingTracker))]
class EndingTrackerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Force save")) GameObject.Find("EndingTracker").GetComponent<EndingTracker>().SaveEndings();
        if (GUILayout.Button("Force load")) GameObject.Find("EndingTracker").GetComponent<EndingTracker>().LoadEndings();
    }
}
#endif