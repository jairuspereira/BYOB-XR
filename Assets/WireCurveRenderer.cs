using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class WireCurveRenderer : MonoBehaviour
{
    public Transform plugATip;
    public Transform plugBTip;
    [Range(8,64)] public int segments = 24;
    [Tooltip("Sag per meter of span")] public float sagPerMeter = 0.06f;

    LineRenderer lr;

    void Awake() => lr = GetComponent<LineRenderer>();

    void LateUpdate()
    {
        if (!plugATip || !plugBTip) return;

        Vector3 a = plugATip.position;
        Vector3 b = plugBTip.position;
        float span = Vector3.Distance(a, b);
        Vector3 mid = (a + b) * 0.5f + Vector3.down * (span * sagPerMeter);

        lr.positionCount = segments + 1;
        for (int i = 0; i <= segments; i++)
        {
            float t = i / (float)segments;
            // Quadratic Bezier: a -> mid -> b
            Vector3 p = Vector3.Lerp(Vector3.Lerp(a, mid, t), Vector3.Lerp(mid, b, t), t);
            lr.SetPosition(i, p);
        }
    }
}