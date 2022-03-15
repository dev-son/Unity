using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapArray
{
   
    public GameObject[] Map;
}



public class CreateBoard : MonoBehaviour
{
    public Material BarMaterial;
    public Material BackGroundMaterial;
    public GameObject Block;
    public GameObject Bar;

    public int[ , ]Board = new int[12, 24];


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 24; j++)
            {
                if (i == 0 || i == 11 || j == 0)
                {
                    Instantiate(Bar, new Vector3(i * 1.5f, j * 1.5f, 0f), Quaternion.identity);
                }
                else
                {
                    Instantiate(Block, new Vector3(i * 1.5f, j * 1.5f, 0f), Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
