using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [Header("--- Slimes ---")]
    [SerializeField] GameObject m_bigSlimePrefab;
    [SerializeField] GameObject m_mediumSlimePrefab;
    [SerializeField] GameObject m_smallSlimePrefab;
    [Space(12)]
    [SerializeField] public int m_nbOfBigSlimes = 5;
    [SerializeField] public int m_nbOfMediumSlimes = 10;
    [SerializeField] public int m_nbOfSmallSlimes = 15;
    [Space(12)]
    [SerializeField] public List<GameObject> m_poolListBigSlimes = new List<GameObject>();
    [SerializeField] public List<GameObject> m_poolListMediumSlimes = new List<GameObject>();
    [SerializeField] public List<GameObject> m_poolListSmallSlimes = new List<GameObject>();
    [Space(12)]
    [SerializeField] Transform m_slimeTransform;


    // Start is called before the first frame update
    void Start()
    {
        //Big Slime Spawn
        CreateInstances(m_nbOfBigSlimes, m_poolListBigSlimes, m_bigSlimePrefab, m_slimeTransform);
        //Medium Slime Spawn
        CreateInstances(m_nbOfMediumSlimes, m_poolListMediumSlimes, m_mediumSlimePrefab, m_slimeTransform);
        //Small Slime Spawn
        CreateInstances(m_nbOfSmallSlimes, m_poolListSmallSlimes, m_smallSlimePrefab, m_slimeTransform);
    }


    private void CreateInstances(int _amountOfInstances, List<GameObject> _pooList, GameObject _prefab, Transform _transform)
    {
        for (int i = 0; i < _amountOfInstances; i++)
        {
            GameObject instance = Instantiate(_prefab, _transform);
            instance.SetActive(false);
            _pooList.Add(instance);
        }
    }

    public GameObject GetSmallSlime()
    {
        for (int i = 0; i < m_poolListSmallSlimes.Count; i++)
        {
            if (m_poolListSmallSlimes[i].activeSelf == false) return m_poolListSmallSlimes[i];
        }

        GameObject instanceSomething = Instantiate(m_smallSlimePrefab, m_slimeTransform);
        instanceSomething.SetActive(false);
        m_poolListSmallSlimes.Add(instanceSomething);
        return instanceSomething;
    }




    public GameObject GetFirstAvailable(List<GameObject> _poolList, GameObject _prefab, Transform _transform)
    {
        for (int i = 0; i < _poolList.Count; i++)
        {
            if (_poolList[i].activeSelf == false) return _poolList[i];
        }

        GameObject instanceSomething = Instantiate(_prefab, _transform);
        instanceSomething.SetActive(false);
        _poolList.Add(instanceSomething);
        return instanceSomething;
    }


    void Update()
    {
        
    }
}
