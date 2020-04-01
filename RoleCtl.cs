using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCtl : MonoBehaviour {

    /// <summary>
    /// 移动的目标点
    /// </summary>
	private Vector3 m_TargetPos = Vector3.zero;
    /// <summary>
    /// 移动的速度
    /// </summary>
    public float m_Speed = 10.0f;

    public float m_RotationSpeed = 0.5f;

    public Quaternion m_TarfeQuaternion;

    public bool IsRota=false;
    public CharacterController m_characterController;
	void Start ()
    {
        m_characterController = GetComponent<CharacterController>(); 

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hiyInfo;
            if(Physics.Raycast(ray,out hiyInfo))
            {
                if (hiyInfo.collider.name.Equals("Plane",System.StringComparison.CurrentCultureIgnoreCase))
                {
                    m_TargetPos = hiyInfo.point;
                    m_RotationSpeed = 0.0f;
                    IsRota = true;
                }
            }
        }
        if (!m_characterController.isGrounded)
        {
            m_characterController.Move(transform.position + new Vector3(0, -100, 0)- transform.position); 
        }
            if (m_TargetPos != Vector3.zero)
            {
                if (Vector3.Distance(m_TargetPos, transform.position) > 0.1f)
                {
                    Vector3 direction = m_TargetPos - transform.position;
                    direction = direction.normalized;
                    direction = direction * m_Speed * Time.deltaTime;
                    direction.y = 0;
                // transform.LookAt(new Vector3(m_TargetPos.x,  transform.position.y, m_TargetPos.z));
                if (IsRota)
                {
                    m_RotationSpeed += 5.0f;
                    m_TarfeQuaternion = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_TarfeQuaternion, m_RotationSpeed * Time.deltaTime);

                    if (Quaternion.Angle(m_TarfeQuaternion, transform.rotation) < 1f)
                    {
                        m_RotationSpeed = 1;
                        IsRota = false;
                    }
                }
                    m_characterController.Move(direction);
                }
            }
        CameraAutoFollow();
        if(Input.GetKey(KeyCode.A))
        {
            CamerCtl.Instance.SetCameraRotate(0);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            CamerCtl.Instance.SetCameraRotate(1);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            CamerCtl.Instance.SetCameraUpandDomn(0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            CamerCtl.Instance.SetCameraUpandDomn(1);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
           
            CamerCtl.Instance.SetCameraZoom(0);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            CamerCtl.Instance.SetCameraZoom(1);
        }
    }

    private void CameraAutoFollow()
    {
        if (CamerCtl.Instance == null)
            return;
        CamerCtl.Instance.transform.position = gameObject.transform.position;
        CamerCtl.Instance.AutoLookAt(gameObject.transform.position);
    }
}
