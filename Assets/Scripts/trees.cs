using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trees : MonoBehaviour
{

    Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
         if (collision.CompareTag("wolf"))
        {
            animator.SetTrigger("disturb");
            Debug.Log("wolf");
            
        }
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("disturb");
        }

    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    
}
