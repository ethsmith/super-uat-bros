using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller {

    // Audio source for playing sound effects
    public AudioSource audioSource;
    // Audio clip for jump sound effect
    public AudioClip jump;

    // rigidbody variable for access to velocity
    private Rigidbody2D rb2d;
    // a variable for the sprite renderer
    private SpriteRenderer sr;
    // a variable for the player pawn
    private PlayerPawn playerPawn;

    // Use this for initialization
    void Start () {
        // initialize variables
        playerPawn = (PlayerPawn) pawn;
        rb2d = pawn.GetComponent<Rigidbody2D>();
        sr = pawn.GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update() {
        Move();
    }

    // Handle movement keys
    public override void Move() {
        // Move left on a
        if (Input.GetKey(KeyCode.A)) {
            sr.flipX = true;
            rb2d.velocity = new Vector2((Input.GetAxis("Horizontal") * playerPawn.speed), rb2d.velocity.y);

            pawn.GetComponent<Animator>().Play("Run");
        // Move right on d
        } else if (Input.GetKey(KeyCode.D)) {
            sr.flipX = false;
            rb2d.velocity = new Vector2((Input.GetAxis("Horizontal") * playerPawn.speed), rb2d.velocity.y);

            pawn.GetComponent<Animator>().Play("Run");
        // Jump on space
        } else if (Input.GetKeyDown(KeyCode.Space)) {
            if (playerPawn.jumpsLeft > 0) {
                playerPawn.jumpsLeft = playerPawn.jumpsLeft - 1;
                playerPawn.isGrounded = false;
         
                rb2d.velocity = new Vector2(rb2d.velocity.x, playerPawn.jumpForce);

                pawn.GetComponent<Animator>().Play("Jump");
                audioSource.clip = jump;
                audioSource.Play();
            }
        // Pressing left shift and e at the same time shoots a fireball to kill enemies
        } else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E)) {
            // Instatiate the ability prefab
            GameObject ability = Instantiate(playerPawn.ability) as GameObject;

            // Set the position of the fireball based on where the player is and looking
            if (sr.flipX) {
                ability.transform.position = pawn.transform.position + new Vector3(-1.0f, 0, 0);
            } else {
                ability.transform.position = pawn.transform.position + new Vector3(1.0f, 0, 0);
            }

            // If the ability has not already been destroyed destroy it after five seconds
            if (ability != null) {
                Destroy(ability, 5);
            }; 
        }

        // Check if the player is touching the ground
        if (!playerPawn.isGrounded) {
            RaycastHit2D[] hits = Physics2D.RaycastAll(playerPawn.transform.position, Vector2.down, 0.7f);

            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.gameObject.tag != "Player") {
                    playerPawn.jumpsLeft = playerPawn.maxJumps;
                    playerPawn.isGrounded = true;
                }
            }
        }

        // Switch back to the idle animation once the player has finished moving
        if (Input.GetKeyUp(KeyCode.A)) {
            pawn.GetComponent<Animator>().Play("Idle");
        } else if (Input.GetKeyUp(KeyCode.D)) {
            pawn.GetComponent<Animator>().Play("Idle");
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            pawn.GetComponent<Animator>().Play("Idle");
        }
    }
}
