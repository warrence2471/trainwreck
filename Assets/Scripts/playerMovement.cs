using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    Rigidbody rb;
    public Image overlayTint;
    public int speed = 50;

    public GameObject maleCharacter;
    public GameObject femaleCharacter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Awake()
    {
        if(PlayerVars.CharacterModel == "male")
        {
            maleCharacter.SetActive(true);
            femaleCharacter.SetActive(false);
        }
        else
        {
            maleCharacter.SetActive(false);
            femaleCharacter.SetActive(true);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // WASD + Cursor Keys
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.velocity += Vector3.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.velocity += Vector3.left * Time.deltaTime * speed;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector3.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.velocity += Vector3.back * Time.deltaTime * speed;
        }

        // "Friction"
        rb.velocity *= 0.79f;

        // Look towards mouse cursor
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit)) {
            Vector3 viewAngle = hit.point - transform.position;
            Vector3 viewAngleS = new Vector3(viewAngle.x, 0, viewAngle.z); // look straigt
            rb.MoveRotation(Quaternion.LookRotation(viewAngleS, Vector3.up));
        }

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
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        } else
            Debug.Log("Autsch");
    }
}
