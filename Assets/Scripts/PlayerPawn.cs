using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn {

    // How high the jump is
    public float jumpForce = 1.0f;
    // How many jumps left
    public int jumpsLeft;
    // Max amount of jumps before needing to touch the ground
    public int maxJumps = 2;
    // Boolean for whether or not the player is on the ground
    public bool isGrounded = true;

    // Game object for the players special power
    public GameObject ability;

	// Use this for initialization
	void Start () {
        jumpsLeft = maxJumps;
	}
}
