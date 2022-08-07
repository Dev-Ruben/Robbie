using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : SingletonMonobehaviour<CameraManager>
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;

    private CinemachineVirtualCamera currentCamera;
    // Start is called before the first frame update
    void Start()
    {
        currentCamera = GetVirtualCamera(CameraType.PlayerCamera);
        currentCamera.Priority = 1;
    }

    // Update is called once per frame
    void Update()
    {
         if (InputManager.Instance.getButtonDown("Camera")) {
            switchCameraPriority(GetVirtualCamera(CameraType.BossCamera));

         }

         if (InputManager.Instance.getButtonDown("Camera2")) {
            switchCameraPriority(GetVirtualCamera(CameraType.PlayerCamera));

         }

    }

    private void switchCameraPriority(CinemachineVirtualCamera targetCamera){
        CinemachineVirtualCamera nextCamera = targetCamera;
        
        if(nextCamera != currentCamera){

            nextCamera.Priority = 1;
            currentCamera.Priority = 0;

            currentCamera = nextCamera;
        }
    }

    private CinemachineVirtualCamera GetVirtualCamera(CameraType targetCamera){
        return cameras[((int)targetCamera)];

    }

    public void setBossCamera(GameObject bossObject){
        GetVirtualCamera(CameraType.BossCamera).Follow = bossObject.transform;

    }
                    

}
