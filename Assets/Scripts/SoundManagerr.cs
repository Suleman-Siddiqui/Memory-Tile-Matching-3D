using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerr : MonoBehaviour
{
    public AudioSource aSource;
    public List<Clip> clips;
    public static SoundManagerr Instance;
    [HideInInspector]
    internal bool IsPlaying { get { return aSource.isPlaying; } }

    public List<string> playingSounds = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
          //  DontDestroyOnLoad(gameObject);
        }
        else
        {
           // Destroy(gameObject);
        }
    }

    public float Volume
    {
        get
        {
            return aSource.volume;
        }
        set
        {
            aSource.volume = value;
        }
    }

    // new 

    public float Pitch
    {
        get
        {
            return aSource.pitch;
        }
        set
        {
            aSource.pitch = value;
        }
    }

    public void PlayOneShot(string name, float volume = 1)
    {

        if (UI_SettingController.IsSoundOnn)
        {
            Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.PlayOneShot(aSource.clip, volume);
            }
        }
    }

    public void PlayAudio(string name, bool loop = false, bool vibration = true)
    {
        if (UI_SettingController.IsSoundOnn)
        {
            Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.loop = loop;
                aSource.Play();
            }
        }

        if (vibration && (UI_SettingController.IsHapticOnn))
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }
    }



    public void PlayRandomAudio()
    {
        Clip clip = clips[Random.Range(0, clips.Count)];

        if (clip.clip != null && (UI_SettingController.IsSoundOnn))
        {
            aSource.clip = clip.clip;
            aSource.Play();
        }
    }




    public void StopAudio()
    {
        aSource.Stop();
    }

    public void PauseAudio()
    {
        aSource.Pause();
    }
    Clip GetClip(string name)
    {
        return clips.Find(c => c.name.Equals(name));
    }
    [System.Serializable]
    public class Clip
    {

        public string name;
        public AudioClip clip;
    }

    public void PlayAudio2(List<string> names, bool loop = false, bool vibration = true)
    {
        if (UI_SettingController.IsSoundOnn)
        {
            foreach (string name in names)
            {
                Clip clip = GetClip(name);
                if (clip != null && clip.clip != null)
                {
                    aSource.PlayOneShot(clip.clip);
                }
            }

            aSource.loop = loop;
            if (!aSource.isPlaying)
            {
                aSource.Play();
            }
        }

        if (vibration && (UI_SettingController.IsHapticOnn))
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }
    }




    public void PlayAudio2(string name, bool loop = false, bool vibration = true)
    {
        if (UI_SettingController.IsSoundOnn)
        {
            Clip clip = GetClip(name);
            if (clip != null && clip.clip != null)
            {
                aSource.clip = clip.clip;
                aSource.loop = loop;
                aSource.Play();
                Debug.Log("Playing audio: " + name);
                playingSounds.Add(name);
            }
        }

        if (vibration && (UI_SettingController.IsHapticOnn))
        {
            MMVibrationManager.Haptic(HapticTypes.SoftImpact);
        }
    }

    public void PlayRandomAudio2()
    {
        Clip clip = clips[Random.Range(0, clips.Count)];
        if (clip.clip != null && (UI_SettingController.IsSoundOnn))
        {
            aSource.clip = clip.clip;
            aSource.Play();
            Debug.Log("Playing random audio: " + clip.name);
            playingSounds.Add(clip.name);
        }
    }
}
