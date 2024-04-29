using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalView : MonoBehaviour
{


    // Based upon AlexDev's code:
    // https://www.youtube.com/watch?v=VituktEvIY8
    //public PortalView otherPortal;
    //[HideInInspector] public Camera camView;
    //[SerializeField] private Shader portalShader;
    //public bool isDuped = false;

    //private Transform mainCamTrans;

    //private MeshRenderer mirrorMesh;

    //private Material mirrorMat;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    mainCamTrans = Camera.main.transform;

    //    camView = GetComponentInChildren<Camera>();
    //    mirrorMesh = GetComponentInChildren<MeshRenderer>();

    //    if (isDuped)
    //    {
    //        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, -transform.localScale.z);
    //        mirrorMesh.transform.localScale = new Vector3(-mirrorMesh.transform.localScale.x, mirrorMesh.transform.localScale.y, -mirrorMesh.transform.localScale.z);
    //    }

    //    // Make sure only two mirrors are linked, remove this to have 3+ mirrors linked
    //    otherPortal.otherPortal = this;

    //    otherPortal.camView.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

    //    mirrorMat = new Material(portalShader);
    //    mirrorMat.mainTexture = otherPortal.camView.targetTexture;
    //    mirrorMesh.material = mirrorMat;
    //}

    //private void Update()
    //{
    //    // Position to match player
    //    Vector3 lookerPosition = otherPortal.transform.worldToLocalMatrix.MultiplyPoint3x4(mainCamTrans.position);
    //    lookerPosition = new Vector3(-lookerPosition.x, lookerPosition.y, lookerPosition.z * (isDuped ? 1f : -1f));
    //    camView.transform.localPosition = lookerPosition;

    //    // Rotation to match player
    //    Quaternion rotDifference = transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation * Quaternion.Euler(0, 180, 0));
    //    camView.transform.rotation = rotDifference * mainCamTrans.rotation;

    //    // Clipping
    //    camView.nearClipPlane = lookerPosition.magnitude;
    //}
}
