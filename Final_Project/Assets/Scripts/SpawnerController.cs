using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [SerializeField] private Transform[] m_Positions;
    [SerializeField] public List<GameObject> m_Spawneables;

    void Awake()
    {
        for (int i = 0; i < m_Positions.Length; i++)
        {
            Spawn(i);
        } 
    }

    private void Spawn(int i)
    {
        int l_type = Random.Range(0, m_Positions.Length) ;
        Instantiate(m_Spawneables[l_type], m_Positions[i].position ,Quaternion.identity);
    }
}
