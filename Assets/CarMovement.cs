using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float totalTime = 10f;
    private float currentTime;

    private int direction = 1;
    public GameObject pivot;
    Vector3 localUp = new Vector3(0, -1, 0);

    // Start is called before the first frame update
    void Start()
    {
        calculatePivot();
        rb = GetComponent<Rigidbody2D>();
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            direction = -direction;
            currentTime = totalTime;
        }
        transform.RotateAround(pivot.transform.position, Vector3.forward, direction * 10 * Time.deltaTime);
        float angle = direction * 10 * Time.deltaTime * (MathF.PI / 180f);
        localUp = new Vector3(localUp.x * Mathf.Cos(angle), localUp.x * Mathf.Sin(angle), 0f) + new Vector3(localUp.y * -Mathf.Sin(angle), localUp.y * Mathf.Cos(angle), 0f);
        // Get the forward direction of the object's rotation
        //Vector3 forwardDirection = transform.forward;

        //// Set the velocity based on the forward direction and speed
        //Vector3 velocity = forwardDirection * 10;
        //velocity.z = 0;
        //// Set the Rigidbody's velocity
        //transform.position += new Vector3(0.01f, 0.01f, 0);
        //pivot.transform.position += velocity;
        // Get the local up direction of the GameObject
        // Transform the local up direction to world space
        //Vector3 worldUp = transform.TransformDirection(localUp);

        // Calculate the movement in the world space
        Vector3 movement = localUp  * Time.deltaTime;

        // Move the GameObject
        transform.position += movement;
    }

    Vector3 RotateVectorByAngle(Vector3 vector, float angle)
    {
        // Create a rotation quaternion based on the angle
        Quaternion rotationQuaternion = Quaternion.Euler(angle, 0, 0);

        // Rotate the vector using the quaternion
        Vector3 rotatedVector = rotationQuaternion * vector;

        return rotatedVector;
    }

    void calculatePivot()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Get the size of the sprite (which corresponds to the bounds of the square)
            Vector2 squareSize = spriteRenderer.bounds.size;

            // Assuming the square is a perfect square, get the length of one side
            //squareLength = Mathf.Max(squareSize.x, squareSize.y);
            pivot.transform.position = new Vector3(spriteRenderer.transform.position.x, spriteRenderer.transform.position.y - squareSize.x / 2, spriteRenderer.transform.position.z);
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found in child objects.");
        }
    }
}
