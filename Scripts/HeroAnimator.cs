using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimator : MonoBehaviour
{
    private IEnumerator coroutine; 

    private bool isTouchingGround = true;
    private bool rolling = false;       //used to track when the ball is supposed to roll

    [Header("Set In Inspecter")]
    Animator anim;
    float speed = 2f;   //speed of animations

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.speed = speed;
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");

        //walking animations
        if (xAxis != 0 && isTouchingGround)
            anim.SetBool("Walk_Anim", true);
        else
            anim.SetBool("Walk_Anim", false);

        //rolling animation starts when the hero jumps
        if (Input.GetButtonDown("Vertical") && isTouchingGround)
        {
            isTouchingGround = false;
            anim.speed = 5;
            anim.SetBool("Roll_Anim", true);
            rolling = true;
        }

        if (isTouchingGround && rolling)
        {
            rolling = false;
            anim.speed = speed;

            coroutine = WaitStopRoll(1);
            StartCoroutine(coroutine);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" && other.transform.position.y < this.transform.position.y)
            isTouchingGround = true;
    }
    
    private IEnumerator WaitStopRoll(float waitTime)    //Stops the hero from rolling after few seconds if the hero is on the ground (problem if falling off ledge)
    {
        yield return new WaitForSeconds(waitTime);
        //print("Coroutine ended: " + Time.time + " seconds");
        if (isTouchingGround)
        {
            anim.SetBool("Roll_Anim", false);
        }
    }
}
