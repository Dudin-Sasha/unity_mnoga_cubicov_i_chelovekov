using System.Collections;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private float timeToFall;
    private Rigidbody2D rb;
    private LayerMask floorLayer;
    private Vector2 floorPos;
    void Start()
    {
        timeToFall = SharedVariables.Instance.timeToStoneFall;
        floorLayer = SharedVariables.Instance.floorLayer;
        rb = GetComponent<Rigidbody2D>();

        floorPos = transform.position;
        floorPos.y-=SharedVariables.Instance.sellSize;


        StartCoroutine(DeleteFloor());
    }

    IEnumerator DeleteFloor()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToFall);
            if (!IsFloorDown())
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                yield return new WaitForSeconds(2f);
                Destroy(gameObject);
            }   
        }
    }



    private bool IsFloorDown()
    {
        return Physics2D.OverlapPoint(floorPos, floorLayer) != null;
    }
}
