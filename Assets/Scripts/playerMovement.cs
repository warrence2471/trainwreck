using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // WASD + Cursor Keys
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector3.right * Time.deltaTime * 10;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector3.left * Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.forward * Time.deltaTime * 10;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector3.back * Time.deltaTime * 10;
        }

        // "Friction"
        rb.velocity *= 0.99f;

        // Look towards mouse cursor
        Vector3 vec = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector3 viewDir = new Vector3(vec.x, 0, vec.y);
        transform.rotation = Quaternion.LookRotation(viewDir, Vector3.up);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Autsch");
    }
}
