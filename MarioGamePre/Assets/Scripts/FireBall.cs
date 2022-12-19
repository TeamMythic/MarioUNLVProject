using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Vector2 velocity;
    PlayerMovement myPlayerMovement;
    private void Awake()
    {
        myPlayerMovement = GameObject.FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
        myRigidbody = this.gameObject.GetComponent<Rigidbody2D>();
        if(myPlayerMovement.isFacingRight)
        {
            myRigidbody.velocity = new Vector2(10, -10);
        }
        else
        {
			myRigidbody.velocity = new Vector2(-10, -10);
		}
        velocity = myRigidbody.velocity;
        Destroy(this.gameObject, 10);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        myRigidbody.velocity = new Vector2(velocity.x, -velocity.y);
        if(other.gameObject.GetComponent<Enemy>() != null)
        {
            Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
        if (other.contacts[0].normal.x != 0 && other.gameObject.layer != 6)
        {
            Debug.Log(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        if (myRigidbody.velocity.y < velocity.y)
        {
            myRigidbody.velocity = velocity;
        }
        /*if (myRigidbody.velocity.x <= .1f || myRigidbody.velocity.x >= -0.1f)
        {
			Destroy(this.gameObject);
		}*/
    }
}
