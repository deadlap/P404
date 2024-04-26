using UnityEngine;

public class PlayerSpawnPoint : MonoBehaviour
{
    public static PlayerSpawnPoint Instance;
    [SerializeField] GameObject playerPrefab;
    GameObject player;
    void Awake()
    {
        Instance = this;
        if(GameObject.Find("Player(Clone)") == null)
            player = Instantiate(playerPrefab);
        GameObject.Find("XR Origin").transform.position = transform.position;
        GameObject.Find("XR Origin").transform.rotation = transform.rotation;
    }

    public void ResetPlayerPos()
    {
        GameObject.Find("XR Origin").transform.position = transform.position;
        GameObject.Find("XR Origin").transform.rotation = transform.rotation;
    }
}
