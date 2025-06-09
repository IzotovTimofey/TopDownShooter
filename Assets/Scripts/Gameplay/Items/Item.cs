using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    //TODO: Сделать интерактор для персонажа, не важно, какой предмет подбирает игрок, сам объект это контейнер для СО
    [SerializeField] private RangedWeapon _item;

    private SpriteRenderer _renderer;
    public ScriptableObject Collectable => _item;

    public void SetUp()
    {
        _renderer.sprite = _item.Sprite;
    }

    public void OnPickUp()
    {
        gameObject.SetActive(false);
    }
}
