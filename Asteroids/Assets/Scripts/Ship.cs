using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Rigidbody2D rbody;

    const float ThrustForce = 1f;

    Vector2 thrustDirection = new Vector2(1, 0);

    float colliderRadius;

    const float RotateDegreesPerSecond = 45f;


    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        colliderRadius = gameObject.GetComponent<CircleCollider2D>().radius;
        
    }

    void FixedUpdate()
    {
        float ThrustInput = Input.GetAxis("Thrust");
        if (ThrustInput != 0)
        {
            rbody.AddForce(ThrustForce * thrustDirection, ForceMode2D.Force);
        }
    }

    private void OnBecameInvisible()
    {
        
        if (transform.position.x >= ScreenUtils.ScreenRight)
        {
            transform.position = new Vector2(ScreenUtils.ScreenLeft, transform.position.y);
        } else if (transform.position.x <= ScreenUtils.ScreenLeft)
        {
            transform.position = new Vector2(ScreenUtils.ScreenRight, transform.position.y);
        } else if (transform.position.y >= ScreenUtils.ScreenTop)
        {
            transform.position = new Vector2(transform.position.y, ScreenUtils.ScreenBottom);
        } else if (transform.position.y <= ScreenUtils.ScreenBottom)
        {
            transform.position = new Vector2(transform.position.y, ScreenUtils.ScreenTop);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        float rotationInput = Input.GetAxis("Rotate");
        float rotationAmount = RotateDegreesPerSecond * Time.deltaTime;
        if (rotationInput < 0)
        {
            rotationAmount *= -1;
        }
        transform.Rotate(Vector3.forward, rotationAmount);
        thrustDirection = new Vector2(Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
    }
}
