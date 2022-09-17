using System.Collections;
using UnityEngine;


public class MenusManager : MonoBehaviour {


    [SerializeField] CameraManager cameraManager;
    [SerializeField] GameObject goMenuMain;
    [SerializeField] GameObject goMenuNewGame;
    [SerializeField] GameObject goMenuSettings;


    void Start() {
        ShowMenuMain();
    }

    public void OnSelectMainMenu() {
        ShowMenuMain();
    }

    public void OnSelectContinue() {
        StartGame();
    }

    public void OnSelectNewGame() {
        ShowNewGameMenu();
    }

    public void OnSelectNewGameDifficulty() {

        Debug.Log("Selected game difficulty " + goMenuNewGame.GetComponent<MenuNewGameBehavior>()?.difficulty);

        StartGame();
    }

    public void OnSelectSettings() {
        ShowMenuSettings();
    }

    public void OnSelectQuitGame() {
        QuitGame();
    }

    void HideAllMenus() {

        goMenuMain.SetActive(false);
        goMenuNewGame.SetActive(false);
        goMenuSettings.SetActive(false);
    }

    void ShowMenuMain() {

        HideAllMenus();

        cameraManager.SelectVCamMainMenu();

        StartCoroutine(ShowMenuAfterDelay(goMenuMain));
    }

    void ShowNewGameMenu() {

        HideAllMenus();

        cameraManager.SelectVCamNewGame();

        StartCoroutine(ShowMenuAfterDelay(goMenuNewGame));
    }

    void ShowMenuSettings() {

        HideAllMenus();

        cameraManager.SelectVCamSettings();

        StartCoroutine(ShowMenuAfterDelay(goMenuSettings));
    }

    void StartGame() {

        HideAllMenus();

        cameraManager.SelectVCamStartGame();

    }

    void QuitGame() {

        HideAllMenus();

        cameraManager.SelectVCamQuitGame();

    }

    IEnumerator ShowMenuAfterDelay(GameObject goMenu) {

        yield return new WaitForSeconds(1);

        goMenu.SetActive(true);
    }

}
