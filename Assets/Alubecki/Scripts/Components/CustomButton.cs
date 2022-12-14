using System;
using UnityEngine;
using UnityEngine.UIElements;


public class CustomButton {


    protected TemplateContainer Instance { get; }
    Button button;


    public CustomButton(UIDocument doc, string buttonId) : this(doc.rootVisualElement.Q<TemplateContainer>(buttonId)) {
    }

    public CustomButton(TemplateContainer instance) {

        Instance = instance;
        button = instance.Q<Button>();

        button.clicked += PlaySoundButtonClick;
        button.RegisterCallback<PointerOverEvent>(_ => ButtonsManager.Instance.PlaySoundButtonHover());

        Init(instance);
    }

    protected virtual void Init(TemplateContainer instance) {
        //override if necessary
    }

    public void SetEnabled(bool isEnabled) {
        button.SetEnabled (isEnabled);
    }

    public void SetText(string text) {
        button.text = text;
    }

    public void AddOnClickListener(Action onClick) {
        button.clicked += onClick;
    }

    public virtual void AddOnHoverListener(Action onPointerEnter, Action onPointerExit) {

        button.RegisterCallback<PointerOverEvent>(_ => onPointerEnter());
        button.RegisterCallback<PointerOutEvent>(_ => onPointerExit());
    }

    void PlaySoundButtonClick() {
        ButtonsManager.Instance.PlaySoundButtonClick();
    }

}
