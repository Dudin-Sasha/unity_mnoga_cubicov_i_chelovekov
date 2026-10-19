using UnityEngine;

public class SharedVariables : MonoBehaviour
{

    public static SharedVariables Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }


    [Header("ячейки")]
        [Tooltip("нужна для генерации, расчета длины шага, и для расчёта падения камней")] public float sellSize = 1f; 
        [Tooltip("Высота карты")] public int gridHeight = 10;
        [Tooltip("Ширина карты")] public int gridWidth = 10;

        [Tooltip("Примерное время за которое ячейка удалится")] public float timeToDeleteFloor = 1f;
        [Tooltip("количество шагов за которые прозрачность упадет до нуля, и удалится объект")] public int oppSteps = 10;


    [Header("Настройки уровня")]
        public int stonesCount = 5;
        public int heartsCount = 5;

    [Header("камни")]
        [Tooltip("Примерное время до падения камня")]public float timeToStoneFall = 1f;


    [Header("игрок")]
        [Tooltip("Скорость игрока")] public float playerSpeed = 5f;

    [Header("Общее")]
        public LayerMask floorLayer;
        public LayerMask stoneLayer;

}
