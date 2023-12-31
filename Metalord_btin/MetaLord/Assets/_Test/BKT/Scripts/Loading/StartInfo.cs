using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;


public class StartInfo : MonoBehaviour
{
    public static StartInfo instance;

    public bool isLoaded = default;

    //[SerializeField] private AudioMixer audioMixer;

    public int info_ScreenMode;
    public int info_ResolutionIndex;
    public float info_Brightness;
    public ButtonSize info_TextSize;
    public ButtonSize info_CrossLine;
    public float info_MasterSlider;
    public float info_BGMSlider;
    public float info_SFXSlider;
    public int info_MasterToggle;
    public int info_BGMToggle;
    public int info_SFXToggle;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SetPersonalSetting();
    }

    public void SetPersonalSetting()
    {
        if (PlayerPrefs.HasKey("DisplaySetting"))
        {
            LoadPersonalSetting(); // 개인 환경설정 불러오기
            //SetAudio();
        }
        else InitPersonalSetting();
    }

    // 개인 환경설정 불러오기
    private void LoadPersonalSetting()
    {
        //if (PlayerPrefs.HasKey("DisplaySetting"))
        info_ScreenMode = PlayerPrefs.GetInt("DisplaySetting");

        //if (PlayerPrefs.HasKey("ResolutionSettings"))
        info_ResolutionIndex = PlayerPrefs.GetInt("ResolutionSettings");

        //if (PlayerPrefs.HasKey("BrightnessSetting"))
        info_Brightness = PlayerPrefs.GetFloat("BrightnessSetting");

        //if(PlayerPrefs.HasKey("CrossLineSetting"))
        info_CrossLine = (ButtonSize)PlayerPrefs.GetInt("CrossLineSetting");

        //if (PlayerPrefs.HasKey("TextSizeSetting"))
        info_TextSize = (ButtonSize)PlayerPrefs.GetInt("TextSizeSetting");

        //if (PlayerPrefs.HasKey("MasterSlider"))
        {
            info_MasterSlider = PlayerPrefs.GetFloat("MasterSlider");
            info_BGMSlider = PlayerPrefs.GetFloat("BGMSlider");
            info_SFXSlider = PlayerPrefs.GetFloat("SFXSlider");

            info_MasterToggle = PlayerPrefs.GetInt("MasterToggle");
            info_BGMToggle = PlayerPrefs.GetInt("BGMToggle");
            info_SFXToggle = PlayerPrefs.GetInt("SFXToggle");
        }

    }

    private void InitPersonalSetting()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        info_MasterToggle = 1;
        info_BGMToggle = 1;
        info_SFXToggle = 1;
        info_MasterSlider = 1f;
        info_BGMSlider = 1f;
        info_SFXSlider = 1f;
    }

    //private void SetAudio()
    //{
    //    audioMixer.SetFloat("Master", info_MasterSlider);
    //    audioMixer.SetFloat("BGM", info_BGMSlider);
    //    audioMixer.SetFloat("SFX", info_SFXSlider);
    //}


}
