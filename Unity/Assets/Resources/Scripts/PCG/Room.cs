using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{

    public List<Room> neighbours = new List<Room>();
    public GameObject roomTemp;

    [Range(0f, 100f)]
    public float lootSpawnChance;

    protected GameObject roomLayoutObj;
    public FloorGenerator floorGenerator = null;
    private bool reducedNumberOfRooms = false;

    private bool votingCommenced = false;
    private bool votingComplete = false;
    private bool enemiesDefeated = false;
    private bool locked = false;

    private GameObject loot;
    private int lootVoteId;
    private bool lootVotingCommenced = false;
    private bool lootVotingComplete = false;

    protected RoomMap roomLayout;
    private int jobId;

    [SerializeField]
    private Filter filter = null;
    public Filter Filter { get => filter; set => filter = value; }

    void Start()
    {
        floorGenerator = GameObject.Find("Generator").GetComponent<FloorGenerator>();
    }

    public void SetFloorGenerator(FloorGenerator fgen)
    {
        floorGenerator = fgen;
    }

    public GameObject RoomLayout
    {
        get
        {
            if (!votingComplete)
            {
                throw new System.InvalidOperationException("Voting not yet completed for this room");
            }
            return roomLayoutObj;
        }
        set
        {
            if (votingComplete)
            {
                throw new System.InvalidOperationException("Voting already completed for this room");
            }
            Destroy(roomTemp);
            try 
            {
                roomLayoutObj = Instantiate(value, gameObject.transform); 
            }
            catch (ArgumentException e) 
            {
                Debug.LogError("Room layout failed to instantiate, value: " + value + ". Error: " + e);
            }

            roomLayout = roomLayoutObj.GetComponent<RoomMap>();
            votingComplete = true;
            roomLayout.room = gameObject.GetComponent<Room>();
        }
    }

    public int JobId
    {
        get
        {
            if (!votingCommenced)
            {
                throw new System.InvalidOperationException("Voting not yet commenced for this room");
            }
            return jobId;
        }
        set
        {
            if (votingCommenced)
            {
                throw new System.InvalidOperationException("Voting has already begun for this room");
            }
            votingCommenced = true;
            jobId = value;
        }
    }

    public GameObject Loot
    {
        get
        {
            if (!lootVotingComplete)
            {
                throw new System.InvalidOperationException("Voting not yet completed for this item");
            }
            return loot;
        }
        set
        {
            if (lootVotingComplete)
            {
                throw new System.InvalidOperationException("Voting already completed for this item");
            }
            lootVotingComplete = true;
            loot = value;
        }
    }

    public int LootJobId
    {
        get
        {
            if (!lootVotingCommenced)
            {
                throw new System.InvalidOperationException("Voting not yet commenced for this item");
            }
            return lootVoteId;
        }
        set
        {
            if (lootVotingCommenced)
            {
                throw new System.InvalidOperationException("Voting has already begun for this item");
            }
            lootVotingCommenced = true;
            lootVoteId = value;
        }
    }

    public async void beginVote()
    {

        if (!votingCommenced)
        {
            determineLoot();
            reducedNumberOfRooms = true;
            floorGenerator.ReduceNumberOfRooms();

            if (floorGenerator.ReadyForBossRoom())
            {
                votingCommenced = true;
                RoomLayout = RoomList.Instance.BossRoom;
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

    private async void determineLoot()
    {
        if (!lootVotingCommenced)
        {
            if (UnityEngine.Random.Range(0f, 100f) >= lootSpawnChance)
            {
                Debug.Log("there will be loot for " + gameObject.name);
                if (NetworkManager.Online)
                {
                    await NetworkManager.BeginItemVote(this, GameObject.Find("DeathCollider").GetComponent<OnDeathTrapEnterPlayer>().souls);
                }
                else
                {
                    LootJobId = -1;
                }
            }
            else
            {
                LootJobId = -1;
                Loot = null;
            }
        }
    }
    private IEnumerator lootResult()
    {
        while (!lootVotingComplete)
        {
            yield return new WaitForSeconds(5);
            if (NetworkManager.Online)
            {
                _ = NetworkManager.CheckItemVote(this);
            }
            else
            {
                Loot = ItemList.Instance.getRandomWithSouls(GameObject.Find("DeathCollider").GetComponent<OnDeathTrapEnterPlayer>().souls);
            }
        }
        roomLayout.spawnLoot(loot);
    }
    private IEnumerator verifyRoom()
    {
        yield return new WaitForSeconds(10);
        if (NetworkManager.Online)
        {
            while (!votingComplete)
            {
                _ = NetworkManager.CheckRoomVote(this);
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
                StartCoroutine(lootResult());
                roomLayout.switchDynamicDanger(false);
            }
        }
    }
}
