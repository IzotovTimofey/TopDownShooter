using UnityEngine;

public class Item : MonoBehaviour
{
    //TODO: Сделать интерактор для персонажа, не важно, какой предмет подбирает игрок, сам объект это контейнер для СО
    [SerializeField] private ScriptableObject _item;

    public ScriptableObject Collectable => _item;
}
