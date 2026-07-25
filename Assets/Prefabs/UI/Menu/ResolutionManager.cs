using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    [Header("Dropdown UI Elements")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown windowModeDropdown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions = new List<Resolution>();


    private const string ResolutionKey = "SavedResolutionIndex";
    private const string WindowModeKey = "SavedWindowModeIndex";

    void Awake()
    {

        if (resolutionDropdown == null) resolutionDropdown = GetComponent<TMP_Dropdown>();
        if (windowModeDropdown == null) windowModeDropdown = transform.Find("WindowModeDropdown")?.GetComponent<TMP_Dropdown>();
    }

    void Start()
    {
        InitResolutions();
        InitWindowMode();
    }


    private void InitResolutions()
    {
        if (resolutionDropdown == null) return;

        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        filteredResolutions.Clear();

        List<string> options = new List<string>();
        RefreshRate currentRefreshRate = Screen.currentResolution.refreshRateRatio;
        int defaultResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRateRatio.Equals(currentRefreshRate))
            {
                filteredResolutions.Add(resolutions[i]);
                options.Add($"{resolutions[i].width} x {resolutions[i].height}");


                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    defaultResolutionIndex = filteredResolutions.Count - 1;
                }
            }
        }


        if (filteredResolutions.Count == 0)
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                filteredResolutions.Add(resolutions[i]);
                options.Add($"{resolutions[i].width} x {resolutions[i].height}");
                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    defaultResolutionIndex = i;
                }
            }
        }

        resolutionDropdown.AddOptions(options);


        int savedResolutionIndex = PlayerPrefs.GetInt(ResolutionKey, defaultResolutionIndex);


        if (savedResolutionIndex >= filteredResolutions.Count)
        {
            savedResolutionIndex = defaultResolutionIndex;
        }

        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        ApplyResolution(savedResolutionIndex);

        resolutionDropdown.onValueChanged.AddListener(SetResolutionAndSave);
    }


    private void InitWindowMode()
    {
        if (windowModeDropdown == null) return;

        int defaultModeIndex = 0; 

        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.FullScreenWindow:
            case FullScreenMode.ExclusiveFullScreen:
                defaultModeIndex = 0;
                break;
            case FullScreenMode.MaximizedWindow:
                defaultModeIndex = 1;
                break;
            case FullScreenMode.Windowed:
                defaultModeIndex = 2;
                break;
        }

  
        int savedModeIndex = PlayerPrefs.GetInt(WindowModeKey, defaultModeIndex);

        windowModeDropdown.value = savedModeIndex;
        windowModeDropdown.RefreshShownValue();


        ApplyWindowMode(savedModeIndex);

 
        windowModeDropdown.onValueChanged.AddListener(SetWindowModeAndSave);
    }


    private void SetResolutionAndSave(int resolutionIndex)
    {
        ApplyResolution(resolutionIndex);
        PlayerPrefs.SetInt(ResolutionKey, resolutionIndex);
        PlayerPrefs.Save(); 
    }


    private void SetWindowModeAndSave(int modeIndex)
    {
        ApplyWindowMode(modeIndex);
        PlayerPrefs.SetInt(WindowModeKey, modeIndex);
        PlayerPrefs.Save(); 
    }

 
    private void ApplyResolution(int index)
    {
        if (index >= 0 && index < filteredResolutions.Count)
        {
            Resolution resolution = filteredResolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            Debug.Log($"[Load/Change] Resolution: {resolution.width} x {resolution.height}");
        }
    }


    private void ApplyWindowMode(int index)
    {
        FullScreenMode targetMode = FullScreenMode.FullScreenWindow;

        switch (index)
        {
            case 0: targetMode = FullScreenMode.FullScreenWindow; break; 
            case 1: targetMode = FullScreenMode.MaximizedWindow; break; 
            case 2: targetMode = FullScreenMode.Windowed; break; 
        }

        Screen.SetResolution(Screen.width, Screen.height, targetMode);
        Debug.Log($"[Load/Change] Screen Mode: {targetMode}");
    }
}
