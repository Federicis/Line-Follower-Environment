using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensorsPositioning : MonoBehaviour
{
    public GameObject sensor;
    public GameObject[] sensors;  // Reference to the smaller square prefab
    public float squareLength;   // Distance of smaller squares from the center

    void Start()
    {
        GetSquareLength();
        ArrangeSmallerSquares();
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
            sensors.Append(smallerSquare);
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
