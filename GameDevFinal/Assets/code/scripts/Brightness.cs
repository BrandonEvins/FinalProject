using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class NewBehaviourScript : MonoBehaviour
{
    public Slider brightnessSlider;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;
    private const string BrightnessKey = "Brightness";
    AutoExposure exposure;

    void Start()
    {
        brightness.TryGetSettings(out exposure);
        float savedBrightness = PlayerPrefs.GetFloat(BrightnessKey, 0.5f);
        AdjustBrightness(savedBrightness);
    }

    public void AdjustBrightness(float value){
        if(value != 0){
        exposure.keyValue.value=value;
        PlayerPrefs.SetFloat(BrightnessKey, value);
    }else{
        exposure.keyValue.value=.05f;
        PlayerPrefs.SetFloat(BrightnessKey, 0.05f);
    }
}
}
