using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

//herb��������Ҫͬ����ҩˮҲ����Ҫ�����ߵ�ҩˮ����ת�ͻ����������Ӽ�
//��Ҫͬ����: ��û�б������� �ɴ��ͬ���������ʱ���������õ�ҩˮ������һ�������еľͿ�����
public class IngredientManager : MonoBehaviour
{
    public List<string> PotionName = new List<string>();
    public List<string> FragName = new List<string>();
    public List<int> PotionCount = new List<int>();
    public List<int> FragCount = new List<int>();
    public List<bool> PotionGenerated = new List<bool>();
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
        PotionGenerated = new List<bool>(new bool[FragName.Count]);
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
                    //��������ĳ�ͬ��destroy
                    Destroy(hitCollider.gameObject);
                    break; // ƥ��ɹ�������ѭ��
                }
            }
        }

        if (FragCount[0] >=1 && PotionCount[0] >= 1)
        {
            PotionGenerated[0] = true;
            // ����list����������б�
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else if(FragCount[1] >= 1 && PotionCount[1] >= 1)
        {
            PotionGenerated[1] = true;
            // ����list����������б�
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        else if(FragCount[2] >= 1 && PotionCount[2] >= 1)
        {
            PotionGenerated[2] = true;
            // ����list����������б�
            FragCount = FragCount.Select(i => 0).ToList();
            PotionCount = PotionCount.Select(i => 0).ToList();
        }
        
    }
}
