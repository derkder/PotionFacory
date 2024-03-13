using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

// ͳ�Ʋɼ����Ĳ�ҩ�ĸ���
// ֻҪ�κ�һ��player���͵������¼�����֮���������жεĲ�ҩ
public class CollectPot : MonoBehaviour
{
    //���ﲻ�ô���Ϣ�ɣ����ߵ��������Ա�������ͬ����
    public float radius = 2.7f; // ���İ뾶
    public List<string> tags; // �洢��ҩ��ǩ���б�
    public List<int> counts; // �洢��Ӧ��ҩ�������б�

    public List<int> tempCounts;

    void Start()
    {
        tags.Add("HerbA");
        tags.Add("HerbB");
        tags.Add("HerbC");

        // ȷ�������б�Ĵ�С���ǩ�б���ͬ������ʼ��Ϊ0
        if (counts.Count != tags.Count)
        {
            counts = new List<int>(new int[tags.Count]);
            tempCounts = new List<int>(new int[tags.Count]);
        }
    }

    public List<int> GetHerbsCount()
    {
        List<int> temp = new List<int>(counts);
        ResetPot();
        return temp;
    }

    private void ResetPot()
    {
        // ��������������Ϊ0
        for (int i = 0; i < counts.Count; i++)
        {
            counts[i] = 0;
            tempCounts[i] = 0;
        }
        // ʹ��Physics.OverlapSphere��ȡָ���뾶�ڵ�������ײ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                // �����ײ��ı�ǩ�Ƿ����б��е�ĳ����ǩ��ƥ��
                if (hitCollider.CompareTag(tags[i]))
                {
                    var GrabObjSync = hitCollider.gameObject.GetComponent<GrabObjAsync>();
                    GrabObjSync.DestroySync();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ��������������Ϊ0
        for (int i = 0; i < counts.Count; i++)
        {
            tempCounts[i] = 0;
        }

        // ʹ��Physics.OverlapSphere��ȡָ���뾶�ڵ�������ײ��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach (var hitCollider in hitColliders)
        {
            for (int i = 0; i < tags.Count; i++)
            {
                // �����ײ��ı�ǩ�Ƿ����б��е�ĳ����ǩ��ƥ��
                if (hitCollider.CompareTag(tags[i]))
                {
                    // ���ƥ�䣬��Ӧ��ǩ��������һ
                    tempCounts[i]++;
                    break; // ƥ��ɹ�������ѭ��
                }
            }
        }

        counts = tempCounts;
    }

}
