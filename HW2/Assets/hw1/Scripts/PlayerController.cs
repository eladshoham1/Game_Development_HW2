using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 14.0f;
    public float jumpHeight = 10.0f;
    public float gravity = 20f;

    private Vector3 moveDir = Vector3.zero;

    private CharacterController controller;

    private AudioSource footStepSound;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        footStepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                moveDir *= (speed * 1.5f);
            else
                moveDir *= speed;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            footStepSound.Stop();
            moveDir.y = jumpHeight;
        }

        moveDir.y -= gravity * Time.deltaTime;

        controller.Move(moveDir * Time.deltaTime);

        // add sound of foot steps
        if (!footStepSound.isPlaying && controller.velocity.magnitude > 0.1f)
            footStepSound.Play();
    }
}