using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public AnimationClip animationClip;
    private float animationtime;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        animationtime = animationClip.length;
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
            Destroy(other.gameObject);
        }
    }

    public void Attacking()
    {
        animator.SetTrigger("Attack");
    }

}
