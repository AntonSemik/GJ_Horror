using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] Light2D _playerLight;

    [SerializeField] AnimationCurve _lightIntencity;

    [SerializeField] float[] _decayTime; //decay speed equals 1 / _decayTime
    float _timerLightLevel = 1; //always reset to 1
    int _lightLevel = 0; //light level depends on keys collected or something else;

    bool _isActive = false;
    float _temp;

    [SerializeField] bool _IsFlickering = true;
    [SerializeField] float _flickerTime; float _flickerTimer;
    [SerializeField] float _flickerDelta;

    private void Start()
    {
        KeyRegister.OnKeyUpdate += OnKeyCollected;
    }

    private void Update()
    {
        if (_isActive)
        {
            _flickerTimer -= Time.deltaTime;
            if (_flickerTimer <= 0)
            {
                _IsFlickering = false;
            }

            SetLightLevel();
        }
    }

    void SetLightLevel()
    {
        _timerLightLevel -= (1.0f / _decayTime[_lightLevel]) * Time.deltaTime;
        if (_timerLightLevel < 0) _timerLightLevel = 0;

        _temp = _timerLightLevel;

        if (!_IsFlickering)
        {
            _playerLight.intensity = _lightIntencity.Evaluate(_temp);
        } else
        {
            StartCoroutine(FlickIntensity());
        }

    }

    private IEnumerator FlickIntensity()
    {
        float t0 = Time.time;
        float t = t0;
        WaitUntil wait = new WaitUntil(() => Time.time > t0 + t);
        yield return new WaitForSeconds(Random.Range(0.01f, 0.5f));

        while (true)
        {
            if (_IsFlickering)
            {
                t0 = Time.time;
                float r = Random.Range(_temp - _flickerDelta, _temp);
                _playerLight.intensity = r;
                t = Random.Range(0.05f, 0.15f);
                yield return wait;
            }
            else yield return null;
        }
    }

    void OnKeyCollected(int _collected)
    {
        _lightLevel = _collected;

        _timerLightLevel = 1;

        if (_collected == 1)
        {
            _IsFlickering = true; _flickerTimer = _flickerTime;
            _isActive = true;
        }

        if (_collected == 6)
        {
            _IsFlickering = true; _flickerTimer = _flickerTime/2;
        }
    }

    private void OnDestroy()
    {
        KeyRegister.OnKeyUpdate -= OnKeyCollected;
    }
}
