using UnityEngine;

public class TestScaleWindowsTrigger : MonoBehaviour
{
    public ScaleWindowsTrigger scaleWindowsTrigger; // MoveTrigger 컴포넌트에 대한 참조

    /* void Update()
    {
        if (Input.anyKeyDown)
        {
            if (scaleWindowsTrigger != null)
            {
                scaleWindowsTrigger.TriggerMovement(); // MoveTrigger 인스턴스의 메서드 호출
            }
        }
    } */
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("호출tr");
        if (other.tag == "line")
        {
            Debug.Log("호출li");
            if (scaleWindowsTrigger != null)
            {
                scaleWindowsTrigger.TriggerMovement();
            }
        }
    }
}
