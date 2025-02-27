using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MainMenu : MonoBehaviour
{
    // menu objects
    public GameObject HomeMenu;
    public GameObject SettingsMenu;
    public GameObject EndingTrackerMenu;
    public GameObject ClearProgressConfirmMenu;
    public GameObject CreditsMenu;
    // new ending popup
    public GameObject EndingNotification;
    public GameObject NewEndingNotification;
    public TextMeshProUGUI PopupDescription;
    string endingResultDescription;
    public TextMeshProUGUI PopupName;
    string endingResultName;
    public Image PopupImage;
    Sprite endingResultSprite;
    public TextMeshProUGUI PopupNumber;
    int endingResultNumber;
    // ending tracker stuff
    public GameObject endingMenuContent;
    EndingTracker et;
    // volume shenanigans
    public AudioMixer mix;
    public Slider BgmSlider;
    public Slider SfxSlider;
    void Start() // might be able to use void awake here? i just don't wanna risk this code running before EndingTracker runs LoadEndings
    {
        // Load endings.
        et = GameObject.Find("EndingTracker").GetComponent<EndingTracker>();
        // this menu needs to be temporarily active while we run code for it.
        EndingTrackerMenu.SetActive(true);
        int i = 0;
        foreach (Ending ending in endingMenuContent.GetComponentsInChildren<Ending>())
        {
            ending.SetUnlocked(et.endings[i]);
            if(i == et.endingResult)
            {
                endingResultDescription = ending.endingDescription;
                endingResultName = ending.endingName;
                endingResultSprite = ending.endingPicture;
                endingResultNumber = i + 1;
            }
            i++;
        }
        EndingTrackerMenu.SetActive(false); // no longer needed.
        // Load audio settings from PlayerPrefs. stole this from reflections
        float BgmVolume = PlayerPrefs.GetFloat("BgmVolume");
        float SfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        SettingsMenu.SetActive(true); // this menu needs to be temporarily active while we run code for it.
        BgmSlider.value = VolumeToSlider(BgmVolume);
        SfxSlider.value = VolumeToSlider(SfxVolume);
        SettingsMenu.SetActive(false); // no longer needed
        mix.SetFloat("BgmVolume", BgmVolume);
        mix.SetFloat("SfxVolume", SfxVolume);

        // did we just get an ending? if so, display popup!
        if (et.endingResult != -1) {
            HomeMenu.SetActive(false);
            EndingNotification.SetActive(true);
            NewEndingNotification.SetActive(et.newEndingFlag);
            PopupDescription.text = endingResultDescription;
            PopupName.text = endingResultName;
            PopupImage.sprite = endingResultSprite;
            PopupNumber.text = $"Ending { endingResultNumber } of { et.endings.Length }";
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
        HomeMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }
    public void CreditsButton()
    {
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }
    public void MainMenuButton()
    {
        EndingNotification.SetActive(false);
        SettingsMenu.SetActive(false);
        EndingTrackerMenu.SetActive(false);
        HomeMenu.SetActive(true);
    }
    public void EndingTrackerButton()
    {
        ClearProgressConfirmMenu.SetActive(false);
        HomeMenu.SetActive(false);
        EndingTrackerMenu.SetActive(true);
    }
    public void ClearEndingsConfirmation()
    {
        EndingTrackerMenu.SetActive(false);
        ClearProgressConfirmMenu.SetActive(true);
    }
    public void ClearEndings()
    {
        et.endings = new bool[et.endings.Length];
        et.SaveEndings();
        Application.Quit();
    }

    // stole all of this volume stuff from reflections.
    public void UpdateBgmVolume()
    {
        mix.SetFloat("BgmVolume", SliderToVolume(BgmSlider.value));
        PlayerPrefs.SetFloat("BgmVolume", SliderToVolume(BgmSlider.value));
    }
    public void UpdateSfxVolume()
    {
        mix.SetFloat("SfxVolume", SliderToVolume(SfxSlider.value));
        PlayerPrefs.SetFloat("SfxVolume", SliderToVolume(SfxSlider.value));
    }
    float SliderToVolume(float slider)
    {
        slider *= 100;
        slider -= 80;
        return slider;
    }
    float VolumeToSlider(float volume)
    {
        volume += 80;
        volume /= 100;
        return volume;
    }
}
