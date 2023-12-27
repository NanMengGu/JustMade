using UnityEngine;

public class TestCreateWindowsTrigger : MonoBehaviour
{
    public CreateWindowTrigger createWindowTrigger; // MoveTrigger 컴포넌트에 대한 참조
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "line")
        {
            if (createWindowTrigger != null)
            {
                createWindowTrigger.CreateWindow();
                createWindowTrigger.CreateCamera();
            }
        }
    }
}
