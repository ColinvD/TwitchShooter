using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public enum Object
    {
        normal = 0,
        tank = 1,
        medkit = 2,
        ammobox = 3
    }

    class GameObj
    {
        public GameObject obj;
        public Transform loc;
    }

    public List<GameObject> addObjects;
    public List<Transform> addTransforms;

    public Dictionary<Object, GameObject> objects = new Dictionary<Object, GameObject>();
    public Dictionary<int, Transform> spawnLocations = new Dictionary<int, Transform>();
    private List<string> objectsName;
    private List<GameObj> spawnList;

    private float currentTime;

    void Start()
    {
        for (int i = 0; i < addObjects.Count; i++)
        {
            objects.Add(0, addObjects[i]);
        }
    }

    private void FillList(Object obj, int loc)
    {
        if (objects.ContainsKey(obj) && spawnLocations.ContainsKey(loc))
        {
            GameObj myObj = new GameObj();
            myObj.obj = objects[obj];
            myObj.loc = spawnLocations[loc];
            spawnList.Add(myObj);
        }
    }

    private void ListFiller()
    {
        GameObj myObj = new GameObj();
        myObj.obj = objects[0];
        myObj.loc = spawnLocations[0];
        spawnList.Add(myObj);
    }

    private IEnumerator Spawn()
    {
        Instantiate(spawnList[0].obj, spawnList[0].loc);
        yield return new WaitForSeconds(5f);
    }
}
