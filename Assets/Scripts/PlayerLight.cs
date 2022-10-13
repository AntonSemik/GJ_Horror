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

    bool _isInLight;
    float _temp;

    private void Start()
    {
        Key.OnKeyCollected += OnKeyCollected;
    }

    private void Update()
    {
        if (!_isInLight)
        {
            _timerLightLevel -= (1.0f / _decayTime[_lightLevel]) * Time.deltaTime;
            if (_timerLightLevel < 0) _timerLightLevel = 0; 
        } else
        {
            _timerLightLevel = 1;
        }
        _temp = _timerLightLevel;

        _playerLight.intensity = _lightIntencity.Evaluate(_temp);
    }

    void OnKeyCollected()
    {
        Debug.Log("Key collected");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            _isInLight = true;
            _timerLightLevel = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Light"))
        {
            _isInLight = false;
        }
    }

    private void OnDestroy()
    {
        Key.OnKeyCollected -= OnKeyCollected;
    }
}
