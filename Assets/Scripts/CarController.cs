using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelCollider frontWheel;
    public WheelCollider backLeftWheel;
    public WheelCollider backRightWheel;

    public Transform frontWheelMesh;
    public Transform backLeftWheelMesh;
    public Transform backRightWheelMesh;

    public float maxMotorTorque = 150f; 
    public float maxSteeringAngle = 30f; 

    void Start()
{
    Rigidbody rb = GetComponent<Rigidbody>();
    rb.centerOfMass = new Vector3(0, -0.5f, 0); // Adjust Y based on your car model
}

    void Update()
    {
        // Get input
        float motor = Input.GetAxis("Vertical") * maxMotorTorque;
        float steering = Input.GetAxis("Horizontal") * maxSteeringAngle;

        // Debug input
        //Debug.Log($"Motor Input: {motor}, Steering Input: {steering}");

        // Apply torque and steering
        backLeftWheel.motorTorque = motor;
        backRightWheel.motorTorque = motor;
        frontWheel.steerAngle = steering;

        // Debug Wheel Collider state
        //Debug.Log($"Front Steer Angle: {frontWheel.steerAngle}, Back Motor Torque: {backLeftWheel.motorTorque}");

        // Update wheel visuals
        UpdateWheelVisual(frontWheel, frontWheelMesh);
        UpdateWheelVisual(backLeftWheel, backLeftWheelMesh);
        UpdateWheelVisual(backRightWheel, backRightWheelMesh);
    }

    void UpdateWheelVisual(WheelCollider collider, Transform mesh)
    {
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
        mesh.position = position;
        mesh.rotation = rotation;
    }

    void FixedUpdate()
{
    Debug.Log($"Wheel RPM: {backLeftWheel.rpm}, Torque: {backLeftWheel.motorTorque}");
    Debug.Log($"Car Velocity: {GetComponent<Rigidbody>().linearVelocity}");

    // Optional: Check if WheelColliders are grounded
    DebugGroundStatus(frontWheel);
    DebugGroundStatus(backLeftWheel);
    DebugGroundStatus(backRightWheel);
}

void DebugGroundStatus(WheelCollider wc)
{
    WheelHit hit;
    if (wc.GetGroundHit(out hit))
    {
        Debug.Log($"{wc.name} is grounded. Hit Point: {hit.point}, Force: {hit.force}");
    }
    else
    {
        Debug.Log($"{wc.name} is not grounded.");
    }
}


}
