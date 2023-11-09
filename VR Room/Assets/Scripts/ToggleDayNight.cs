using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDayNight : MonoBehaviour
{
    public Light mLight;

    private Color _dayColor;
    private float _dayIntensity;
    private bool _toggled = false;

    // Cache default day settings
    void Start()
    {
        if (mLight != null)
        {
            _dayColor = mLight.color;
            _dayIntensity = mLight.intensity;
        }
    }

    // Toggle function
    public void ToggleLight()
    {
        if (mLight == null)
            return;

        if (!_toggled) // If currently day, switch to night
        {
            mLight.color = Color.black; // Assume night is completely dark
            mLight.intensity = 0f; // No light intensity for night
        }
        else // If currently night, switch back to day
        {
            mLight.color = _dayColor;
            mLight.intensity = _dayIntensity;
        }

        _toggled = !_toggled;
    }
}
