using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioVolume : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider audioSlider;
    
    //슬라이더로 오디오 음량 조절
    public void AudioControl()
    {
        float sound = audioSlider.value;

        if (sound == -40f)
            audioMixer.SetFloat("Audios", -80);
        else
            audioMixer.SetFloat("Audios", sound);
    }
}
