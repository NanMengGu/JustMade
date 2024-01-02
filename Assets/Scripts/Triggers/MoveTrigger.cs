using UnityEngine;
using System.Collections;

public class MoveTrigger : MonoBehaviour
{
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    public Vector2 movement; // 이동할 X, Y 값
    public float duration; // 이동에 걸리는 시간
    public GameObject targetObject;
    float previousLerpValueX;
    float previousLerpValueY;
    float targetMovementX;
    float targetMovementY;
    float initialMovementX;
    float initialMovementY;
    float elapsedTime = 0;

    public void TriggerMovement()
    {
        StartCoroutine(MoveObject());
        initialMovementX = targetObject.transform.position.x;
        initialMovementY = targetObject.transform.position.y;
        targetMovementX = initialMovementX + movement.x;
        targetMovementY = initialMovementY + movement.y;
        previousLerpValueX = initialMovementX;
        previousLerpValueY = initialMovementY;
        elapsedTime = 0;
    }

    IEnumerator MoveObject()
    {
        while (elapsedTime <= duration)
        {
            elapsedTime += Time.fixedDeltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);
            float easedTime = EasingFunctions.GetEasingFunction(easingType, normalizedTime);

            float currentMovementX = Mathf.Lerp(initialMovementX, targetMovementX, easedTime);
            float currentMovementY = Mathf.Lerp(initialMovementY, targetMovementY, easedTime);
            float deltaMovementX = currentMovementX - previousLerpValueX;
            float deltaMovementY = currentMovementY - previousLerpValueY;

            Vector3 newPosition = targetObject.transform.position;
            newPosition.x += deltaMovementX;
            newPosition.y += deltaMovementY;
            targetObject.transform.position = newPosition;

            previousLerpValueX = currentMovementX;
            previousLerpValueY = currentMovementY;

            yield return null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line") TriggerMovement();
    }
}
