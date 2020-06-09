using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodAudioSettings : MonoBehaviour
{
    
    FMOD.Studio.Bus Music;
    FMOD.Studio.Bus SFX;

    float MusicVolume = 2.5f;
    float SFXVolume = 1.5f;
    // Start is called before the first frame update

    void Awake()
    {
        Music = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        SFX = FMODUnity.RuntimeManager.GetBus("bus:/SFX");
    }
    void Update()
    {
        Music.setVolume(MusicVolume);
        SFX.setVolume(SFXVolume);
    }

    public void MusicVolumeLevel (float newMusicVolume)
    {
        MusicVolume = newMusicVolume;
    }

    public void SFXVolumeLevel (float newSFXVolume)
    {
        SFXVolume = newSFXVolume;
    }
}
