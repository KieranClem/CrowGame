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
    public float maxDashTime;
    private float dashTime = 0;
    private bool isDashing = false;

    public Text scoreDisplay;
    private int score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        AttackObject.gameObject.SetActive(false);

        for(int i = 0; i < HeartHolder.transform.childCount; i++)
        {
            Hearts[i] = HeartHolder.transform.GetChild(i).gameObject;
        }

        scoreDisplay.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Get player's inputs
        Movement.x = Input.GetAxisRaw("Horizontal");
        Movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        //Look at mouse
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition - transform.position);

        difference.Normalize();

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if(Input.GetKeyDown(KeyCode.E))
        {
            isDashing = true;
            dashTime = maxDashTime;
        }

        if(isDashing)
        {
            rb.AddForce(Movement * dashSpeed, ForceMode.VelocityChange);
            Debug.Log("sep");

            if (maxDashTime <= dashTime)
            {
                dashTime -= Time.deltaTime;
                Debug.Log("time lower");
            }
            else
            {
                isDashing = false;
                rb.velocity = Vector3.zero;
                Debug.Log("time stop");
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
        if(other.tag == "Enemy")
        {
            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if(PlayerHealth <= 0)
        {
            //Game over
        }
        else
        {
            Hearts[PlayerHealth - 1].gameObject.SetActive(false);
            PlayerHealth -= 1;
        }
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        scoreDisplay.text = score.ToString();
    }
}
