using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuMainBehavior : BaseMenuBehavior {


    public UnityEvent eventOnQuitGame;
    public UnityEvent eventOnNewGame;
    public UnityEvent eventOnContinue;
    public UnityEvent eventOnSettings;
    public UnityEvent eventOnCredits;
    public UnityEvent eventOnBuyDLC;


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonQuitGame"), null, eventOnQuitGame);

        var buttonContinue = RegisterButton(new FloatingButton(doc, "ButtonContinue"), "CONTINUE", eventOnContinue);
        buttonContinue.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonContinue"));

        var buttonNewGame = RegisterButton(new FloatingButton(doc, "ButtonNewGame"), "NEW GAME", eventOnNewGame);
        buttonNewGame.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonNewGame"));

        var buttonSettings = RegisterButton(new FloatingButton(doc, "ButtonSettings"), "SETTINGS", eventOnSettings);
        buttonSettings.SetHorizontalDirection(true);
        buttonSettings.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonSettings"));

        var buttonCredits = RegisterButton(new FloatingButton(doc, "ButtonCredits"), "CREDITS", eventOnCredits);
        buttonCredits.SetHorizontalDirection(true);
        buttonCredits.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonCredits"));

        RegisterButton(new CustomButton(doc, "ButtonBuyDLC"), "BUY NOW", eventOnBuyDLC, () => EnableUI());
    }

}
