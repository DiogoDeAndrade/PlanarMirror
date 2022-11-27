using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickFPSMove : MonoBehaviour
{
    public float mouseSensitivity;
    public float moveSpeed;

    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, 0));
        transform.position  = transform.position + (transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime) + (transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
    }
}
