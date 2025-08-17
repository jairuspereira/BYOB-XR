using UnityEngine;

public class MotorController : MonoBehaviour
{
    public Transform wheel;
    public float rpm = 120f;

    bool powered;
    int direction; // +1 forward, -1 reverse

    public void SetPower(bool on, int dir)
    {
        powered = on;
        direction = (int)(on ? Mathf.Sign(dir) : 0);
    }

    void Update()
    {
        if (!powered || !wheel) return;
        float degreesPerSec = rpm * 6f; // 360 * rpm / 60
        wheel.Rotate(Vector3.up, degreesPerSec * direction * Time.deltaTime, Space.Self);
    }
}