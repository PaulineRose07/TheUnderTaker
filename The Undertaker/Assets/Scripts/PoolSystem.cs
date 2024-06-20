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

    [Header("--- Hero Weapons ---")]
    [SerializeField] GameObject m_shovelPrefab;
    [SerializeField] public int m_nbOfShovels = 15;
    [SerializeField] Transform m_shovelTransfom;
    [SerializeField] public List<GameObject> m_poolOfShovels = new List<GameObject>();

    [Header("--- Mini Necromancer ---")]
    [SerializeField] GameObject m_miniNecromancerPrefab;
    [SerializeField] public int m_nbOfMiniN = 2;
    [SerializeField] Transform m_miniNTransfom;
    [SerializeField] public List<GameObject> m_poolOfMiniN = new List<GameObject>();


    [Header("--- Skeleton ---")]
    [SerializeField] GameObject m_skeletonPrefab;
    [SerializeField] public int m_nbOfSkeleton = 2;
    [SerializeField] Transform m_SkeletonTransfom;
    [SerializeField] public List<GameObject> m_poolOfSkeleton = new List<GameObject>();
    [Header("--- Bone ---")]
    [SerializeField] GameObject m_bonePrefab;
    [SerializeField] public int m_nbOfBone = 2;
    [SerializeField] Transform m_BoneTransfom;
    [SerializeField] public List<GameObject> m_poolOfBone = new List<GameObject>();


    [Header("--- Items ---")]
    [SerializeField] GameObject m_healPrefab;
    [SerializeField] GameObject m_speedPrefab;
    [SerializeField] GameObject m_shieldPrefab;
    [SerializeField] public int m_nbOfPotions = 2;
    [SerializeField] public List<GameObject> m_poolOfHealPotions = new List<GameObject>();
    [SerializeField] public List<GameObject> m_poolOfSpeedPotions = new List<GameObject>();
    [SerializeField] public List<GameObject> m_poolOfShieldPotions = new List<GameObject>();
    [SerializeField] Transform m_healTransform;
    [SerializeField] Transform m_speedTransform;
    [SerializeField] Transform m_shieldTransform;


    // Start is called before the first frame update
    void Start()
    {
        //Big Slime Spawn
        CreateInstances(m_nbOfBigSlimes, m_poolListBigSlimes, m_bigSlimePrefab, m_slimeTransform);
        //Medium Slime Spawn
        CreateInstances(m_nbOfMediumSlimes, m_poolListMediumSlimes, m_mediumSlimePrefab, m_slimeTransform);
        //Small Slime Spawn
        CreateInstances(m_nbOfSmallSlimes, m_poolListSmallSlimes, m_smallSlimePrefab, m_slimeTransform);
        //Shovels
        CreateInstances(m_nbOfShovels, m_poolOfShovels, m_shovelPrefab, m_shovelTransfom);

        // Shield potion
        CreateInstances(m_nbOfPotions, m_poolOfShieldPotions, m_shieldPrefab, m_shieldTransform);
        // Speed potion
        CreateInstances(m_nbOfPotions, m_poolOfSpeedPotions, m_speedPrefab, m_speedTransform);
        // Heal Potion
        CreateInstances(m_nbOfPotions, m_poolOfHealPotions, m_healPrefab, m_healTransform);
        // Mini Necromancer
        CreateInstances(m_nbOfMiniN, m_poolOfMiniN, m_miniNecromancerPrefab, m_miniNTransfom);
        // Skeleton
        CreateInstances(m_nbOfSkeleton, m_poolOfSkeleton, m_skeletonPrefab, m_SkeletonTransfom);
        // Bones
        CreateInstances(m_nbOfBone, m_poolOfBone, m_bonePrefab, m_BoneTransfom);
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

    public GameObject GetAvailableBone()
    {
        for (int i = 0; i < m_poolOfBone.Count; i++)
        {
            if (m_poolOfBone[i].activeSelf == false) return m_poolOfBone[i];
        }

        GameObject instanceSomething = Instantiate(m_bonePrefab, m_BoneTransfom);
        instanceSomething.SetActive(false);
        m_poolOfBone.Add(instanceSomething);
        return instanceSomething;
    }
    public GameObject GetAvailableSkeleton()
    {
        for (int i = 0; i < m_poolOfSkeleton.Count; i++)
        {
            if (m_poolOfSkeleton[i].activeSelf == false) return m_poolOfSkeleton[i];
        }

        GameObject instanceSomething = Instantiate(m_skeletonPrefab, m_SkeletonTransfom);
        instanceSomething.SetActive(false);
        m_poolOfSkeleton.Add(instanceSomething);
        return instanceSomething;
    }
    public GameObject GetAvailableNecromancer()
    {
        for (int i = 0; i < m_poolOfMiniN.Count; i++)
        {
            if (m_poolOfMiniN[i].activeSelf == false) return m_poolOfMiniN[i];
        }

        GameObject instanceSomething = Instantiate(m_miniNecromancerPrefab, m_miniNTransfom);
        instanceSomething.SetActive(false);
        m_poolOfMiniN.Add(instanceSomething);
        return instanceSomething;
    }
    public GameObject GetAvailableShovel()
    {
        for (int i = 0; i < m_poolOfShovels.Count; i++)
        {
            if (m_poolOfShovels[i].activeSelf == false) return m_poolOfShovels[i];
        }

        GameObject instanceSomething = Instantiate(m_shovelPrefab, m_shovelTransfom);
        instanceSomething.SetActive(false);
        m_poolOfShovels.Add(instanceSomething);
        return instanceSomething;
    }
    public GameObject GetBigSlime()
    {
        for (int i = 0; i < m_poolListBigSlimes.Count; i++)
        {
            if (m_poolListBigSlimes[i].activeSelf == false) return m_poolListBigSlimes[i];
        }

        GameObject instanceSomething = Instantiate(m_smallSlimePrefab, m_slimeTransform);
        instanceSomething.SetActive(false);
        m_poolListBigSlimes.Add(instanceSomething);
        return instanceSomething;
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
    public GameObject GetMediumSlime()
    {
        for (int i = 0; i < m_poolListMediumSlimes.Count; i++)
        {
            if (m_poolListMediumSlimes[i].activeSelf == false) return m_poolListMediumSlimes[i];
        }

        GameObject instanceSomething = Instantiate(m_mediumSlimePrefab, m_slimeTransform);
        instanceSomething.SetActive(false);
        m_poolListMediumSlimes.Add(instanceSomething);
        return instanceSomething;
    }

    public GameObject GetHealingPotion()
    {
        for (int i = 0; i < m_poolOfHealPotions.Count; i++)
        {
            if (m_poolOfHealPotions[i].activeSelf == false) return m_poolOfHealPotions[i];
        }

        GameObject healPotion = Instantiate(m_healPrefab, m_healTransform);
        healPotion.SetActive(false);
        m_poolOfHealPotions.Add(healPotion);
        return healPotion;
    }

    public GameObject GetSpeedPotion()
    {
        for (int i = 0; i < m_poolOfSpeedPotions.Count; i++)
        {
            if (m_poolOfSpeedPotions[i].activeSelf == false) return m_poolOfSpeedPotions[i];
        }

        GameObject speedPotion = Instantiate(m_speedPrefab, m_speedTransform);
        speedPotion.SetActive(false);
        m_poolOfSpeedPotions.Add(speedPotion);
        return speedPotion;
    }

    public GameObject GetShieldPotion()
    {
        for (int i = 0; i < m_poolOfShieldPotions.Count; i++)
        {
            if (m_poolOfShieldPotions[i].activeSelf == false) return m_poolOfShieldPotions[i];
        }

        GameObject shieldPotion = Instantiate(m_shieldPrefab, m_shieldTransform);
        shieldPotion.SetActive(false);
        m_poolOfShieldPotions.Add(shieldPotion);
        return shieldPotion;
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

}
