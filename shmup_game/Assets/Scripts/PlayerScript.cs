using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /// The speed of the ship
    public Vector2 speed = new Vector2(50, 50);

    // Store the movement and the component
    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    void Update()
    {
        // Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Movement per direction
        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        // Shooting
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        // Careful: For Mac users, ctrl + arrow is a bad idea

        if (shoot)
            {
                WeaponScript weapon = GetComponent<WeaponScript>();
                if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.Attack(false);
            }
        }
 
        // Make sure we are not outside the camera bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
        new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
        Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
        Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
        transform.position.z
        );
    }

    void FixedUpdate()
    {
        // Get the component and store the reference
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        // Move the game object
        rigidbodyComponent.velocity = movement;
    }
}
