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
    public TerominoColor terominoColor;                 // 테트로미노 색깔
    public GameObject emptyBlock;                       // 빈 블럭
    public GameObject bar;                              // 테두리
    public GameObject[] blocks = new GameObject[7];     // 7개 블럭

    public int[,] board = new int[24, 12];
    public GameObject[,] renderBoard = new GameObject[24, 12];

    // 테트로미노컬러 오브젝트의 컴포넌트인 Tetromino 스크립트를 가져오겠다!
    //Tetromino tetroColor = GameObject.Find("Tetromino").GetComponent<Tetromino>();

    // 생성될 위치
    public int startPosX = 4;
    public int startPosY = 20; 
    
    public int randomTetromino; // 테트로미노 랜덤으로 인덱스 주기위한 변수

    public int[,,] TetrominoBlock; // 7개의 테트로미노 모양을 가지고있는 배열

    SpriteRenderer SpRenderer;
    Color m_NewColor;

    // spreteRender에 사용할 색깔
    public float Red, Blue, Green;

    private void Awake()
    {
        SpRenderer = GetComponent<SpriteRenderer>();

        SpRenderer.color = Color.black;

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
        // 보드 초기화
        InitBoard();
    }

    // Update is called once per frame
    void Update()
    {
        // 테트리로미노 출력 테스트용
        //PrintTetromino();
         if (Input.GetKey(KeyCode.DownArrow))
        {
            // board[i, j] = -1;
            if(startPosY >= 0)
            {
                startPosY += -1;
            }
        }


        // 생성될 테트로미노를 상단 중앙으로 위치시키는 함수
        TetrominoPostionSetting();

        // 실제로 테트리스를 그려주는 함수.
        RenderTetrisBoard();

        // 보드 초기화
        //InitBoard();

       
    }

    // 테트리스 판 초기화
    public void InitBoard()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
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

    // 생성될 테트로미노를 상단 중앙으로 위치시키는 함수
    public void TetrominoPostionSetting()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if ((i >= 0 && i < 4) && (j >= 0 && j < 4))
                {
                    board[i + startPosY, j + startPosX] = TetrominoBlock[randomTetromino, i, j];
                }

            }
        }
    }

    // 테트리미노 출력 되나 확인
    public void PrintTetromino()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[6], new Vector3((j + 20) * 1.5f, (i + 20) * 1.5f, 0f), Quaternion.identity); //.GetComponent<SpriteRenderer>.color(255, 156, 0, 255); 
                                                                                                                                    //Instantiate(blocks[6], new Vector3((j+20) * 1.5f, (i+20) * 1.5f, 0f), Quaternion.identity);

                /*
                 * GetComponent<Renderer>().material = newMaterrial..GetComponent<Renderer>().materia
       
                 * 
                 */
            }
        }
    }

    // 실제로 테트리스를 그려주는 함수.
    public void RenderTetrisBoard()
    {
        for (int i = 0; i < 24; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (board[i, j] == 1)
                {
                    m_NewColor = new Color(1, 1, 1);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(bar, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                else if (board[i, j] == 0)
                {
                    m_NewColor = new Color(0, 0, 0);
                    SpRenderer.color = m_NewColor;
                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(emptyBlock, new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // I
                else if (board[i, j] == 2)
                {
                    m_NewColor = new Color(0, 252, 255);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[0], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // L
                else if (board[i, j] == 3)
                {
                    m_NewColor = new Color(0, 55, 255);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[1], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // J
                else if (board[i, j] == 4)
                {
                    m_NewColor = new Color(255, 56, 0);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[2], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // O
                else if (board[i, j] == 5)
                {
                    m_NewColor = new Color(246, 255, 0);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[3], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // S
                else if (board[i, j] == 6)
                {
                    m_NewColor = new Color(28, 255, 0);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[4], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // T
                else if (board[i, j] == 7)
                {
                    m_NewColor = new Color(233, 255, 0);
                    SpRenderer.color = m_NewColor;

                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[5], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
                // J
                else if (board[i, j] == 8)
                {
                    m_NewColor = new Color(255, 156, 0);
                    SpRenderer.color = m_NewColor;
                    renderBoard[i, j].GetComponent<SpriteRenderer>().color = SpRenderer.color; //Instantiate(blocks[6], new Vector3(j * 1.5f, i * 1.5f, 0f), Quaternion.identity);
                }
            }
        }
    }
}
