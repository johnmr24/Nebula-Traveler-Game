using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroMovement : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    private Rigidbody playerRigidBody;
    bool isTouchingGround = false;
    public float distanceToCheck = 5f;

    private bool alreadyTouching = false;
    private bool WaitStart = false;
    

    void Start()
    {
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2);
        WaitStart = true;
        this.GetComponent<Rigidbody>().maxAngularVelocity = 7.5f;
    }

    void FixedUpdate()
    {
        while (!WaitStart)
        {
            return;
        }
        playerRigidBody = GetComponent<Rigidbody>();
        float xAxis = Input.GetAxis("Horizontal");

        //this.GetComponent<Rigidbody>().velocity = new Vector3(xAxis * speed, playerRigidBody.velocity.y, 0);
        this.GetComponent<Rigidbody>().AddForce(new Vector3(60 * xAxis * speed, 0));

        //Vector3 pos = transform.position;
        //pos.x += xAxis * speed * Time.deltaTime;

        //transform.position = pos;
        if (xAxis < 0)
            if (transform.rotation.y != -90f)
                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, -90f, transform.rotation.eulerAngles.z);
        if (xAxis > 0)
            if (transform.rotation.y != 90f)
                transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, 90f, transform.rotation.eulerAngles.z);
    }

    void Update()
    {
        while (!WaitStart)
        {
            return;
        }
        if (Input.GetButtonDown("Vertical") && isTouchingGround)
        {
            playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, jumpSpeed);
            isTouchingGround = false;
            alreadyTouching = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" && other.transform.position.y + (other.GetComponent<Collider>().bounds.size.y)/3 <= transform.position.y)
        {
            isTouchingGround = true;
            alreadyTouching = true;
        }
        else
            if (!alreadyTouching)
                isTouchingGround = false;
    }
}
