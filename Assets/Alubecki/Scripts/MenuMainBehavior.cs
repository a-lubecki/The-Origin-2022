using UnityEngine.Events;


public class MenuMainBehavior : BaseMenuBehavior {


    public UnityEvent eventOnNewGame;
    public UnityEvent eventOnContinue;
    public UnityEvent eventOnSettings;
    public UnityEvent eventOnQuitGame;


    protected override void Awake() {
        base.Awake();

        InitButton("ButtonNewGame", eventOnNewGame);
        InitButton("ButtonContinue", eventOnContinue);
        InitButton("ButtonSettings", eventOnSettings);
        InitButton("ButtonQuitGame", eventOnQuitGame);
    }

}
