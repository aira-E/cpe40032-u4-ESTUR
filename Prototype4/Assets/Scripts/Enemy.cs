using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
     // AddForce towards in the direction between the Player and the Enemy
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        // The enemy is now rolling towards the player,
        enemyRb.AddForce (lookDirection * speed);
        // Destroy the enemy if they got out of the bounds
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
