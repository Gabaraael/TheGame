using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    
    Animator animator;

    public float horizontal;
    
    public float vertical;

    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }   
    
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        
    }
}
