using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumScale : MonoBehaviour
{
    public GameObject targetObject;
    public AudioSource audioSource;
    public float size;
    public int sampleCount;
    private float[] sample = new float[64];

    void Awake()
    {
        Application.runInBackground = true;
    }

    void Update()
    {
        audioSource.GetSpectrumData(sample, 0, FFTWindow.BlackmanHarris);
        targetObject.transform.localScale = new Vector2(sample[sampleCount] * size, sample[sampleCount] * size);
    }
}
