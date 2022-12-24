using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Animator animator;

    float _velocityx = 0.0f;
    float _velocityz = 0.0f;

    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;   
    public float maximumWalkStrafeVelocity = 0.5f;
    public float maximumRunStrafeVelocity = 2.0f;

    int VelocityXHash;
    int VelocityZHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityXHash = Animator.StringToHash("VelocityX");
        VelocityZHash = Animator.StringToHash("VelocityZ");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);

        //Velocity Changes
        ChangeVelocity(forwardPressed, shiftPressed, leftPressed, rightPressed);
        ResetVelocity(forwardPressed, shiftPressed, leftPressed, rightPressed);

        animator.SetFloat(VelocityXHash, _velocityx);
        animator.SetFloat(VelocityZHash, _velocityz);
    }

    void ChangeVelocity(bool forwardPressed, bool shiftPressed, bool leftPressed, bool rightPressed)
    {

        //Transition from Idle to Walk
        if (forwardPressed && _velocityz < maximumWalkVelocity && !shiftPressed)
        {
            _velocityz += Time.deltaTime * acceleration;
        }

        //Transition to Idle
        if (!forwardPressed && _velocityz > 0.0f && !shiftPressed)
        {
            _velocityz -= Time.deltaTime * deceleration;
        }
        //Transition from Run to Walk
        if (forwardPressed && !shiftPressed && _velocityz >= maximumWalkVelocity)
        {
            _velocityz -= Time.deltaTime * deceleration;
        }

        //Run
        if (forwardPressed && _velocityz < maximumRunVelocity && shiftPressed)
        {
            _velocityz += Time.deltaTime * acceleration;
        }

        //Right Strafe
        if (rightPressed && _velocityx < maximumWalkStrafeVelocity && !leftPressed && !shiftPressed)
        {
            _velocityx += Time.deltaTime * acceleration;
        }

        //Right Strafe run
        if (rightPressed && _velocityx < maximumRunStrafeVelocity && !leftPressed && shiftPressed)
        {
            _velocityx += Time.deltaTime * acceleration;
        }

        //Right Strafe run To Right Strafe
        if (rightPressed && _velocityx >= maximumWalkStrafeVelocity && !shiftPressed)
        {
            _velocityx -= Time.deltaTime * deceleration;
        }

        //Left Strafe
        if (leftPressed && _velocityx > -maximumWalkStrafeVelocity && !rightPressed && !shiftPressed)
        {
            _velocityx -= Time.deltaTime * acceleration;
        }

        //Left Strafe run
        if (leftPressed && _velocityx > -maximumRunStrafeVelocity && !rightPressed && shiftPressed)
        {
            _velocityx -= Time.deltaTime * acceleration;
        }

        //Left Strafe run To Left Strafe
        if (leftPressed && _velocityx <= -maximumWalkStrafeVelocity && !shiftPressed)
        {
            _velocityx += Time.deltaTime * deceleration;
        }

        //Any Strafe Transition to Idle
        if (!rightPressed && _velocityx > 0 && !leftPressed && !shiftPressed)
        {
            _velocityx -= Time.deltaTime * deceleration;
        }
        if (!leftPressed && _velocityx < 0.0f && !rightPressed && !shiftPressed)
        {
            _velocityx += Time.deltaTime * deceleration;
        }
    }

    void ResetVelocity(bool forwardPressed, bool shiftPressed, bool leftPressed, bool rightPressed)
    {

        //Reset Velocities
        if (!forwardPressed && _velocityz < 0.0f && !shiftPressed)
        {
            _velocityz = 0.0f;
        }
        if (forwardPressed && shiftPressed && _velocityz > maximumRunVelocity)
        {
            _velocityz = maximumRunVelocity;
        }

        //Reset Velocities
        if (leftPressed && _velocityx < -maximumRunStrafeVelocity)
        {
            _velocityx = -maximumRunStrafeVelocity;
        }
        if (rightPressed && _velocityx > maximumRunStrafeVelocity)
        {
            _velocityx = maximumRunStrafeVelocity;
        }
        if (!leftPressed && _velocityx != 0.0f && !rightPressed && (_velocityx < 0.05 && _velocityx > -0.05))
        {
            _velocityx = 0.0f;
        }
    }

}
