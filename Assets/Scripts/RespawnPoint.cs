using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    // Handle entering a trigger with the tag "Respawn" indicating a checkpoint
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Respawn") {
            Vector3 respawn = other.gameObject.transform.position;
            if (GameManager.instance.respawnPoint != respawn) {
                GameManager.instance.respawnPoint = respawn;
            }
        }
    }
}
