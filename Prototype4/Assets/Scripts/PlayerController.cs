using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrenght = 15.0f;
    public GameObject powerupIndicator;
 //   public bool gameOver;
 //   public GameObject GameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }
    // Update is called once per frame
    void Update()
    {
        // Add forward force to the player
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        
   //  if (playerRb.transform.position.y < -1)
   //     {
    //        gameOver = true;
    //        GameOverPanel.SetActive(true);
    //    }
  
    }



    private void OnTriggerEnter(Collider other)
    {
        // If the player collided with the power-up...
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            // ... the power-up will be destroyed,...
            Destroy(other.gameObject);
            // ... the PowerupCountdownRoutine() func will be executed
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true); 
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        // Countdown Routine for powerup
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(true);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Applying extra knockback with powerup
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            //  Console states that we've collided with our enemy + power-up (if any)
            Debug.Log("Collided with " + collision.gameObject.name
                + "with powerup set to " + hasPowerup);

            // On collision, the enemy will fly off on the opposite direction
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrenght, ForceMode.Impulse);
        }



    }
}
