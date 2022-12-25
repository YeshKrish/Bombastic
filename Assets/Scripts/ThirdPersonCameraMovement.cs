using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    //Player Movement Input Variables
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float RotationSpeed;

    float _hInput;
    float _vInput;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Movement
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        _hInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        Vector3 inputDir = orientation.forward * _vInput + orientation.right * _hInput;

        if (inputDir != Vector3.zero)
        {
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * RotationSpeed);
        }
    }
}
