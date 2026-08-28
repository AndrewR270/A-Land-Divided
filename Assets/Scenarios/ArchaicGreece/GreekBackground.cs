using UnityEngine;
using UnityEngine.UI;

public class GreekBackground : MonoBehaviour
{
    [Header("Oval Wave Motion")]
    public RectTransform wave1;
    public RectTransform wave2;
    public RectTransform wave3;

    public float wave1Speed = 1f;
    public float wave1XRadius = 50f;
    public float wave1YRadius = 25f;

    public float wave2Speed = 1.2f;
    public float wave2XRadius = 60f;
    public float wave2YRadius = 30f;

    public float wave3Speed = 0.8f;
    public float wave3XRadius = 40f;
    public float wave3YRadius = 20f;

    private Vector2 wave1Start;
    private Vector2 wave2Start;
    private Vector2 wave3Start;

    [Header("Geometric Infinite Scroll")]
    public RectTransform geo1A;
    public RectTransform geo1B;
    public float geo1ScrollSpeed = 50f;

    public RectTransform geo2A;
    public RectTransform geo2B;
    public float geo2ScrollSpeed = 30f;

    // Your exact thresholds
    private const float GEO1_RESET_POS = -7806f;
    private const float GEO1_END_POS = 5162f;

    private const float GEO2_RESET_POS = -5374f;
    private const float GEO2_END_POS = 4351f;

    void Awake()
    {
        if (wave1 != null) wave1Start = wave1.anchoredPosition;
        if (wave2 != null) wave2Start = wave2.anchoredPosition;
        if (wave3 != null) wave3Start = wave3.anchoredPosition;
    }

    void Update()
    {
        float t = Time.time;

        // --- Oval Motion ---
        if (wave1 != null)
        {
            float x = Mathf.Cos(t * wave1Speed) * wave1XRadius;
            float y = Mathf.Sin(t * wave1Speed) * wave1YRadius;
            wave1.anchoredPosition = wave1Start + new Vector2(x, y);
        }

        if (wave2 != null)
        {
            float x = Mathf.Cos(t * wave2Speed) * wave2XRadius;
            float y = Mathf.Sin(t * wave2Speed) * wave2YRadius;
            wave2.anchoredPosition = wave2Start + new Vector2(x, y);
        }

        if (wave3 != null)
        {
            float x = Mathf.Cos(t * wave3Speed) * wave3XRadius;
            float y = Mathf.Sin(t * wave3Speed) * wave3YRadius;
            wave3.anchoredPosition = wave3Start + new Vector2(x, y);
        }

        // --- Geo1 Infinite Scroll ---
        if (geo1A != null && geo1B != null)
        {
            float delta = geo1ScrollSpeed * Time.deltaTime;

            geo1A.anchoredPosition += new Vector2(delta, 0);
            geo1B.anchoredPosition += new Vector2(delta, 0);

            if (geo1A.anchoredPosition.x >= GEO1_END_POS)
                geo1A.anchoredPosition = new Vector2(GEO1_RESET_POS, geo1A.anchoredPosition.y);

            if (geo1B.anchoredPosition.x >= GEO1_END_POS)
                geo1B.anchoredPosition = new Vector2(GEO1_RESET_POS, geo1B.anchoredPosition.y);
        }

        // --- Geo2 Infinite Scroll ---
        if (geo2A != null && geo2B != null)
        {
            float delta = geo2ScrollSpeed * Time.deltaTime;

            geo2A.anchoredPosition += new Vector2(delta, 0);
            geo2B.anchoredPosition += new Vector2(delta, 0);

            if (geo2A.anchoredPosition.x >= GEO2_END_POS)
                geo2A.anchoredPosition = new Vector2(GEO2_RESET_POS, geo2A.anchoredPosition.y);

            if (geo2B.anchoredPosition.x >= GEO2_END_POS)
                geo2B.anchoredPosition = new Vector2(GEO2_RESET_POS, geo2B.anchoredPosition.y);
        }
    }
}
