using MostyProUI.PreferencesControl;

public class VolumeDefaultsController : DefaultsController
{
    
    protected override void Start()
    {
        base.Start();
        CheckForVolumeChangers();
    }

    //Used for performance optimization to prevent calling on Update
    private void CheckForVolumeChangers()
    {
        var volumeControllers = FindObjectsOfType<VolumeController>();
        foreach (var prefSlider in optionSliders)
        {
            foreach (var volumeController in volumeControllers)
            {
                if (volumeController.GetVolumeKey() == prefSlider.prefKey)
                    prefSlider.slider.onValueChanged.AddListener(volumeController.OnVolumeChanged);
            }
        }
    }
}