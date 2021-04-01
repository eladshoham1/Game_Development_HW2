using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public float speed = 14.0f;
    public float jumpHeight = 10.0f;
    public float gravity = 20f;

    public AudioClip FootStepsSound;
    public AudioClip JumpSound;
    public AudioClip LandingSound;

    private Vector3 moveDir = Vector3.zero;

    private CharacterController controller;
    private AudioSource audioSource;
    private bool wasGrounded = false;
    private bool isJumpPressed = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            isJumpPressed = true;
    }

    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
                moveDir *= (speed * 1.5f);
            else
                moveDir *= speed;

            if (controller.velocity.sqrMagnitude > 0f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = FootStepsSound;
                    audioSource.Play();
                }
            }

            else
            {
                if (audioSource.clip != JumpSound && audioSource.clip != LandingSound)
                    if (audioSource.isPlaying)
                        audioSource.Stop();
            }

            if (controller.isGrounded != wasGrounded)
            {
                audioSource.Stop();
                audioSource.clip = LandingSound;
                audioSource.Play();
            }
            wasGrounded = true;
        }
        else
        {
            if (audioSource.clip != JumpSound && audioSource.clip != LandingSound)
                if (audioSource.isPlaying)
                    audioSource.Stop();

            if (controller.isGrounded != wasGrounded)
            {
                audioSource.Stop();
                audioSource.clip = JumpSound;
                audioSource.Play();
            }
            wasGrounded = false;
        }

        if(isJumpPressed)
        {
            moveDir.y = jumpHeight;
            isJumpPressed = false;
        }

        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

}