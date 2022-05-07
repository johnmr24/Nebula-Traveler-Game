using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public float projectileSpeed = 20f;
    public GameObject projectilePrefab;
    public float shootSpeed = 1f;
    public bool shootLeft = true;

    private GameObject hero;
    private Rigidbody projectileRigidBody;
    private bool awake = false;

    private void Awake()
    {
        hero = GameObject.Find("robotSphere");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!awake && hero.transform.position.x > this.transform.position.x - 75)
        {
            awake = true;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = transform.position;

        if (shootLeft)
            targetPos.x -= 1;
        else
            targetPos.x += 1;

        GameObject projectile = Instantiate(projectilePrefab);
        projectileRigidBody = projectile.GetComponent<Rigidbody>();

        projectile.transform.position = transform.position;
        projectileRigidBody.velocity = ((targetPos - currentPos).normalized * projectileSpeed);

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
