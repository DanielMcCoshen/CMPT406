using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    public List<Room> neighbours = new List<Room>();
    public GameObject roomTemp;
    
    
    protected GameObject roomLayoutObj;
    public FloorGenerator floorGenerator = null;
    private bool reducedNumberOfRooms = false;
    
    private bool votingCommenced = false;
    private bool votingComplete = false;
    private bool enemiesDefeated = false;
    private bool locked = false;


    protected RoomMap roomLayout;
    private int jobId;

    [SerializeField]
    private Filter filter = null;
    public Filter Filter { get => filter; set => filter = value; }

    void Start()
    {
        floorGenerator = GameObject.FindWithTag("MainCamera").GetComponent<FloorGenerator>();
    }

    public void SetFloorGenerator(FloorGenerator fgen)
    {
        floorGenerator = fgen;
    }

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
            votingComplete = true;
            roomLayout.room = gameObject.GetComponent<Room>();
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
            if (!reducedNumberOfRooms)
            {
                reducedNumberOfRooms = true;
                floorGenerator.ReduceNumberOfRooms();
                Debug.Log("reduced");
            }
            if (floorGenerator.ReadyForBossRoom())
            {
                votingCommenced = true;
                RoomLayout = RoomList.Instance.getBossRoom();
            }
            else
            {
                if (NetworkManager.Online)
                {
                    await NetworkManager.BeginRoomVote(this);
                }
                else
                {
                    votingCommenced = true;
                }
                StartCoroutine(verifyRoom());
            }
            
        }
    }
    private IEnumerator verifyRoom()
    {
        yield return new WaitForSeconds(10);
        if (NetworkManager.Online)
        {
            while (!votingComplete)
            {
                _ = NetworkManager.CheckVote(this);
                yield return new WaitForSeconds(10);
            }
        }
        else
        {
            RoomLayout = RoomList.Instance.getRandomWithFilter(filter);
        }
    }

    public void enterRoom()
    {
        if (!enemiesDefeated)
        {
            locked = true;
            roomLayout.switchDynamicDanger(true);
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
                roomLayout.switchDynamicDanger(false);
                Debug.Log("All Enemies Dead");
            }
        }
    }
}
