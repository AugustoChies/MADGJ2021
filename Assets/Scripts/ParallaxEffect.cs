using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _lastCameraPos;
    [SerializeField] private Vector2 effectMultiplier;

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPos = _cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPos;
        transform.position += new Vector3(deltaMovement.x * effectMultiplier.x, deltaMovement.y * effectMultiplier.y, deltaMovement.z) ;
        _lastCameraPos = _cameraTransform.position;
    }
}
