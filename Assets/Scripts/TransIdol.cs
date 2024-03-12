using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static SIPSorcery.Net.Mjpeg;

public class TransIdol : MonoBehaviour
{
    public float radius = 3f; // ���İ뾶

    [SerializeField]
    private InputActionAsset _actionAsset;
    private InputAction _buttonXPressed;

    private void Awake()
    {
        _buttonXPressed = _actionAsset.FindActionMap("XRI LeftHand").FindAction("XButton");
        _buttonXPressed.performed += OnXButtonPressed;
    }

    void Start()
    {
        
    }



    // Update is called once per frame
    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        // ʹ��Physics.OverlapSphere��ȡָ���뾶�ڵ�������ײ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            // �����ײ��ı�ǩ�Ƿ����б��е�ĳ����ǩ��ƥ��
            if (hitCollider.CompareTag("Player"))
            {
                GameManager.Instance.Transport();
            }
        }
    }
}
