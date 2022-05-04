﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private DoorInfo[] doors;
    [SerializeField] private WaypointInfo[] waypoints;

    public void OpenDoor(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name != doorName) continue;
            t.gameObject.SetActive(false);
            t.open = true;
            break;
        }
    }

    public void CloseDoor(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name != doorName) continue;
            t.gameObject.SetActive(false);

            t.open = false;
            break;
        }
    }
    
    public void BargeDoor(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name != doorName) continue;
            t.gameObject.SetActive(false);
            t.open = true;
            t.locked = false;
            break;
        }
    }

    public bool DoorIsOpen(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name == doorName) return t.open;
        }

        return false;
    }

    public Transform GetWaypoint(string name)
    {
        foreach (WaypointInfo wp in waypoints)
        {
            if (wp.name == name) return wp.transform;
        }

        return null;
    }

    public bool DoorIsLocked(string doorName)
    {
        foreach (DoorInfo t in doors)
        {
            if (t.name == doorName) return t.locked;
        }

        return false;
    }
}