using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensorsPositioning : MonoBehaviour
{
    public GameObject sensor;
    private List<GameObject> sensors;  // Reference to the smaller square prefab
    private List<SensorColliderScript> sensorsScripts;
    public float squareLength;   // Distance of smaller squares from the center
    public GameObject object1, object2;
    public int output;
    public int[] rewards = { 1, 5, 10, 10, 5, 1 };
    public int[] weights = { -50, -20, 0, 0, 20, 50 };
    public int finalReward;
    public int positionRelativeToLine;

    void Start()
    {
        sensors = new();
        sensorsScripts = new();
        GetSquareLength();
        ArrangeSmallerSquares();
        //InvokeRepeating("TimerFunction", 0.2f, 0.5f);
    }

    // This function will be called every 0.5 seconds
    void TimerFunction()
    {

    }

    private void Update()
    {
        positionRelativeToLine = 0;
        finalReward = 0;
        if (sensorsScripts.Count == 6)
        {
            for (int i = 0; i < 6; i++)
            {
                positionRelativeToLine += weights[i] * (sensorsScripts[i].isOnLine ? 1 : 0);
                finalReward += rewards[i] * (sensorsScripts[i].isOnLine ? 1 : 0);
            }
            if (finalReward == 0)
            {
                positionRelativeToLine = -1000;
            }
            Debug.Log("Reward: " + finalReward);
            Debug.Log("Position: " + positionRelativeToLine);
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
            smallerSquare.name = "Sensor " + i; 
            sensors.Add(smallerSquare);
            SensorColliderScript script = smallerSquare.GetComponent<SensorColliderScript>();

            if (script != null)
                sensorsScripts.Add(script);
            else
                Debug.Log("sensor not found");
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
