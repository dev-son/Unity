using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapArray
{

    public GameObject[] Map;
}

public enum TerominoColor
{
    Empty = 0,
    BlockLine = 1,
    I = 2,
    L,
    J,
    O,
    S,
    T,
    Z
}

public class CreateBoard : MonoBehaviour
{
    public TerominoColor terominoColor; // 테트로미노 색깔
    public GameObject emptyBlock;
    public GameObject bar;
    public GameObject[] blocks = new GameObject[7];

    public int[,] board = new int[24, 12];
    public GameObject[,] renderBoard = new GameObject[24, 12];

    // 테트로미노컬러 오브젝트의 컴포넌트인 Tetromino 스크립트를 가져오겠다!
    //Tetromino tetroColor = GameObject.Find("Tetromino").GetComponent<Tetromino>();

    public int startPosX = 3;
    public int startPosY = 0;
    public int randomTetromino;

    public int[,,] TetrominoBlock;

    private void Awake()
    {
        randomTetromino = Random.Range(0, 7);
        TetrominoBlock = new int[7, 4, 4]
            {
        // I
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 2, 2, 2, 2 },
            { 0, 0, 0, 0 }
        },    
        
        // L
        {
            { 0, 0, 0, 0 },
            { 0, 3, 0, 0 },
            { 0, 3, 3, 3 },
            { 0, 0, 0, 0 }
        },     
        
        // J
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 4 },
            { 0, 4, 4, 4 },
            { 0, 0, 0, 0 }
        },             

        // O
        {
            { 0, 0, 0, 0 },
            { 0, 5, 5, 0 },
            { 0, 5, 5, 0 },
            { 0, 0, 0, 0 }
        },     
          
        // S
        {
            { 0, 0, 0, 0 },
            { 0, 0, 6, 6 },
            { 0, 6, 6, 0 },
            { 0, 0, 0, 0 }
        },    
          
        // T
        {
            { 0, 0, 0, 0 },
            { 0, 0, 7, 0 },
            { 0, 7, 7, 7 },
            { 0, 0, 0, 0 }
        },     
          
        // Z
        {
            { 0, 0, 0, 0 },
            { 0, 8, 8, 0 },
            { 0, 0, 8, 8 },
            { 0, 0, 0, 0 }
        },
            };
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (i == 0 || j == 0 || j == 11)
                {
                    board[i, j] = 1;
                    renderBoard[i, j] = Instantiate(bar, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                else
                {
                    board[i, j] = 0;
                    renderBoard[i, j] = Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if ((i >= 0 && i < 4) || (j >= 0 && j < 4))
                {
                    // int a 
                    board[j - startPosY, i - startPosX] = TetrominoBlock[randomTetromino, j, i];

                }
            }
        }



        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (board[i, j] == 1)
                {
                    renderBoard[i, j] = Instantiate(bar, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                else if (board[i, j] == 0)
                {
                    renderBoard[i, j] = Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // I
                else if (board[i, j] == 2)
                {
                    renderBoard[i, j] = Instantiate(blocks[0], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // L
                else if (board[i, j] == 3)
                {
                    renderBoard[i, j] = Instantiate(blocks[1], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // J
                else if (board[i, j] == 4)
                {
                    renderBoard[i, j] = Instantiate(blocks[2], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // O
                else if (board[i, j] == 5)
                {
                    renderBoard[i, j] = Instantiate(blocks[3], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // S
                else if (board[i, j] == 6)
                {
                    renderBoard[i, j] = Instantiate(blocks[4], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // T
                else if (board[i, j] == 7)
                {
                    renderBoard[i, j] = Instantiate(blocks[5], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // J
                else if (board[i, j] == 8)
                {
                    renderBoard[i, j] = Instantiate(blocks[6], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
            }
        }

        //PrintTetris();
    }



    public void PrintTetris()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if ((i >= 0 && i < 4) || (j >= 0 && j < 4) )
                {
                    // int a 
                    board[j - startPosY, i - startPosX] = TetrominoBlock[randomTetromino, j, i];

                }
            }
        }
    }

}
