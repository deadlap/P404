using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyMovement : MonoBehaviour
{
    [Header("Up/Down Settings")]
    [SerializeField, Min(0f)] private float verticalMovement = 0.2f;
    [SerializeField] private float verticalSpeed = 1f;
    [SerializeField] private bool startAtRandomHeight = true;

    [Header("Local Rotation Settings")]
    [SerializeField] private bool rotateAroundSelf = false;
    [SerializeField] private float degreesPerSecondSelf = 3f;

    [Header("World Rotation Settings")]
    [SerializeField] private bool rotateRoundWorld = false;
    [SerializeField, Tooltip("Defaults to world center if not set")] private Transform rotateTarget;
    [SerializeField] private float degreesPerSecondWorld = 3f;

    private float startY;
    private bool rotateAroundTarget = false;

    private float currentVertical;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
        currentVertical = startAtRandomHeight ? Random.Range(0f, verticalMovement) - (0.5f * verticalMovement) : 0f;

        verticalMovement *= 0.5f;

        transform.position += Vector3.up * currentVertical;

        currentVertical = Mathf.Asin(currentVertical / verticalMovement);

        // Set a random start position
        rotateAroundTarget = (rotateTarget != null);
    }

    // Update is called once per frame
    void Update()
    {
        FloatUpDown();
        if (rotateAroundSelf) RotateSelf();
        if (rotateRoundWorld) RotateWorld();
    }

    void FloatUpDown()
    {
        currentVertical += verticalSpeed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.Sin(currentVertical) * verticalMovement + startY, transform.position.z);
    }

    void RotateSelf()
    {
        transform.Rotate(0f, degreesPerSecondSelf * Time.deltaTime, 0f);
    }

    void RotateWorld()
    {
        Vector3 rotationalPoint = rotateAroundTarget ? rotateTarget.position : Vector3.zero;

        transform.RotateAround(rotationalPoint, Vector3.up, degreesPerSecondWorld * Time.deltaTime);
    }
}
