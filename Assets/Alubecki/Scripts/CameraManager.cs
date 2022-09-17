using UnityEngine;
using Cinemachine;


public class CameraManager : MonoBehaviour {


    [SerializeField] CinemachineVirtualCamera vcamMainMenu;
    [SerializeField] CinemachineVirtualCamera vcamNewGame;
    [SerializeField] CinemachineVirtualCamera vcamStartGame;
    [SerializeField] CinemachineVirtualCamera vcamSettings;
    [SerializeField] CinemachineVirtualCamera vcamQuitGame;


    void DeselectAllCameras() {

        vcamMainMenu.Priority = 0;
        vcamNewGame.Priority = 0;
        vcamStartGame.Priority = 0;
        vcamSettings.Priority = 0;
        vcamQuitGame.Priority = 0;
    }

    public void SelectVCamMainMenu() {

        DeselectAllCameras();

        vcamMainMenu.Priority = 1;
    }

    public void SelectVCamNewGame() {

        DeselectAllCameras();

        vcamNewGame.Priority = 1;
    }

    public void SelectVCamStartGame() {

        DeselectAllCameras();

        vcamStartGame.Priority = 1;
    }

    public void SelectVCamSettings() {

        DeselectAllCameras();

        vcamSettings.Priority = 1;
    }

    public void SelectVCamQuitGame() {

        DeselectAllCameras();

        vcamQuitGame.Priority = 1;
    }

}
