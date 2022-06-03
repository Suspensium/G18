using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : MonoBehaviour, IActivatable
{
    [SerializeField] GameObject rotator;
    [SerializeField] bool opened;
    [SerializeField] bool freezed;
    [SerializeField] bool locked;
    [SerializeField] Item key;
    [SerializeField] float freezeTime = 1f;

    void Start()
    {
        if (opened)
        {
            Open();
        }
    }

    public void ActPrimary()
    {
        if (!locked && !freezed)
        {
            if (!opened)
            {
                Open();
            }
            else
            {
                Close();
            }
        }
        else if (locked && !key)
        {
            Debug.Log("Door is locked");
        }
        else
        {
            Item doorKey = SC_FPSController.inventory.GetItemList().Find(item => item.name == key.name);
            if (doorKey)
            {
                if (!opened)
                {
                    Open();
                }
                else
                {
                    Close();
                }
            }
            else
            {
                Debug.Log("Door is locked");
            }
        }
    }
    public void ActSecondary() { }
    public void Deact() { }

    void Open()
    {
        StartCoroutine(Freeze());
        if (!opened)
        {
            opened = !opened;
        }
        rotator.GetComponent<DoorRotator>().SetRot(true);
    }
    void Close()
    {
        StartCoroutine(Freeze());
        opened = !opened;
        rotator.GetComponent<DoorRotator>().SetRot(false);
    }

    IEnumerator Freeze()
    {
        freezed = true;
        yield return new WaitForSeconds(freezeTime);
        freezed = false;
    }

}
/*
    assign to obj with collider! door panel etc
    inside of rotator
*/