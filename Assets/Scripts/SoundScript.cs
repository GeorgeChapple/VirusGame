using UnityEngine;

/*
    Script created by : George Chapple
    Edited by         : George Chapple
*/

public class SoundScript : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource;

    private void Awake() {
        AudioSource newAudioSource;
        if (TryGetComponent<AudioSource>(out newAudioSource)) {
            audioSource = newAudioSource;
        } else {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlaySound(int index, float minPitch, float maxPitch) {
        audioSource.clip = sounds[index];
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
