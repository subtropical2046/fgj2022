using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DrunkControl
{
    public class DrunkControlCore
    {
        private readonly DrunkControlData _data;
        private readonly Action<bool> _onDrunk;

        public bool IsDrunk { get; private set; }

        private MonoBehaviour _targetBehaviour;

        public DrunkControlCore(DrunkControlData data, Action<bool> onDrunk)
        {
            _data = data;
            _onDrunk = onDrunk;
        }

        public void Start(MonoBehaviour monoBehaviour)
        {
            _targetBehaviour = monoBehaviour;
            _targetBehaviour.StartCoroutine(DrunkCheckingCoroutine());
        }

        private IEnumerator DrunkCheckingCoroutine()
        {
            while (true) {
                UpdateIsDrunk(false);
                yield return new WaitForSeconds(_data.CheckingPeriod);
                UpdateIsDrunk(Random.Range(0, 100) < _data.Ratio);
                if (IsDrunk)
                    yield return new WaitForSeconds(_data.Period);
            }
        }

        private void UpdateIsDrunk(bool newIsDrunk)
        {
            if (newIsDrunk == IsDrunk)
                return;

            IsDrunk = newIsDrunk;
            _onDrunk.Invoke(IsDrunk);
        }
    }
}
