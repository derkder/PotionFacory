﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LiquidEffect : MonoBehaviour
{
    private float RefillRadius = 1.5f;
    private float PouredRadius = 5f;
    public bool isPouring;
    public bool hasPoured;
    public float _curAmount;
    public Transform PotionRecharge;
    public Transform CookPot;
    public IngredientManager IngredientManager;

    public event Action HasPoured;


    // Start is called before the first frame update
    private Renderer _rend;
    private Material _material;

    [SerializeField]
    private float _minAmont;
    [SerializeField]
    private float _maxAmont;
    private float _step = 0.001f;


    void Start()
    {
        _rend = GetComponent<Renderer>();
        _material = _rend.material;
        _material.SetFloat("_FillAmount", _minAmont);
        _curAmount = _minAmont;
        IngredientManager = CookPot.GetComponent<IngredientManager>();
        HasPoured += IngredientManager.PotionNumUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        _material.SetFloat("_WorldCoordY", transform.position.y);
        _material.SetFloat("_FillAmount", _curAmount);
        if ((_curAmount != _minAmont) && (Vector3.Distance(PotionRecharge.position, transform.position) < RefillRadius))
        {
            Debug.Log("LiquidRefill");
            _curAmount = _minAmont;
            hasPoured = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = transform.up;
        float dotProduct = Vector3.Dot(forwardDirection, Vector3.up);

        // angle larger than 90
        if (dotProduct < Mathf.Cos(50 * Mathf.Deg2Rad))
        {
            // only enter once
            if (Mathf.Abs(_minAmont - _curAmount) <= _step - 0.0005)
            {
                AudioManager.Instance.PlaySFX(SfxType.Pour);
                //fix when first pour, it refill to max
                Debug.Log("FirstBiggerThan50");
                _curAmount = _minAmont + 0.08f + _step;
                if (Vector3.Distance(CookPot.position, this.transform.position) < PouredRadius)
                {
                    if (!hasPoured)
                    {
                        HasPoured?.Invoke();
                        hasPoured = true;
                    }
                }
            }
            isPouring = true;
            _curAmount = Mathf.Min(_curAmount + _step, _maxAmont + 0.4f);
            _material.SetFloat("_FillAmount", _curAmount);
        }
        else
        {
            isPouring = false;
        }
    }
}