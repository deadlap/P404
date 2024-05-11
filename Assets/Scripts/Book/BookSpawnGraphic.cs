using System;
using Unity.Mathematics;
using UnityEngine;

public class BookSpawnGraphic : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 originPos;
    Vector3 targetPos;
    float threshold = 0.001f;

    void Update()
    {
        if(Camera.main != null)
            transform.LookAt(Camera.main.transform);
        transform.rotation = new Quaternion(-90,90,90,0);
        
        targetPos = GameObject.FindWithTag("NonDominantHand").transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPos) < threshold)
            Destroy(this);
    }
}
