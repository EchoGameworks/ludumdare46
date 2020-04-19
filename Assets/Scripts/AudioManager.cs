using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public enum SoundEffects {  Select, Order,
                                SaiMalor_Gun, Brecht_Gun,
                                DeathStinger_Spawn, DeathStinger_Attack, DeathStinger_Death,
                                Ravager_Spawn, Ravager_Attack, Ravager_Death,
                                SandHunter_Spawn, SandHunter_Attack, SandHunter_Death,
                                StartGame, Water, Revive,
                                None
    }

    public static AudioManager instance;
    
    [Range(0f, 1f)]
    public float MasterMusicVolume = 1f;
    [Range(0f, 1f)]
    public float MasterSFXVolume = 1f;

    public List<Sound> Sounds;

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
            return;
        }

        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
        }
    }


    public void PlaySound(SoundEffects soundEffect, bool varyPitch = true)
    {
        if (soundEffect == SoundEffects.None) return;
        Sound s = Sounds.FirstOrDefault(o => o.SoundName == soundEffect);
        if (s == null) return;
        s.source.volume = MasterSFXVolume * s.Volume;
        float tempPitch = s.Pitch;
        if (varyPitch)
        {
            tempPitch = Random.Range(0.5f, 1.5f);
        }
        s.source.pitch = s.Pitch;
        //print("playing: " + soundEffect + " at " + s.source.volume + " (" + MasterSFXVolume + " | " + s.Volume + ")");
        s.source.Play();
    }  

    public void PlayIntro()
    {
        PlaySound(SoundEffects.StartGame, false);
    }
}
