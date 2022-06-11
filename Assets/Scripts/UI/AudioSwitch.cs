using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSwitch : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;

    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    private Toggle toggle;
    private Image backgroundImage;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        backgroundImage = toggle.GetComponentInChildren<Image>();
        backgroundImage.sprite = on;
    }

    public void StartSwitch(bool enebled)
    {
        if (enebled)
        {
            mixer.audioMixer.SetFloat("masterVol", 0);
            backgroundImage.sprite = on;
        } else
        { 
            mixer.audioMixer.SetFloat("masterVol", -80);
            backgroundImage.sprite = off;
        }

    }
}
