using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AbstractBaseSave : MonoBehaviour, ISaveSystem
{
    public string saveID;

    public virtual void Start()
    {
        Load();
    }

    public virtual void Load()
    {

    }

    public virtual void Save()
    {
        
    }
}
