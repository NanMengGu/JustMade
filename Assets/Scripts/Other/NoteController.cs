using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, -1, 0); // 노트가 하강할 방향
    public float speed = 10.0f; // 노트의 속도

    void Update()
    {
        // 노트를 지정된 방향과 속도로 이동시킵니다.
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
