using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    public int rows;
    public int cols;
    public float cellSize;
    public List<List<Vector3>> points;
    public Vector2 minMaxX;
    public Vector2 minMaxY;

    public Grid(int rows,int cols, float cellSize)
    {
        this.rows = rows;
        this.cols = cols;
        this.cellSize = cellSize;

        points = CreateGrid(this.rows,this.cols,this.cellSize);
        minMaxX = new Vector2(0, this.cellSize * this.rows);
        minMaxY = new Vector2(0, -1*this.cellSize * this.cols);
    }

    public List<List<Vector3>> CreateGrid(int rows, int cols, float cellSize)
    {
        List<List<Vector3>> grid = new List<List<Vector3>>();

        for (int x = 0; x < rows; x++)
        {
            grid.Add(new List<Vector3>());
            for (int y = 0; y < cols; y++)
            {
                var point = new Vector3(1 * x * cellSize, -1 * y * cellSize, 0);
                grid[x].Add(point);

            }
        }

        return grid;
    }
}

public class GridTest : MonoBehaviour
{
    [SerializeField]public  int ROWS = 5;
    public  int COLS = 2;    
    private Grid grid;
    public float cellSize = 10;
    private List<List<GameObject>> cubes;
    public Material mat;
    public Vector2 cellIndex;
   
    void Start()
    {
        cubes = new List<List<GameObject>>();
        grid = new Grid(ROWS,COLS,cellSize);
        FillGridWithCubes(grid);
        
        ColourThisCell(cellIndex);
    }
    
    private void FillGridWithCubes(Grid g)
    {
        Vector3 cellCentreOffset = new Vector3(g.cellSize / 2, -1*(g.cellSize / 2), 0);

        for (int x = 0; x < g.rows; x++)
        {
            cubes.Add(new List<GameObject>());
            for (int y = 0; y < g.cols; y++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = g.points[x][y] + cellCentreOffset;
                cube.transform.localScale = new Vector3(g.cellSize, g.cellSize, g.cellSize);
                cube.GetComponent<Renderer>().material = mat;
                cubes[x].Add(cube);
            }
        }
    }

    private void ColourThisCell(Vector2 index)
    {
        var mat = cubes[(int)index.x][(int)index.y].GetComponent<Renderer>().material;
        mat.color = Color.HSVToRGB(index.x / cubes.Count, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        cellIndex = GetActiveCell(grid);
        ColourThisCell(cellIndex);
    }  

    private Vector2 GetActiveCell(Grid g)
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var cell = Vector2.zero;
        if (mousePos.x > g.minMaxX.x 
            && mousePos.x < g.minMaxX.y
            && mousePos.y < g.minMaxY.x
            && mousePos.y > g.minMaxY.y)
        {
            int xIndex = Mathf.Abs(Mathf.FloorToInt(mousePos.x / g.cellSize));
            int yIndex = Mathf.Abs(Mathf.FloorToInt(1 + mousePos.y / g.cellSize));
            cell = new Vector2(xIndex, yIndex);
        }

            return cell;
    }
}
