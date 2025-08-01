using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        float dB = volume <= -50f ? -80f : volume;
        Debug.Log("Set MasterVolume: " + dB);
        audioMixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        float dB = volume <= -50f ? -80f : volume;
        Debug.Log("Set MusicVolume: " + dB);
        audioMixer.SetFloat("MusicVolume", dB);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetBackgVolume(float volume)
    {
        float dB = volume <= -50f ? -80f : volume;
        Debug.Log("Set SetBackgVolume: " + dB);
        audioMixer.SetFloat("SetBackgVolume", dB);
        PlayerPrefs.SetFloat("SetBackgVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        float dB = volume <= -50f ? -80f : volume;
        Debug.Log("Set SFXVolume: " + dB);
        audioMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("MasterVolume", 0));
        SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0));
        SetBackgVolume(PlayerPrefs.GetFloat("BackgVolume", 0));
        SetSFXVolume(PlayerPrefs.GetFloat("SFXVolume", 0));
    }
}
