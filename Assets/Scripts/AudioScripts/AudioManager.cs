using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
/*
THIS SCRIPT HAS EVERYTHING THAT DEALS WITH THE AUDIO
by Tyler McMillan
*/
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //list of sounds in game
    public static AudioManager instance; //instance of audio manager to make sure there is only one in game
    private bool _soundMuted = false; //Should you hear sounds (for sound toggle)
    private bool _musicMuted = true; //should you hear Background music (start on true so that you can toggle false, on awake)
     Toggle _muteToggle;
    //Initialization
    void Awake()
    {
        if (instance == null)//check only one instance
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject); //dont destroy if changing scenes

        foreach (Sound s in sounds) //add sounds to game (set each to their own audio source)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        _muteToggle = GameObject.FindGameObjectWithTag("MuteToggle").GetComponent<Toggle>();
        _soundMuted = _muteToggle.isOn;
        //ToggleBackgroundMusic(); //START BACKGROUND MUSIC
    }


    //FindObjectOfType<AudioManager>().Play("AUDIOCLIPNAME");
    public void Play(string name) //called from other scripts to play audio 
    {
        if (_soundMuted == false) //check if the sounds are muted (through sound toggle)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " was not found!");
                return;
            }
            s.source.Play();
        }
    }
    public void ToggleSound()
    {
        _soundMuted = !_soundMuted;
        //ToggleBackgroundMusic();
    }
    public void ToggleSoundButton(GameObject m_buttonPressed)
    {
        _soundMuted = !_soundMuted;
        if (_soundMuted == true)
        {
            m_buttonPressed.GetComponent<Image>().color = Color.red;
        }
        else
        {
            m_buttonPressed.GetComponent<Image>().color = Color.white;

        }
    }
    public void ToggleBackgroundMusic()
    {
        _musicMuted = !_musicMuted;
        if (_musicMuted == false)
        {
            Play("BackgroundMusic");
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == "backgroundMusic"); //get audio source of bg music to pause it!
            if (s == null)
            {
                Debug.LogWarning("Sound: backgroundMusic was not found!");
                return;
            }
            s.source.Pause();
        }
    }
    public bool GetSoundMuted()
    {
        return _soundMuted;
    }
}