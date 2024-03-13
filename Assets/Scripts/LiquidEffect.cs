using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;

/*
 * �����������ȷһ�����_maxAmont��Щ��Ҫ���������rotation���任
 */
public class LiquidEffect : MonoBehaviour
{
    public bool isPouring;

    // Start is called before the first frame update
    private Renderer _rend;
    private Material _material;

    [SerializeField]
    private float _minAmont;
    [SerializeField]
    private float _maxAmont;
    private float _curAmount;
    private float _step = 0.001f;

    private void Awake()
    {
        
    }

    void Start()
    {
        _rend = GetComponent<Renderer>();
        _material = _rend.material;
        _material.SetFloat("_FillAmount", _minAmont);
        _curAmount = _minAmont;
    }

    // Update is called once per frame
    void Update()
    {
        _material.SetFloat("_WorldCoordY", transform.position.y);
    }

    private void FixedUpdate()
    {
        Vector3 forwardDirection = transform.up;
        float dotProduct = Vector3.Dot(forwardDirection, Vector3.up);

        // �жϼн��Ƿ񳬹�90��
        if (dotProduct < Mathf.Cos(45 * Mathf.Deg2Rad))
        {
            if(Mathf.Abs(_minAmont - _curAmount) <= 0.0005f)
            {
                //��һ�ο�ʼ�㵹����Ϊ��ת��������Ҫ����ƫ��
                //��ʵ�����ƫ�Ƹ���rotation����lerp������0.05f
                //��ͼ�޸�����һ�����㵹�ܻ���һ���ӱ����Ĵ���
                _maxAmont += 0.4f;
                _minAmont += 0.15f;
                _curAmount = _minAmont;
            }
            //print(_curAmount);
            isPouring = true;
            _curAmount = Mathf.Min(_curAmount + _step, _maxAmont);
            _material.SetFloat("_FillAmount", _curAmount);
            Debug.Log("�����ǰ����y��ļнǳ���90��");
        }
        else
        {
            isPouring = false;
        }
    }
}
