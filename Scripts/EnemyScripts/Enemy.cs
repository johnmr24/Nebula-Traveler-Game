using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 20f;
    public float projectileSpeed = 20f;
    public float chanceToChangeDirectionsX = 0.002f;
    public float chanceToChangeDirectionsY = 0.002f;
    public GameObject projectilePrefab;
    public float shootSpeed = 1f;
    public bool isMovable = true;

    private Rigidbody projectileRigidBody;
    private float elapsedTime = 0f;
    private int Xdir = 1;
    private int Ydir = 1;
    private GameObject hero;
    private bool awake = false;

    
    void Awake()
    {
        hero = GameObject.Find("robotSphere");
    }

    // Update is called once per frame
    void Update()
    {
        if (awake == false && this.transform.position.x - 75 < hero.transform.position.x)
        {
            awake = true;
            Shoot();
            if (isMovable)
                Move();
        }
        if (isMovable)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < 4f)
                MoveDown();
            else if (elapsedTime < 14f)
            {
                if (Random.value < chanceToChangeDirectionsX)
                    Xdir *= -1;
                if (Random.value < chanceToChangeDirectionsY)
                    Ydir *= -1;
                Move();
            }
            else if (elapsedTime > 14f)
            {
                if (transform.position.y <= 55f)
                    MoveUp();
                else
                    Destroy(gameObject);
            }

            if (Xdir == 1)
                transform.rotation = Quaternion.Euler(0, 90, -25);
            else
                transform.rotation = Quaternion.Euler(0, -90, -25);
        }
    }

    void MoveDown()
    {
        Vector3 pos = transform.position;
        pos.y -= speed * Time.deltaTime;
        transform.position = pos;
    }

    void MoveUp()
    {
        Vector3 pos = transform.position;
        pos.y += speed * 2 * Time.deltaTime;
        transform.position = pos;
    }

    void Move()
    {
        Rigidbody rig = this.GetComponent<Rigidbody>();
        Vector3 pos = transform.position;

        if (transform.position.y > 45f)
            pos.y -= speed * 2 * Time.deltaTime;
        else if (transform.position.y < 25f)
            pos.y += speed * 2 * Time.deltaTime;
        else
            pos.y += Ydir * speed * 2 * Time.deltaTime;

        if (transform.position.x < hero.transform.position.x - 10)
            pos.x += speed * 2 * Time.deltaTime;
        else if (transform.position.x > hero.transform.position.x + 30)
            pos.x -= speed * 2 * Time.deltaTime;
        else
            pos.x += Xdir * speed * 2 * Time.deltaTime;

        transform.position = pos;
    }

    void Shoot()
    {
        Vector3 targetPos = hero.transform.position;
        Vector3 currentPos = transform.position;

        GameObject projectile = Instantiate(projectilePrefab);
        projectileRigidBody = projectile.GetComponent<Rigidbody>();

        projectile.transform.position = transform.position;
        projectileRigidBody.velocity = ((targetPos-currentPos).normalized * projectileSpeed);

        Invoke("Shoot", shootSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        
        if (go.tag == "HeroProjectile")
        {
            Destroy(go);
            Destroy(gameObject);
            Main.S.AddScore(1);
        }
    }
}
