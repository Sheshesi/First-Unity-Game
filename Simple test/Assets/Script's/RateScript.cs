using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(7 , 7);
    public Vector2 direction = new Vector2(-1, 0);
    Vector2 movement;
    Rigidbody2D rb;
    public bool isLookingLeft = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLookingLeft)
        {
            movement = new Vector2(speed.x * direction.x, speed.y * direction.y);
        }
        else
        {
            movement = new Vector2(speed.x * (-direction.x), speed.y * (-direction.y));
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movement;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            isLookingLeft = !isLookingLeft;
            Flip();
        }
    }

    void Flip()
    {
        //isLookingLeft = !isLookingLeft;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
