using UnityEngine;

public class TestMoveTrigger : MonoBehaviour
{
    public MoveTrigger moveTrigger; // MoveTrigger 컴포넌트에 대한 참조

    /* void Update()
    {
        if (Input.anyKeyDown)
        {
            if (moveTrigger != null)
            {
                moveTrigger.TriggerMovement(); // MoveTrigger 인스턴스의 메서드 호출
            }
        }
    } */
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            Debug.Log("호출");
            if (moveTrigger != null)
            {
                moveTrigger.TriggerMovement();
            }
        }
    }
}
