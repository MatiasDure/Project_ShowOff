using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedIndicator : MonoBehaviour
{
    [SerializeField] private DoorLight[] _doorLights;

    private Color _emissionColor = new Color(0,.82f,.1f);
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(string questFinished in PlayerInfoTracker.Instance.Finished)
        {
            foreach(var light in _doorLights)
            {
                if (!questFinished.Equals(light.Level)) continue;

                light.Mat.EnableKeyword("_EMISSION");
                light.Mat.SetColor("_EmissionColor", _emissionColor);
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var light in _doorLights)
        {
            light.Mat.DisableKeyword("_EMISSION");
            light.Mat.SetColor("_EmissionColor", _emissionColor);
        }
    }
}

[System.Serializable]
public class DoorLight
{
    public Material Mat;
    public string Level;
}
