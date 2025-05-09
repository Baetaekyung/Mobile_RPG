using UnityEngine;

[CreateAssetMenu(menuName = "SO/Pool/GameObject Pool Data")]
public class GameObjectPoolDataSO : BasePoolDataSO
{
    [SerializeField] private GameObject go;

    public GameObject GetGameObject => go;
}
