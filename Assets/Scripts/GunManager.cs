using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab;
    Transform player;

    List<Vector2> gunPositions = new List<Vector2>();
    int spawnedGuns = 0;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        
        gunPositions.Add(new Vector2(-1.2f, -0.25f));
        gunPositions.Add(new Vector2(1.2f, -0.25f));
        gunPositions.Add(new Vector2(-1f, 0.7f));
        gunPositions.Add(new Vector2(1f, 0.7f));
        gunPositions.Add(new Vector2(-1f, -1.5f));
        gunPositions.Add(new Vector2(1f, -1.5f));

        
        AddGun();
        
    }

   

    
    public void AddGun()
    {
        
        if (spawnedGuns >= gunPositions.Count)
        {
            UnityEngine.Debug.Log("Ai atins numarul maxim de arme!");
            return;
        }

        Vector2 position = gunPositions[spawnedGuns];
        GameObject newGun = Instantiate(gunPrefab, player.position, Quaternion.identity);
        newGun.GetComponent<Gun>().SetOffset(position);

        spawnedGuns++;
    }
}