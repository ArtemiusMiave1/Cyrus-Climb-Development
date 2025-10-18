using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    public Animator LeftDoor;
    public Animator RightDoor;
    public bool Door_Active;
    public bool InTheZone;
    public static bool InZone;
    public int collectableTotal;
    public int currentColelctables;


    public static int collectableCount;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        currentColelctables = collectableCount / 2;
        InTheZone = InZone;


        if (collectableCount / 2  == collectableTotal)
        {
            Door_Active = true;
        }

       



        if (Door_Active == true && InZone == true)
        { 
            if (Input.GetKeyDown(KeyCode.E))
            {
                LeftDoor.SetBool("Door_Activated", true);
                RightDoor.SetBool("Door_Activated", true);

            }
        }


        



    }
}
