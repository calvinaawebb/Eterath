using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings_Menu : MonoBehaviour
{
    public AudioMixer main;
    Resolution[] qualities;
    public TMP_Dropdown resDrop;
    List<string> options;
    // Start is called before the first frame update
    void Start()
    {
        qualities = Screen.resolutions;
        resDrop.ClearOptions();
        options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < qualities.Length; i++) 
        {
            string option = qualities[i].width + "x" + qualities[i].height;
            options.Add(option);

            if (qualities[i].width == Screen.currentResolution.width && qualities[i].height == Screen.currentResolution.height) 
            {
                currentResIndex = i;
            }
        }
        resDrop.AddOptions(options);
        resDrop.value = currentResIndex;
        resDrop.RefreshShownValue();
    }

    public void SetResolution(int resIndex) 
    {
        Resolution resTemp = qualities[resIndex];
        Screen.SetResolution(resTemp.width, resTemp.height, Screen.fullScreen);
    }

    public void SetFullscreen(bool isFullscreen) 
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMaster(float masterVolume)
    {
        main.SetFloat("Master", masterVolume);
    }

    public void SetMusic(float musicVolume)
    {
        if (musicVolume == -30f) 
        {
            main.SetFloat("Music", -80f);
        } else 
        {
            main.SetFloat("Music", musicVolume);
        }
        
    }

    public void SetEffects(float effectsVolume)
    {
        main.SetFloat("Effects", effectsVolume);
    }

    public void SetVSync(bool vSync)
    {
        if (vSync) 
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
