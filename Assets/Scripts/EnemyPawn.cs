using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn {
    
    // States of the enemy AI
    public enum State {Smash, Ram, Recover};
    // The current state of the enemy AI
    public State currentState;
    // The max height the enemy will go in the air to smash
    public float maxSmashHeight;
    // Whether the enemy is currently smashing
    public bool smashing = false;
    // Whether the enemy is currently ramming
    public bool ramming = false;
    // The transform of the target to ram
    public Transform target;

    // Use this for initialization
    void Start () {
        // Set the inital state of the enemy to recover
        currentState = State.Recover;
	}

    // Return the state of the enemy AI
    public State GetState() {
        return currentState;
    }
    
    // Set the state of the enemy AI
    public void SetState(State state) {
        currentState = state;
    }
}
