using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    [SerializeField] Light2D _light;

    public bool _flickIntensity;
    public float _intensityRange;
    public float _intensityTimeMin;
    public float _intensityTimeMax;

    float _baseIntensity;

    private void Start()
    {
        _baseIntensity = _light.intensity;

        StartCoroutine(FlickIntensity());
    }

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (_flickIntensity)
            {
                t0 = Time.time;
                float r = Random.Range(_baseIntensity - _intensityRange, _baseIntensity + _intensityRange);
                _light.intensity = r;
                t = Random.Range(_intensityTimeMin, _intensityTimeMax);
                yield return wait;
            }
            else yield return null;
        }
    }
}
