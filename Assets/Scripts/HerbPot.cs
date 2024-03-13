using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Spawning;

//ֻҪ���͵������¼�����֮�����ɶ�Ӧ�����Ĳ�ҩ
public class HerbPot : MonoBehaviour
{
    public GameObject CollectPot;
    public List<int> counts; // �洢��Ӧ��ҩ�������б�
    public List<GameObject> HerbList;
    public Transform SpawnPoint;

    private CollectPot _collectPotScript;
    private NetworkSpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.TransportToRoom += GenerateHerbs;
        _collectPotScript = CollectPot.GetComponent<CollectPot>();
        _spawnManager = NetworkSpawnManager.Find(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S)) 
        {
            GenerateHerbs();
        }
    }

    private void GenerateHerbs()
    {
        if (_collectPotScript != null)
        {
            counts =  _collectPotScript.GetHerbsCount();
        }
        for (int i = 0; i < counts.Count; i++)
        {
            GameObject prefab = HerbList[i];
            int quantity = counts[i];

            for (int j = 0; j < quantity; j++)
            {
                // ʵ����GameObject
                var go = _spawnManager.SpawnWithPeerScope(prefab);
                //var potParticle = go.GetComponent<PotParticle>();
                //GameObject instance = Instantiate(prefab, transform.position, Quaternion.identity);
                go.transform.position = SpawnPoint.position;
                // ����Rigidbody���������
                Rigidbody rb = go.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;   
                }
            }
        }

    }
}
