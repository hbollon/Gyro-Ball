using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeControl : MonoBehaviour
{
    // STATE
    private Transform rawGyroRotation;
    private Quaternion initialRotation;
    private Quaternion gyroInitialRotation;

    private bool gyroEnabled;
    public bool GyroEnabled { get; set; }

    // SETTINGS
    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private float speed = 60.0f;

    private void Awake()
    {
        Input.gyro.enabled = true;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        gyroEnabled = true;

        /* Get object and gyroscope initial rotation */
        initialRotation = transform.rotation;
        gyroInitialRotation.x = -Input.gyro.attitude.x;
        gyroInitialRotation.y = 0.0f; // Fixed Y axis
        gyroInitialRotation.z = -Input.gyro.attitude.y; // We rotate object on Y with Z axis gyro
        gyroInitialRotation.w = Input.gyro.attitude.w;

        /* GameObject instance used to prepare object movement */
        rawGyroRotation = new GameObject("GyroRaw").transform;
        rawGyroRotation.position = transform.position;
        rawGyroRotation.rotation = transform.rotation;
    }

    private void Update()
    {
        if (Time.timeScale == 1 && gyroEnabled)
        {
            ApplyGyroRotation(); // Get rotation state in rawGyroRotation
            Quaternion offsetRotation = Quaternion.Inverse(gyroInitialRotation) * rawGyroRotation.rotation; // Apply initial offset for calibration

            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation * offsetRotation, smoothing); // Progressive rotation of the object
        }
    }

    private void ApplyGyroRotation()
    {
        float curSpeed = Time.deltaTime * speed;
        Quaternion tempGyroRotation = new Quaternion(
            -Input.gyro.attitude.x * curSpeed,
            0.0f,
            -Input.gyro.attitude.y * curSpeed,
            Input.gyro.attitude.w * curSpeed);
        rawGyroRotation.rotation = tempGyroRotation;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void Recalibrate()
    {
        gyroInitialRotation.x = -Input.gyro.attitude.x;
        gyroInitialRotation.y = 0.0f;
        gyroInitialRotation.z = -Input.gyro.attitude.y;
        gyroInitialRotation.w = Input.gyro.attitude.w;
        print("Successfully recalibrated !");
    }
}