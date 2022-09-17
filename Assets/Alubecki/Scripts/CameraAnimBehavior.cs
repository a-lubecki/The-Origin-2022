using UnityEngine;
using Cinemachine;


[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CameraAnimBehavior : MonoBehaviour {


    public float speed = 1;

    CinemachineVirtualCamera vcam;
    CinemachineTrackedDolly trackedDolly;


    void Awake() {

        vcam = GetComponent<CinemachineVirtualCamera>();
        trackedDolly = vcam.GetCinemachineComponent<CinemachineTrackedDolly>();
    }

    void Update() {

        trackedDolly.m_PathPosition = (trackedDolly.m_PathPosition + Time.deltaTime * speed) % 1;
    }

}
