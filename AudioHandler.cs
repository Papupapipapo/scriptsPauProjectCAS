using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    AudioSource audioSourceMaster;
    [SerializeField]
    AudioSource? backupSound;
    [SerializeField]
    AudioClip[] audioList;
    AudioClip? lastAudioPlayed;
    
    float? lastTimePlayed;
    private void Awake()
    {
        audioSourceMaster = GetComponent<AudioSource>();
    }

    public void IdentifySound(string soundName)
    {
        switch (soundName)
        {
            case "Jump":
                loadClip(audioList[0]);
                break;
            case "Hurt":
                loadClip(audioList[1]);
                break;
            case "Hit":
                loadClip(audioList[2]);
                break;
            case "GemGet":
                loadClip(audioList[3]);
                break;
            case "EnableSpike":
                loadClip(audioList[4]);
                break;
            case "DisableSpike":
                loadClip(audioList[5]);
                break;
            default:
                Debug.LogError($"Audio clip by the name {soundName} doesn't exist");
                break;
        }
    }

    private void loadClip(AudioClip actualAudio)
    {
        if(audioSourceMaster.isPlaying && lastAudioPlayed == actualAudio){
            chainAudio(backupSound,actualAudio);
        }else{
            chainAudio(audioSourceMaster,actualAudio);
        }
    }

    private void chainAudio(AudioSource currentAudioSource,AudioClip actualAudio){
        currentAudioSource.clip = actualAudio;
        if(lastAudioPlayed == actualAudio && (Time.time - lastTimePlayed) < 1f){ //Makes it so if a sound plays again in a short time it will pitch it higher, mainly for the jump
            currentAudioSource.pitch = (Random.Range(1.05f, 1.1f));
            lastAudioPlayed = null;
            lastTimePlayed = null;
        }else{
            currentAudioSource.pitch = (Random.Range(1f, 1.05f));
            lastAudioPlayed = actualAudio;
            lastTimePlayed = Time.time;
        }
        
        currentAudioSource.Play();
    }
}
