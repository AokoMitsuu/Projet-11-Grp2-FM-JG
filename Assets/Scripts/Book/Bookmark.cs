using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bookmark : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _backgroundImage;

    [Header("Sprites")]
    [SerializeField] private Sprite _defaultIcon;
    [SerializeField] private Sprite _selectedIcon;
    [SerializeField] private Sprite _defaultBackground;
    [SerializeField] private Sprite _selectedBackground;

    public void SetSelected(bool isSelected)
    {
        _iconImage.sprite = isSelected ? _selectedIcon : _defaultIcon;
        _backgroundImage.sprite = isSelected ? _selectedBackground : _defaultBackground;
    }
}
