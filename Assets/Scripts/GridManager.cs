using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject playerPrefab;
    public static int row = 3;
    public static int column = 3;
    public TextMeshPro text;
    public struct Node
    {
        public Vector3 position;
        public GameObject cubeObj;
        public bool isCheckPlayer;
        public bool isCheckEnemy;
    }
    public Node[,] grid = new Node[row, column];

   
    void GridBuild() 
    {
        for (int i = 0; i < row; i++) 
        {
            for (int j = 0; j < column; j++)
            {
                grid[i, j].position += new Vector3(i, 0, j);
                grid[i, j].cubeObj = Instantiate(cubePrefab, grid[i, j].position, cubePrefab.transform.rotation);
                grid[i, j].isCheckPlayer = false;
                grid[i, j].isCheckEnemy = false;
            }
        }
        Instantiate(playerPrefab, grid[0, 0].position + Vector3.up, playerPrefab.transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        GridBuild();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
