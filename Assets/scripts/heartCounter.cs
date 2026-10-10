//using UnityEngine;
//using static UnityEditor.Progress;
//using TMPro;

//public class heartCounter : MonoBehaviour
//{
//    public int score = 0;
//    // public Text scoreText; // Ссылка на UI текст
//     public TextMeshProUGUI scoreText;

//    private void OnTriggerEnter2D(Collider2D other) // для 3D (или OnTriggerEnter2D для 2D)
//    {
//        if (other.CompareTag("Player"))
//        {
//            //Debug.Log("Player entered the trigger area!");
//            UpdateUI
//        }
//    }

//    private void UpdateUI()
//    {
//         if (scoreText != null) scoreText.text = "Собрано сердец: " + score.ToString();
//        Debug.Log("Score: " + score);
//    }
//}

