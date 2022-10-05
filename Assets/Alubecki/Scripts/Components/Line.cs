using UnityEngine;
using UnityEngine.UIElements;


public class Line : VisualElement {


    public Vector2 Origin { get; private set; }
    public Vector2 Destination { get; private set; }
    public float Thickness { get; private set; }


    public Line(Vector2 origin, Vector2 destination, float thickness) {

        SetOrigin(origin);
        SetDestination(destination);
        SetThickness(thickness);

        generateVisualContent = OnGenerateVisualContent;
    }

    void OnGenerateVisualContent(MeshGenerationContext mgc) {

        var p = mgc.painter2D;

        p.strokeColor = Color.white;
        p.lineCap = LineCap.Round;
        p.lineWidth = Thickness;

        p.BeginPath();
        p.MoveTo(Origin);
        p.LineTo(Destination);
        p.Stroke();
    }

    public void SetOrigin(Vector2 origin) {

        Origin = origin;
        MarkDirtyRepaintSafe();
    }

    public void SetDestination(Vector2 destination) {

        Destination = destination;
        MarkDirtyRepaintSafe();
    }

    public void SetThickness(float thickness) {

        Thickness = thickness;
        MarkDirtyRepaintSafe();
    }

    /// <summary>
    /// Avoid a crash not resolved by Unity when the visuale element is already repainting
    /// </summary>
    void MarkDirtyRepaintSafe() {

        try {
            MarkDirtyRepaint();
        } catch {
            //no need to flood logs with errors
        }
    }

}