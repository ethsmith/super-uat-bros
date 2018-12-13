using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller {

    // The pawn of the enemy
    public EnemyPawn enemyPawn;

    // Use this for initialization
    void Start () {
        // Cast the controller pawn to enemy pawn for access to special variables
        enemyPawn = (EnemyPawn)pawn;
    }
	
	// Update is called once per frame
	void Update () {
        // Check the state of movement each frame
        Move();
	}

    public override void Move() {

        // Handle enemy AI states
        switch (enemyPawn.GetState()) {
            // If the state is Recover, then the enemy will rise
            case EnemyPawn.State.Recover:
                if (enemyPawn.transform.position.y < enemyPawn.maxSmashHeight && !enemyPawn.smashing) {
                    enemyPawn.transform.position += new Vector3(0f, pawn.speed * Time.deltaTime, 0f);
                // If the max smash height is reached, then the state is switched to smash
                } else {
                    enemyPawn.SetState(EnemyPawn.State.Smash);
                }
                break;

            // If the state is Smash, then the enemy will drop super quickly and possibly kill the player
            case EnemyPawn.State.Smash:
                if (enemyPawn.transform.position.y > -1.42f) {
                    // Set smashing to true
                    enemyPawn.smashing = true;
                    // Lower the enemy object each frame
                    enemyPawn.transform.position -= new Vector3(0f, pawn.speed * Time.deltaTime * 15, 0f);
                // If the enemy pawn has reached the ground then switch to ram or recover
                } else {
                    // Set smashing to false to allow the enemy to recover
                    enemyPawn.smashing = false;
                    if (enemyPawn.ramming == true) {
                        enemyPawn.SetState(EnemyPawn.State.Ram);
                    } else {
                        enemyPawn.SetState(EnemyPawn.State.Recover);
                    }
                }
                break;
            
            // If the state is Ram, then the enemy will move toward the player to try to kill them
            case EnemyPawn.State.Ram:
                if (!GameManager.instance.isDead) {
                    float step = enemyPawn.speed * Time.deltaTime;
                    enemyPawn.transform.position = Vector2.MoveTowards(enemyPawn.transform.position, enemyPawn.target.position, step * 2);
                } else {
                    enemyPawn.SetState(EnemyPawn.State.Recover);
                }
                break;
        }
    }
}
