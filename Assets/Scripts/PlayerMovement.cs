using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    Vector3 Movement;
    public Rigidbody rb;
    public float MovementSpeed = 10f;

    public PlayerAttack AttackObject;

    public int PlayerHealth = 3;

    public GameObject HeartHolder;
    private GameObject[] Hearts = new GameObject[3];

    public float dashSpeed = 10;

    public Text scoreDisplay;
    public Text finalScoreDisplay;
    public Button restartButton;
    public Button closGameButton;
    public GameObject hideScreen;
    private int score = 0;

    private bool canAttack = true;
    private bool canBeHit = true;

    public CameraShake cameraShake;

    // Start is called before the first frame update
    void Start()
    {
        AttackObject.gameObject.SetActive(false);

        for(int i = 0; i < HeartHolder.transform.childCount; i++)
        {
            Hearts[i] = HeartHolder.transform.GetChild(i).gameObject;
        }

        scoreDisplay.text = score.ToString();
        finalScoreDisplay.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        closGameButton.gameObject.SetActive(false);
        hideScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Get player's inputs
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
        {
            Attack();
        }

        //Look at mouse

        Vector3 mousePosition = Input.mousePosition;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;

        if (Input.GetKeyDown(KeyCode.E))
        {

            bool checkForWall = CheckForWall(transform.position += Movement * dashSpeed);
            if(checkForWall)
            {
                PlayerDeath();
            }
            else
            {
                transform.position += Movement * dashSpeed;
            }
            
        }
    }

    private void FixedUpdate()
    {
        //Move player
        rb.MovePosition(rb.position + Movement * MovementSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        AttackObject.gameObject.SetActive(true);
        AttackObject.Attacking();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && canBeHit)
        {
            CheckHealth();
        }

        if(other.tag == "FakeEnemy")
        {
            CancelInvoke();
            Invoke("stunned", 0);
            other.GetComponent<EnemyAI>().enemyTracker.updateEnemiesOnScreen(other.GetComponent<EnemyAI>());
            Destroy(other.gameObject);
        }
    }

    IEnumerator stunned()
    {
        canAttack = false;

        yield return new WaitForSeconds(3);

        canAttack = true;
    }

    IEnumerator invincable()
    {
        canBeHit = false;

        yield return new WaitForSeconds(3);

        canBeHit = true;
    }

    private void CheckHealth()
    {
        PlayerHealth -= 1;
        if (PlayerHealth <= 0)
        {
            //Game over
            PlayerDeath();
        }
        else
        {
            Hearts[PlayerHealth].gameObject.SetActive(false);
            StartCoroutine(cameraShake.Shake(0.1f, .1f));
        }
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        scoreDisplay.text = score.ToString();
    }

    private bool CheckForWall(Vector3 TeleportPosition)
    {

        Collider[] hitCollider = Physics.OverlapSphere(TeleportPosition, 1f);
        if(hitCollider.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void PlayerDeath()
    {
        scoreDisplay.gameObject.SetActive(false);
        finalScoreDisplay.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        closGameButton.gameObject.SetActive(true);
        hideScreen.SetActive(true);

        finalScoreDisplay.text = "Final score: " + score.ToString();
        Time.timeScale = 0;
    }
}
