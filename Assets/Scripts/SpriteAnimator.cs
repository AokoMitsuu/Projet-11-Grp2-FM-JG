using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private AnimatedSpriteSO _sprite;

    [SerializeField] private float _speed;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        UpdateSprite();
    }

    void Update()
    {
        UpdateSprite();
    }

    public void SetSprite(AnimatedSpriteSO sprite)
    {
        _sprite = sprite;
        UpdateSprite();
    }

    public void UpdateSprite()
    {
        int longTime = (int)(Time.time * _speed);
        _image.sprite = _sprite.Sprites[longTime % _sprite.Sprites.Count];
    }
}
