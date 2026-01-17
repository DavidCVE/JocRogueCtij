using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunManager : MonoBehaviour
{
    // În video el pune [SerializeField] la început, deci apar în Inspector
    [SerializeField] GameObject gunPrefab;
    Transform player; // Aici e diferența! Dacă scoți [SerializeField], dispare din Inspector

    List<Vector2> gunPositions = new List<Vector2>(); // Inițializăm lista direct
    int spawnedGuns = 0;

    void Start()
    {
        // Această linie găsește singură player-ul, deci NU trebuie să-l pui tu în Inspector
        player = GameObject.Find("Player").transform;

        // Adaugă pozițiile pentru cele 2 arme inițiale (Stânga și Dreapta)
        // În video valorile sunt +/- 0.7 și 0.15
        gunPositions.Add(new Vector2(-1.2f, -0.25f));
        gunPositions.Add(new Vector2(1.2f, -0.25f));
        gunPositions.Add(new Vector2(-1f, 0.7f));
        gunPositions.Add(new Vector2(1f, 0.7f));
        gunPositions.Add(new Vector2(-1f, -1.5f));
        gunPositions.Add(new Vector2(1f, -1.5f));

        // Spawnează primele 2 arme
        AddGun();
        AddGun();
    }

    void Update()
    {
        // 2. CODUL PENTRU NOUL INPUT SYSTEM
        // În loc de Input.GetKeyDown(KeyCode.G)
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            if (spawnedGuns < gunPositions.Count)
            {
                AddGun();
            }
        }
    }

    void AddGun()
    {
        // Luăm poziția din listă
        Vector2 position = gunPositions[spawnedGuns];

        // Creăm arma
        GameObject newGun = Instantiate(gunPrefab, player.position, Quaternion.identity);

        // Setăm offset-ul
        newGun.GetComponent<Gun>().SetOffset(position);

        // Numărăm arma
        spawnedGuns++;
    }
}