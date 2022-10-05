using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuNewGameBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;
    public UnityEvent eventOnDifficultyChosen;

    public GameDifficulty difficulty;


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);

        var buttonEasy = RegisterButton(new FloatingButton(doc, "ButtonEasy"), "EASY", eventOnDifficultyChosen, () => difficulty = GameDifficulty.EASY);
        buttonEasy.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonEasy"));

        var buttonMedium = RegisterButton(new FloatingButton(doc, "ButtonMedium"), "MEDIUM", eventOnDifficultyChosen, () => difficulty = GameDifficulty.MEDIUM);
        buttonMedium.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonMedium"));

        var buttonHard = RegisterButton(new FloatingButton(doc, "ButtonHard"), "HARD", eventOnDifficultyChosen, () => difficulty = GameDifficulty.HARD);
        buttonHard.SetLineDestination(Body, doc.rootVisualElement.Q<VisualElement>("TargetButtonHard"));
    }

}

public enum GameDifficulty {
    EASY,
    MEDIUM,
    HARD
}