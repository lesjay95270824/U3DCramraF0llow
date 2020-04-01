using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerCtl : MonoBehaviour {

    public static CamerCtl Instance;
    /// <summary>
    /// 控制摄像机上下
    /// </summary>
    [SerializeField]
    private Transform m_CameraUPandDown;
   
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    [SerializeField]
    private Transform m_CameraZoom;
    /// <summary>
    /// 摄像机容器
    /// </summary>
    [SerializeField]
    private Transform m_CameraContainer;
    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        m_CameraUPandDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUPandDown.transform.localEulerAngles.z, 35, 80));
    }
    /// <summary>
    /// 摄像机旋转
    /// </summary>
    /// <param name="type">0=左，1=右</param>
    public void SetCameraRotate(int type)
    {
        transform.Rotate(0, 20 * Time.deltaTime * (type == 0 ? -1 : 1), 0);
    }
    /// <summary>
    /// 设置摄像机上下
    /// </summary>
    /// <param name="type">0=上，1=下</param>
    public void SetCameraUpandDomn(int type)
    {
        m_CameraUPandDown.transform.Rotate(0, 0, 15 * Time.deltaTime * (type == 1 ? -1 : 1), 0);
        //插值保证在一定范围上下
        m_CameraUPandDown.transform.localEulerAngles = new Vector3(Mathf.Clamp(m_CameraUPandDown.transform.localEulerAngles.x, 10, 80), 0,0);
    }
    /// <summary>
    /// 设置摄像机缩放
    /// </summary>
    /// <param name="type">0=拉远，1=拉近</param>
    public void SetCameraZoom(int type)
    {
        m_CameraZoom.Translate(Vector3.forward * 10 * Time.deltaTime * (type == 1 ? -1 : 1));
        m_CameraZoom.localPosition = new Vector3(0, 0, Mathf.Clamp(m_CameraZoom.localPosition.z,-8.5f,-6.0f)); 
    }
    /// <summary>
    /// 朝向物体
    /// </summary>
    public void AutoLookAt(Vector3 pos)
    {
        m_CameraZoom.LookAt(pos); 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,13f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 11f);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
