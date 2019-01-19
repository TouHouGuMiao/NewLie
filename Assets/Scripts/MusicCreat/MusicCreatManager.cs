using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCreatManager
{
    private static MusicCreatManager _Instanc=null;

    public static MusicCreatManager Instance
    {
        get
        {
            if (_Instanc == null)
            {
                _Instanc = new MusicCreatManager();
            }
            return _Instanc;
        }
    }

    
}

