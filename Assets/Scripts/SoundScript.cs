using UnityEngine;
using UnityEngine.Audio;

/*
    Script created by : George Chapple
    Edited by         : George Chapple, Jason Lodge
*/

public class SoundScript : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;
    public AudioMixerGroup audioMixerGroup;

    private void Awake() {
        AudioSource newAudioSource;
        if (TryGetComponent<AudioSource>(out newAudioSource)) {
            audioSource = newAudioSource;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
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
}
