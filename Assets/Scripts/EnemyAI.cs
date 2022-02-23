using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject Player;
    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
