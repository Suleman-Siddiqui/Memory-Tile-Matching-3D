using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;

    public int gridWidth = 11;
    public int gridHeight = 11;

   public  float hexWidth = 1.732f;
    public float hexHeight = 2.0f;
    public float gap = 0.0f;

    Vector3 startPos;
    private List<GameObject> hexaObj;

    [ContextMenu("Spawn")]
   public void Start1()
    {
        hexaObj = new List<GameObject>();
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    void CalcStartPos()
    {
        float offset = 0;

        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, z);
       // startPos = new Vector3(-1.50999999f, 0.407999992f, -0.560000002f);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.transform.localPosition = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
                hex.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                hexaObj.Add(hex.gameObject);
            }
        }
    }

    [ContextMenu("Delete")]
    public void DeleteHexa()
    {
        foreach(var hex in hexaObj)
        {
            Destroy(hex.gameObject);
        }
    }
}
