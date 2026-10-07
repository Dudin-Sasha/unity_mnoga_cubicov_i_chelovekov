using System.Collections;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;        // Скорость перемещения
    [SerializeField] private Transform movePoint;         // Вспомогательная точка-таргет
    [SerializeField] private LayerMask obstaclesLayer;    // Слои объектов, через которые нельзя пройти

    private bool isMoving = false;
    private const float tileSize = 1f;                    // Размер одной ячейки сетки

    private void Start()
    {
        // Отвязываем точку-таргет от родителя, чтобы двигать её независимо
        movePoint.parent = null;
        // Задаем стартовую позицию
        movePoint.position = transform.position;
    }

    private void Update()
    {
        // Плавно перемещаем игрока к целевой точке каждую секунду
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Проверяем ввод только тогда, когда игрок почти дошел до предыдущей цели
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && !isMoving)
        {
            float inputX = Input.GetAxisRaw("Horizontal"); // Получаем -1, 0 или 1
            float inputY = Input.GetAxisRaw("Vertical");   // Получаем -1, 0 или 1

            // Запрещаем движение по диагонали (выбираем приоритетную ось)
            if (inputX != 0) inputY = 0;

            if (inputX != 0 || inputY != 0)
            {
                Vector3 targetPosition = movePoint.position + new Vector3(inputX * tileSize, inputY * tileSize, 0f);

                // Если путь свободен — обновляем целевую точку
                if (!IsObstacleAt(targetPosition))
                {
                    StartCoroutine(MoveToGrid(targetPosition));
                }
            }
        }
    }

    private IEnumerator MoveToGrid(Vector3 targetPos)
    {
        isMoving = true;
        movePoint.position = targetPos;

        // Ждем, пока игрок физически не доберется до новой точки
        while (Vector3.Distance(transform.position, movePoint.position) > 0.001f)
        {
            yield return null;
        }

        isMoving = false;
    }

    // Проверка препятствий с помощью маленького невидимого круга (OverLapCircle)
    private bool IsObstacleAt(Vector3 targetPos)
    {
        // 0.2f — радиус проверки (должен быть меньше, чем половина размера плитки)
        return Physics2D.OverlapCircle(targetPos, 0.45f, obstaclesLayer);
    }
}
