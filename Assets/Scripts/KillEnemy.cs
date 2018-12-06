using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {

    // Audio source for playing sound effects
    public AudioSource audioSource;

    // Destroy the enemy and play the enemy death sound
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy") {
            audioSource.clip = GameManager.instance.enemyDeath;
            audioSource.Play();
            other.gameObject.SetActive(false);
        }
    }
}
