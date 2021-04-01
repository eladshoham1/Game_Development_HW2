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

    IEnumerator doorSoundDelay()
    {
        yield return new WaitForSeconds(0.85f);
        doorSound.Play();
    }
    void OnTriggerEnter(Collider col)
    {
        anim.SetBool("opening", true);
        anim.SetBool("closening", false);
        StartCoroutine(doorSoundDelay());

    }
    void OnTriggerExit(Collider col)
    {
        anim.SetBool("closening", true);
        anim.SetBool("opening", false);
        StartCoroutine(doorSoundDelay());
    }
}
