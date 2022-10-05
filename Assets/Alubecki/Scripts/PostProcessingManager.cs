using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessingManager : MonoBehaviour {


    AutoExposure autoExposure;


    void Awake() {

        GetComponent<PostProcessVolume>().profile.TryGetSettings(out autoExposure);
    }

    public void SetBlackScreen(bool animated) {

        SetAnimated(animated);
        autoExposure.keyValue.value = 0f;
    }

    public void SetDefaultScreen(bool animated) {

        SetAnimated(animated);
        autoExposure.keyValue.value = 1f;
    }

    public void SetWhiteScreen(bool animated) {

        SetAnimated(animated);
        autoExposure.keyValue.value = 100f;
    }

    void SetAnimated(bool animated) {

        autoExposure.eyeAdaptation.value = animated ? EyeAdaptation.Progressive : EyeAdaptation.Fixed;
    }

}
