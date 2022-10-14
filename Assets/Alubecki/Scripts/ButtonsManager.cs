using UnityEngine;


public class ButtonsManager : MonoBehaviour {


    public static ButtonsManager Instance { get; private set; }

    [SerializeField] AudioSource audioSourceSounds;

    [SerializeField] AudioClip audioClipHover;
    [SerializeField] AudioClip audioClipClick;


    void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void PlaySoundButtonHover() {
        audioSourceSounds.PlayOneShot(audioClipHover);
    }

    public void PlaySoundButtonClick() {
        audioSourceSounds.PlayOneShot(audioClipClick);
    }

}
