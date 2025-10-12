using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    [Serializable]
    public enum EnvSoundType
    {
        eEnvSound_Temple = 0,
        eEnvSound_Outside,
        eEnvSound_SemiOutside,
        eEnvSound_Scifi,
        eEnvSound_Num
    }

    [Serializable]
    public class EnvSoundPair
    {
        public EnvSoundType type;
        public string clipName;
    }

    public List<Sound> sounds = new List<Sound>(); // List of sounds
    private Dictionary<string, AudioSource> activeSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    public List<EnvSoundPair> envSounds = new List<EnvSoundPair>(); // List of sounds
    private Dictionary<EnvSoundType, AudioClip> envDictionary = new Dictionary<EnvSoundType, AudioClip>();

    private AudioSource oneShotSource; // For one-shot sounds
    private AudioSource environmentSource; // For environment sounds

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create a single AudioSource for one-shot sounds
        oneShotSource = gameObject.AddComponent<AudioSource>();

        // Create environment audio source
        environmentSource = gameObject.AddComponent<AudioSource>();

        // Populate dictionary for quick access
        foreach (Sound sound in sounds)
        {
            if (!soundDictionary.ContainsKey(sound.name))
            {
                soundDictionary.Add(sound.name, sound.clip);
            }
            else
            {
                Debug.LogWarning($"Duplicate sound name detected: {sound.name}");
            }
        }

        foreach (EnvSoundPair pair in envSounds)
        {
            envDictionary.TryAdd(pair.type, soundDictionary[pair.clipName]);
        }
    }

    /// <summary>
    /// Play a one-shot sound effect at a specified volume (cannot be stopped)
    /// </summary>
    public void PlayOneShot(string soundName, float volume = 1.0f)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            oneShotSource.PlayOneShot(clip, Mathf.Clamp01(volume));
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
        }
    }

    /// <summary>
    /// Play environment sound that can keep single environment sound at a time
    /// </summary>
    public void PlayEnvironmentSound(EnvSoundType type, float volume = 1.0f)
    {
        if (envDictionary.TryGetValue(type, out AudioClip clip)){
            StopEnvironmentSound(); // stop current one
            environmentSource.clip = clip;
            environmentSource.volume = Mathf.Clamp01(volume);
            environmentSource.loop = true;
            environmentSource.Play();
            Debug.LogWarning($"Playing env {type}");
        }
        else
            Debug.LogWarning($"Not Playing env {type}");

    }

    /// <summary>
    /// Stop environment sound
    /// </summary>
    public void StopEnvironmentSound()
    {
        if (environmentSource.isPlaying)
        {
            environmentSource.Stop();
        }
    }


    /// <summary>
    /// Play a sound at a specified volume that can be stopped later. Option to loop.
    /// </summary>
    public void PlaySound(string soundName, float volume = 1.0f, bool loop = false)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            if (!activeSources.ContainsKey(soundName))
            {
                AudioSource newSource = gameObject.AddComponent<AudioSource>();
                newSource.clip = clip;
                newSource.volume = Mathf.Clamp01(volume);
                newSource.loop = loop;
                newSource.Play();

                activeSources.Add(soundName, newSource);

                if (!loop)
                {
                    StartCoroutine(WaitForSoundToEnd(soundName, newSource));
                }
            }
            else
            {
                // If already playing and should loop, do nothing
                if (loop && activeSources[soundName].isPlaying)
                    return;

                // If restarting non-looping sound, stop and restart it
                activeSources[soundName].Stop();
                activeSources[soundName].volume = Mathf.Clamp01(volume);
                activeSources[soundName].loop = loop;
                activeSources[soundName].Play();

                if (!loop)
                {
                    StartCoroutine(WaitForSoundToEnd(soundName, activeSources[soundName]));
                }
            }
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
        }
    }

    /// <summary>
    /// Stop a playing sound
    /// </summary>
    public void StopSound(string soundName)
    {
        if (activeSources.TryGetValue(soundName, out AudioSource source))
        {
            source.Stop();
            Destroy(source); // Remove AudioSource after stopping
            activeSources.Remove(soundName);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' is not playing or already stopped!");
        }
    }
    public void PauseSound(string soundName)
    {
        if (activeSources.TryGetValue(soundName, out AudioSource source))
        {
            source.Pause();
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' is not playing!");
        }
    }

    public void ResumeSound(string soundName)
    {
        if (activeSources.TryGetValue(soundName, out AudioSource source))
        {
            source.UnPause();
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' is not paused or doesn't exist!");
        }
    }
    private IEnumerator WaitForSoundToEnd(string soundName, AudioSource source)
    {
        yield return new WaitUntil(() =>
        {
            if (source) return !source.isPlaying;
            else return true;
        });

        if (activeSources.ContainsKey(soundName))
        {
            if (source) source.Play(); // Restart the sound
        }
    }

    public void StopAllActiveSounds()
    {
        string[] keys = new string[activeSources.Keys.Count];
        activeSources.Keys.CopyTo(keys, 0);
        foreach (string key in keys)
        {
            StopSound(key);
        }
    }
}
