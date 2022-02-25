using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject Player;
    public float moveSpeed;
    public int ScoreGiven;

    public bool isFake = false;
    public Material fakeMat;

    [HideInInspector] public EnemySpawner enemyTracker;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if(isFake)
        {
            GetComponent<MeshRenderer>().material = fakeMat;
            this.tag = "FakeEnemy";
        }
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
