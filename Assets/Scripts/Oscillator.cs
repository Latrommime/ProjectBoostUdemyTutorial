using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPos;
    float movementFactor;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) return;

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2f;

        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1f) / 2f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
