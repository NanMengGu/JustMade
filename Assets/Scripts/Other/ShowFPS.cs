using UnityEngine;
using UnityEngine.UI;  // UI 네임스페이스를 추가해야 합니다.

public class ShowFPS : MonoBehaviour
{
    public Text fpsText;  // UI 텍스트 컴포넌트를 할당할 변수
    private float deltaTime = 0.0f;

    void Update()
    {
        // deltaTime을 갱신합니다.
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // FPS를 계산합니다.
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} fps", fps);

        // UI 텍스트 컴포넌트에 FPS를 표시합니다.
        fpsText.text = text;
    }
}
