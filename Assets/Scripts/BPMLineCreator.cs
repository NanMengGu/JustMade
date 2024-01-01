using UnityEngine;
using UnityEditor; // Handles를 사용하기 위해 추가

public class BPMLineCreator : MonoBehaviour
{
    public float bpm = 60f; // BPM 값
    public int bpb = 1; // BPB 값 (한 마디에 있는 박자 수)
    public float referenceDistance = 10f; // 기준 거리
    public int maxLineCount = 10; // 최대 라인 수
    public Color color; // 라인 색상
    private float distanceBetweenLines; // 라인 간 거리

    void OnDrawGizmos()
    {
        // BPM과 BPB에 따른 라인 간격 계산
        distanceBetweenLines = CalculateDistance(bpm, bpb);

        for (int i = 0; i < maxLineCount * bpb; i++)
        {
            float positionX = i * distanceBetweenLines;
            Vector3 start = new Vector2(positionX, 0);
            Vector3 end = new Vector2(positionX, 10); // 라인의 끝점 설정 (Y값은 조절 가능)

            Gizmos.color = color; // 라인 색상 설정
            Gizmos.DrawLine(start, end); // 라인 그리기

            // 선 아래에 x 좌표 레이블 추가
            Handles.Label(new Vector3(positionX - 0.025f, -1, 0), positionX.ToString());
        }
    }

    float CalculateDistance(float currentBpm, int currentBpb)
    {
        // BPM과 BPB에 따른 라인 간격 계산
        return referenceDistance * (60f / currentBpm) / currentBpb;
    }
}
