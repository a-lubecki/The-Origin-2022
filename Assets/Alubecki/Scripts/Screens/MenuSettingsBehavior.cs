using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuSettingsBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;


    protected override void InitUI(UIDocument doc) {

        InitButton("ButtonBack", eventOnBack);
    }

}
