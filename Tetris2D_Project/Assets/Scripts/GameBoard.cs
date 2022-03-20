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

    public TerominoColor terominoColor;                 // ��Ʈ�ι̳� ����
    public GameObject emptyBlock;                       // �� ��
    public GameObject bar;                              // �׵θ�
    public GameObject[] blocks = new GameObject[7];     // 7�� ��

    public int[,] board = new int[24, 12];
    public GameObject[,] renderBoard = new GameObject[24, 12];
    public int[,] fillBoard = new int[24, 12];


    // ��Ʈ�ι̳��÷� ������Ʈ�� ������Ʈ�� Tetromino ��ũ��Ʈ�� �������ڴ�!
    //Tetromino tetroColor = GameObject.Find("Tetromino").GetComponent<Tetromino>();

    // ������ ��ġ
    public int startPosX = 4;
    public int startPosY = 20;

    // �̵� ��ų ��ǥ
    public int movePosX = 0;
    public int movePosY = 0;

    public int randomTetromino; // ��Ʈ�ι̳� �������� �ε��� �ֱ����� ����

    public int[,,,] TetrominoBlock; // 7���� ��Ʈ�ι̳� ����� �������ִ� �迭

    // ��Ʈ�ι̳븦 �ٲٴ� ����
    public int changeTetromino = 0;
    public const int TETROMINO_CHANGE_COUNT = 4;

    SpriteRenderer SpRenderer;
    Color m_NewColor;

    // spreteRender�� ����� ����
    public float Red, Blue, Green;

    // rotationRightChek
    public bool RightRotationCheck = false;
    public bool LeftRotationCheck = false;

    public const int EMPTY = 0;
    public const int WALL = 1;
    public const int I_TETROMINO = 2;
    public const int L_TETROMINO = 3;
    public const int J_TETROMINO = 4;
    public const int O_TETROMINO = 5;
    public const int S_TETROMINO = 6;
    public const int T_TETROMINO = 7;
    public const int Z_TETROMINO = 8;

    public const int TETROMINOSIZE = 7;

    // I���� ���� ����
    public bool IBlock = false;


    // ���� ����
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

        randomTetromino = Random.Range(1, 2);
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
                         { 0, 3, 0, 0 },
                         { 0, 3, 3, 3 },
                         { 0, 0, 0, 0 }
                     },
                     {
                         { 0, 0, 3, 0 },
                         { 0, 0, 3, 0 },
                         { 0, 3, 3, 0 },
                         { 0, 0, 0, 0 }
                     },
                      {
                         { 0, 0, 0, 0 },
                         { 3, 3, 3, 0 },
                         { 0, 0, 3, 0 },
                         { 0, 0, 0, 0 }
                     },
                     {
                         { 0, 0, 0, 0 },
                         { 0, 3, 3, 0 },
                         { 0, 3, 0, 0 },
                         { 0, 3, 0, 0 }
                     },
        },
        // J
        { 
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 4, 0 },
                        { 4, 4, 4, 0 },
                        { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 4, 4, 0 },
                        { 0, 0, 4, 0 },
                        { 0, 0, 4, 0 },
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 4, 4, 4 },
                        { 0, 4, 0, 0 },
                        { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 4, 0, 0 },
                        { 0, 4, 0, 0 },
                        { 0, 4, 4, 0 },
                        { 0, 0, 0, 0 }
                    },
        },
        // O
        { 
                    {
                        { 0, 0, 0, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 5, 5, 0 },
                        { 0, 0, 0, 0 }
                    },
          },
        // S
        {
                    {
                         { 0, 0, 0, 0 },
                         { 0, 0, 6, 6 },
                         { 0, 6, 6, 0 },
                         { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 6, 0, 0 },
                        { 0, 6, 6, 0 },
                        { 0, 0, 6, 0 }
                    },
                      {
                         { 0, 0, 0, 0 },
                         { 0, 0, 6, 6 },
                         { 0, 6, 6, 0 },
                         { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 6, 0, 0 },
                        { 0, 6, 6, 0 },
                        { 0, 0, 6, 0 }
                    },

        },

        // T
        { 
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 7, 0 },
                        { 0, 7, 7, 7 },
                        { 0, 0, 0, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 7, 0 },
                        { 0, 7, 7, 0 },
                        { 0, 0, 7, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 0, 0 },
                        { 0, 7, 7, 7 },
                        { 0, 0, 7, 0 }
                    },
                    {
                        { 0, 0, 0, 0 },
                        { 0, 0, 7, 0 },
                        { 0, 0, 7, 7 },
                        { 0, 0, 7, 0 }
                    },
        },
        // Z
        
        {
                    { 
                         { 0, 0, 0, 0 },
                         { 0, 8, 8, 0 },
                         { 0, 0, 8, 8 },
                         { 0, 0, 0, 0 }
                    },

                    {
                         { 0, 0, 0, 0 },
                         { 0, 0, 8, 0 },
                         { 0, 8, 8, 0 },
                         { 0, 8, 0, 0 }
                    },

                    {
                         { 0, 0, 0, 0 },
                         { 0, 8, 8, 0 },
                         { 0, 0, 8, 8 },
                         { 0, 0, 0, 0 }
                    },

                    {
                        { 0, 0, 0, 0 },
                         { 0, 0, 8, 0 },
                         { 0, 8, 8, 0 },
                         { 0, 8, 0, 0 }
                    },
        },
        
            };
    }


    // Start is called before the first frame update
    void Start()
    {
        // ���� �ʱ�ȭ
        InitBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (randomTetromino < TETROMINOSIZE)
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
            // ��Ʈ���̳� 
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

        // ���� �׷���
        BaseBoard();

        // ������ ��Ʈ�ι̳븦 ��� �߾����� ��ġ��Ű�� �Լ�
        TetrominoPostionSetting();

        // ������ ��Ʈ������ �׷��ִ� �Լ�.
        RenderTetrisBoard();

    }

    // ��Ʈ���� �� �ʱ�ȭ
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
                else if(DownCheckTetris() == false)
                {
                    fillBoard[i, j] = board[i, j];
                }
                else
                {
                    board[i, j] = 0;
                }
            }
        }
    }

    // ������ ��Ʈ�ι̳븦 ��� �߾����� ��ġ��Ű�� �Լ�
    public void TetrominoPostionSetting()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if ((i >= 0 && i < 4) && (j >= 0 && j < 4))
                {
                    if (TetrominoBlock[randomTetromino, changeTetromino, i, j] != 0)
                    {
                        board[i + startPosY + movePosY, j + startPosX + movePosX] = TetrominoBlock[randomTetromino, changeTetromino, i, j];
                    }
                }
            }
        }
    }

    // ��Ʈ���̳� ��� �ǳ� Ȯ��
    public void PrintTetromino()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[6], new Vector3((j + 20) * 1.5f, (i + 20) * 1.5f, 0f), Quaternion.identity); //.GetComponent<SpriteRenderer>.color(255, 156, 0, 255); 
            }
        }
    }

    // ������ ��Ʈ������ �׷��ִ� �Լ�.
    public void RenderTetrisBoard()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                blocksKind = board[i, j];
                switch(blocksKind)
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
                }

                
                //if (board[i, j] == 1)
                //{
                //    m_NewColor = new Color(1, 1, 1);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(bar, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //else if (board[i, j] == 0)
                //{
                //    m_NewColor = new Color(0, 0, 0);
                //    SpRenderer.color = m_NewColor;
                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// I
                //else if (board[i, j] == 2)
                //{
                //    m_NewColor = new Color(0, 252, 255);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[0], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// L
                //else if (board[i, j] == 3)
                //{
                //    m_NewColor = new Color(0, 55, 255);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[1], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// J
                //else if (board[i, j] == 4)
                //{
                //    m_NewColor = new Color(255, 56, 0);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[2], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// O
                //else if (board[i, j] == 5)
                //{
                //    m_NewColor = new Color(246, 255, 0);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[3], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// S
                //else if (board[i, j] == 6)
                //{
                //    m_NewColor = new Color(28, 255, 0);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[4], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// T
                //else if (board[i, j] == 7)
                //{
                //    m_NewColor = new Color(233, 255, 0);
                //    SpRenderer.color = m_NewColor;

                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[5], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
                //// Z
                //else if (board[i, j] == 8)
                //{
                //    m_NewColor = new Color(255, 156, 0);
                //    SpRenderer.color = m_NewColor;
                //    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[6], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                //}
            }
        }
    }

    // üũ�� �ؾ��Ѵ�. ������ �迭�� ������� �� �������
    public bool LeftCheckTetris()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (board[i, j] != 0 && board[i, j] != 1 )
                {
                    if (board[i, j - 1] == 1) // ������ ���� ������.
                    {
                        return false;
                    }
                    else
                    {
                        // ���� ���̾���?
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
                    if (board[i, j + 1] == 1) // �������� ���� ������.
                    {
                        return false;
                    }
                    else
                    {
                        // ������ ���̾���?
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
                    if (board[i - 1, j] == 1) // �Ʒ�
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

    // ü���� �� �� ���� ������ ��
    public bool ChangeCheckTetris()
    {
        for (int i = 0; i < VerticalBoard; i++)
        {
            for (int j = 0; j < HorzontalBoard; j++)
            {
                if (board[i, j] != 0 && board[i, j] != 1 && board[i, j] != 5)
                {
                    if((board[i, j] == 2 && board[i, j - 1] == 1) || (board[i, j] == 2 && board[i, j - 2] == 1)) // I��� ���̸鼭 ���� ���� ������.
                    {
                        IBlock = true;
                        LeftRotationCheck = true;
                        return false;
                    }
                    else if (board[i, j - 1] == 1) // ������ ���� ������.
                    {
                        LeftRotationCheck = true;
                        return false;
                    }
                    else if(board[i, j + 1] == 1) // ������ ���� ������.
                    {
                        RightRotationCheck = true;
                        return false;
                    }
                    else
                    {
                        // ���� ���̾���?
                        continue;
                    }
                }


            }


        }
        return true;
    }
}
