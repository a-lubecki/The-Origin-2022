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

    VisualElement[] circles = new VisualElement[CIRCLES_COUNT];
    Vector3[] circlesHalfSizes = new Vector3[CIRCLES_COUNT];


    protected override void InitUI(UIDocument doc) {

        var root = doc.rootVisualElement;
        topBackground = root.Q<VisualElement>("TopBackground");
        labelScreenTitle = root.Q<Label>("ScreenTitle");

        for (int i = 0; i < CIRCLES_COUNT; i++) {
            circles[i] = root.Q<VisualElement>("Circle" + i);
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
            AnimateCircleRotations(circles[i], ANIMATED_CIRCLE_ANGLES[i], 0.5f * i, 2);
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

            var circle = circles[i];
            var endScale = scales[i];
            var endPos = positions[i] - circlesHalfSizes[i];//Vector3.Scale(circlesHalfSizes[i], endScale);//shift half the size of the circle to have it centered

            var t = circle.transform;

            if (durationSec <= 0) {
                //instant
                t.position = endPos;
                t.scale = endScale;

            } else {
                //animated
                DOTween.To(() => t.position, x => t.position = x, endPos, durationSec).SetEase(Ease.OutQuad);
                DOTween.To(() => t.scale, x => t.scale = x, endScale, durationSec).SetEase(Ease.OutQuad);
            }
        }
    }

}
