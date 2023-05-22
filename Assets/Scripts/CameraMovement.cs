using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform CameraLookPoint;
    public Vector3 Distance;
    public float SlerpSpeed;

    public void Start()
    {
        this.transform.position = CameraLookPoint.position + Distance;
    }

    public void Update()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, CameraLookPoint.position + Distance, SlerpSpeed * Time.deltaTime);
    }
}