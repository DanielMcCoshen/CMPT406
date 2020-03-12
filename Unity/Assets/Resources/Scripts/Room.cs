using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    public List<Room> neighbours = new List<Room>();
    public GameObject roomTemp;
    
    
    protected GameObject roomLayoutObj;

    public List<GameObject> bridges = new List<GameObject>();
    


    private bool votingCommenced = false;
    private bool votingComplete = false;
    private bool enemiesDefeated = false;
    private bool locked = false;


    protected RoomMap roomLayout;
    private int jobId;

    [SerializeField]
    private int filter;
    public int Filter { get => filter; set => filter = value; }

    public GameObject RoomLayout {
        get {
            if (!votingComplete)
            {
                throw new System.InvalidOperationException("Voting not yet completed for this room");
            }
            return roomLayoutObj;
        }
        set {
            if (votingComplete)
            {
                throw new System.InvalidOperationException("Voting already completed for this room");
            }
            DestroyImmediate(roomTemp);
            roomLayoutObj = Instantiate(value, gameObject.transform);
            roomLayout = roomLayoutObj.GetComponent<RoomMap>();
            //roomLayout.room = this;
            votingComplete = true;
        }
    }

    public int JobId {
        get {
            if (!votingCommenced)
            {
                throw new System.InvalidOperationException("Voting not yet commenced for this room");
            }
            return jobId;
        }
        set {
            if (votingCommenced)
            {
                throw new System.InvalidOperationException("Voting has already begun for this room");
            }
            votingCommenced = true;
            jobId = value;
        }
    }




    public async void beginVote()
    {
        if (!votingCommenced)
        {
            await NetworkManager.BeginRoomVote(this);
            StartCoroutine(verifyRoom());
        }
    }
    private IEnumerator verifyRoom()
    {
        yield return new WaitForSeconds(10);
        while (!votingComplete)
        {
            _ = NetworkManager.CheckVote(this);
            yield return new WaitForSeconds(10);
        }
    }

    public void enterRoom()
    {
        if (!enemiesDefeated)
        {
            locked = true;
            roomLayout.setDynamicDanger(true);
            foreach (GameObject bridge in bridges)
            {
                bridge.SetActive(false);

            }
            roomLayout.spawnEnemies();   
        }
        foreach (Room r in neighbours)
        {
            r.beginVote();
        }
    }

    public void FixedUpdate()
    {
        if (locked)
        {
            if (roomLayout.isComplete())
            {
                enemiesDefeated = true;
                locked = false;
                roomLayout.spawnLoot();
                roomLayout.setDynamicDanger(false);
                foreach (GameObject bridge in bridges)
                {
                    bridge.SetActive(true);

                }
                Debug.Log("All Enemies Dead");
            }
        }
    }
}
