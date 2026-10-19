using System;
using System.Collections;
using UnityEngine;

public class floor : MonoBehaviour
{
    private float timeToDelete;
    private SpriteRenderer spriteRenderer; // только ради прозрачности
    private int steps; 
    void Start()
    {
        timeToDelete = SharedVariables.Instance.timeToDeleteFloor;
        steps = SharedVariables.Instance.oppSteps;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator DeleteFloor()
    {
        //уровень прозрачности
        Color color = spriteRenderer.color;

        for (int i = 1; i < steps; i++)
        {
            color.a -= 1f/steps;
            // Debug.Log(color.a+"   "+i);
            spriteRenderer.color = color;
            yield return new WaitForSeconds(timeToDelete/(steps+1));
        }
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Игрок ушел с клетки: " + gameObject.name);
            StartCoroutine(DeleteFloor());
        }
    }
}
