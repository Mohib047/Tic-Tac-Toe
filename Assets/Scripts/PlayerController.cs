using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable
    public GameObject grid;
    public int rowIndex = 0;
    public int colIndex = 0;
    public bool isPlayerOne;
    public bool isPlayerTwo;
    public bool playerWin = false;
    public bool enemyWin = false;
    void Movement() 
    {
        //Player Grid Movement
        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            if (rowIndex < grid.GetComponent<GridManager>().grid.GetLength(0) - 1)
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex + 1, colIndex].position + Vector3.up;
                rowIndex++;
            }
            else if (rowIndex == grid.GetComponent<GridManager>().grid.GetLength(0) - 1) 
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex, colIndex].position + Vector3.up;
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (rowIndex > 0)
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex - 1, colIndex].position + Vector3.up;
                rowIndex--;
            }
            else if (rowIndex == 0) 
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex, colIndex].position + Vector3.up;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (colIndex > 0)
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex, colIndex - 1].position + Vector3.up;
                colIndex--;
            }
            else if (colIndex == 0) 
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex, colIndex].position + Vector3.up;
            }

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (colIndex < grid.GetComponent<GridManager>().grid.GetLength(1) - 1)
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex, colIndex + 1].position + Vector3.up;
                colIndex++;
            }
            else if (colIndex == grid.GetComponent<GridManager>().grid.GetLength(1) - 1)
            {
                transform.position = grid.GetComponent<GridManager>().grid[rowIndex, colIndex].position + Vector3.up;
            }
        }
    }
    void Marking() 
    {
        //Marking your Mark on the grid
        if (isPlayerOne)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (grid.GetComponent<GridManager>().grid[rowIndex, colIndex].cubeObj.GetComponentInChildren<TextMeshPro>().text == "")
                {
                    grid.GetComponent<GridManager>().grid[rowIndex, colIndex].cubeObj.GetComponentInChildren<TextMeshPro>().text = "X";
                    grid.GetComponent<GridManager>().grid[rowIndex, colIndex].isCheckPlayer = true;
                    Checking();
                    isPlayerOne = !isPlayerOne;
                    isPlayerTwo = !isPlayerTwo;
                }
            }
           
        }
        else if (isPlayerTwo)
        {
            /*if (grid.GetComponent<GridManager>().grid[rowIndex, colIndex].cubeObj.GetComponentInChildren<TextMeshPro>().text == "") 
            {
                grid.GetComponent<GridManager>().grid[rowIndex, colIndex].cubeObj.GetComponentInChildren<TextMeshPro>().text = "O";
                grid.GetComponent<GridManager>().grid[rowIndex, colIndex].isCheckEnemy = true;
                Checking();
                isPlayerOne = !isPlayerOne;
                isPlayerTwo = !isPlayerTwo;
            }*/
            AIPlayer();
        }
        
    }

    void AIPlayer() 
    {
        int[,] solution = new int[grid.GetComponent<GridManager>().grid.GetLength(0), grid.GetComponent<GridManager>().grid.GetLength(1)];
        for (int i = 0; i < grid.GetComponent<GridManager>().grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetComponent<GridManager>().grid.GetLength(1); j++)
            {
                if (grid.GetComponent<GridManager>().grid[i, j].isCheckPlayer)
                {
                    solution[i, j] = 1;
                }
                else if (grid.GetComponent<GridManager>().grid[i, j].isCheckEnemy)
                {
                    solution[i, j] = 2;
                }
                else
                {
                    solution[i, j] = 0;
                }
            }
        }
        
        for (int i = 0; i < solution.GetLength(0); i++) 
        {
            for (int j = 0; j < solution.GetLength(1); j++) 
            {
                if (solution[i, j] == 0) 
                {
                    grid.GetComponent<GridManager>().grid[i, j].cubeObj.GetComponentInChildren<TextMeshPro>().text = "O";
                    grid.GetComponent<GridManager>().grid[i, j].isCheckEnemy = true;
                    isPlayerOne = !isPlayerOne;
                    isPlayerTwo = !isPlayerTwo;
                    goto LoopBreak;
                }
            }
        }
    LoopBreak:;
    }
    void Checking() 
    {
        //Array for solutions
        int[,] solution = new int[grid.GetComponent<GridManager>().grid.GetLength(0), grid.GetComponent<GridManager>().grid.GetLength(1)];
        for (int i = 0; i < grid.GetComponent<GridManager>().grid.GetLength(0); i++) 
        {
            for (int j = 0; j < grid.GetComponent<GridManager>().grid.GetLength(1); j++) 
            {
                if (grid.GetComponent<GridManager>().grid[i, j].isCheckPlayer)
                {
                    solution[i, j] = 1;
                }
                else if (grid.GetComponent<GridManager>().grid[i, j].isCheckEnemy)
                {
                    solution[i, j] = 2;
                }
                else 
                {
                    solution[i, j] = 0;
                }
            }
        }

        //To display Array
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    Debug.Log("Array :" + solution[i, j]);
                }
            }
        }
        //SOLUTION ALGO

        //Checking Rows
        for (int i = 0; i < solution.GetLength(0); i++) 
        {
            if (isPlayerOne)
            {
                if (solution[i, colIndex] != 1)
                {
                    break;
                }
                if (i == solution.GetLength(0) - 1)
                {
                    Debug.Log("Player 1 Wins");
                    playerWin = true;
                }
            }
            else if (isPlayerTwo) 
            {
                if (solution[i, colIndex] != 2)
                {
                    break;
                }
                if (i == solution.GetLength(0) - 1)
                {
                    Debug.Log("Player 2 Wins");
                    enemyWin = true;
                }
            }
           
        }
        //Checking Columns
        for (int i = 0; i < solution.GetLength(1); i++)
        {
            if (isPlayerOne) 
            {
                if (solution[rowIndex, i] != 1)
                {
                    break;
                }
                if (i == solution.GetLength(1) - 1)
                {
                    Debug.Log("Player 1 Wins");
                    playerWin = true;

                }
            }
            else if (isPlayerTwo) 
            {
                if (solution[rowIndex, i] != 2)
                {
                    break;
                }
                if (i == solution.GetLength(1) - 1)
                {
                    Debug.Log("Player 2 Wins");
                    enemyWin = true;

                }
            }
            
        }
        //Checking Diagonal
        for (int i = 0; i < grid.GetComponent<GridManager>().grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetComponent<GridManager>().grid.GetLength(1); j++)
            {
                if (i == j) 
                {
                    if (isPlayerOne)
                    {
                        if (solution[i, j] != 1)
                        {
                            goto loopBreak;
                        }
                        if (i == solution.GetLength(0) - 1)
                        {
                            Debug.Log("Player 1 Wins");
                            playerWin = true;

                        }
                    }
                    else if (isPlayerTwo)
                    {
                        if (solution[i, j] != 2)
                        {
                            goto loopBreak;
                        }
                        if (i == solution.GetLength(0) - 1)
                        {
                            Debug.Log("Player 2 Wins");
                            enemyWin = true;

                        }
                    }
                    
                }
            }
        }
        loopBreak:

        //Anti Diagonal
        for (int i = 0; i < grid.GetComponent<GridManager>().grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetComponent<GridManager>().grid.GetLength(1); j++)
            {
                if (i + j == grid.GetComponent<GridManager>().grid.GetLength(0)-1)
                {
                    if (isPlayerOne) 
                    {
                        if (solution[i, j] != 1)
                        {
                            goto loopBreakAnti;
                        }
                        if (i == solution.GetLength(0) - 1)
                        {
                            Debug.Log("Player 1 Wins");
                            playerWin = true;
                        }
                    }
                    else if (isPlayerTwo) 
                    {
                        if (solution[i, j] != 2)
                        {
                            goto loopBreakAnti;
                        }
                        if (i == solution.GetLength(0) - 1)
                        {
                            Debug.Log("Player 2 Wins");
                            enemyWin = true;
                        }
                    }
                    
                }
            }
        }
    loopBreakAnti:;

    }
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid");
        isPlayerOne = true;
        isPlayerTwo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerWin && !enemyWin) 
        {
            Movement();
            Marking();
        }
        
    }
}
