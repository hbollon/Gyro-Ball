using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeControl : MonoBehaviour
{
    // STATE
    private Transform _rawGyroRotation;

    Quaternion initialRotation; 
    Quaternion gyroInitialRotation;

    // SETTINGS
    [SerializeField] private float _smoothing = 0.1f;

    private void Awake() {
        Input.gyro.enabled = true;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        initialRotation = transform.rotation; 
        gyroInitialRotation.x = -Input.gyro.attitude.x;
        gyroInitialRotation.y = 0.0f; // Fixed Y axis
        gyroInitialRotation.z = -Input.gyro.attitude.y; // We rotate object on Y with Z axis gyro
        gyroInitialRotation.w = Input.gyro.attitude.w;

        _rawGyroRotation = new GameObject("GyroRaw").transform;
        _rawGyroRotation.position = transform.position;
        _rawGyroRotation.rotation = transform.rotation;
    }

    private void Update()
    {
        ApplyGyroRotation();
        Quaternion offsetRotation = Quaternion.Inverse(gyroInitialRotation) * _rawGyroRotation.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation * offsetRotation, _smoothing);
    }

    /* private IEnumerator CalibrateAngles()
    {
        _tempSmoothing = _smoothing;
        _smoothing = 1;
        _calibrationXAngle = _appliedGyroXAngle - _initialAngles.x; // Offsets the y angle in case it wasn't 0 at edit time.
        _calibrationYAngle = _appliedGyroYAngle - _initialAngles.y; // Offsets the y angle in case it wasn't 0 at edit time.
        _calibrationZAngle = _appliedGyroZAngle - _initialAngles.z; // Offsets the y angle in case it wasn't 0 at edit time.
        yield return null;
        _smoothing = _tempSmoothing;
    } */

    private void ApplyGyroRotation()
    {
        Quaternion tempGyroRotation = new Quaternion(
            -Input.gyro.attitude.x, 
            0.0f, 
            -Input.gyro.attitude.y, 
            Input.gyro.attitude.w);
        _rawGyroRotation.rotation = tempGyroRotation;
    }
}