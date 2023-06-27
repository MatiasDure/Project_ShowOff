using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerAlpha : MonoBehaviour
{
    [Tooltip("The material to lower the alpha")]
    [SerializeField] private Material _material;

    [Range(0f, 1f)]
    [SerializeField] float _fadeStrength;

    private Color _original;
    private Color _faded;

    private bool IsFaded = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateColorVariants();
        ToggleCamera.OnCameraModeChanged += OnShootingMode;
    }

    private void CreateColorVariants()
    {
        _original = _material.color;
        _original.a = 1f;
        _material.color = _original;
        _faded = _material.color;
        _faded.a = _fadeStrength;
    }

    private void OnShootingMode(string pMode)
    {
        if (SadnessQuest.Instance.CurrentQuestState != LevelQuest.QuestState.InQuest || (!IsFaded 
            && !pMode.Equals("Shooting"))) return;

        ToggleAlpha();
    }

    private void ToggleAlpha()
    {
        IsFaded = !IsFaded;
        _material.color = IsFaded ? _faded : _original;
    }

    private void OnDestroy()
    {
        ToggleCamera.OnCameraModeChanged -= OnShootingMode;
    }
}
