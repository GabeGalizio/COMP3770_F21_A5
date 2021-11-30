using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonGenerator : MonoBehaviour
{

    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [SerializeField] NavMeshSurface[] navMeshSurfaces;
    public Vector2 size;
    public int startPos=0;
    public GameObject[] room;
    public GameObject player;
    public Vector2 offset;

    private List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
        
    }
    
    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                if (i==0 && j==0)
                {
                   // int randomRoom = Random.Range(0, room.Length);
                    var newRoom= Instantiate(room[0], new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviourScript>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);
                    newRoom.name += " " + i + "-" + j;
                    Instantiate(player, new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity);
                }
                else
                {
                    int randomRoom = Random.Range(0, room.Length);
                    var newRoom= Instantiate(room[randomRoom], new Vector3(i * offset.x, 0, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviourScript>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);
                    newRoom.name += " " + i + "-" + j;
                }
            }
        }

        BakeNavMesh();
    }


    void MazeGenerator()
    {
        board = new List<Cell>();
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();
        int k = 0;

        while (k < 1000){
            k++;
            board[currentCell].visited = true;
            
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
                }else
                {
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

    List<int> CheckNeighbours(int cell)
    {
        List<int> neighbour = new List<int>();
        //check up neighbor
        // looks to see if there is an option for an up room
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell-size.x));
        }
        //check down neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell+size.x)].visited)
        {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell+size.x));
        }
        //check Right neighbor
        if ((cell +1) % size.x  != 0 && !board[Mathf.FloorToInt(cell+1)].visited)
        {
            //adds the unvisited room to the list of visited rooms 
            neighbour.Add(Mathf.FloorToInt(cell+ 1));
        }
        //check left neighbor
        
        if (cell  % size.x  != 0 && !board[Mathf.FloorToInt(cell-1)].visited)
        {
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
