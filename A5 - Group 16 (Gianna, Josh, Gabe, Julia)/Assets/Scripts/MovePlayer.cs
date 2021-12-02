using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

//Script to control player movement. Also will detect change in camera

public class MovePlayer : MonoBehaviour {

    //Player attributes
    private Camera camera;
    private NavMeshAgent agent;
    public GameObject menu;

    void Start() {
        camera = Camera.main;
        if (camera == null) {
            Debug.LogError("CAMERA NULL");
        }
        agent = GetComponent<NavMeshAgent>();
    }

    void OnMouseClick() {
        if (GameObject.Find("Pause Menu(Clone)") == null) {
            Debug.Log("Mouse clicked, posn is " + Mouse.current.position.ReadValue() + ".");
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
            }
        }
    }

    void OnOpenMenu() {
        if (GameObject.Find("Pause Menu(Clone)") == null) {
            Instantiate(menu, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
