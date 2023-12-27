using UnityEngine;

public static class EasingFunctions
{
    public enum EasingType
    {
        None,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo
        // 추가적인 이징 타입을 여기에 추가
    }
    public static float None(float t)
    {
        return t;
    }
    // Sine
    public static float EaseInSine(float t)
    {
        return 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
    }

    public static float EaseOutSine(float t)
    {
        return Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float EaseInOutSine(float t)
    {
        return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
    }

    // Cubic
    public static float EaseInCubic(float t)
    {
        return t * t * t;
    }

    public static float EaseOutCubic(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3);
    }

    public static float EaseInOutCubic(float t)
    {
        return t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }

    // Quart
    public static float EaseInQuart(float t)
    {
        return t * t * t * t;
    }

    public static float EaseOutQuart(float t)
    {
        return 1 - (--t * t * t * t);
    }

    public static float EaseInOutQuart(float t)
    {
        return t < 0.5f ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
    }

    // Expo
    public static float EaseInExpo(float t)
    {
        return t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
    }

    public static float EaseOutExpo(float t)
    {
        return t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
    }

    public static float EaseInOutExpo(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        return t < 0.5f ? Mathf.Pow(2, 20 * t - 10) / 2 : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
    }

    // 여기에 추가적인 이징 함수를 구현할 수 있습니다.
    public static float GetEasingFunction(EasingType type, float t)
    {
        switch (type)
        {
            case EasingType.None:
                return None(t);
            case EasingType.EaseInSine:
                return EaseInSine(t);
            case EasingType.EaseOutSine:
                return EaseOutSine(t);
            case EasingType.EaseInOutSine:
                return EaseInOutSine(t);
            case EasingType.EaseInCubic:
                return EaseInCubic(t);
            case EasingType.EaseOutCubic:
                return EaseOutCubic(t);
            case EasingType.EaseInOutCubic:
                return EaseInOutCubic(t);
            case EasingType.EaseInQuart:
                return EaseInQuart(t);
            case EasingType.EaseOutQuart:
                return EaseOutQuart(t);
            case EasingType.EaseInOutQuart:
                return EaseInOutQuart(t);
            case EasingType.EaseInExpo:
                return EaseInExpo(t);
            case EasingType.EaseOutExpo:
                return EaseOutExpo(t);
            case EasingType.EaseInOutExpo:
                return EaseInOutExpo(t);
            default:
                return t; // 기본적으로 선형 이동
        }
    }
}
