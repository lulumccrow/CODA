using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public IEnumerator MoveTo(Vector2 target, float time)
    {
        var startPosition = transform.position;
        var elapsedTime = 0.0f;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(
                startPosition,
                target,
                (elapsedTime / time)
            );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
