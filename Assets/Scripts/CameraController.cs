using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float m_YawSpeed;
    [SerializeField] float m_PitchSpeed;
    [SerializeField] float m_MinPitch = -45.0f;
    [SerializeField] float m_MaxPitch = 75.0f;
    [SerializeField] float m_MaxDistanceFromLookAt = 10.0f;
    [SerializeField] float m_MinDistanceFromLookAt = 3.0f;

    
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    bool m_AngleLocked = false;
    bool m_CursorLocked = true;
    
    [SerializeField] Transform lookAt;
    private Vector3 offset;

    [SerializeField] LayerMask layerMask;
    [SerializeField] float m_AvoidObstacleOffset = 0.3f;

    private Vector3 m_LastPosition;

    [SerializeField] GameObject gameManager;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_CursorLocked = true;
        offset = lookAt.position - transform.position;
    }

    void OnApplicationFocus()
    {
        if(m_CursorLocked)
            Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
		if (Input.GetKeyDown(m_DebugLockAngleKeyCode))
			m_AngleLocked = !m_AngleLocked;
		if (Input.GetKeyDown(m_DebugLockKeyCode))
		{
			if (Cursor.lockState == CursorLockMode.Locked)
				Cursor.lockState = CursorLockMode.None;
			else
				Cursor.lockState = CursorLockMode.Locked;
			m_CursorLocked = Cursor.lockState == CursorLockMode.Locked;
		}
#endif

        float l_MouseAxisX = Input.GetAxis("Mouse X");
        float l_MouseAxisY = Input.GetAxis("Mouse Y");

        Vector3 l_Direction = lookAt.position - transform.position;
        float l_Distance = l_Direction.magnitude;

        //Vector3 l_DesiredPosition = m_LastPosition;
        Vector3 l_DesiredPosition = transform.position;

        if(!m_AngleLocked && (Mathf.Abs(l_MouseAxisX) > 0.01f || Mathf.Abs(l_MouseAxisY) > 0.01f))
        {
            //l_DesiredPosition = transform.position;

            Vector3 l_EulerAngles = transform.rotation.eulerAngles;
            float l_Yaw = l_EulerAngles.y + 180.0f;
            float l_Pitch = l_EulerAngles.x;

            l_Yaw += l_MouseAxisX * m_YawSpeed;
            if(l_Yaw > 360.0f) l_Yaw -= 360.0f;
            if(l_Yaw < 0.0f) l_Yaw += 360.0f;
            l_Yaw *= Mathf.Deg2Rad;

            l_Pitch += -l_MouseAxisY * m_PitchSpeed;
            if(l_Pitch > 180.0f) l_Pitch -= 360.0f;
            l_Pitch = Mathf.Clamp(l_Pitch, m_MinPitch, m_MaxPitch);
            l_Pitch *= Mathf.Deg2Rad;

            l_DesiredPosition = lookAt.position + new Vector3(
                Mathf.Sin(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance,
                Mathf.Sin(l_Pitch) * l_Distance,
                Mathf.Cos(l_Yaw) * Mathf.Cos(l_Pitch) * l_Distance);
            l_Direction = lookAt.position - l_DesiredPosition;
        }
        
        l_Direction /= l_Distance;

        if(l_Distance > m_MaxDistanceFromLookAt || l_Distance < m_MaxDistanceFromLookAt)
        {
            l_Distance = Mathf.Clamp(l_Distance, m_MinDistanceFromLookAt, m_MaxDistanceFromLookAt);
            l_DesiredPosition = lookAt.position - l_Direction * l_Distance;
        }

        Ray l_Ray = new Ray(lookAt.position, -l_Direction);
        if(Physics.Raycast(l_Ray, out RaycastHit l_hit, l_Distance, layerMask))
        {
            l_DesiredPosition = l_hit.point + m_AvoidObstacleOffset * l_Direction;
        }
        
        transform.position = l_DesiredPosition;
        //m_LastPosition = transform.position;
        transform.forward = l_Direction;
    }
}
