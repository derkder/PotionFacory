using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static SIPSorcery.Net.Mjpeg;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

//herb��������Ҫͬ����ҩˮҲ����Ҫ�����ߵ�ҩˮ����ת�ͻ����������Ӽ�
//��Ҫͬ����: ��û�б������� �ɴ��ͬ���������ʱ���������õ�ҩˮ������һ�������еľͿ�����
public class IngredientManager : MonoBehaviour
{
    public List<string> PotionName = new List<string>();
    public List<string> FragName = new List<string>();
    public List<int> PotionCount = new List<int>();
    public List<int> FragCount = new List<int>();
    public List<bool> PotionGenerated = new List<bool>(3);
    public float Radius = 2.7f;

    [SerializeField]
    private ActionBasedController _rightController;
    public GameObject StirStick;
    private StirStick ss;
    private float stirTime;


    void Start()
    {
        PotionName.Add("PotionR");
        PotionName.Add("PotionG");
        PotionName.Add("PotionB");
        FragName.Add("fragA");
        FragName.Add("fragB");
        FragName.Add("fragC");
        ss = StirStick.GetComponent<StirStick>();
        ss.OnPotionMade += GeneratePotion;
        PotionCount = new List<int>(new int[PotionName.Count]);
        FragCount = new List<int>(new int[FragName.Count]);
    }

    // Update is called once per frame
    void Update()
    {
        if (ss.IsStiring)
        {
            stirTime += Time.deltaTime;
        }
        if(stirTime > 1.0f)
        {
            stirTime = 0;
            if (_rightController)
            {
                if (_rightController.SendHapticImpulse(0.5f, 1))
                {
                    print("GeneratePotion");
                }
            }
        }
    }



    public void PotionNumUpdate()
    {
        // ʹ��Physics.OverlapSphere��ȡָ���뾶�ڵ�������ײ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < PotionName.Count; i++)
            {
                // �����ײ��ı�ǩ�Ƿ����б��е�ĳ����ǩ��ƥ��
                if (hitCollider.CompareTag(PotionName[i]))
                {
                    // ���ƥ�䣬��Ӧ��ǩ��������һ
                    PotionCount[i]++;
                    break; // ƥ��ɹ�������ѭ��
                }
            }
        }
    }

    public void GeneratePotion()
    {
        // ʹ��Physics.OverlapSphere��ȡָ���뾶�ڵ�������ײ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < PotionName.Count; i++)
            {
                // �����ײ��ı�ǩ�Ƿ����б��е�ĳ����ǩ��ƥ��
                if (hitCollider.CompareTag(FragName[i]))
                {
                    // ���ƥ�䣬��Ӧ��ǩ��������һ
                    FragCount[i]++;
                    break; // ƥ��ɹ�������ѭ��
                }
            }
        }

        if (FragCount[0] >=1 && PotionCount[0] >= 1)
        {
            PotionGenerated[0] = true;
            
        }
        
    }
}
