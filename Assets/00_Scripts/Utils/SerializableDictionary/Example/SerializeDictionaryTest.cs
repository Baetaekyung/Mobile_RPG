using UnityEngine;

public class SerializeDictionaryTest : MonoBehaviour
{
    [SerializeField] private SerializableDictionary<string, Rigidbody> datas = new();
}
