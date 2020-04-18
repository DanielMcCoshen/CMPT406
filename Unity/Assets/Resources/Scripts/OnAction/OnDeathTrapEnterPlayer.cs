using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDeathTrapEnterPlayer : OnDeathTrapEnter
{
    public Vector3 respawnPosition;
    public Transform canvas;
    public int souls;
    public GameObject soulPrefab;
    private List<GameObject> soulObjects;
    public Rigidbody2D rb;
    [Header("SFX")]
    public AudioSource newSoulSFX;
    public AudioSource deathSFX;

    //The two spawnpoints for the argus boss room
    private GameObject spawn1;
    private GameObject spawn2;

    //Starting spawn
    private int currentSpawn;

    // Start is called before the first frame update
    void Start()
    {
        soulObjects = new List<GameObject>();
        for (int x = 0; x< souls; x++)
        {
            InstantiateNewSoul(x);
        }

        //Set starting spawn
        currentSpawn = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (souls <= 0)
        {
            GameObject menuManager = GameObject.Find("MenuManager");
            GameObject.Destroy(GameObject.FindWithTag("Player"));
            menuManager.GetComponent<PauseMenu>().PlayerDefeat();
            //DestroyImmediate(GameObject.Find("PlayerCameraAndCanvas"));
            //DestroyImmediate(GameObject.Find("ServerRoomManager"));
            //SceneManager.LoadScene("Main Menu");

        }
    }

    public void SetRespawnPosition(Transform pos)
    {
        respawnPosition = pos.position;
    }

    public void MoveToRespawnPosition()
    {
        gameObject.transform.parent.gameObject.transform.position = respawnPosition;
        rb.velocity = new Vector3(0, 0, 0);
    }

    public IEnumerator iFrames()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("PlayerContainer").layer = 8;
    }

    public override void OnDeathTrapTrigger(string trapType)
    {
        //Eyes of Argus boss has two spawnpoints to alternate betweeen. Check if the room is eyes of argus
        if (SceneManager.GetActiveScene().name == "EyesOfArgus")
        {
            //If it is, get the Respawn control in the room
            //ArgusRespawn argusRespawn = GameObject.Find("Respawn").GetComponent<ArgusRespawn>();
            //Switch the active respawn point
            //argusRespawn.SwitchSpawn();
            //Put the player at the new spawn point
            //gameObject.transform.parent.gameObject.transform.position = argusRespawn.GetSpawn().position;
            if(currentSpawn == 1)
            {
                currentSpawn = 2;
                respawnPosition = GameObject.Find("Spawn").GetComponent<ArgusRespawn>().spawn2.transform.position;
            }
            else
            {
                currentSpawn = 1;
                respawnPosition = GameObject.Find("Spawn").GetComponent<ArgusRespawn>().spawn1.transform.position;
            }
        }

        else
        {
            gameObject.transform.parent.gameObject.transform.position = respawnPosition;
        }

        rb.velocity = new Vector3(0, 0, 0);
        MoveToRespawnPosition();
        deathSFX.Play();
        souls -= 1;
        Destroy(soulObjects[souls]);
        soulObjects.RemoveAt(souls);
        StartCoroutine(iFrames());
        GameObject.Find("PlayerContainer").layer = 0;
    }

    public override string NameForDeathTrap()
    {
        return gameObject.transform.parent.gameObject.name;
    }

    private void InstantiateNewSoul(int position)
    {
        newSoulSFX.Play();
        soulObjects.Add(Instantiate(soulPrefab, new Vector3(canvas.position.x - 8f + (position * 1f), canvas.position.y + 4.25f, canvas.position.z), Quaternion.identity, canvas));
    }

    public void AddLife()
    {
        InstantiateNewSoul(souls);
        souls+=1;
    }
}
