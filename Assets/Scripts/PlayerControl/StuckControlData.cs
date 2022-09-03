using System;
using UnityEngine;

namespace PlayerControl
{
    [Serializable]
    public class StuckControlData
    {
        [SerializeField]
        private float _stuckPeriod;

        public float StuckPeriod => _stuckPeriod;
    }
}
