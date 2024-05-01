using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Levels[] levels;
    public static LevelManager inst;
  
    // public Levels currentLevel;
  //  public List<GameObject> levellist = new List<GameObject>();
 
    private void Start()
    {
        inst = this;
    }

    public void Levels(LevelName name)
    {
      
      
      
        foreach (var item in levels)
        {
            if (item.name == name)
            {
                //ChildDestroy();
               Instantiate(item.level,transform.position, transform.rotation,transform);
                break;
            }


        }
    }

    public void ChildDestroy()
    {
        Debug.Log(transform.childCount);
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        Debug.Log(transform.childCount);
    }

}
[System.Serializable]
public class Levels
{
    public LevelName name;
    public GameObject level;
}
public enum LevelName
{
    Level1,
    Level2,
    Level3,
}
