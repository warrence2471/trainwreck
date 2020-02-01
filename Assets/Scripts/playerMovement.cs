using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    public Image overlayTint;

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

        if (overlayTint.color.a > 0)
        {
            overlayTint.color = new Color(
                overlayTint.color.r,
                overlayTint.color.g,
                overlayTint.color.b,
                overlayTint.color.a - (0.66f * Time.deltaTime)
            );
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Game Loco") {
            Debug.Log("Aua! Pass doch auf!");
            overlayTint.color = new Color(1, 0, 0, 1);
        } else
            Debug.Log("Autsch");
    }
}
