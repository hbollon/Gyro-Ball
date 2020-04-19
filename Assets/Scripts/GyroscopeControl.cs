using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeControl : MonoBehaviour
{
    // STATE
    private float _initialXAngle = 0f;
    private float _initialYAngle = 0f;
    private float _initialZAngle = 0f;
    private float _appliedGyroXAngle = 0f;
    private float _appliedGyroYAngle = 0f;
    private float _appliedGyroZAngle = 0f;
    private float _calibrationXAngle = 0f;
    private float _calibrationYAngle = 0f;
    private float _calibrationZAngle = 0f;
    private Transform _rawGyroRotation;
    private float _tempSmoothing;

    // SETTINGS
    [SerializeField] private float _smoothing = 0.1f;

    private IEnumerator Start()
    {
        Input.gyro.enabled = true;
        Application.targetFrameRate = 60;
        _initialYAngle = transform.eulerAngles.y;

        _rawGyroRotation = new GameObject("GyroRaw").transform;
        _rawGyroRotation.position = transform.position;
        _rawGyroRotation.rotation = transform.rotation;

        // Wait until gyro is active, then calibrate to reset starting rotation.
        yield return new WaitForSeconds(1);

        StartCoroutine(CalibrateAngles());
    }

    private void Update()
    {
        ApplyGyroRotation();
        ApplyCalibration();

        transform.rotation = Quaternion.Slerp(transform.rotation, _rawGyroRotation.rotation, _smoothing);
    }

    private IEnumerator CalibrateAngles()
    {
        _tempSmoothing = _smoothing;
        _smoothing = 1;
        _calibrationXAngle = _appliedGyroXAngle - _initialXAngle; // Offsets the y angle in case it wasn't 0 at edit time.
        _calibrationYAngle = _appliedGyroYAngle - _initialYAngle; // Offsets the y angle in case it wasn't 0 at edit time.
        _calibrationZAngle = _appliedGyroZAngle - _initialZAngle; // Offsets the y angle in case it wasn't 0 at edit time.
        yield return null;
        _smoothing = _tempSmoothing;
    }

    private void ApplyGyroRotation()
    {
        Quaternion tempGyroRotation = new Quaternion(
            -Input.gyro.attitude.x, 
            0f, 
            -Input.gyro.attitude.y, 
            Input.gyro.attitude.w);
        _rawGyroRotation.rotation = tempGyroRotation;

        _appliedGyroXAngle = _rawGyroRotation.eulerAngles.x;
        _appliedGyroYAngle = _rawGyroRotation.eulerAngles.y;
        _appliedGyroZAngle = _rawGyroRotation.eulerAngles.z;
    }

    private void ApplyCalibration()
    {
        _rawGyroRotation.Rotate(_calibrationXAngle, 0f, 0f, Space.World); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
        _rawGyroRotation.Rotate(0f, -_calibrationYAngle, 0f, Space.World); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
        _rawGyroRotation.Rotate(0f, 0f, _calibrationZAngle, Space.World); // Rotates y angle back however much it deviated when calibrationYAngle was saved.
    }

    public void SetEnabled(bool value)
    {
        enabled = true;
        StartCoroutine(CalibrateAngles());
    }
}
