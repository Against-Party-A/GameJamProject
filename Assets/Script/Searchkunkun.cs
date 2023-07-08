using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Searchkunkun : MonoBehaviour
{
    public GameObject shelter;

    public float 默认搜寻时间;

    public List<GameObject> shelterName;
    
    public List<string> shelterTime;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
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
