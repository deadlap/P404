using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour {
    [SerializeField] GameObject windSpawnPoint;
    [SerializeField] GameObject windPrefab;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Wind") && windSpawnPoint.transform.childCount == 0) {
            GameObject wind = Instantiate(windPrefab, windSpawnPoint.transform);
            Destroy(wind, 2f);
        }
    }
}
