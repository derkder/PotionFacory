using System.Collections;
using System.Collections.Generic;
using Ubiq.Spawning;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Dropper : MonoBehaviour
{
    //����Ҫ���ɱ�ץ�˶����ǵ�����gripped
    public GameObject CookPot;
    public List<GameObject> PotionLists;
    [SerializeField]
    private InputActionAsset _actionAsset;
    private IngredientManager _ingredientManager;
    private XRGrabInteractable interactable;
    private InputAction _selectAction;
    private InputAction _triggerAction;
    private NetworkSpawnManager _spawnManager;
    private bool _isGripped;
    private bool _isTriggered;
    private bool _hasTrggered;
    private bool _hasPotion;

    // Start is called before the first frame update
    void Start()
    {
        //_selectAction = _actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Select");
        _triggerAction = _actionAsset.FindActionMap("XRI RightHand Interaction").FindAction("Activate");

        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(_ => _isGripped = true);
        interactable.lastSelectExited.AddListener(_ => _isGripped = false);

        _triggerAction.performed += _ => _isTriggered = true;
        _triggerAction.canceled += _ => _isTriggered = false;

        _spawnManager = NetworkSpawnManager.Find(this);
        _ingredientManager = CookPot.GetComponent<IngredientManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(_isGripped && _isTriggered && !_hasTrggered)
        {
            _hasTrggered = true;
        }
        else if(_isGripped && !_isTriggered && _hasTrggered)
        {
            _hasTrggered = false;
            Debug.Log("xishui");
            _hasPotion = true;
        }
        else if(_isGripped && _isTriggered && _hasPotion)
        {
            _hasPotion = false;
            _hasTrggered = false;
            Debug.Log("fangshui");

            GenerateMagicPotion();
        }
    }

    void GenerateMagicPotion()
    {
        for (int i = 0; i < _ingredientManager.PotionGenerated.Count; i++)
        {
            Debug.Log("Dropper GenerateMagicPotion()");
            // ����Ƿ���Ҫ����GameObject
            if (_ingredientManager.PotionGenerated[i] && PotionLists[i] != null)
            {
                GameObject prefab = PotionLists[i];
                var go = _spawnManager.SpawnWithPeerScope(prefab);
                go.transform.position = transform.position;
                _ingredientManager.PotionGenerated[i] = false;
            }
        }

        
    }
}
