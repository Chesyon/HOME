using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    // menu objects
    public GameObject HomeMenu;
    public GameObject SettingsMenu;
    public GameObject EndingTrackerMenu;
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
        SettingsMenu.SetActive(true);
    }
    public void MainMenuButton()
    {
        SettingsMenu.SetActive(false);
        EndingTrackerMenu.SetActive(false);
        HomeMenu.SetActive(true);
    }
    public void EndingTrackerButton()
    {
        HomeMenu.SetActive(false);
        EndingTrackerMenu.SetActive(true);
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
