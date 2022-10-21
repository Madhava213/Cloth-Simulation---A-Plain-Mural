using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CamShooter : MonoBehaviour
{
    public Transform playerBody;
    public CharacterController controller;
    public float speed = 12f;
    public GameObject bullet;
    public float shootPower = 1f;
    public float moveOffset = 0.01f;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;
    public TextMeshProUGUI fanStatus;
    private float fanSpeed = 0.0f;
    private RaycastHit fanHit;
    private Collider m_Collider;

    void Start()
    {
        // Lock the cursor to prevent it from going out of window
        Cursor.lockState = CursorLockMode.Locked;
        m_Collider = GetComponent<Collider>();
    }


    // Update is called once per frame
    void Update()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Clamp Rotation
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply Mouse Look Movement
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out fanHit, transform.rotation, 10))
        {
            fanHit.collider.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * fanSpeed);
        }

        if (Input.GetMouseButtonDown(0)) {
            GameObject shell = Instantiate(bullet, transform.position + transform.forward, transform.rotation);
            Vector3 shootDir = transform.forward;
            shootDir.y += 0.1f;
            shell.GetComponent<Rigidbody>().AddForce(shootDir * shootPower);
        }

        if(Input.GetKey("1")){
                fanStatus.text = "Fan : Off";
                fanSpeed = 0.0f;
            }
        if(Input.GetKey("2")){
            fanStatus.text = "Fan : Low";
            fanSpeed = 1.0f;
        }
        if(Input.GetKey("3")){
                fanStatus.text = "Fan : Med";
                fanSpeed = 2.0f;
            }
        if(Input.GetKey("4")){
                fanStatus.text = "Fan : High";
                fanSpeed = 3.0f;
            }
        
        if(Input.GetKey("q")){
            Application.Quit();
        }
    }


}
