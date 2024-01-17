using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensorsPositioning : MonoBehaviour
{
    public GameObject sensor;
    private List<GameObject> sensors;  // Reference to the smaller square prefab
    public float squareLength;   // Distance of smaller squares from the center
    public GameObject object1, object2;

    void Start()
    {
        sensors = new();
        GetSquareLength();
        ArrangeSmallerSquares();
    }

    private void Update()
    {
        object1 = sensors[3];
        object2 = GameObject.FindGameObjectWithTag("Line");

        if (object1.GetComponent<BoxCollider2D>() && object2.GetComponent<CompositeCollider2D>())
        {
            // Get the bounds of each game object
            Bounds bounds1 = object1.GetComponent<BoxCollider2D>().bounds;
            Bounds bounds2 = object2.GetComponent<CompositeCollider2D>().bounds;

            // Calculate the overlap bounds
            Bounds overlapBounds = bounds1;
            overlapBounds.Encapsulate(bounds2.min);
            overlapBounds.Encapsulate(bounds2.max);

            // Calculate and print the size of the overlap area
            Vector3 overlapSize = overlapBounds.size;
            Debug.Log("Overlap Size: " + overlapSize.x * overlapSize.y);
        }
        else
        {
            Debug.LogError("Both game objects must have colliders for overlap calculation.");
        }
    }

    void ArrangeSmallerSquares()
    {
        SpriteRenderer bigSquareRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector2 bigSquareSize = bigSquareRenderer.bounds.size;

        // Calculate the position of the right-down corner of the big square
        float cornerX = bigSquareRenderer.gameObject.transform.position.x + bigSquareSize.x / 2f;
        float cornerY = bigSquareRenderer.gameObject.transform.position.y - bigSquareSize.y / 2f;

        for (int i = 0; i < 6; i++)
        {
            // Calculate the position based on polar coordinates
            float x =cornerX - i * squareLength / 6 - squareLength / 12;
            float y = cornerY ;

            // Instantiate and position the smaller square
            GameObject smallerSquare = Instantiate(sensor, new Vector3(x, y, 0f), Quaternion.identity);
            smallerSquare.transform.localScale = Vector3.one / 12f;
            smallerSquare.transform.SetParent(transform);  // Set the center GameObject as the parent
            sensors.Add(smallerSquare);
        }
    }

    void GetSquareLength()
    {
        // Assuming the square is represented by a child GameObject with a SpriteRenderer component
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            // Get the size of the sprite (which corresponds to the bounds of the square)
            Vector2 squareSize = spriteRenderer.bounds.size;

            // Assuming the square is a perfect square, get the length of one side
            squareLength = Mathf.Max(squareSize.x, squareSize.y);

            Debug.Log($"Length of the square: {squareLength}");
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found in child objects.");
        }
    }
}
