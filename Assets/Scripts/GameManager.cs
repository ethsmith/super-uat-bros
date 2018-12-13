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
    // The enemy game object
    public GameObject enemy;
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

    // The list of enemy spawns
    public List<GameObject> spawns;
    // The locations of the enemies
    public List<Vector3> enemyLocations;

    // How low you can fall before dying
    public float fallThreshold;
    // The respawn time for the player
    public int respawnTime;
    // The amount of lives left for the player
    public int livesLeft;
    // The amount of enemies that can be alive at one time
    public int maxEnemies;
    // The amount of enemies currently alive
    public int currentEnemies = 0;
    // Boolean for whether the player is dead
    public bool isDead = false;

    // Offset of the camera and the player
    private Vector3 offset;

    // Use this for initialization
    void Start() {
        // initialize the instance variable
        instance = this;

        // If the play button is not null delegate the button event
        if (play != null) {
            play.onClick.AddListener(delegate { SwitchScene("Game"); });
        }

        // If the player is not null, set the camera offset
        if (player != null) {
            offset = mainCamera.transform.position - player.transform.position;
        }

        // If the scene is the main level then initialize the game
        if (SceneManager.GetActiveScene().name == "Game") {
            InitGame();
        }
    }

    // Update is called once per frame
    void Update() {
        /*
         * If the player is dead, check if the player has zero lives, if so, go to the defeat screen
         * If not, start the respawn process
         */
        if (isDead) {
            if (livesLeft == 0) {
                SwitchScene("Defeat");
                return;
            }

            StartCoroutine(Respawn());
        }

        // Check if the player has fallen
        if (SceneManager.GetActiveScene().name == "Game") {
            OnPlayerFall();
        }

        /*
         * Spawn enemies when the game starts
         * Right now only the max enemies can spawn and more won't respawn after they die
         */
        if (currentEnemies < maxEnemies) {
            SpawnEnemies();
        }
    }

    // Move the camera with the player
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

        if (respawnPoint == new Vector3().normalized) {
            respawnPoint = player.transform.position;
        }
    }

    // Spawn an enemy object at a random spawn location
    void SpawnEnemies() {
        int index = Random.Range(0, 3);
        Transform enemyTf = spawns[index].transform;

        // If an enemy is already at that location, do not spawn another
        if (enemyLocations.Contains(enemyTf.position)) {
            return;
        }

        Instantiate(enemy, enemyTf.position, Quaternion.identity);
        // Increase current enemies
        currentEnemies++;
        // Add the enemy location to the location list
        enemyLocations.Add(enemyTf.position);
    }

    // Handle the player falling
    void OnPlayerFall() {
        // Check if the player is not dead first
        if (!isDead) {
            // If the player is below the fall threshold, kill the player
            if (player.transform.position.y < fallThreshold) {
                audioSource.clip = playerDeath;
                audioSource.Play();

                isDead = true;

                livesLeft--;
                UpdateLives(livesLeft);
            }
        }
    }

    // Update the text UI for lives left
    public void UpdateLives(int livesLeft) {
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
        player.SetActive(false);

        yield return new WaitForSeconds(respawnTime);

        player.transform.position = respawnPoint;
        isDead = false;
        player.SetActive(true);
    }
}
