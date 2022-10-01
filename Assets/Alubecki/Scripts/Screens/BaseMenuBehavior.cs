using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]
public abstract class BaseMenuBehavior : MonoBehaviour {


    [field: SerializeField] public string ScreenTitle { get; private set; }

    UIDocument document;
    VisualElement body;

    List<CustomButton> buttons = new List<CustomButton>();

    //Coroutine coroutineVisibility;


    protected abstract void InitUI(UIDocument doc);

    protected virtual void Awake() {

        document = GetComponent<UIDocument>();
        body = document.rootVisualElement.Q<VisualElement>("Body");
    }

    protected virtual void OnEnable() {

        //reinit UI references every time the game object become active to avoid a bug with the buttons click
        buttons.Clear();
        InitUI(document);

        EnableAllButtons();
    }

    protected virtual void OnDisable() {

        DisableAllButtons();
    }

    protected T RegisterButton<T>(T button, string buttonText, UnityEvent buttonEvent, Action additionalAction = null) where T : CustomButton{

        buttons.Add(button);

        if (!string.IsNullOrEmpty(buttonText)) {
            button.SetText(buttonText);
        }

        button.AddOnClickListener(() => {

            DisableAllButtons();

            additionalAction?.Invoke();
            buttonEvent?.Invoke();
        });

        return button;
    }

    protected Button InitButton(string buttonName, UnityEvent buttonEvent, Action additionalAction = null) {
/*
        Button button = document.rootVisualElement.Q<Button>(buttonName);
        if (button == null) {
            throw new ArgumentException("The button " + buttonName + " was not declard in the UIDocument");
        }

        buttons.Add(button);

        button.clicked += () => {

            DisableAllButtons();

            additionalAction?.Invoke();
            buttonEvent?.Invoke();
        };

        return button;*/
        return null;
    }

    public void Show(bool animated) {
/*
        if (coroutineVisibility != null) {
            StopCoroutine(coroutineVisibility);
            coroutineVisibility = null;
        }
*/
        gameObject.SetActive(true);
/*        if (animated && body != null) {
            coroutineVisibility = StartCoroutine(ShowAnimated());
        } else {
            document.enabled = true;
        }*/
    }

    IEnumerator ShowAnimated() {

        document.enabled = true;

Debug.LogError("TODO");yield return null;
/*        body.AddToClassList("hidden");

        yield return new WaitForEndOfFrame();

        //body.AddToClassList("visible-animated");
        //body.RemoveFromClassList("hidden");

        //yield return new WaitWhile(() => body.style.opacity.value < 1);

        //body.RemoveFromClassList("visible-animated");*/
    }

    public void Hide(bool animated) {

        gameObject.SetActive(false);
/*
        if (animated && body != null) {
            coroutineVisibility = StartCoroutine(HideAnimated());
        } else {
            document.enabled = false;
        }*/
    }

    IEnumerator HideAnimated() {

Debug.LogError("TODO");yield return null;
/*
        body.AddToClassList("hidden-animated");

        yield return new WaitWhile(() => body.style.opacity.value > 0);

        body.RemoveFromClassList("hidden-animated");
*/
        document.enabled = false;
    }

    void EnableAllButtons() {

        foreach (var button in buttons) {
            button.SetEnabled(true);
        }
    }

    void DisableAllButtons() {

        foreach (var button in buttons) {
            button.SetEnabled(false);
        }
    }

}
