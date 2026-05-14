using UnityEngine;

public class TrebuchetAimer : MonoBehaviour
{
    [Header("Регулировка угла")]
    public float minAngle = -10f;
    public float maxAngle = 70f;
    public float rotationSpeed = 50f;

    private float currentAngle = 35f;

    void Update()
    {
        float input = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) input = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) input = -1f;

        currentAngle -= input * rotationSpeed * Time.deltaTime;
        currentAngle = Mathf.Clamp(currentAngle, minAngle, maxAngle);
    }

    public Vector3 GetLaunchDirection()
    {
        return Quaternion.Euler(currentAngle, 0, 0) * Vector3.forward;
    }
}