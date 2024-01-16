using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 0.01f;
    public float totalTime = 10f;
    private float currentTime;

    private int direction = 1;
    public GameObject pivot;

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

        // Get the forward direction of the object's rotation
        Vector3 forwardDirection = transform.forward;

        // Set the velocity based on the forward direction and speed
        Vector3 velocity = forwardDirection * speed;

        // Set the Rigidbody's velocity
        rb.velocity = velocity;
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
