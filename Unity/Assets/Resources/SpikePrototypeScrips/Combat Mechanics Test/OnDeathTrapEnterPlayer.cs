using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathTrapEnterPlayer : OnDeathTrapEnter
{
    public Vector3 respawnPosition;
    public Transform canvas;
    public int souls;
    public GameObject soulPrefab;
    private List<GameObject> soulObjects;

    // Start is called before the first frame update
    void Start()
    {
        soulObjects = new List<GameObject>();
        for (int x = 0; x< souls; x++)
        {
            InstantiateNewSoul(x);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRespawnPosition(Transform pos)
    {
        respawnPosition = pos.position;
    }


    public override void OnDeathTrapTrigger(string trapType)
    {
        gameObject.transform.parent.gameObject.transform.position = respawnPosition;
        souls -= 1;
        Destroy(soulObjects[souls]);
        soulObjects.RemoveAt(souls);
    }

    public override string NameForDeathTrap()
    {
        return gameObject.transform.parent.gameObject.name;
    }

    private void InstantiateNewSoul(int position)
    {
        soulObjects.Add(Instantiate(soulPrefab, new Vector3(canvas.position.x - 7.5f + (position * 1f), canvas.position.y + 4f, canvas.position.z), Quaternion.identity, canvas));
    }

    public void AddLife()
    {
        InstantiateNewSoul(souls);
        souls+=1;
    }
}
