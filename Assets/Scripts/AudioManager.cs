using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {

    // Audio source for playing sound effects
    public AudioSource audioSource;
    // Audio clip for intro music
    public AudioClip intro;
    // Audio clip for playing level music
    public AudioClip levelMusic;
    // Volume of the music
    public float volume = 0.1f;

	// Use this for initialization
	void Start () {
        // Set source volume
        audioSource.volume = volume;

        // Change music based on scene
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Menu") {
            audioSource.clip = intro;
            audioSource.Play();
        } else if (currentScene.name == "Game") {
            audioSource.clip = levelMusic;
            audioSource.Play();
        }
	}
}
