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
        bool forwardPressed = Input.GetKey("w");
        bool rightPressed = Input.GetKey("d");
        bool leftPressed = Input.GetKey("a");
        bool shiftPressed = Input.GetKey("left shift");

        //Transition from Idle to Walk
        if (forwardPressed && _velocityz < 0.5f && !shiftPressed)
        {
            _velocityz += Time.deltaTime * acceleration;
        }

        //Transition to Idle
        if (!forwardPressed && _velocityz > 0.0f && !shiftPressed)
        {
            _velocityz -= Time.deltaTime * deceleration;
        }

        //Reset Velocities
        if (!forwardPressed && _velocityz < 0.0f && !shiftPressed)
        {
            _velocityz = 0.0f;
        }    
        if(forwardPressed && shiftPressed && _velocityz > 2.0f)
        {
            _velocityz = 2.0f;
        }
        
        //Transition from Run to Walk
        if(forwardPressed && !shiftPressed && _velocityz >= 0.5f)
        {
            _velocityz -= Time.deltaTime * deceleration;
        }

        //Run
        if (forwardPressed && _velocityz < 2.0f && shiftPressed)
        {
            _velocityz += Time.deltaTime * acceleration;
        }
        
        //Right Strafe
        if (rightPressed && _velocityx < 0.5f && !leftPressed && !shiftPressed)
        {
            _velocityx += Time.deltaTime * acceleration;
        }
        //Right Strafe run
        if (rightPressed && _velocityx < 2.0f && !leftPressed && shiftPressed)
        {
            _velocityx += Time.deltaTime * acceleration;
        }

        //Right Strafe run To Right Strafe
        if (rightPressed && _velocityx >= 0.5f && !shiftPressed)
        {
            _velocityx -= Time.deltaTime * deceleration;
        }
      

        //Left Strafe
        if (leftPressed && _velocityx > -0.5f && !rightPressed && !shiftPressed)
        {
            _velocityx -= Time.deltaTime * acceleration;
        }
        //Left Strafe run
        if (leftPressed && _velocityx > -2.0f && !rightPressed && shiftPressed)
        {
            _velocityx -= Time.deltaTime * acceleration;
        }

        //Left Strafe run To Left Strafe
        if (leftPressed &&  _velocityx <= -0.5f && !shiftPressed)
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

        //Reset Velocities
        if (leftPressed && _velocityx < -2.0f)
        {
            _velocityx = -2.0f;
        }
        if (rightPressed && _velocityx > 2.0f)
        {
            _velocityx = 2.0f;
        }
        if (!leftPressed && _velocityx != 0.0f && !rightPressed && (_velocityx < 0.05 && _velocityx > -0.05))
        {
            _velocityx = 0.0f;
        }

        animator.SetFloat(VelocityXHash, _velocityx);
        animator.SetFloat(VelocityZHash, _velocityz);
    }
}
