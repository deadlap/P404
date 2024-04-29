using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamMove : MonoBehaviour
{
    private Transform myRelativePortal;
    [SerializeField] private Transform renderPortal;
    private Transform mainCam;

    private Transform portalQuad;

    private Camera myCam;
    private float camFieldHeight;

    // Create the rendermaterial at runtime
    [SerializeField] private Shader portalShader;
    private Material portalMat;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        myRelativePortal = transform.parent;

        // If no other portal, make this a mirror
        renderPortal = renderPortal == null ? myRelativePortal : renderPortal;

        for (int i = 0; i < renderPortal.childCount; i++)
        {
            if (renderPortal.GetChild(i).name == "PortalViewQuad")
            {
                portalQuad = renderPortal.GetChild(i);
                break;
            }
        }
        myCam = GetComponent<Camera>();
        camFieldHeight = Mathf.Abs(portalQuad.localScale.y);
        Debug.Log($"{name} carries a camFieldHeight of {camFieldHeight}");

        myCam.targetTexture = new RenderTexture(Mathf.RoundToInt(1024 * Mathf.Abs(portalQuad.localScale.x)), Mathf.RoundToInt(1024 * camFieldHeight), 24);

        portalMat = new Material(portalShader);
        portalMat.mainTexture = myCam.targetTexture;
        portalQuad.GetComponent<MeshRenderer>().material = portalMat;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = renderPortal.worldToLocalMatrix.MultiplyPoint3x4(mainCam.position);

        targetPosition = new Vector3(-targetPosition.x, targetPosition.y, -targetPosition.z);

        transform.localPosition = targetPosition;

        // Make camera always look at portal and clip to fit the size the player sees the portal as
        transform.LookAt(myRelativePortal);
        myCam.nearClipPlane = transform.localPosition.magnitude;
        myCam.fieldOfView = FOVFromDistance(transform.localPosition.magnitude, camFieldHeight);
    }

    private float FOVFromDistance(float distance, float height)
    {
        //Debug.Log($"Distance of {distance}, height {height}, angle there would be {Mathf.Rad2Deg * Mathf.Atan(height / 2 / distance)}");
        return 2 * Mathf.Rad2Deg * Mathf.Atan(height / 2 / distance);
    }
}
