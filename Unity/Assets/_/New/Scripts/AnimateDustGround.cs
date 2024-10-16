using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimateDustGround : MonoBehaviour
{
    public GameObject PrefabDust;
    public int DensityDust;
    private List<GameObject> dustInstance = new List<GameObject>();

    public Vector3 SpeedAnimation;

    public Transform transformIn;
    public Transform transformOut;
    
    public void InstantiateDust()
    {
        for (int i = 0; i < DensityDust; i++)
        {
            GameObject instance = Instantiate(PrefabDust, transform);
            dustInstance.Add(instance);
            SetDispersionDusts();
        }
    }

    public void MoveDusts()
    {
        foreach (var dust in dustInstance)
        {
            dust.transform.position += SpeedAnimation * Time.deltaTime;
        }
    }

    public void SetDispersionDusts()
    {
        foreach (var dust in dustInstance)
        {
            Vector3 area = transformIn.position - transformOut.position;
            dust.transform.position = area * Random.Range(0, 1.0f) - area *.5f + Vector3.forward * Random.Range(-5.0f, 5.0f);
        }
    }

    public void ResetPositionDusts()
    {
        foreach (var dust in dustInstance)
        {
            if(dust.transform.position.x < transformOut.position.x)
            {
                dust.transform.position = transformIn.position + 
                Vector3.left * Random.Range(.0f, 1.0f) +
                Vector3.forward * Random.Range(-5.0f, 5.0f);

            }
        }
    }

    void Start()
    {
        InstantiateDust();
    }

    void Update()
    {
        MoveDusts();
        ResetPositionDusts();
    }
}
