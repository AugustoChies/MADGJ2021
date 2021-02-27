using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerLightningSpawn : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _intensityDiminishAmt;
    [SerializeField] private float _minimumIntensity;
    [SerializeField] private float _initialRadius;

    [Header("References")]
    [SerializeField] private GameObject _playerDeathLight;

    private readonly List<Light2D> _playerDeathLightList = new List<Light2D>();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void InstantiatePlayerDeath(Vector2 collisor)
    {
        GameObject temp = Instantiate(_playerDeathLight, collisor, Quaternion.Euler(0,0,-90));

        if (_playerDeathLightList.Count > 30)
        {
            Destroy(_playerDeathLightList[0].transform.parent.gameObject);
            _playerDeathLightList.RemoveAt(0);
        }

        _playerDeathLightList.Add(temp.GetComponentInChildren<Light2D>());
        UpdateDeathLightsIntensities();
    }

    public void UpdateDeathLightsIntensities()
    {
        float currentRadius = _initialRadius;

        for(int i = _playerDeathLightList.Count - 1; i >=0; i--)
        {
            _playerDeathLightList[i].pointLightOuterRadius = Mathf.Max(_minimumIntensity, currentRadius);

            currentRadius -= _intensityDiminishAmt;
        }
    }
}
