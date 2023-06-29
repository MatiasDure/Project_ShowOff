using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class ImageSwitch : MonoBehaviour
{
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private Sprite _doorSprite;

    private void OnTriggerEnter(Collider other)
    {
        _loadingScreen.sprite = _doorSprite;
    }
}
