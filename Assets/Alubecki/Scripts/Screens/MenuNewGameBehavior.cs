using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuNewGameBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;
    public UnityEvent eventOnDifficultyChosen;


    public GameDifficulty difficulty;


    protected override void InitUI(UIDocument doc) {

        InitButton("ButtonBack", eventOnBack);
        InitButton("ButtonEasy", eventOnDifficultyChosen, () => difficulty = GameDifficulty.EASY);
        InitButton("ButtonMedium", eventOnDifficultyChosen, () => difficulty = GameDifficulty.MEDIUM);
        InitButton("ButtonHard", eventOnDifficultyChosen, () => difficulty = GameDifficulty.HARD);
    }

}

public enum GameDifficulty {
    EASY,
    MEDIUM,
    HARD
}