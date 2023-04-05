using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // ���� �̸�.
    public AudioClip clip; // ��.
}

public class AudioManager : MonoBehaviour
{

    static public AudioManager instance;
    #region singleton
    void Awake() // ��ü ������ ���� ����.
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);
    }
    #endregion singleton

    public AudioSource[] audioSourceEffects;
    public AudioSource audioSourceBgm;

    public string[] playSoundName;

    public Sound[] effectSounds;
    public Sound[] bgmSounds;

    // ����� ����
    public string currentBGM;

    // ����� �Ҹ� On/Off üũ ����
    public bool isAudioPlay;

    void Start()
    {
        isAudioPlay = true;
        audioSourceBgm.volume = 0.5f;
        playSoundName = new string[audioSourceEffects.Length];
    }

    // PauseMenu: Setting���� ����� ���� ��������
    public void SetMusicVolume(float vol)
    {
        audioSourceBgm.volume = vol;
    }
    public void SetMusicMute()
    {
        audioSourceBgm.mute = true;
    }
    public void SetMusicPlay()
    {
        audioSourceBgm.mute = false;
    }
    public void SetSFXVolume(float vol)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].volume = vol;
        }
    }
    public void SetSFXMute()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].mute = true;
        }
    }
    public void SetSFXPlay()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].mute = false;
        }
    }

    public void PlaySE(string _name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (_name == effectSounds[i].name)
            {
                for (int j = 0; j < audioSourceEffects.Length; j++)
                {
                    if (!audioSourceEffects[j].isPlaying)
                    {
                        playSoundName[j] = effectSounds[i].name;
                        audioSourceEffects[j].clip = effectSounds[i].clip;
                        audioSourceEffects[j].Play();
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ������Դϴ�.");
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }

    public void StopAllSE()
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            audioSourceEffects[i].Stop();
        }
    }

    public void StopSE(string _name)
    {
        for (int i = 0; i < audioSourceEffects.Length; i++)
        {
            if (playSoundName[i] == _name)
            {
                audioSourceEffects[i].Stop();
                break;
            }
        }
        Debug.Log("��� ����" + _name + "���尡 �����ϴ�.");
    }

    public void PlayBGM(string _name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBgm.clip = bgmSounds[i].clip;
                audioSourceBgm.Play();
            }
        }
    }

    public void SetBGMVolume(string _name, float _vol)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (_name == bgmSounds[i].name)
            {
                audioSourceBgm.clip = bgmSounds[i].clip;
                audioSourceBgm.volume = _vol;
            }
        }
    }

    public void SetAllBGMVolume(float _vol)
    {
        audioSourceBgm.volume = _vol;
    }

    public void StopBGM()
    {
        audioSourceBgm.Stop();
    }

    public void ContinueBGM()
    {
        audioSourceBgm.Play();
    }

    public void SetAudioPlay(bool _active)
    {
        isAudioPlay = _active;
    }
}