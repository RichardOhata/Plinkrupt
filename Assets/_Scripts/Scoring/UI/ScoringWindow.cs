using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoringWindow : MonoBehaviour
{
    public GameObject panel; // Assign your UI panel in the Inspector
    public int maxScores = 4; // Maximum number of displayed scores
    private Queue<GameObject> scoreQueue = new Queue<GameObject>(); // Queue to manage score images
    private float spacing = 120f; // Vertical spacing between score entries

    public void Start()
    {
        ScoreManager.Instance.OnScoreUpdatedScoreWithColor += AddScore; // Subscribe to the event
        ScoreManager.Instance.OnNextRoundReset += ResetScores;
    }

    private void OnDestroy()
    {
        ScoreManager.Instance.OnScoreUpdatedScoreWithColor -= AddScore; // Unsubscribe from the event
        ScoreManager.Instance.OnNextRoundReset -= ResetScores;
    }

    public void AddScore(Color elementColor, float score)
    {
        // If queue is full, remove the oldest image
        if (scoreQueue.Count >= maxScores)
        {
            GameObject oldScore = scoreQueue.Dequeue();
            Destroy(oldScore);
        }

        // Move existing images up
        foreach (GameObject obj in scoreQueue)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            rt.anchoredPosition += new Vector2(0, spacing);
        }

        // Create a new blank white image for the score
        GameObject scoreObject = new GameObject("ScoreEntry");
        scoreObject.transform.SetParent(panel.transform, false);

        // Add Image component and set it to white
        Image img = scoreObject.AddComponent<Image>();
        img.color = elementColor; // Set to white
        RectTransform imgRect = img.rectTransform;
        imgRect.sizeDelta = new Vector2(100, 100); // Set size
        imgRect.anchorMin = new Vector2(0.5f, 1); // Center top
        imgRect.anchorMax = new Vector2(0.5f, 1);
        imgRect.pivot = new Vector2(0.5f, 1); // Align to top center
        imgRect.anchoredPosition = new Vector2(0, -3 * spacing - 20); // Start at bottom

        // Create TextMeshPro text inside the image
        GameObject textObject = new GameObject("ScoreText");
        textObject.transform.SetParent(scoreObject.transform, false);

        TextMeshProUGUI tmp = textObject.AddComponent<TextMeshProUGUI>();
        tmp.text = "Score: " + score;
        tmp.fontSize = 24;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.color = Color.black; // Set text color to black for visibility

        // Stretch text to fit inside the white image
        RectTransform textRT = tmp.rectTransform;
        textRT.anchorMin = new Vector2(0, 0);
        textRT.anchorMax = new Vector2(1, 1);
        textRT.offsetMin = Vector2.zero;
        textRT.offsetMax = Vector2.zero;

        // Add new score entry to queue
        scoreQueue.Enqueue(scoreObject);
    }

        public void ResetScores(bool status)
    {
        while (scoreQueue.Count > 0)
        {
            Destroy(scoreQueue.Dequeue());
        }
        scoreQueue.Clear();
    }
}
