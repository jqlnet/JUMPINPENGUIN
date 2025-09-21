using UnityEngine;
using System.Collections;

public class MovingSpike : MonoBehaviour
{
    public float moveDistance = 2f;       // Total movement range
    public float moveSpeed = 2f;          // Movement speed
    public float moveDuration = 2f;       // How long the spike moves before pausing
    public float pauseDuration = 2f;      // How long the spike pauses

    private Vector3 startPos;
    private bool isPaused = false;

    void Start()
    {
        startPos = transform.position;
        StartCoroutine(PauseMoveRoutine());
    }

    void Update()
    {
        if (!isPaused)
        {
            float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - (moveDistance / 2f);
            transform.position = startPos + Vector3.up * offset;
        }
    }

    IEnumerator PauseMoveRoutine()
    {
        while (true)
        {
            isPaused = false;
            yield return new WaitForSeconds(moveDuration);
            isPaused = true;
            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
