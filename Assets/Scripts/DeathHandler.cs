using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour {

    // Audio source for playing sound effects
    private AudioSource audioSource;

    // Destroy the enemy and play the enemy death sound
    private void OnCollisionEnter2D(Collision2D other) {
        // Find the audio source
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        
        /*
         * If the enemy and the player collided then the player will die
         * If the players ability collided with the enemy then the enemy will die
         */
        if (other.gameObject.tag == "Enemy") {
            if (gameObject.tag == "Player") {
                if (!(other.collider is CircleCollider2D)) {
                    audioSource.clip = GameManager.instance.playerDeath;
                    audioSource.Play();

                    // Set isDead to true so that the respawn process can start
                    GameManager.instance.isDead = true;
                    // Decrement the player lives by one
                    GameManager.instance.livesLeft -= 1;
                    // Update the UI to show the new lives
                    GameManager.instance.UpdateLives(GameManager.instance.livesLeft);

                    EnemyPawn enemyPawn = other.gameObject.GetComponent<EnemyPawn>();
                    
                    // Set the enemy state to recover since the player is dead
                    enemyPawn.SetState(EnemyPawn.State.Recover);

                    if (enemyPawn.ramming) {
                        // Set ramming to false if its true
                        other.gameObject.GetComponent<EnemyPawn>().ramming = false;
                    }
                }
            } else {
                audioSource.clip = GameManager.instance.enemyDeath;
                audioSource.Play();

                // Destroy the enemy game object
                Destroy(other.gameObject);
                GameManager.instance.currentEnemies -= 1;

                // Destroy the players fireball too
                if (gameObject.tag == "Weapon") {
                    Destroy(gameObject);
                }
            }
        }
    }
}
