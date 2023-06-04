using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    Animator animator;
    private bool isAnimationPlaying = false;
    private float animationTime = 0f;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fire");
        }
    }
}
