using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerBehavior : MonoBehaviour
{
    private CharacterController _controller = null;
    private Animator _animator = null;

    public float speed = 1.0f;
    public float turnRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the movement to be equal to the input
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

        //Move the player
        _controller.SimpleMove(transform.forward * movement.z * speed);
        transform.Rotate(transform.up, movement.x * turnRate);

        //Animate based on movement
        _animator.SetFloat("Speed", movement.z * speed);

        Debug.DrawRay(transform.position, transform.forward);

        //Fire when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                RagdollBehavior ragdoll = hit.transform.GetComponent<RagdollBehavior>();
                if (ragdoll != null)
                {
                    ragdoll.ragdollEnabled = true;
                }
            }
        }

        //Close the game when the escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}