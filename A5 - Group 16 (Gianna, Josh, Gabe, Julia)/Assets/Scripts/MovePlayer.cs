using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Script to control player movement. Also will detect change in camera

public class MovePlayer : MonoBehaviour
{
    //Player attributes
    private Rigidbody player_Rigid;
    private float player_Velo = 10f;
    Vector3 movementVector;
    Vector2 direction;
    float rotation;
    Vector3 rotationVelocity;
    private int jump_count = 0;
    public Transform camera;
    private bool thirdtoggle = false;

    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("Main Camera");
        player_Rigid = GetComponent<Rigidbody>();
        movementVector = new Vector3(0, 0, 0);
        rotationVelocity = new Vector3(0, 0, 0);
    }

    //Action to do upon jumping (Spacebar input).
    void OnJump() {
        if(jump_count < 2)
        {
            player_Rigid.AddForce(0, 5, 0, ForceMode.Impulse);
            jump_count++;
        }
    }

    void OnSwitchCamera()
    {
        Debug.Log(thirdtoggle);
        if (thirdtoggle)
        {
            thirdtoggle = false;
            camera.localPosition = (new Vector3(0f,0f,0f));
            camera.Rotate(new Vector3(-25,0,0));
        }
        else
        { 
            thirdtoggle = true;
            camera.localPosition = (new Vector3(0f,3f,-3f));
            camera.Rotate(new Vector3(25,0,0));
        }
    }
    
    //Action to do upon movement (WSAD input)
    void OnMove(InputValue value) {
        direction = value.Get<Vector2>();
        movementVector.x = direction.x * player_Velo;
        movementVector.z = direction.y * player_Velo;
        movementVector = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * movementVector;
    }

    //Action to do upon turning (Arrow key L/R input)
    void OnTurn(InputValue value) {
        rotation = value.Get<float>();
        rotationVelocity = new Vector3(0, 100 * rotation, 0);
    }

    //Update every frame (movement, rotation)
    void FixedUpdate() {
        player_Rigid.AddForce(movementVector, ForceMode.Force);
        Quaternion deltaRotation = Quaternion.Euler(rotationVelocity * Time.fixedDeltaTime);
        player_Rigid.MoveRotation(player_Rigid.rotation * deltaRotation);
    }

    //Detect collison w floow (below player)
    void OnCollisionEnter(Collision collision) { 
        if (collision.contacts.Length > 0) {
            ContactPoint contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5) {
                jump_count = 0;
            }
        }
    }
    
}
