using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;
    [SerializeField] public AudioSource audioSource;
    public Audio[] clips;
    public AudioBG[] BGclips;

    void Start()
    {
        inst = this;
        PlayAudioBG(AudioNameBG.LobbyAudio);
    }

    public void PlayAudio(AudioName name)
    {
        foreach (var item in clips)
        {
            if (item.name == name)
            {
                audioSource.PlayOneShot(item.clip);


                break;
            }
        }
    }


    public void PlayAudioBG(AudioNameBG name)
    {

        foreach (var item in BGclips)
        {
            if (item.bgname == name)
            {
                audioSource.clip = item.BGclip;
                audioSource.Play();
                break;
            }
        }
    }
}
    [System.Serializable]
    public class Audio
    {
        public AudioName name;
        public AudioClip clip;
    }


    public enum AudioName
    {
        
        Fire,
        Walk,
        damage,
        Heal,
        Gems,
        GemsCollect,
        Attack,
        Win,

        //LobbyAudio,
        //GameAudio,
    }

[System.Serializable]
public class AudioBG
{
    public AudioNameBG bgname;
    public AudioClip BGclip;
}

public enum AudioNameBG
{

   
    LobbyAudio,
    GameAudio,
}


