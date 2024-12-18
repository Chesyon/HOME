using System.Linq;
using UnityEngine;
using TMPro;

public class DebugMode : MonoBehaviour
{
    string inputString;
    bool debugEnabled;
    public GameObject debugIndicator;
    public TextMeshProUGUI gameStateIndicator;

    void Awake()
    {
        // i stole this from reflections lol
        GameObject[] objs = GameObject.FindGameObjectsWithTag("EndingTracker");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        inputString += Input.inputString;
        if (inputString.ToLower().Contains("debug") && !debugEnabled)
        {
            debugEnabled = true;
            inputString = string.Empty;
        }
        if (inputString.ToLower().Contains("gubed") && debugEnabled)
        {
            debugEnabled = false;
            inputString = string.Empty;
        }
        debugIndicator.SetActive(debugEnabled);
        gameStateIndicator.gameObject.SetActive(debugEnabled);
        if (debugEnabled)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                ScreenCapture.CaptureScreenshot("screenshot.png");
            }
        }
    }
    public void UpdateGameStateIndicator(string newText)
    {
        gameStateIndicator.text = newText;
    }
}