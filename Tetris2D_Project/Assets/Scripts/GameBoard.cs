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



public class GameBoard : MonoBehaviour
{
    public const int HorzontalBoard = 12;
    public const int VerticalBoard = 24;

    public TerominoColor terominoColor;                 // 테트로미노 색깔
    public GameObject emptyBlock;                       // 빈 블럭
    public GameObject bar;                              // 테두리
    public GameObject[] blocks = new GameObject[7];     // 7개 블럭

    public int[,] board = new int[24, 12];
    public GameObject[,] renderBoard = new GameObject[24, 12];
    public int[,] fillBoard = new int[24, 12];          // 바닥에 닿였을 때 고정시킬 배열


    // 테트로미노컬러 오브젝트의 컴포넌트인 Tetromino 스크립트를 가져오겠다!
    //Tetromino tetroColor = GameObject.Find("Tetromino").GetComponent<Tetromino>();

    // 생성될 위치
    public int startPosX = 4;
    public int startPosY = 20;

    // 이동 시킬 좌표
    public int movePosX = 0;
    public int movePosY = 0;

    // 테트로미노 랜덤으로 인덱스 주기위한 변수
    public int randomTetromino;

    // 7개의 테트로미노 모양을 가지고있는 배열
    public int[,,,] TetrominoBlock; 

    // 테트로미노를 바꾸는 변수
    public int changeTetromino = 0;
    public const int TETROMINO_CHANGE_COUNT = 4;

    /// 색깔 변환 관련
    SpriteRenderer SpRenderer;
    Color m_NewColor;

    // spreteRender에 사용할 색깔
    public float Red, Blue, Green;

    // rotationRightChek
    public bool RightRotationCheck = false;
    public bool LeftRotationCheck = false;

    /// <summary>
    /// 상수 모음 ///////////////////////////////////////////////////////
    /// </summary>
    public const int EMPTY = 0;
    public const int WALL = 1;
    public const int I_TETROMINO = 2;
    public const int L_TETROMINO = 3;
    public const int J_TETROMINO = 4;
    public const int O_TETROMINO = 5;
    public const int S_TETROMINO = 6;
    public const int T_TETROMINO = 7;
    public const int Z_TETROMINO = 8;

    public const int TETROMINO_SIZE = 7;
    public const int TETROMINO_ARRAY_SIZE = 4;
    /// <summary>
    /// ////////////////////////////////////////////////////////////////
    /// </summary>

    // I 블럭을 위한 변수
    public bool IBlock = false;

    // 블럭의 종류
    public int blocksKind;

    //enum CShape
    //{
    //    line,
    //    rect,

    //};
    //CShape m_Shape;

       

    private void Awake()
    {
        SpRenderer = GetComponent<SpriteRenderer>();
        //m_Shape = CShape.line;

        SpRenderer.color = Color.black;

        randomTetromino = Random.Range(1, 7);
        TetrominoBlock = new int[7, 4, 4, 4]
        {
        // I
            {
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                            { I_TETROMINO, I_TETROMINO, I_TETROMINO, I_TETROMINO },
                            { 0, 0, 0, 0 },
                        },
                        {
                            { 0, 0, I_TETROMINO, 0 },
                            { 0, 0, I_TETROMINO, 0 },
                            { 0, 0, I_TETROMINO, 0 },
                            { 0, 0, I_TETROMINO, 0 },
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                            { I_TETROMINO, I_TETROMINO, I_TETROMINO, I_TETROMINO },
                            { 0, 0, 0, 0 },
                        },
                         {
                           { 0, 0, I_TETROMINO, 0 },
                            { 0, 0, I_TETROMINO, 0 },
                            { 0, 0, I_TETROMINO, 0 },
                            { 0, 0, I_TETROMINO, 0 },
                        },

            },
                 // L
            {
                         {
                             { 0, 0, 0, 0 },
                             { 0, L_TETROMINO, 0, 0 },
                             { 0, L_TETROMINO, L_TETROMINO, L_TETROMINO },
                             { 0, 0, 0, 0 }
                         },
                         {
                             { 0, 0,          L_TETROMINO, 0 },
                             { 0, 0,           L_TETROMINO, 0 },
                             { 0, L_TETROMINO, L_TETROMINO, 0 },
                             { 0, 0, 0, 0 }
                         },
                          {
                             { 0, 0, 0, 0 },
                             { L_TETROMINO, L_TETROMINO, L_TETROMINO, 0 },
                             { 0, 0,                     L_TETROMINO, 0 },
                             { 0, 0, 0, 0 }
                         },
                         {
                             { 0, 0, 0, 0 },
                             { 0, L_TETROMINO, L_TETROMINO, 0 },
                             { 0, L_TETROMINO, 0, 0 },
                             { 0, L_TETROMINO, 0, 0 }
                         },
            },
            // J
            { 
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0,                     J_TETROMINO, 0 },
                            { J_TETROMINO, J_TETROMINO, J_TETROMINO, 0 },
                            { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, J_TETROMINO,J_TETROMINO, 0 },
                            { 0, 0,          J_TETROMINO, 0 },
                            { 0, 0,          J_TETROMINO, 0 },
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, J_TETROMINO, J_TETROMINO, J_TETROMINO },
                            { 0, J_TETROMINO, 0, 0 },
                            { 0, 0, 0, 0 }
                        },
                        {
                            { 0, J_TETROMINO, 0, 0 },
                            { 0, J_TETROMINO, 0, 0 },
                            { 0, J_TETROMINO, J_TETROMINO, 0 },
                            { 0, 0, 0, 0 }
                        },
            },
            // O
            { 
                        {
                            { 0, 0, 0, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, O_TETROMINO, O_TETROMINO, 0 },
                            { 0, 0, 0, 0 }
                        },
              },
            // S
            {
                        {
                             { 0, 0, 0, 0 },
                             { 0, 0,            S_TETROMINO, S_TETROMINO },
                             { 0,S_TETROMINO,   S_TETROMINO, 0 },
                             { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, S_TETROMINO, 0, 0 },
                            { 0, S_TETROMINO, S_TETROMINO, 0 },
                            { 0, 0,           S_TETROMINO, 0 }
                        },
                          {
                               { 0, 0, 0, 0 },
                             { 0, 0,            S_TETROMINO, S_TETROMINO },
                             { 0,S_TETROMINO,   S_TETROMINO, 0 },
                             { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, S_TETROMINO, 0, 0 },
                            { 0, S_TETROMINO, S_TETROMINO, 0 },
                            { 0, 0,           S_TETROMINO, 0 }
                        },

            },

            // T
            { 
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0,           T_TETROMINO, 0 },
                            { 0, T_TETROMINO, T_TETROMINO, T_TETROMINO },
                            { 0, 0, 0, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0,          T_TETROMINO, 0 },
                            { 0, T_TETROMINO,T_TETROMINO, 0 },
                            { 0, 0,          T_TETROMINO, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0, 0, 0 },
                            { 0, T_TETROMINO, T_TETROMINO, T_TETROMINO },
                            { 0, 0,           T_TETROMINO, 0 }
                        },
                        {
                            { 0, 0, 0, 0 },
                            { 0, 0, T_TETROMINO, 0 },
                            { 0, 0, T_TETROMINO, T_TETROMINO },
                            { 0, 0, T_TETROMINO, 0 }
                        },
            },
            // Z
            
            {
                        { 
                             { 0, 0, 0, 0 },
                             { 0,   Z_TETROMINO, Z_TETROMINO, 0 },
                             { 0,             0, Z_TETROMINO, Z_TETROMINO },
                             { 0, 0, 0, 0 }
                        },

                        {
                             { 0, 0, 0, 0 },
                             { 0, 0          , Z_TETROMINO, 0 },
                             { 0, Z_TETROMINO, Z_TETROMINO, 0 },
                             { 0, Z_TETROMINO, 0, 0 }
                        },

                        {
                             { 0, 0, 0, 0 },
                             { 0,   Z_TETROMINO, Z_TETROMINO, 0 },
                             { 0,             0, Z_TETROMINO, Z_TETROMINO },
                             { 0, 0, 0, 0 }
                        },

                        {
                             { 0, 0, 0, 0 },
                             { 0, 0          , Z_TETROMINO, 0 },
                             { 0, Z_TETROMINO, Z_TETROMINO, 0 },
                             { 0, Z_TETROMINO, 0, 0 }
                        },
            },
        
        };
    }


    // Start is called before the first frame update
    void Start()
    {
        // 보드 초기화
        InitBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (randomTetromino < TETROMINO_SIZE)
            {
                if (randomTetromino != 6)
                {
                    randomTetromino++;
                }
                else
                {
                    randomTetromino = 0;
                }
            }            
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // 테트리미노 
            if(changeTetromino < TETROMINO_CHANGE_COUNT)
            {
                if( changeTetromino != 3)
                { 
                    changeTetromino++;
                    if(ChangeCheckTetris() == true)
                    {
                    }
                    else if(ChangeCheckTetris() == false && LeftRotationCheck == true && IBlock == true)
                    {
                        movePosX += 2;
                        LeftRotationCheck = false;
                        IBlock = false;
                    }
                    else if(ChangeCheckTetris() == false && LeftRotationCheck == true)
                    {
                        movePosX += 1;
                        LeftRotationCheck = false;
                    }
                    else if(ChangeCheckTetris() == false && RightRotationCheck == true)
                    {
                        movePosX += -1;
                        RightRotationCheck = false;
                    }

                }
                else
                {
                    changeTetromino = 0;
                    if (ChangeCheckTetris() == true)
                    {
                    }
                    else if (ChangeCheckTetris() == false)
                    {
                        movePosX += 1;
                        //if(RightRotationCheck == false)
                        //{
                        //    movePosX += -1;
                        //}
                    }
                }
            }
           
        }


        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (DownCheckTetris() == true)
            {
                movePosY += -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (LeftCheckTetris() == true)
            {
                movePosX += -1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (RightCheckTetris() == true)
            {
                movePosX += 1;
            }

        }

        // 보드 그려줌
        BaseBoard();

        // 생성될 테트로미노를 상단 중앙으로 위치시키는 함수
        TetrominoPostionSetting();

        // 실제로 테트리스를 그려주는 함수.
        RenderTetrisBoard();

    }

    // 테트리스 판 초기화
    public void InitBoard()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (i == 0 || j == 0 || j == 11)
                {
                    board[i, j] = 1;
                    renderBoard[i, j] = Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                else
                {
                    board[i, j] = 0;
                    renderBoard[i, j] = Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
            }
        }
    }

    public void BaseBoard()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (i == 0 || j == 0 || j == 11)
                {
                    board[i, j] = 1;
                }
                else if (DownCheckTetris() == false)
                {
                    if (i < 4 && j < 4)
                    {
                        board[i/*+ movePosY*/, j + movePosX + startPosX] = TetrominoBlock[randomTetromino, changeTetromino, i, j];

                    }
                }
                else 
                {
                    board[i, j] = 0;
                }
            }
        }
    }

    // 생성될 테트로미노를 상단 중앙으로 위치시키는 함수
    public void TetrominoPostionSetting()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if ((i >= 0 && i < 4) && (j >= 0 && j < 4))
                {
                    /// 문제점 1. 
                    /// 
                    // 바닥에 닿였다면
                    if(DownCheckTetris() == false)
                    {
                        // +2 한 이유는 랜덤테트리미노 는 0 ~ 6까지 인데 테트리미노 실제 들어있는 값은 2~8까지 라서 
                        if (TetrominoBlock[randomTetromino, changeTetromino, i, j] == randomTetromino+2)
                        {
                            board[i  , j + movePosX + startPosX] = TetrominoBlock[randomTetromino, changeTetromino, i, j] + 10;
                        }

                      
                    }
                    // 바닥에 안 닿였다면
                    if(DownCheckTetris() == true)
                    {
                        if (TetrominoBlock[randomTetromino, changeTetromino, i, j] == randomTetromino + 2)
                        {
                            board[i + startPosY + movePosY, j + startPosX + movePosX] = TetrominoBlock[randomTetromino, changeTetromino, i, j];
                        }
                    }
                  
                }
            }
        }
       if (DownCheckTetris() == false)
        {
            startPosX = 4;
            startPosY = 20;
            movePosX = 0;
            movePosY = 0;
        }
       
    }

 

    // 실제로 테트리스를 그려주는 함수.
    public void RenderTetrisBoard()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                blocksKind = board[i, j];
                switch (blocksKind)
                {
                    case EMPTY:
                        {
                            m_NewColor = new Color(100, 100, 100);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(bar, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case WALL:
                        {
                            m_NewColor = new Color(0, 0, 0);
                            SpRenderer.color = m_NewColor;
                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case I_TETROMINO:
                        {
                            m_NewColor = new Color(0, 252, 255);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[0], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;

                    case L_TETROMINO:
                        {
                            m_NewColor = new Color(0, 55, 255);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[1], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case J_TETROMINO:
                        {
                            m_NewColor = new Color(255, 156, 0);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[2], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case O_TETROMINO:
                        {
                            m_NewColor = new Color(246, 255, 0);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[3], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case S_TETROMINO:
                        {
                            m_NewColor = new Color(28, 255, 0);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[4], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case T_TETROMINO:
                        {
                            m_NewColor = new Color(233, 0, 255);
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[5], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case Z_TETROMINO:
                        {
                            m_NewColor = new Color(255, 0, 41);
                            SpRenderer.color = m_NewColor;
                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[6], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                        // -------------------------------
                    case I_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[0], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;

                    case L_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[1], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case J_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[2], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case O_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[3], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case S_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[4], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case T_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;

                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[5], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                    case Z_TETROMINO + 10:
                        {
                            m_NewColor = Color.green;
                            SpRenderer.color = m_NewColor;
                            renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[6], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                        }
                        break;
                }
            }
        }
    }

    // 체크를 해야한다. 정수형 배열이 비었는지 안 비었는지
    public bool LeftCheckTetris()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (board[i, j] != 0 && board[i, j] != 1 )
                {
                    if (board[i, j - 1] == 1) // 왼쪽이 벽을 만났다.
                    {
                        return false;
                    }
                    else
                    {
                        // 왼쪽 벽이없다?
                        continue;
                    }
                }
            }
        }
        return true;
    }

    public bool RightCheckTetris()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (board[i, j] != 0 && board[i, j] != 1)
                {
                    if (board[i, j + 1] == 1) // 오른쪽이 벽을 만났다.
                    {
                        return false;
                    }
                    else
                    {
                        // 오른쪽 벽이없다?
                        continue;
                    }
                }
            }
        }
        return true;
    }

    public bool DownCheckTetris()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (board[i, j] != 0 && board[i, j] != 1)
                {
                    if (board[i - 1, j] == 1) // 아래
                    {
                        return false;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        return true;
    }

    // 체인지 할 때 벽을 만났을 때
    public bool ChangeCheckTetris()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (board[i, j] != 0 && board[i, j] != 1 && board[i, j] != 5)
                {
                    if((board[i, j] == 2 && board[i, j - 1] == 1) || (board[i, j] == 2 && board[i, j - 2] == 1)) // I모양 블럭이면서 왼쪽 벽을 만났다.
                    {
                        IBlock = true;
                        LeftRotationCheck = true;
                        return false;
                    }
                    else if (board[i, j - 1] == 1) // 왼쪽이 벽을 만났다.
                    {
                        LeftRotationCheck = true;
                        return false;
                    }
                    else if(board[i, j + 1] == 1) // 오른쪽 벽을 만났다.
                    {
                        RightRotationCheck = true;
                        return false;
                    }
                    else
                    {
                        // 왼쪽 벽이없다?
                        continue;
                    }
                }
            }
        }
        return true;
    }
}
