using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [Flags]
    public enum EProjectileType
    {
        Normal = 1,
        Water = 2,
        Fire = 4,
        Light = 8,
        Dark = 16
    }

    [CreateAssetMenu(menuName = "ShootDemo/BulletSO", fileName = "BulletSO", order = 1)]
    public class BulletSO : ScriptableObject
    {
        [field: SerializeField] public EProjectileType BulletType { get; private set; }

        public void SetBulletType(EProjectileType newBulletType)
        {
            BulletType = newBulletType;
        }
    }
}

