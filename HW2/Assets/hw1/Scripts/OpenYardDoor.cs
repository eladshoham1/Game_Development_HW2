using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenYardDoor : MonoBehaviour
{
    private Animator anim;
    private AudioSource doorSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        doorSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        anim.SetBool("opening", true);
        doorSound.Play();
    }

    void OnTriggerExit(Collider col)
    {
        anim.SetBool("opening", false);
        doorSound.Play();
    }
}
