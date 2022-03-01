using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject Player;
    public float moveSpeed;
    public int ScoreGiven;

    public bool isFake = false;

    [HideInInspector] public EnemySpawner enemyTracker;

    public SpriteRenderer CrowSpirte;
    public Transform spriteTransform;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if(isFake)
        {
            CrowSpirte.color = new Color(1f, 1f, 1f, 0.5f);
            this.tag = "FakeEnemy";
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player.transform);
        CrowSpirte.transform.rotation = Quaternion.Euler(this.transform.rotation.x, 0f, this.transform.rotation.x);

    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
