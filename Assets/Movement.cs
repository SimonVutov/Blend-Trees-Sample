using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Vector2 rawRotation;
    Vector2 rotation;
    public Camera cam;
    public float lookSensitivity = 5f;
    public float moveSpeed = 2f;
    public float jumpStrength = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move;
        int w, a, s, d;
        if (Input.GetKey("w")) w = 1; else w = 0;
        if (Input.GetKey("a")) a = 1; else a = 0;
        if (Input.GetKey("s")) s = 1; else s = 0;
        if (Input.GetKey("d")) d = 1; else d = 0;

        move.y = w - s;
        move.x = d - a;

        rawRotation.y += Input.GetAxis("Mouse X");
        rawRotation.x += Input.GetAxis("Mouse Y");
        rotation = Vector2.Lerp(rotation, rawRotation, Time.deltaTime * 10f);

        cam.transform.eulerAngles = new Vector2(-rotation.x * lookSensitivity, transform.eulerAngles.y);
        cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position + new Vector3(0, 1, 0), 1f); //smoothly moves camera to correct pos

        transform.eulerAngles = new Vector2(0, rotation.y * lookSensitivity);
        Vector3 disiredvelocity = transform.TransformDirection(moveSpeed * new Vector3(move.x, 0, move.y));
        rb.velocity = new Vector3(disiredvelocity.x * moveSpeed, rb.velocity.y, disiredvelocity.z * moveSpeed);

        if (Input.GetButtonDown("Jump")) Jump();
    }

    void Jump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.2f))
        {
            rb.AddForce(transform.up * jumpStrength, ForceMode.Impulse);
        }
    }
}
