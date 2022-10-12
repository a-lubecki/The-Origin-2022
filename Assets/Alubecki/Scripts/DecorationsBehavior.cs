using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


public class DecorationsBehavior : BaseMenuBehavior {


    private const int CIRCLES_COUNT = 4;
    private float[][] ANIMATED_CIRCLE_ANGLES = {
        new float[] { 45, -10 },
        new float[] { -90, 180, 90 },
        new float[] {},
        new float[] { -10, -30, -15 }
    };


    VisualElement topBackground;
    Label labelScreenTitle;

    VisualElement[] circles = new VisualElement[CIRCLES_COUNT];//outer elements, used to scale circle
    VisualElement[] circlesImage = new VisualElement[CIRCLES_COUNT];//inner elements containing image, used to rotate circle
    Vector3[] circlesHalfSizes = new Vector3[CIRCLES_COUNT];


    protected override void InitUI(UIDocument doc) {

        var root = doc.rootVisualElement;
        topBackground = root.Q<VisualElement>("TopBackground");
        labelScreenTitle = root.Q<Label>("ScreenTitle");

        for (int i = 0; i < CIRCLES_COUNT; i++) {
            var circle = root.Q<VisualElement>("Circle" + i);
            circles[i] = circle;
            circlesImage[i] = circle[0];
        }

        //wait for the first draw to calculate the circles bounds
        doc.rootVisualElement.generateVisualContent += OnGenerateVisualContent;
    }

    void OnGenerateVisualContent(MeshGenerationContext mgc) {

        Document.rootVisualElement.generateVisualContent -= OnGenerateVisualContent;

        //calculte the circles bounds to shift them half the size during the move to have them centered
        for (int i = 0; i < CIRCLES_COUNT; i++) {
            var bound = circles[i].localBound;
            circlesHalfSizes[i] = new Vector3(0.5f * bound.width, 0.5f * bound.height, 0);
        }
    }

    protected override void OnMenuShow() {
        base.OnMenuShow();

        //animate circle rotation
        for (int i = 0; i < CIRCLES_COUNT; i++) {
            AnimateCircleRotations(circlesImage[i], ANIMATED_CIRCLE_ANGLES[i], 0.5f * i, 2);
        }
    }

    void AnimateCircleRotations(VisualElement circle, float[] anglesSequence, float delaySec, float durationSec) {

        if (anglesSequence.Length <= 0) {
        //no rotation
            return;
        }

        var t = circle.transform;
        var s = DOTween.Sequence()
            .AppendInterval(delaySec);

        foreach (var angle in anglesSequence) {
            s.Append(DOTween.To(() => t.rotation, x => t.rotation = x, new Vector3(0, 0, angle), durationSec));
        }

        s.SetLoops(-1, LoopType.Yoyo);
    }

    public void UpdateScreenTitle(string title) {

        if (string.IsNullOrEmpty(title)) {
            topBackground.style.display = DisplayStyle.None;
            return;
        }

        topBackground.style.display = DisplayStyle.Flex;

        labelScreenTitle.text = title;
    }

    public void MoveCircles(Vector3[] positions, Vector3[] scales, float durationSec) {

        if (positions.Length != CIRCLES_COUNT) {
            throw new ArgumentException("Wrong number of circle positions, it must be " + CIRCLES_COUNT);
        }

        if (scales.Length != CIRCLES_COUNT) {
            throw new ArgumentException("Wrong number of circle scales, it must be " + CIRCLES_COUNT);
        }

        for (int i = 0; i < CIRCLES_COUNT; i++) {

            var endScale = scales[i];
            var endPos = positions[i] - circlesHalfSizes[i];

            var t = circles[i].transform;
            var s = circles[i].style;

            if (durationSec <= 0) {
                //instant
                t.position = endPos;
                s.scale = new Scale(endScale);

            } else {
                //animated
                DOTween.To(() => t.position, x => t.position = x, endPos, durationSec).SetEase(Ease.OutQuad);
                DOTween.To(() => s.scale.value.value, x => s.scale = new Scale(x), endScale, durationSec).SetEase(Ease.OutQuad);
            }
        }
    }

}
