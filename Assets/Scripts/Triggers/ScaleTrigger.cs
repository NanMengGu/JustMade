using UnityEngine;
using System.Collections;

public class ScaleTrigger : MonoBehaviour
{
    public EasingFunctions.EasingType easingType; // 인스펙터에서 선택

    public Vector2 movement; // 크기거 변화할 X, Y 값
    public float duration; // 크기에 걸리는 시간
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

            targetObject.transform.localScale += new Vector3(movement.x, movement.y, 0) * t; // 현재 위치에 이동량을 더함
            elapsedTime += Time.deltaTime; // 시간 업데이트

            yield return null;
        }
    }
}
