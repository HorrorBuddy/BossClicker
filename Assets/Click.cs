using UnityEngine;

public class Click : MonoBehaviour
{
    private Vector3 originalScale;
    private float timer;
    private SpriteRenderer sr;
    private Color originalColor;

    public AudioClip clickSound;
    private AudioSource audioSource;

    public FloatingText floatingText;  // Assign in Inspector
    public AddClick addClick;          // Assign in Inspector

    public Variables stats;

    void Start()
    {
        originalScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        // Visual feedback
        transform.localScale = originalScale * 1.2f;
        sr.color = new Color(1f, 0.5f, 0.5f, 1f);

        audioSource.PlayOneShot(clickSound);

        stats.damage = addClick.GetDamage();

        floatingText.ShowFloatingText("Damage: " + stats.damage);

        timer = 0.1f;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                transform.localScale = originalScale;
                sr.color = originalColor;
            }
        }
    }
}
