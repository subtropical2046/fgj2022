using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DrunkControl
{
    public class DrunkControlCore
    {
        private readonly MonoBehaviour _targetBehaviour;
        private readonly DrunkControlData _data;
        private readonly Action<bool> _onDrunk;

        public bool IsDrunk { get; private set; }

        private Coroutine _coroutine;

        public DrunkControlCore(
            MonoBehaviour targetBehaviour,
            DrunkControlData data, Action<bool> onDrunk)
        {
            _targetBehaviour = targetBehaviour;
            _data = data;
            _onDrunk = onDrunk;
        }

        public void Start()
        {
            _coroutine =
                _targetBehaviour.StartCoroutine(DrunkCheckingCoroutine());
        }

        public void Stop()
        {
            if (_coroutine == null)
                return;

            _targetBehaviour.StopCoroutine(_coroutine);
            _coroutine = null;
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
