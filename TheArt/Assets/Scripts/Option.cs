using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    [SerializeField]
    private GameObject settingMenu;

    [SerializeField] private Text musicVolumePersentage;
    [SerializeField] private Text sfxVolumePersentage;

    [SerializeField] private Slider musicVolumeSldier;
    [SerializeField] private Slider sfxVolumeSldier;

    [SerializeField] private Toggle musicVolumeToggle;
    [SerializeField] private Toggle sfxVolumeToggle;

    // Start is called before the first frame update
    void Start()
    {
        settingMenu.SetActive(false);
    }

    private void Awake()
    {
        musicVolumeSldier.value = AudioManager.instance.audioSourceBgm.volume;
        musicVolumePersentage.text = Mathf.RoundToInt(musicVolumeSldier.value * 100) + "%";
        musicVolumeToggle.isOn = AudioManager.instance.audioSourceBgm.mute;

        sfxVolumeSldier.value = AudioManager.instance.audioSourceEffects[0].volume;
        sfxVolumePersentage.text = Mathf.RoundToInt(sfxVolumeSldier.value * 100) + "%";
        sfxVolumeToggle.isOn = AudioManager.instance.audioSourceEffects[0].mute;
    }

    public void OpenOption()
    {
        settingMenu.SetActive(true);
        AudioManager.instance.audioSourceBgm.Pause();
        Time.timeScale = 0;
    }

    public void CloseOption()
    {
        settingMenu.SetActive(false);
        AudioManager.instance.audioSourceBgm.UnPause();
        Time.timeScale = 1;
    }

    // Setting

    public void SetMusicVolume(Slider _slider)
    {
        float musicVolmue = _slider.value;
        musicVolumePersentage.text = Mathf.RoundToInt(musicVolmue * 100) + "%";
        AudioManager.instance.SetMusicVolume(musicVolmue);
    }

    public void MuteMusice(Toggle _toggle)
    {
        if (_toggle.isOn) AudioManager.instance.SetMusicMute();
        else AudioManager.instance.SetMusicPlay();
    }

    public void SetSFXVolume(Slider _slider)
    {
        float sfxVolmue = _slider.value;
        sfxVolumePersentage.text = Mathf.RoundToInt(sfxVolmue * 100) + "%";
        AudioManager.instance.SetSFXVolume(sfxVolmue);
    }

    public void MuteSFX(Toggle _toggle)
    {
        if (_toggle.isOn)
        {
            AudioManager.instance.SetSFXMute();
        }
        else
        {
            AudioManager.instance.SetSFXPlay();
        }
    }
}
