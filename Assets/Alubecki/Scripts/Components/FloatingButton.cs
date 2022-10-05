using UnityEngine;
using UnityEngine.UIElements;


public class FloatingButton : CustomButton {


    const float LINE_THICKNESS = 4;
    const float HALF_LINE_THICKNESS = 0.5f * LINE_THICKNESS;


    VisualElement container;
    VisualElement destinationElement;
    Line line;

    public bool IsLeftOriented { get; private set; }


    public FloatingButton(UIDocument doc, string buttonId) : base(doc, buttonId) {
    }

    public FloatingButton(TemplateContainer instance) : base(instance) {
    }

    protected override void Init(TemplateContainer instance) {
        base.Init(instance);

        container = instance.ElementAt(0);
        SetHorizontalDirection(false);

        instance.generateVisualContent = OnGenerateVisualContent;
    }

    void OnGenerateVisualContent(MeshGenerationContext mgc) {

        //update origin and destination
        if (line != null) {
            line.SetOrigin(GetLineOrigin());
            line.SetDestination(GetAbsolutePos(destinationElement));
        }
    }

    public void SetHorizontalDirection(bool isLeft) {

        IsLeftOriented = isLeft;
        container.style.flexDirection = isLeft ? FlexDirection.RowReverse : FlexDirection.Row;

        //redraw the line
        Instance.MarkDirtyRepaint();
    }

    public void SetLineDestination(VisualElement parent, VisualElement destinationElement) {

        this.destinationElement = destinationElement;

        var destination = GetAbsolutePos(destinationElement);

        if (line != null) {
            //redraw the line
            Instance.MarkDirtyRepaint();
            return;
        }

        line = new Line(GetLineOrigin(), destination, LINE_THICKNESS);

        parent.Add(line);
    }

    Vector2 GetLineOrigin() {

        var b = Instance.worldBound;
        var y = b.yMax - HALF_LINE_THICKNESS;

        return IsLeftOriented ? new Vector2(b.xMin, y) : new Vector2(b.xMax, y);
    }

    static Vector2 GetAbsolutePos(VisualElement element) {

        var b = element.worldBound;
        return new Vector2(
            b.x + 0.5f * b.width,
            b.y + 0.5f * b.height
        );
    }

}
