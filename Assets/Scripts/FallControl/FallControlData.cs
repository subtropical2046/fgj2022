using System;
using UnityEngine;

namespace FallControl
{
    [Serializable]
    public class FallControlData
    {
        [SerializeField]
        private float _fallPeriod;
        [SerializeField]
        private float _fallDistance;
        [SerializeField]
        private float _fallIdlePeriod;

        public float FallPeriod => _fallPeriod;
        public float FallDistance => _fallDistance;
        public float FallIdlePeriod => _fallIdlePeriod;
    }
}
