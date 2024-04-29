// Based upon Daniel Ilett's "Portals URP"
// https://github.com/daniel-ilett/portals-urp/tree/main
// https://www.youtube.com/watch?v=PkGjYig8avo

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using RenderPipeline = UnityEngine.Rendering.RenderPipelineManager;

public class PortalView : MonoBehaviour
{
    [SerializeField]
    public Transform otherPortal;

    [SerializeField]
    private int iterations = 7;

    private Camera portalCamera;

    private MeshRenderer myRenderer;

    private RenderTexture tempTexture;

    private Camera mainCamera;

    private void Awake()
    {
        // If this is linked to another portal, link that back to this (if it doesn't already have another link)
        if (otherPortal != null) { if (otherPortal.GetComponent<PortalView>().otherPortal == null) { otherPortal.GetComponent<PortalView>().otherPortal = transform; otherPortal.GetComponent<PortalView>().WokenUp(); } }
        else { Debug.LogWarning($"Missing otherPortal at {name}, script ended prematurely"); this.enabled = false; return; }

        WokenUp();
    }

    public void WokenUp()
    {
        //Debug.Log($"Nevermind previous message from {name}, I got woken up by {otherPortal.name} :D");

        mainCamera = Camera.main;
        myRenderer = GetComponentInChildren<MeshRenderer>();
        portalCamera = GetComponentInChildren<Camera>();

        tempTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32);
    }

    private void Start()
    {
        myRenderer.material.mainTexture = tempTexture;
    }

    private void OnEnable()
    {
        RenderPipeline.beginCameraRendering += UpdateCamera;
    }

    private void OnDisable()
    {
        RenderPipeline.beginCameraRendering -= UpdateCamera;
    }

    void UpdateCamera(ScriptableRenderContext SRC, Camera camera)
    {
        if (myRenderer.isVisible)
        {
            portalCamera.targetTexture = tempTexture;
            for (int i = iterations - 1; i >= 0; --i)
            {
                RenderCamera(transform, otherPortal, i, SRC);
            }
        }
    }

    private void RenderCamera(Transform inTransform, Transform outTransform, int iterationID, ScriptableRenderContext SRC)
    {
        Transform cameraTransform = portalCamera.transform;
        cameraTransform.position = transform.position;
        cameraTransform.rotation = transform.rotation;

        for (int i = 0; i <= iterationID; ++i)
        {
            // Position the camera behind the other portal.
            Vector3 relativePos = inTransform.InverseTransformPoint(cameraTransform.position);
            relativePos = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativePos;
            cameraTransform.position = outTransform.TransformPoint(relativePos);

            // Rotate the camera to look through the other portal.
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * cameraTransform.rotation;
            relativeRot = Quaternion.Euler(0.0f, 180.0f, 0.0f) * relativeRot;
            cameraTransform.rotation = outTransform.rotation * relativeRot;
        }

        // Set the camera's oblique view frustum.
        Plane p = new Plane(-outTransform.forward, outTransform.position);
        Vector4 clipPlaneWorldSpace = new Vector4(p.normal.x, p.normal.y, p.normal.z, p.distance);
        Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * clipPlaneWorldSpace;

        var newMatrix = mainCamera.CalculateObliqueMatrix(clipPlaneCameraSpace);
        portalCamera.projectionMatrix = newMatrix;

        // Render the camera to its render target.
        UniversalRenderPipeline.RenderSingleCamera(SRC, portalCamera);
    }
}
