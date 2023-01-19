using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider slider;
    float volume = 0.5f;

    public void OnVolumeChanged()
    {
        volume = slider.value;
    }

    public float GetVolume()
    {
        return volume;
    }

    public void Initialize()
    {
        slider.value = 0.5f;
    }
}
