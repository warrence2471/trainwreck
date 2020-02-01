using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrain : MonoBehaviour
{
    [SerializeField]
    public float maxSpeed = 1.5f;
    [SerializeField]
    public bool makeSmoke = true;
    [SerializeField]
    public GameObject smoke;
    [SerializeField]
    public float smokeAge = 0.2f;
    [SerializeField]
    public float acceleration = 0.1f;
    [SerializeField]
    public float negativeAcceleration = 0.3f;
    [SerializeField]
    public GameTrain preceding;

    private float age = 0.0f;
    private bool isDriving = false;
    private float currentSpeed = 0;
    private float currentTurnAngle;
    private Collider turn;
    private Collider lastTurn;
    private float turnIncrement;
    private float turnDir;
    private Vector3 turnAnchor;

    private const float CUpdatesPerSecond = 120;
    private const float CTurnExtent = 0.5f;
    private const float CWagonDistance = 0.75f;

    public bool IsTurning { get; set; } = false;

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
            if (makeSmoke)
            {
                age += Time.deltaTime;
                if (age > smokeAge)
                {
                    age = 0;
                    Instantiate(smoke, transform.position + 0.2f * transform.forward + 0.5f * transform.up, Quaternion.identity);
                }
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
        if (!IsTurning)
        {
            if (preceding == null || preceding.IsTurning)
            {
                transform.position += transform.forward * currentSpeed / CUpdatesPerSecond;
            }
            else
            {
                var newPosition = preceding.transform.position - CWagonDistance * preceding.transform.forward;
                var currentDistance = Vector3.Distance(transform.position, preceding.transform.position);
                var newDistance = Vector3.Distance(newPosition, preceding.transform.position);
                if (currentDistance > newDistance)
                {
                    transform.position = newPosition;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsTurning)
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
                    transform.forward = turnDir > 0 ? turn.transform.forward : turn.transform.right;
                    var end = turn.transform.position + transform.forward * CTurnExtent;
                    transform.position = new Vector3(end.x, transform.position.y, end.z);
                    StopTurning();
                }
            }
        }
        else 
        {
            // Collider is a railturn and the center of the loco is within the collider
            if (other != lastTurn && IsCollidingRailturn(other) && other.bounds.Contains(transform.position))
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
        Debug.Log($"{name} starts driving");
        isDriving = true;
    }

    private void StopTrain()
    {
        Debug.Log($"{name} stops driving");
        isDriving = false;
    }

    private void StartTurning(Collider other)
    {
        Debug.Log($"{name} starts turning");
        IsTurning = true;
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
        Debug.Log($"{name} stops turning");
        IsTurning = false;
        lastTurn = turn;
    }
}
