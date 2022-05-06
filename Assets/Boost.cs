using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class Boost : MonoBehaviour
{
    private float _boostCountDown = 1f;
    public float BoostCountDown
    {
        get 
        {
            return _boostCountDown;
        }
        set
        {
            _boostCountDown = Mathf.Clamp01(value);
        }
    }
    public float BoostReduceRate = 0.2f;
    public AnimationCurve BoostCurve;
    public float TopSpeedMultiplier = 2f, AccelerationMultiplier = 3f;

    private ArcadeKart _kart;

    private float _kartOriginalAcceleration, _kartOriginalTopSpeed;

    void Awake()
    {
        _kart = GetComponent<ArcadeKart>();
        _kartOriginalAcceleration = _kart.baseStats.Acceleration;
        _kartOriginalTopSpeed = _kart.baseStats.TopSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (BoostCountDown > 0)
        {
            BoostCountDown -= Time.deltaTime * BoostReduceRate;
            _kart.baseStats.Acceleration = Mathf.Max(_kartOriginalAcceleration, _kartOriginalAcceleration * AccelerationMultiplier * BoostCurve.Evaluate(BoostCountDown));
            _kart.baseStats.TopSpeed = _kartOriginalTopSpeed + (_kartOriginalTopSpeed * TopSpeedMultiplier * BoostCurve.Evaluate(BoostCountDown));
        }
    }
}
