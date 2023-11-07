using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MenuState {Main, Setting}

public class MenuManager : MonoBehaviour
{
    public AudioMixer master;
    public Dropdown resolutionDropDown;

    [Header("Menu Elements")]
    public GameObject main;
    public GameObject settings;

    public MenuState state = MenuState.Main;

    private Resolution[] reso;

    public void Start()
    {
        UpdateUI();
        SetUpResolutionDropDown();
    }

    public void PressPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void AdjustAudioVolume(float val)
    {
        master.SetFloat("Volume", val);
    }

    public void AdjustResolution(int val)
    {
        Resolution newReso = reso[val];
        Screen.SetResolution(newReso.width, newReso.height, Screen.fullScreen);
    }

    public void AdjustQuality(int val)
    {
        QualitySettings.SetQualityLevel(val);
    }

    public void ToggleFullScreen(bool screen)
    {
        Screen.fullScreen = screen;
    }

    public void SetMenuState()
    {
        _ = state == MenuState.Setting ? state = MenuState.Main : state = MenuState.Setting;
        Debug.Log(state);
        UpdateUI();
    }

    private void SetUpResolutionDropDown()
    {
        reso = Screen.resolutions;

        resolutionDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currenResolutionIndex = 0;
        for(int i = 0; i < reso.Length; i++)
        {
            Debug.Log("Resolution: " + reso[i].width + "x" + reso[i].height);
            string option = reso[i].width + "x" + reso[i].height;
            options.Add(option);

            if(reso[i].EqualTo(Screen.currentResolution))
            {
                currenResolutionIndex = i;
            }
        }

        resolutionDropDown.AddOptions(options);
    }

    private void UpdateUI()
    {
        switch(state)
        {
            case MenuState.Main:
                main.SetActive(true);
                settings.SetActive(false);
                break;
            case MenuState.Setting:
                main.SetActive(false);
                settings.SetActive(true);
                break;
        }
    }
}

static class ExtensionMethods
{
    public static bool EqualTo(this Resolution a, Resolution b)
    {
        if(a.width == b.width && a.height == b.height)
        {
            return true;
        }

        return false;
    }
}
