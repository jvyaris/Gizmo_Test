using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public float move_Speed = 0f;
    public float rotate_Speed = 0f;
    public float yaw = 0f;
    public float pitch = 0f;

    // Update is called once per frame
    void Update()
    {

        float xPos = Input.GetAxis("Horizontal") * move_Speed * Time.deltaTime;
        float zPos = Input.GetAxis("Vertical") * move_Speed * Time.deltaTime;
        Vector3 newPos = new Vector3(xPos, 0, zPos);
        newPos = Camera.main.transform.TransformDirection(newPos);
        transform.position += newPos;
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += new Vector3(0f, move_Speed * Time.deltaTime, 0f);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= new Vector3(0f, move_Speed * Time.deltaTime, 0f);
        }
        if (Input.GetMouseButton(1))
        {
            yaw += rotate_Speed * Input.GetAxis("Mouse X");
            pitch -= rotate_Speed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }
    
}
