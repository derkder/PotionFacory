using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using static SIPSorcery.Net.Mjpeg;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

//herb��������Ҫͬ��
//��Ҫͬ����: ��û�б�������ҩˮ������������ʲôҩˮ
public class IngredientManager : MonoBehaviour
{
    public List<string> PotionName = new List<string>();
    public List<string> FragName = new List<string>();
    public List<int> PotionCount = new List<int>();
    public List<int> FragCount = new List<int>();
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
        ss = StirStick.GetComponent<StirStick>();
        PotionCount = new List<int>(new int[PotionName.Count]);
        FragCount = new List<int>(new int[PotionName.Count]);
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
}
