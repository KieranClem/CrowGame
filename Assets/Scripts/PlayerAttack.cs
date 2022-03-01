using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public AnimationClip animationClip;
    private PlayerMovement player;
    private float animationtime;
    float timer = 0;
    public CameraShake cameraShake;
    
    // Start is called before the first frame update
    void Start()
    {
        animationtime = animationClip.length;
        player = transform.parent.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.activeSelf)
        {
            timer += Time.deltaTime;

            if(timer >= animationtime)
            {
                timer = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            player.AddScore(other.GetComponent<EnemyAI>().ScoreGiven);
            other.GetComponent<EnemyAI>().enemyTracker.updateEnemiesOnScreen(other.GetComponent<EnemyAI>());
            StartCoroutine(cameraShake.Shake(0.1f, .1f));
            Destroy(other.gameObject);
        }
    }

    public void Attacking()
    {
        animator.SetTrigger("Attack");
    }

}
