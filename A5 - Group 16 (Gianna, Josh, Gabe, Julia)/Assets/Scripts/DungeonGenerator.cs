using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{
    //used to get cell information 
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    //a class used for creating new rules for spawning in each room
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;
        
        public bool obligatory;

        //Used to see if the room should spawn or not
        public int probOfSpawn(int x, int y)
        {
            //0 can not spawn at that position 
            //1 may spawn at that position 
            //2 has to spawn at that position 
            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligatory ? 2 : 1;
            }

            return 0;
        }


    }
    
    [SerializeField] NavMeshSurface[] navMeshSurfaces;
    public Vector2Int size;
    public int startPos=0;
    public Rule[] room;
    public Rule[] startRoom;
    public GameObject player;
    public Vector2 offset;

    private List<Cell> board;

    // Start is called before the first frame update
    void Start() {
        MazeGenerator();
    }
    
    //used to instantiate each room in the gameboard as well as the maze pattern
    void GenerateDungeon() {
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                //on the first room it generates a blank room for the spawn room 
                if (i==0 && j==0) {
                    var newRoom= Instantiate(startRoom[0].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviourScript>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);
                    newRoom.name += " " + i + "-" + j;
                    Instantiate(player, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity);
                    //generates a random room for the remainder of the level
                }else{
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();
                    // goes through and decides if the room that is about to be generated needs to spawn
                    //starts with looking to see the rooms that can spawn and the rooms that need to spawn
                    for (int k = 0; k < room.Length; k++) {
                        int p = room[k].probOfSpawn(i, j);
                        if (p==2) {
                            randomRoom = k;
                            break;
                        }else if (p == 1) {
                            availableRooms.Add(k);
                        }
                    }
                    //if by the end of seeing if a room can spawn or needs to spawn
                    //we look at the pool of rooms that can spawn and pick one at random
                    if (randomRoom ==-1) {
                        if (availableRooms.Count > 0) {
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        }
                        else {
                            randomRoom = 0;
                        }
                    }
                    //Instantiates the room and names it x,y
                    Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                    if(currentCell.visited){
                        var newRoom= Instantiate(room[randomRoom].room, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviourScript>();
                        newRoom.UpdateRoom(currentCell.status);
                        newRoom.name += " " + i + "-" + j;}
                }
            }
        }

        BakeNavMesh();
    }
    
    // generaterating the maze 
    void MazeGenerator() {
        // creates a list of the rooms in the board
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }
        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        //goes through and generates each room so that each room is reachable
        while (k < 1000){
            k++;
            board[currentCell].visited = true;

            //allows us to make a game board that does not look like a square
            if (currentCell == board.Count-1) {
                break;
            }
            //check the cells neighbours 
            List<int> neighbours = CheckNeighbours(currentCell);
           
            if (neighbours.Count ==0){
                if (path.Count == 0){
                    break;
                }else{
                    currentCell = path.Pop();
                }
            }else{
                path.Push(currentCell);
                int newCell = neighbours[Random.Range(0, neighbours.Count)];
                if (newCell > currentCell){
                    //either going down or right
                    if (newCell - 1 == currentCell) {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }else{
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }else{
                    //up or left
                    if (newCell + 1 == currentCell) {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }else{
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }
        }
        GenerateDungeon();
    }

    //a function used to check each neighbour to see where an open door/closed door should go 
    List<int> CheckNeighbours(int cell) {
        List<int> neighbour = new List<int>();
        //check up neighbor
        // looks to see if there is an option for an up room
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited) {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell-size.x));
        }
        //check down neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell+size.x)].visited) {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell+size.x));
        }
        //check Right neighbor
        if ((cell +1) % size.x  != 0 && !board[Mathf.FloorToInt(cell+1)].visited) {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell+ 1));
        }
        //check left neighbor
        if (cell  % size.x  != 0 && !board[Mathf.FloorToInt(cell-1)].visited) {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell-1 ));
        }
        return neighbour;
    }
    
    public void BakeNavMesh(){ 
        foreach (var navMeshSurface in navMeshSurfaces)
        {
            navMeshSurface.BuildNavMesh();
        }
    }
}
