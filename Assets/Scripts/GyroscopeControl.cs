using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroscopeControl : MonoBehaviour
{
    // STATE
    private Transform rawGyroRotation;

    Quaternion initialRotation; 
    Quaternion gyroInitialRotation;

    // SETTINGS
    [SerializeField] private float smoothing = 0.1f;
    [SerializeField] private float speed = 60.0f;

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

        rawGyroRotation = new GameObject("GyroRaw").transform;
        rawGyroRotation.position = transform.position;
        rawGyroRotation.rotation = transform.rotation;
    }

    private void Update()
    {
        ApplyGyroRotation();
        Quaternion offsetRotation = Quaternion.Inverse(gyroInitialRotation) * rawGyroRotation.rotation;

        transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation * offsetRotation, smoothing);
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

    public void setSpeed(float speed){
        this.speed = speed;
    }

    public float getSpeed(){
        return speed;
    } 
}