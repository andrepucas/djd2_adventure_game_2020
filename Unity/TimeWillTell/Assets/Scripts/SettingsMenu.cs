using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    private UserInterface   _ui;

    void Start()
    {
        _ui = UserInterface.instance;
    }
    
    public void SetVolume (float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetQuality (int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ToPause()
    {
        _ui.HideSettingsMenu();
        _ui.ShowPauseMenu();
    }
}
