using UnityEngine;

public class MonitorAspectRatioGizmo : MonoBehaviour
{
    public float width = 16f;  // 모니터의 가로 크기
    public float height = 9f;  // 모니터의 세로 크기

    private void OnDrawGizmos()
    {
        // 월드 좌표계의 중심(0, 0)을 기준으로 박스 그리기
        Gizmos.color = Color.green; // 렌더링 되는 부분은 초록색으로 표시
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(width, height, 1));

        // 월드 좌표계의 중심을 기준으로 모니터의 밖쪽 영역 표시 (예시: 1.2배 크기)
        Gizmos.color = Color.red; // 렌더링 되지 않는 부분은 빨간색으로 표시
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(width * 1.2f, height * 1.2f, 1));
    }
}
