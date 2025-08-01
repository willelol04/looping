    using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class Audiomanager : MonoBehaviour
{
    public static Audiomanager instance;

    public sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public AudioMixer audioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (musicSounds.Length > 0)
        {
            PlayMusic(musicSounds[0].name); // Play the first music sound by default
        }
    }
    public void PlayMusic(string name)
    {
        sound s = System.Array.Find(musicSounds, sound => sound.name == name);
        if (s != null)
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music sound not found: " + name);
        }
    }

    public void PlaySFX(string name)
    {
        sound s = System.Array.Find(sfxSounds, sound => sound.name == name);
        if (s != null)
        {
            sfxSource.PlayOneShot(s.clip);
        }
        else
        {
            Debug.LogWarning("SFX sound not found: " + name);
        }
    }

    //for testing sfx make it when you press jump it activates "jump"
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change KeyCode.Space to your desired key
        {
            PlaySFX("jump"); // Ensure "jump" is a valid sound name in sfxSounds
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;

    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        if (audioMixer == null)
        {
            Debug.LogError("AudioMixer is not assigned in the AudioManager!");
            return;
        }
        float dB = volume <= -50f ? -80f : volume;
        Debug.Log("Set MusicVolume: " + dB);
        audioMixer.SetFloat("MusicVolume", dB);
    }
    public void SFXVolume(float volume)
    {
        float dB = volume <= -50f ? -80f : volume;
        Debug.Log("Set SFXVolume: " + dB);
        audioMixer.SetFloat("SFXVolume", dB);
    }
}
