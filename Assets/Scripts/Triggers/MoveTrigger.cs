using UnityEngine;
using System.Collections;

public class MoveTrigger : MonoBehaviour
{
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택
    public Vector2 movement; // 이동할 X, Y 값
    public float duration; // 이동에 걸리는 시간
    public GameObject targetObject;

    public void TriggerMovement()
    {
        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // 정규화된 시간
            t = EasingFunctions.GetEasingFunction(easingType, t); // 선택된 이징 함수 적용

            // Time.deltaTime을 곱하여 일정한 속도로 이동하도록 조정
            Vector3 movementPerFrame = new Vector3(movement.x * t, movement.y * t, 0) * Time.deltaTime;
            targetObject.transform.position += movementPerFrame; // 현재 위치에 이동량을 더함

            elapsedTime += Time.deltaTime; // 시간 업데이트

            yield return null;
        }
    }
}
