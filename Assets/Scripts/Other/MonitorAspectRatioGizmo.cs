using UnityEngine;

public class MonitorGizmo : MonoBehaviour
{
    private float width = Screen.width;
    private float height = Screen.height;
    public Color gizmoColor = Color.blue; // Gizmo의 색상

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(width, height, 0.1f));
    }
}
