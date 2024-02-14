using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 100f;
    private int score = 0;
    public int health = 5;
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public TMP_Text winLoseText;
    public Image winLoseBG;

    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (health <= 0)
        {
            LoseCon();
            // Debug.Log("Game Over!");
            StartCoroutine(LoadScene(3));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
        movement = movement.normalized * speed * Time.deltaTime;

        rb.AddForce(movement, ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score++;
            SetScoreText();
            // Debug.Log($"Score: {score}");
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Trap"))
        {
            health--;
            SetHealthText();
            // Debug.Log($"Health: {health}");
        }
        if (other.CompareTag("Goal"))
        {
            GoalReached();
            // Debug.Log("You win!");
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {this.score}";
    }

    void SetHealthText()
    {
        healthText.text = $"Health: {this.health}";
    }

    void GoalReached()
    {
        winLoseBG.color = Color.green;
        winLoseText.text = "You Win!";
        winLoseText.color = Color.black;
        winLoseBG.gameObject.SetActive(true);
    }

    void LoseCon()
    {
        winLoseBG.color = Color.red;
        winLoseText.text = "Game Over!";
        winLoseText.color = Color.white;
        winLoseBG.gameObject.SetActive(true);
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
