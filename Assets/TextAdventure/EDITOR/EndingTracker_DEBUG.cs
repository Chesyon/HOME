// this script must be commented out or the game will not build!! there's probably some way to automate this but idrc rn
/*
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EndingTracker))]
class EndingTrackerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Force save")) GameObject.Find("EndingTracker").GetComponent<EndingTracker>().SaveEndings();
        if (GUILayout.Button("Force load")) GameObject.Find("EndingTracker").GetComponent<EndingTracker>().LoadEndings();
    }
}*/