using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour {

    // The speed of the ability movement
    public float speed = 0.2f;

    // Transform object for moving the sprite
    private Transform tf;
    // The player object
    private GameObject player;
    // The sprite renderer of the player
    private SpriteRenderer sr;
    // The direction the player is facing
    private bool direction; // If the x is flipped from the original it will return true

	// Use this for initialization
	void Start () {
        // Set tf to the transform component
        tf = GetComponent<Transform>();
        // Set the player object to the object referenced in Game Manager
        player = GameManager.instance.player;
        // Set the sprite renderer to the player sprite renderer
        sr = player.GetComponent<SpriteRenderer>();
        // Set the direction to left or right at creation
        direction = sr.flipX;
	}
	
	// Update is called once per frame
	void Update () {
        // If the direction is left (true)
        if (direction) {
            // Move the ability sprite at a constant speed left
            tf.Translate(new Vector3(-speed, 0, 0));
        } else {
            // Move the ability sprite at a constant speed right
            tf.Translate(new Vector3(speed, 0, 0));
        }
	}
}
