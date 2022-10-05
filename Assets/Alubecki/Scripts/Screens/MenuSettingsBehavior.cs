using UnityEngine.Events;
using UnityEngine.UIElements;


public class MenuSettingsBehavior : BaseMenuBehavior {


    public UnityEvent eventOnBack;


    protected override void InitUI(UIDocument doc) {

        RegisterButton(new CustomButton(doc, "ButtonBack"), null, eventOnBack);
    }

}
