using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{
    [SerializeField] private Text timeScale;
    [SerializeField] private float moveSpeed = 5f;        // Скорость перемещения
    [SerializeField] private Transform movePoint;         // Вспомогательная точка-таргет
    [SerializeField] private LayerMask obstaclesLayer;    // Слои объектов, через которые нельзя пройти
    [SerializeField] private LayerMask floorLayer;        // Слои объектов пола

    private bool isMoving = false;
    private bool hasFallen = false;                       // Флаг: игрок уже упал (чтобы не спамить в лог)
    private const float tileSize = 1f;                    // Размер одной ячейки сетки

    private void Start()
    {
        movePoint.parent = null;
        movePoint.position = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f && !isMoving)
        {
            if (!IsOnFloor(transform.position) && !hasFallen)
            {
                hasFallen = true;
                Debug.Log("упал");
                timeScale.text = "0f";
                // Time.timeScale = 0f;
                return;
            }

            float inputX = Input.GetAxisRaw("Horizontal"); 
            float inputY = Input.GetAxisRaw("Vertical"); 
            if (inputX != 0) inputY = 0;

            if (inputX != 0 || inputY != 0)
            {
                Vector3 targetPosition = movePoint.position + new Vector3(inputX * tileSize, inputY * tileSize, 0f);
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

        while (Vector3.Distance(transform.position, movePoint.position) > 0.001f)
        {
            yield return null;
        }
        isMoving = false;
    }

    private bool IsOnFloor(Vector3 pos)
    {
        return Physics2D.OverlapPoint(pos, floorLayer) != null;
    }

    private bool IsObstacleAt(Vector3 targetPos)
    {
        return Physics2D.OverlapCircle(targetPos, tileSize*0.45f, obstaclesLayer);
    }
}
