using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private PortraitSpriteSO _sprite;

    [SerializeField] private float _speed;

    private Image _image;

    public PortraitSpriteSO Sprite
    {
        get { return _sprite; }
        set { 
            _sprite = value;
            _image.enabled = _sprite != null;
            UpdateSprite(); 
        }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        UpdateSprite();
    }

    void Update()
    {
        if (_sprite != null) UpdateSprite();
    }

    public void UpdateSprite()
    {
        if (_sprite == null)
        {
            _image.sprite = null;
            return;
        }

        int longTime = (int)(Time.time * _speed);
        _image.sprite = _sprite.Sprites[longTime % _sprite.Sprites.Count];
    }
}
