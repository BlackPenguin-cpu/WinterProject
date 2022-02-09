using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Clip
{
    public string Name;
    public AudioClip clip;
}
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public List<Clip> clips;
    public bool On = true;
    public bool SFXOn = true;

    
    float SEvolume = 1;
    protected SoundManager() { }

    public Slider MusicSlider;
    public Slider SESlider;
    public Text Musictext;
    public Text SEtext;

    public void Playbgm(string name)
    //사용법 Sound.Instance.ChangeClip("이름",루프 할껀지안할껀지(bool))
    {
        Clip find = clips.Find((o) => { return o.Name == name; });
        if (find != null)
        {
            audioSource.Stop();
            audioSource.clip = find.clip;
            audioSource.loop = true;
            audioSource.Play();

        }
    }

    public void PlaySound(string _clip)
    {
        Clip find = clips.Find((o) => { return o.Name == _clip; });
        if (find != null)
        {
            GameObject audio_object = new GameObject();
            AudioSource object_source = audio_object.AddComponent<AudioSource>();
            object_source.volume = SEvolume;
            object_source.clip = find.clip;
            object_source.loop = false;
            object_source.Play();

            Destroy(audio_object, find.clip.length);
        }
    }
    public void PlaySound(AudioClip _clip)
    {
        GameObject audio_object = new GameObject();
        AudioSource object_source = audio_object.AddComponent<AudioSource>();
        object_source.volume = SEvolume;
        object_source.clip = _clip;
        object_source.loop = false;
        object_source.Play();

        Destroy(audio_object, _clip.length);
    }

    public void SetMusicVolume(float volume)
    {
        audioSource.volume = volume;
        Musictext.text = (int)(volume * 100) + "%";
    }
    public void SetSEVolume(float volume)
    {
        SEvolume = volume;
        SEtext.text = (int)(volume * 100) + "%";
    }


    public void SoundButtonLeft()
    {
        if (audioSource.volume - 0.1 < 0)
        {
            audioSource.volume = 0;
        }
        else
        {
            Mathf.Round((audioSource.volume -= 0.1f) * 1000f);
        }
        MusicSlider.value = audioSource.volume;
        Musictext.text = (int)(audioSource.volume * 100) + "%";
    }

    public void SoundButtonRight()
    {
        if (audioSource.volume + 0.1 > 1)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume += 0.1f;
        }
        MusicSlider.value = audioSource.volume;
        Musictext.text = (int)(audioSource.volume * 100) + "%";
    }

    public void SoundEffectLeft()
    {
        if(SEvolume - 0.1f < 0)
        {
            SEvolume = 0;
        }
        else
        {
            SEvolume -= 0.1f;
        }
        SESlider.value = SEvolume;
        SEtext.text = (int)(SEvolume * 100) + "%";
    }

    public void SoundEffectRight()
    {
        if(SEvolume + 0.1f > 1)
        {
            SEvolume = 1;
        }
        else
        {
            SEvolume += 0.1f;
        }
        SESlider.value = SEvolume;
        SEtext.text = (int)(SEvolume * 100) + "%";
    }

}
