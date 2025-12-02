using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float parallaxFactorX = 0.3f;
    [SerializeField] private float parallaxFactorY = 0.1f;

    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPos;
        transform.position += new Vector3(delta.x * parallaxFactorX, delta.y * parallaxFactorY, 0);
        previousCamPos = cam.position;
    }
}
