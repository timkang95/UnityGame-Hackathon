using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour {
    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("Punch");
            AudioSource audio = GetComponent<"../Sounds/'whipSound1.mp3">();
            audio.Play();
        }

    }
}
