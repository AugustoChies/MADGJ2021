using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform _cameraTransform;
    private Vector3 _lastCameraPos;
    [SerializeField] private Vector2 effectMultiplier;

    void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPos = _cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPos;
        Vector3 newOffset = new Vector3(deltaMovement.x * effectMultiplier.x, deltaMovement.y * effectMultiplier.y, deltaMovement.z);
        transform.position +=  newOffset;
        _lastCameraPos = _cameraTransform.position;
    }
}
