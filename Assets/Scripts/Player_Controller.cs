using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Animator animator;

    float _velocity = 0.0f;

    public float acceleration = 0.1f;
    public float deceleration = 0.5f;

    int VelocityHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");

        if (forwardPressed && _velocity < 1.0f)
        {
            _velocity += Time.deltaTime * acceleration;
        }
        if (!forwardPressed && _velocity > 0.0f)
        {
            _velocity -= Time.deltaTime * deceleration;
        }
        if(!forwardPressed && _velocity < 0.0f)
        {
            _velocity = 0.0f;
        }
        animator.SetFloat(VelocityHash, _velocity);
    }
}
