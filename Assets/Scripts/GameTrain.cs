﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrain : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed = 1.5f;
    [SerializeField]
    public GameObject smoke;
    [SerializeField]
    public float smokeAge = 0.2f;
    [SerializeField]
    public float acceleration = 0.1f;
    [SerializeField]
    public float negativeAcceleration = 0.3f;

    private float age = 0.0f;
    private bool isDriving = false;
    private float currentSpeed = 0;

    private bool isTurning = false;
    private float currentTurnAngle;
    private Collider turn;
    private float turnIncrement;
    private float turnDir;
    private Vector3 turnAnchor;

    private const float CUpdatesPerSecond = 120;
    private const float CTurnExtent = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartTrain();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDriving)
        {
            age += Time.deltaTime;
            if (age > smokeAge)
            {
                age = 0;
                Instantiate(smoke, transform.position + 0.4f * transform.forward + 0.4f * transform.up, Quaternion.identity);
            }

            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= negativeAcceleration * Time.deltaTime;
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        if (!isTurning)
        {
            transform.position += transform.forward * currentSpeed / CUpdatesPerSecond;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isTurning)
        {
            if (other == turn)
            {
                var incrementAngle = currentSpeed * turnIncrement;
                currentTurnAngle += incrementAngle;
                if (currentTurnAngle < 90)
                {
                    transform.RotateAround(turnAnchor, Vector3.up, turnDir * incrementAngle);
                }
                else
                {
                    var forward = turnDir > 0 ? turn.transform.forward : turn.transform.right;
                    var end = turn.transform.position + forward * CTurnExtent;
                    transform.position = new Vector3(end.x, transform.position.y, end.z);
                    transform.forward = forward;
                    StopTurning();
                }
            }
        }
        else 
        {
            // Collider is a railturn and the center of the loco is within the collider
            if (IsCollidingRailturn(other) && other.bounds.Contains(transform.position))
            {
                StartTurning(other);
            }
        }
    }

    private bool IsCollidingRailturn(Collider other)
    {
        return other.gameObject.CompareTag("railturn");
    }

    private void StartTrain()
    {
        Debug.Log("Start driving");
        isDriving = true;
    }

    private void StopTrain()
    {
        Debug.Log("Stop driving");
        isDriving = false;
    }

    private void StartTurning(Collider other)
    {
        Debug.Log("Start turning");
        isTurning = true;
        turn = other;
        currentTurnAngle = 0;

        turnAnchor = turn.transform.position + (turn.transform.forward + turn.transform.right) * CTurnExtent;
        turnIncrement = Mathf.Rad2Deg / CUpdatesPerSecond / turn.bounds.extents.x * 3; // TODO *3?
        var turnVector = turn.transform.forward + turn.transform.right + transform.forward;
        if (Vector3.Dot(turnVector, transform.right) < 0)
        {
            turnDir = -1;
        }
        else
        {
            turnDir = 1;
        }
    }

    private void StopTurning()
    {
        Debug.Log("Stop turning");
        isTurning = false;
    }
}
