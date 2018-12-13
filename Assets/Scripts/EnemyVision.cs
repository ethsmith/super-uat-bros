using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour {

    /*
     * If the player is within the enemies vision collider, 
     * the enemy has a chance of trying to ram the player
     */
    private void OnTriggerEnter2D(Collider2D collision) {
        EnemyPawn pawn = GetComponent<EnemyPawn>();

        if (collision.gameObject.tag == "Player") {
            bool ram = (Random.Range(0, 2) < 1);
            if (ram) {
                pawn.ramming = true;
                pawn.target = collision.gameObject.transform;
            }
        }
    }

    // Once the player is out of eye sight, go back to the recover state
    private void OnTriggerExit2D(Collider2D collision) {
        EnemyPawn pawn = GetComponent<EnemyPawn>();

        if (collision.gameObject.tag == "Player") {
            pawn.ramming = false;
            pawn.SetState(EnemyPawn.State.Recover);
        }
    }
}
