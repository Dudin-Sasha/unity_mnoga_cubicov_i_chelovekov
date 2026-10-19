using UnityEngine;
using Random = UnityEngine.Random;


public class LevelGenerator : MonoBehaviour
{
    [Header("Настройки сетки")]
    public GameObject floorPrefab;
    public GameObject heartsPrefab;
    public GameObject StonePrefab;
    public GameObject Player;


    [Tooltip("Левый нижний угол")] 
    public Vector2 FirstPosition;
    private int gridY;
    private int gridX;
    private float tileSize; 
    [SerializeField] private int stones;
    [SerializeField] private int hearts;

    void Start()
    {
        gridX = SharedVariables.Instance.gridWidth;
        gridY = SharedVariables.Instance.gridHeight;
        tileSize = SharedVariables.Instance.sellSize;
        stones = SharedVariables.Instance.stonesCount;
        hearts = SharedVariables.Instance.heartsCount;

        GenerateGrid();
        GenerateObj();
        GenerateAt(0,0,Player);
    }


    void GenerateGrid()
    {
        Debug.Log("1 stage of generate");
        for (int x = 0; x < gridX; x++ )
        {
            for (int y = 0; y < gridY; y++ )
            {
                GenerateAt(x,y,floorPrefab);
                Debug.Log($"y:{y} x:{x}");
            }
        }
    }
    void GenerateObj()
    {  
        int[][] ObjPosition = new int[hearts+stones][];

        for(int i = 0; i < ObjPosition.Length; i++)
        {
            int x = Random.Range(0,gridX);
            int y = Random.Range(0,gridY);

            if (!CheckObj(x,y,ObjPosition))
            {
                if(i>=hearts) GenerateAt(x,y,StonePrefab);
                else GenerateAt(x,y,heartsPrefab);

                ObjPosition[i] = new int[2];
                ObjPosition[i][0] = x;
                ObjPosition[i][1] = y;
            }
            else i--;
        }
    }

    bool CheckObj(int x, int y, int[][] ObjPosition)
    {
        foreach (var pos in ObjPosition)
        {
            if (pos == null) continue;
            else if (pos[0] == x && pos[1] == y)
            {
                return true;
            }
        }
        return false;
    }

    void GenerateAt(int x, int y, GameObject type)
    {
        Vector3 spawnPosition = new Vector3(FirstPosition.x + x * tileSize, FirstPosition.y + y * tileSize, 0);
        GameObject spawnTile = Instantiate(type, spawnPosition, Quaternion.identity);
        spawnTile.transform.parent = this.transform;
    }
}
