using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    // Handle entering a trigger with the tag "Respawn" indicating a checkpoint
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Respawn") {
            // The position of the checkpoint sign
            Vector3 respawn = other.gameObject.transform.position;
            
            // If the respawn point is already the position of the checkpoint, don't change it again 
            if (GameManager.instance.respawnPoint != respawn) {
                GameManager.instance.respawnPoint = respawn;
            }

            // If it is the third checkpoint then we go the victory screen
            if (other.gameObject.name == "Checkpoint 3") {
                GameManager.instance.SwitchScene("Victory");
            }
        }
    }
}
