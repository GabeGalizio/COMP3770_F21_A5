using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviourScript : MonoBehaviour
{
    public GameObject[] walls; // 0- up 1- down 2- right 3- left
    public GameObject[] doors; // 0- up 1- down 2- right 3- left
    
    //Used to update each room with open doors
    public void UpdateRoom(bool[] status) {
        for (int i = 0; i < status.Length; i++) {
            // changes the active element between an open door and a closed door
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
