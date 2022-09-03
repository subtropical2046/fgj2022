using System;
using UnityEngine;

namespace PlayerControl
{
    [Serializable]
    public class DrunkControlData
    {
        [SerializeField]
        private float _checkingPeriod;
        [SerializeField]
        [Range(0, 100)]
        private uint _ratio;
        [SerializeField]
        private float _period;

        public float CheckingPeriod => _checkingPeriod;
        public uint Ratio => _ratio;
        public float Period => _period;
    }
}
