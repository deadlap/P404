using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class PortalCamMove : MonoBehaviour
{
    private Transform myRelativePortal;
    [SerializeField] private Transform renderPortal;
    private Transform mainCam;

    private Transform portalQuad;

    private Camera myCam;
    private RenderTexture myTexture;
    private Vector2 camFieldDimensions;

    // Create the rendermaterial at runtime
    [SerializeField] private Shader portalShader;
    private Material portalMat;

    private MeshRenderer endRenderFace;

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
                endRenderFace = portalQuad.GetComponent<MeshRenderer>();
                break;
            }
        }
        myCam = GetComponent<Camera>();
        camFieldDimensions = new Vector2(Mathf.Abs(portalQuad.localScale.x), Mathf.Abs(portalQuad.localScale.y));
        Debug.Log($"{name} carries a camFieldDimensions of {camFieldDimensions}");

        myTexture = new RenderTexture(Mathf.RoundToInt(1024 * camFieldDimensions.x), Mathf.RoundToInt(1024 * camFieldDimensions.y), 24);

        portalMat = new Material(portalShader);
        myCam.targetTexture = myTexture;
        portalMat.mainTexture = myTexture;
        portalQuad.GetComponent<MeshRenderer>().material = portalMat;
    }

    private void OnEnable()
    {
        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    // Make camera only record while it's being rendered
    private void UpdateCamera(ScriptableRenderContext SRC, Camera camera)
    {
        if (endRenderFace.isVisible)
        {
            RenderCamera(SRC);
        }
    }

    private void RenderCamera(ScriptableRenderContext SRC)
    {
        Vector3 targetPosition = renderPortal.worldToLocalMatrix.MultiplyPoint3x4(mainCam.position);

        targetPosition = new Vector3(-targetPosition.x, targetPosition.y, -targetPosition.z);

        Vector2 angleMultiplier = new Vector2(Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(Vector3.zero, new Vector3(targetPosition.x, 0f, targetPosition.z))), Mathf.Cos(Mathf.Deg2Rad * Vector3.Angle(Vector3.zero, new Vector3(0f, targetPosition.y, targetPosition.z))));

        myCam.aspect = camFieldDimensions.x * angleMultiplier.x / camFieldDimensions.y * angleMultiplier.y;

        transform.localPosition = targetPosition;

        // Make camera always look at portal and clip to fit the size the player sees the portal as
        transform.LookAt(myRelativePortal);
        myCam.nearClipPlane = transform.localPosition.magnitude;
        myCam.fieldOfView = FOVFromDistance(transform.localPosition.magnitude, camFieldDimensions.y * angleMultiplier.y);

        // Render the camera to its render target.
        UniversalRenderPipeline.RenderSingleCamera(SRC, myCam);
    }

    private float FOVFromDistance(float distance, float height)
    {
        //Debug.Log($"Distance of {distance}, height {height}, angle there would be {Mathf.Rad2Deg * Mathf.Atan(height / 2 / distance)}");
        return 2 * Mathf.Rad2Deg * Mathf.Atan(height / 2 / distance);
    }
}
