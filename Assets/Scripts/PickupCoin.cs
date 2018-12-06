using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : MonoBehaviour {

    // Audio source for playing effects
    public AudioSource audioSource;
    // Audio clip for item pickup
    public AudioClip coin;

    // Pickup coin and play sound effect when colliding with coin
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Coin") {
            audioSource.clip = coin;
            audioSource.Play();
            other.gameObject.SetActive(false);
        }
    }
}
