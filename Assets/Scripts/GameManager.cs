using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    // Instance variable
    public static GameManager instance;

    // The play button for the menu canvas
    public Button play;
    // The text that shows the result (victory or loss)
    public Text result;
    // The text that shows the amount of lives left
    public Text lives;

    // The game object for the player
    public GameObject player;
    // The game object for the main camera
    public GameObject mainCamera;

    // The last respawn point for the player
    public Vector3 respawnPoint;

    // The audio source for sound effects
    public AudioSource audioSource;
    // The player death sound effect
    public AudioClip playerDeath;
    // The enemy death sound effect
    public AudioClip enemyDeath;

    // How low you can fall before dying
    public float fallThreshold;
    // The respawn time for the player
    public int respawnTime;
    // The amount of lives left for the player
    public int livesLeft;

    // Boolean for whether the player is dead
    private bool isDead = false;
    // Offset of the camera and the player
    private Vector3 offset;

    // Use this for initialization
    void Start() {

        // initialize the instance variable
        instance = this;

        // If the play button is not null delegate the button event
        if (play != null) {
            play.onClick.AddListener(delegate { SwitchScene("Main"); });
        }

        // If the player is not null, set the camera offset
        if (player != null) {
            offset = mainCamera.transform.position - player.transform.position;
        }

        // If the scene is the main level then initialize the game
        if (SceneManager.GetActiveScene().name == "Main") {
            InitGame();
        }
    }

    // Update is called once per frame
    void Update() {
        if (SceneManager.GetActiveScene().name == "Main") {
            OnPlayerFall();
        }
    }

    void LateUpdate() {
        if (player != null) {
            mainCamera.transform.position = player.transform.position + offset;
        }
    }

    // Set the lives left in the UI
    void InitGame() {
        if (lives != null) {
            UpdateLives(livesLeft);
        }
    }

    // Handle the player falling
    void OnPlayerFall() {
        if (!isDead) {
            if (player.transform.position.y < fallThreshold) {
                audioSource.clip = playerDeath;
                audioSource.Play();

                isDead = true;

                livesLeft--;
                UpdateLives(livesLeft);

                if (livesLeft == 0) {
                    SwitchScene("Defeat");
                    return;
                }

                player.SetActive(false);

                StartCoroutine(Respawn());
            }
        }
    }

    // Update the text UI for lives left
    void UpdateLives(int livesLeft) {
        if (lives != null) {
            lives.text = lives.text.Split(':')[0] + ": " + livesLeft;
        }
    }

    // Switch scene function for easy management
    public void SwitchScene(string scene) {
        SceneManager.LoadScene(scene);
    }

    // Coroutine for timed respawns
    private IEnumerator Respawn() {
        yield return new WaitForSeconds(respawnTime);
        player.transform.position = respawnPoint;
        player.SetActive(true);
        isDead = false;
    }
}
