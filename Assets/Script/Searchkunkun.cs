using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchkunkun : MonoBehaviour
{
    public BabyControl _BabyControl;
    public GameObject shelter;

    public float 默认搜寻时间;

    public List<GameObject> shelterName;
    
    public List<float> shelterTime;
    // Start is called before the first frame update
    void Start()
    {
        _BabyControl = GetComponent<BabyControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginSearch()
    {
        float time = 默认搜寻时间;
        if (shelter.transform.childCount > 0)
        {
            var childObj = shelter.transform.GetChild(0);
            for (int i = 0; i < shelterName.Count; i++)
            {
                if (shelterName[i] == childObj)
                {
                    time += shelterTime[i];
                    break;
                }
            }
        }
        StartCoroutine(Search(time));
    }



    private IEnumerator Search(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.LogError("33333333333333333");
        _BabyControl.SearchSuccess();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Shelter") == 0)
        {
            shelter = other.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Shelter") == 0)
        {
            shelter = null;
        }
    }
}
