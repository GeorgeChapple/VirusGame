using UnityEngine;
using UnityEngine.Audio;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
    Purpose           : To play an audio clip, called by other scripts.
*/

public class SoundScript : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    public AudioMixerGroup audioMixerGroup; // Audio Mixer to make sure game is not too loud - Jason

    private void Awake() {
        AudioSource newAudioSource;
        if (TryGetComponent<AudioSource>(out newAudioSource)) {
            audioSource = newAudioSource;
            audioSource.outputAudioMixerGroup = audioMixerGroup; // Set AudioSource mixer to ours - Jason
        } else {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    public void PlaySound(int index, float minPitch, float maxPitch) {
        
        audioSource.clip = sounds[index];
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }

    public void PlayRandomSound() {
        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        audioSource.pitch = 1;
        audioSource.Play();
    }
}
