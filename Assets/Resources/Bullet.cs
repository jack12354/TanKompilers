using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    float speed = 30.0f;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Tank t = collision.collider.GetComponent<Tank>();
        if (t != null)
        {
            t.TakeDamage();
        }
        GameObject.Destroy(gameObject);
        // bang.wav
        //if (collision.relativeVelocity.magnitude > 2)
        //	audio.Play ();
    }
}
