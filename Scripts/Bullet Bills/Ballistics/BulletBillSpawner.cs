using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBillSpawner : MonoBehaviour
{
    public GameObject bulletBill;
    private List<GameObject> spawnedObjs = new List<GameObject>();

    public float shootFreq;
    private float lastSpawn = 0f;
    public float launchForce = 0f;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            if (Time.time > lastSpawn + shootFreq)
            {
                if (spawnedObjs.Count < shootFreq)
                {
                    GameObject newBill = Instantiate(bulletBill, this.transform.position, Quaternion.identity);
                    spawnedObjs.Add(newBill);
                    lastSpawn = Time.time;


                    FiringSolution fs = new FiringSolution();
                    Nullable<Vector3> aimVector = fs.Calculate(transform.position, player.GetComponent<Transform>().position, launchForce, Physics.gravity);

                    if (aimVector.HasValue)
                    {
                        newBill.GetComponent<Rigidbody>().AddForce(aimVector.Value.normalized * launchForce, ForceMode.VelocityChange);
                    }
                    Debug.Log(spawnedObjs.Count);
                }
                else
                {
                    Debug.Log("Else Statement");
                    Debug.Log("Else: " + spawnedObjs.Count);

                    foreach (GameObject i in spawnedObjs)
                    {
                        Destroy(i);
                        Debug.Log("Destroyed");

                        lastSpawn = Time.time;
                       
                    }
                }
            }
        }
    }
}
