using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpawnLocationHolder;
    private List<Transform> spawnLocation = new List<Transform>();
    

    public int NumberToSpawn = 2;
    public int MaxNumPoss = 10;

    public GameObject Enemy;

    private List<EnemyAI> enemies = new List<EnemyAI>();
    private List<EnemyAI> fakeEnemies = new List<EnemyAI>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SpawnLocationHolder.transform.childCount; i++)
        {
            spawnLocation.Add(SpawnLocationHolder.transform.GetChild(i));
        }

        SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        List<Transform> tempList = spawnLocation;
        int enemyFake = Random.Range(0, NumberToSpawn);

        for (int i = 0; i < NumberToSpawn; i++)
        {
            int randomNumber = Random.Range(0, tempList.Count);
            Transform spawnPoint = tempList[randomNumber];
            tempList.RemoveAt(randomNumber);
            
            GameObject newEnemy = Instantiate(Enemy, spawnPoint.position, transform.rotation);

            
            newEnemy.GetComponent<EnemyAI>().enemyTracker = this;
            if(enemyFake == i)
            {
                newEnemy.GetComponent<EnemyAI>().isFake = true;
                fakeEnemies.Add(newEnemy.GetComponent<EnemyAI>());
            }
            else
            {
                enemies.Add(newEnemy.GetComponent<EnemyAI>());
            }
        }
    }

    public void updateEnemiesOnScreen(EnemyAI deadEnemy)
    {
        int enemyIndex = 0;
        if (deadEnemy.tag == "Enemy")
        {
            foreach (EnemyAI enemy in enemies)
            {
                if (deadEnemy == enemy)
                {
                    enemyIndex = enemies.IndexOf(enemy);

                }
            }
            enemies.RemoveAt(enemyIndex);
        }
        else
        {
            foreach (EnemyAI enemy in fakeEnemies)
            {
                if (deadEnemy == enemy)
                {
                    enemyIndex = fakeEnemies.IndexOf(enemy);

                }
            }
            fakeEnemies.RemoveAt(enemyIndex);
        }

        
        Debug.Log(enemies.Count + " " + fakeEnemies.Count);

        if (enemies.Count == 0)
        {
            if(fakeEnemies.Count > 0)
            {
                GameObject[] tempList = GameObject.FindGameObjectsWithTag("FakeEnemy");
                foreach(GameObject fake in tempList)
                {
                    Destroy(fake);
                }    
            }

            enemies.Clear();
            fakeEnemies.Clear();

            Debug.Log(enemies.Count + " " + fakeEnemies.Count);

            if (NumberToSpawn <= MaxNumPoss)
            {
                NumberToSpawn += 1;
                SpawnEnemy();
            }
        }
    }
}
