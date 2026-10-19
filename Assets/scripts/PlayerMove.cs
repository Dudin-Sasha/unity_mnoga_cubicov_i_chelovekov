using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class playerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform movePoint;         // Вспомогательная точка-таргет
    private LayerMask obstaclesLayer;    // Слои объектов, через которые нельзя пройти
    private LayerMask floorLayer;  

    private bool isMoving = false;
    private bool hasFallen = false; 
    private float tileSize; 

    private void Start()
    {
        floorLayer = SharedVariables.Instance.floorLayer;
        obstaclesLayer = SharedVariables.Instance.stoneLayer;
        tileSize = SharedVariables.Instance.sellSize; 
        moveSpeed = SharedVariables.Instance.playerSpeed;

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
                PlayerFail();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            PlayerFail();
        }
    }

    public void PlayerFail()
    {
        Debug.Log("you lose");
        Time.timeScale = 0;
    }
}
