using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float speed = 20f;
    public float heightScale = 3f;  //Used to prevent projectile from spawning in the ground
    public Image healthBar;
    public float maxHealth = 5;
    public float shootSpeed = 0.5f;

    private float health;
    private GameObject projectile;
    private Rigidbody projectileRigidBody;
    private float elapsedTime = 0f;

    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        Vector3 targetPos;

        if (Input.GetMouseButtonDown(0))
        {
            if (elapsedTime >= shootSpeed)
            {
                elapsedTime = 0f;

                targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos -= this.transform.position;   //Match the targetPos to the world position (Scales the targetPos to the object location)
                targetPos.y -= heightScale;     //Subtract heightScale to match spawn position
                                                //print(targetPos);

                projectile = Instantiate(projectilePrefab);
                projectileRigidBody = projectile.GetComponent<Rigidbody>();

                Vector3 vec = transform.position;   //Added heightScale to y to prevent projectile from spawning in the ground
                vec.y += heightScale;
                projectile.transform.position = vec;

                projectileRigidBody.velocity = targetPos.normalized * speed;   //normalized target position to make projectiles travel at similar speeds
            }
        }

        //if (projectile != null)
        //    if (projectile.transform.position.y > 50f)
        //        Destroy(projectile);              //Delete projectiles within projectile script
    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject go = collider.gameObject;
        if (go.tag == "Enemy" || go.tag == "EnemyProjectile")
        {
            Destroy(go);

            health -= 1;
            healthBar.fillAmount = health / maxHealth;

            if (health == 0)
                SceneSwitcher.ChangeScene();
        }
    }
}
