using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    void Awake()
    {
        GameObject.Find("XR Origin").transform.position = transform.position;
        GameObject.Find("XR Origin").transform.rotation = transform.rotation;
    }
}
