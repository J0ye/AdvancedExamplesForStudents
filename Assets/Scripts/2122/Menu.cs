using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum NewMenuState { Main, Setting, Credits }
public class Menu : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject main;
    public GameObject settings;
    public Dropdown dropDown;

    public NewMenuState state = NewMenuState.Main;

    private Resolution[] m_resolution;

    public void Start()
    {
        switch (state)
        {
            case NewMenuState.Main:
                main.SetActive(true);
                settings.SetActive(false);
                break;
            case NewMenuState.Setting:
                main.SetActive(false);
                settings.SetActive(true);
                break;
        }

        DropDownStart();
    }
    public void PressPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void ButtonTest(bool s)
    {
        print("Pressed:");
        print(s.ToString());
    }

    public void AdjustAudio(float val)
    {
        mixer.SetFloat("Volume", val);
    }

    public void SetFullscreen(bool val)
    {
        Screen.fullScreen = val;
    }

    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    public void SetResolution(int index)
    {
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);
    }

    public void SetMenuState()
    {
        _ = state == NewMenuState.Setting ? state = NewMenuState.Main : state = NewMenuState.Setting;

        switch(state)
        {
            case NewMenuState.Main:
                main.SetActive(true);
                settings.SetActive(false);
                break;
            case NewMenuState.Setting:
                main.SetActive(false);
                settings.SetActive(true);
                break;
        }
    }

    private void DropDownStart()
    {
        m_resolution = Screen.resolutions;
        dropDown.ClearOptions();
        List<string> options = new List<string>();
        for(int i = 0; i < m_resolution.Length; i++)
        {
            string s = m_resolution[i].width + "x" + m_resolution[i].height;
            options.Add(s);

            if(m_resolution[i].width == Screen.currentResolution.width && m_resolution[i].height == Screen.currentResolution.height)
            {
                dropDown.value = i;
            }
        }

        dropDown.AddOptions(options);
    }
}
