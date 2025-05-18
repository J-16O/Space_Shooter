using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public Camera mainCamera;
    private float _shaketime;
    private float _shakeLimit;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        _shaketime = 0f;
        _shakeLimit = 1f;
    }

    void Update()
    {
        if (_shaketime > 0)
        {
            _shaketime -= Time.deltaTime;
        }
    }
    
    public void InitiateShake(float incomingTimeToShake)
    {
        _shaketime = incomingTimeToShake;
        StartCoroutine(CameraShake());
    }
    
    IEnumerator CameraShake()
    {
        while (_shaketime > 0)
        {
            float x = Random.Range(-_shakeLimit, _shakeLimit);
            float y = Random.Range(-_shakeLimit, _shakeLimit);
            Vector3 newPos = new Vector3(x, y, -10f);
            mainCamera.transform.position = newPos;

            yield return new WaitForSeconds(0.03f);
        }
        mainCamera.transform.position = new Vector3(0, 0, -10f);
    }
}