using UnityEngine;
using TMPro; // Make sure TMP_Text is used for text control

public class FloatingText : MonoBehaviour
{
    public float displayDuration = 2f;        // How long the text lasts
    public Vector2 floatDistance = new Vector2(1f, 1.5f); // Max distance to float in X/Y

    private TMP_Text feedbackText;
    private Vector3 startPosition;
    private Color originalColor;
    private Coroutine currentCoroutine; // Track the current coroutine

    void Awake()
    {
        feedbackText = GetComponent<TMP_Text>();
        feedbackText.gameObject.SetActive(false); // Initially hide the text
        originalColor = feedbackText.color;
    }

    public void ShowFloatingText(string message)
    {
        // Cancel any previous floating text animation
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        feedbackText.text = message;
        feedbackText.color = originalColor;

        startPosition = Vector3.zero; // Reset to origin (local position)
        transform.localPosition = startPosition;
        
        feedbackText.gameObject.SetActive(true); // Show the text

        startPosition = transform.localPosition;
        Vector3 randomOffset = new Vector3(
            Random.Range(-floatDistance.x, floatDistance.x),
            Random.Range(0.5f, floatDistance.y),
            0f
        );

        // Start new coroutine and store the reference
        currentCoroutine = StartCoroutine(FloatAndFade(randomOffset));
    }

    private System.Collections.IEnumerator FloatAndFade(Vector3 offset)
    {
        float elapsed = 0f;
        Vector3 endPosition = startPosition + offset;

        while (elapsed < displayDuration)
        {
            float t = elapsed / displayDuration;

            // Lerp position
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, t);

            // Fade out text
            Color color = feedbackText.color;
            color.a = Mathf.Lerp(1f, 0f, t);
            feedbackText.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        feedbackText.gameObject.SetActive(false);
        transform.localPosition = startPosition;
        currentCoroutine = null; // Clear the reference
    }
}