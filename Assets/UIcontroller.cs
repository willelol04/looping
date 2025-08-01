using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Linq;

public class UIcontroller : MonoBehaviour
{
    public Slider _musicslider, _sfxslider;

    public void ToggleMusic()
    {
        Audiomanager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        Audiomanager.instance.ToggleSFX();
    }

    public void SetMusicVolume()
    {
        Audiomanager.instance.MusicVolume(_musicslider.value);
    }

    public void SetSFXVolume()
    {
        Audiomanager.instance.SFXVolume(_sfxslider.value);
    }

}
