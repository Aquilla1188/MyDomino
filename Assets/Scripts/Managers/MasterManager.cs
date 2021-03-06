﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName ="Singletons/MasterManager")]
public class MasterManager : ScriptableSingletone<MasterManager>
{
    [SerializeField] public GameSettings _gameSettings;
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }

}
