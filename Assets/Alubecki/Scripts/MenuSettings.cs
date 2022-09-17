using UnityEngine.Events;


public class MenuSettings : BaseMenuBehavior {


    public UnityEvent eventOnBack;


    protected override void Awake() {
        base.Awake();

        InitButton("ButtonBack", eventOnBack);
    }

}
