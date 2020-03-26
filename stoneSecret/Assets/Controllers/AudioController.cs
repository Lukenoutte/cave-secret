using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioController instance;
    public int soundIndex;
    private bool playedFirstSong = false;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
     foreach( Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
        }   
    }


    public void Play()
    {
        int oldIdSound = soundIndex;
        soundIndex = Random.Range(0, 4);
        while(oldIdSound == soundIndex)
        {
            soundIndex = Random.Range(0, 4);
        }
        sounds[soundIndex].source.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if(SaveManager.instance != null) { 
        if (!SaveManager.instance.state.playedTuto)
        {
                if (!playedFirstSong) { 
                    soundIndex = 0;
                }
                if (!sounds[soundIndex].source.isPlaying && !playedFirstSong)
                {
                    sounds[soundIndex].source.Play();
                    playedFirstSong = true;
                }
                else if(!sounds[soundIndex].source.isPlaying && playedFirstSong)
                {
                    Play();
                    Debug.Log("Play new Theme Song");
                }

        }
        else if (!sounds[soundIndex].source.isPlaying)
        {
            Play();
            Debug.Log("Play new Theme Song");
        }
        }


        if (SaveManager.instance != null) {
            if (SaveManager.instance.state.volume != sounds[soundIndex].source.volume)
            {
                sounds[soundIndex].source.volume = SaveManager.instance.state.volume;
            }
        }
    }
}
