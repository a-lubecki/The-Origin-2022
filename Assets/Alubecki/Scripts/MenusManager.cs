using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MenusManager : MonoBehaviour {


    [SerializeField] CameraManager cameraManager;
    [SerializeField] PostProcessingManager postProcessingManager;
    [SerializeField] DecorationsBehavior decorations;
    [SerializeField] MenuMainBehavior menuMain;
    [SerializeField] MenuNewGameBehavior menuNewGame;
    [SerializeField] MenuSettingsBehavior menuSettings;
    [SerializeField] MenuCreditsBehavior menuCredits;

    [SerializeField] AudioSource audioSourceSounds;
    [SerializeField] AudioClip audioClipScreenChange;
    [SerializeField] AudioClip audioClipStartGame;

    BaseMenuBehavior[] menus => new BaseMenuBehavior[] {
        menuMain,
        menuNewGame,
        menuSettings,
        menuCredits
    };


    void Start() {

        postProcessingManager.SetBlackScreen(false);

        HideAllMenus(false);

        decorations.Hide(false);
        decorations.UpdateScreenTitle("");

        StartCoroutine(AnimateFirstMenu());
    }

    IEnumerator AnimateFirstMenu() {

        yield return new WaitForSeconds(0.1f);

        decorations.Show(true);

        yield return new WaitForEndOfFrame();

        decorations.MoveCircles(decorations.CirclesPositions, new Vector3[] {
            new Vector3(1.25f, 1.25f, 1.25f),
            new Vector3(1.5f, 1.5f, 1.5f),
            new Vector3(1.7f, 1.7f, 1.7f),
            new Vector3(2, 2, 2),
        }, 0.2f);

        ShowMenuMain();

        yield return new WaitForSeconds(0.2f);

        postProcessingManager.SetDefaultScreen(true);
    }

    public void OnSelectMainMenu() {
        ShowMenuMain();
    }

    public void OnSelectContinue() {
        StartGame();
    }

    public void OnSelectNewGame() {
        ShowMenuNewGame();
    }

    public void OnSelectNewGameDifficulty() {

        Debug.Log("Selected game difficulty " + menuNewGame.SelectedDifficulty);

        StartGame();
    }

    public void OnSelectSettings() {
        ShowMenuSettings();
    }

    public void OnSelectCredits() {
        ShowMenuCredits();
    }

    public void OnSelectQuitGame() {
        QuitGame();
    }

    public void OnSelectBuyDLC() {
        //open custom URL of a real game
        Application.OpenURL("https://store.steampowered.com/");
    }

    void HideAllMenus(bool animated) {

        decorations.UpdateScreenTitle(null);

        foreach (var m in menus) {
            m.Hide(animated);
        }
    }

    void ShowMenuMain() {

        HideAllMenus(true);

        cameraManager.SelectVCamMainMenu();

        StartCoroutine(ShowMenuAfterDelay(menuMain));
    }

    void ShowMenuNewGame() {

        HideAllMenus(true);

        cameraManager.SelectVCamNewGame();

        StartCoroutine(ShowMenuAfterDelay(menuNewGame));
    }

    void ShowMenuSettings() {

        HideAllMenus(true);

        cameraManager.SelectVCamSettings();

        StartCoroutine(ShowMenuAfterDelay(menuSettings));
    }

    void ShowMenuCredits() {

        HideAllMenus(true);

        cameraManager.SelectVCamCredits();

        StartCoroutine(ShowMenuAfterDelay(menuCredits));
    }

    void StartGame() {

        decorations.Hide(true);
        HideAllMenus(true);

        cameraManager.SelectVCamStartGame();
        postProcessingManager.SetWhiteScreen(true);

        audioSourceSounds.PlayOneShot(audioClipStartGame);

        StartCoroutine(DoActionAfterDelay(2f, () => SceneManager.LoadScene(0)));
    }

    void QuitGame() {

        decorations.Hide(true);
        HideAllMenus(true);

        cameraManager.SelectVCamQuitGame();
        postProcessingManager.SetBlackScreen(true);

        StartCoroutine(DoActionAfterDelay(2f, () => {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }));
    }

    IEnumerator DoActionAfterDelay(float delaySec, Action action) {

        yield return new WaitForSeconds(delaySec);

        action.Invoke();
    }

    IEnumerator ShowMenuAfterDelay(BaseMenuBehavior menu) {

        yield return new WaitForSeconds(0.1f);

        if (menu != decorations) {
            decorations.MoveCircles(menu.CirclesPositions, menu.CirclesScales, 0.7f);
        }

        audioSourceSounds.PlayOneShot(audioClipScreenChange);

        yield return new WaitForSeconds(0.4f);

        menu.Show(true);
        decorations.UpdateScreenTitle(menu.ScreenTitle);
    }

}
