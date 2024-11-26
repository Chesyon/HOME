using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject endingMenuContent;
    EndingTracker et;
    void Start() // might be able to use void awake here? i just don't wanna risk this code running before EndingTracker runs LoadEndings
    {
        // Load endings.
        et = GameObject.Find("EndingTracker").GetComponent<EndingTracker>();
        int i = 0;
        foreach (Ending ending in endingMenuContent.GetComponentsInChildren<Ending>())
        {
            ending.SetUnlocked(et.endings[i]);
            i++;
        }
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void SettingsButton()
    {
        // TODO: open settings menu.
    }
    public void MainMenuButton()
    {
        // TODO: return to main menu.
    }
    public void ClearEndingsConfirmation()
    {
        // TODO: open menu to confirm clearing endings.
    }
    public void ClearEndings()
    {
        et.endings = new bool[et.endings.Length];
        et.SaveEndings();
    }
}
