
using UnityEngine.UIElements;


public class FloatingButton : CustomButton {


    VisualElement container;


    public FloatingButton(UIDocument doc, string buttonId) : base(doc, buttonId) {
    }

    public FloatingButton(TemplateContainer instance) : base(instance) {
    }

    protected override void Init(TemplateContainer instance) {
        base.Init(instance);

        container = instance.ElementAt(0);
    }

    public void SetHoriontalDirection(bool isRight) {

        container.style.flexDirection = isRight ? FlexDirection.Row : FlexDirection.RowReverse;
    }


}
