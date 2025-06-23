namespace FinnSchuuring.Utilities {
    using Sirenix.Utilities;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class SmartCursor : MonoBehaviourSingleton<SmartCursor> {
        private Dictionary<SmartCursorLayerAsset, int> _cachedLayerIndexes = null;
        private List<ISmartCursorObject> _enterObjects = new();
        private List<ISmartCursorObject> _enterManualObjects = new();
        private List<ISmartCursorObject> _downObjects = new();
        private float _elapsedRaycastUpdateStepDuration = 0f;

        public void Clear() {
            _cachedLayerIndexes.Clear();
            _enterObjects.Clear();
            _enterManualObjects.Clear();
            _downObjects.Clear();
            _elapsedRaycastUpdateStepDuration = 0f;
        }

        public void CacheLayerIndexes() {
            _cachedLayerIndexes = new();
            for (int i = 0; i < SmartCursorLibrary.Instance.Layers.Count; i++) {
                _cachedLayerIndexes.Add(SmartCursorLibrary.Instance.Layers[i], i);
            }
        }

        public void UpdateEnterAndExitAuto(Camera camera) {
            if (_elapsedRaycastUpdateStepDuration < SmartCursorLibrary.Instance.RaycastUpdateStepDuration) {
                _elapsedRaycastUpdateStepDuration += Time.deltaTime;
                return;
            }
            _elapsedRaycastUpdateStepDuration = 0f;

            List<ISmartCursorObject> objectsToExit = new();
            objectsToExit.AddRange(_enterObjects);

            Vector2 mousePosition = Input.mousePosition;
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray, float.MaxValue, SmartCursorLibrary.Instance.RaycastLayerMask);
            foreach (RaycastHit hit in hits) {
                if (hit.collider.gameObject.TryGetComponent<ISmartCursorObject>(out var obj)) {
                    objectsToExit.Remove(obj);
                    TryEnter(obj);
                }
            }
            foreach (var obj in objectsToExit) {
                if (!_enterManualObjects.Contains(obj)) {
                    TryExit(obj);
                }
            }
        }

        public void SimulateClick(List<ISmartCursorObject> injectedObjects) {
            foreach (var obj in injectedObjects) {
                TryEnterManual(obj);
            }
            CursorDown();
            CursorUp();
            foreach (var obj in injectedObjects) {
                TryExitManual(obj);
            }
        }

        public void CursorDown() {
            if (_enterObjects.Count <= 0) {
                return;
            }
            FilterAllObjects();
            _enterObjects = SortObjects(_enterObjects);
            foreach (var obj in _enterObjects) {
                obj.OnSmartCursorDown();
                _downObjects.Add(obj);
                if (obj.SmartCursorSettings.DoConsumeCursorDown) {
                    break;
                }
            }
        }

        public void CursorUp() {
            FilterAllObjects();
            foreach (var obj in _downObjects) {
                obj.OnSmartCursorUp();
            }
            _downObjects.Clear();
        }

        public bool TryEnterManual(ISmartCursorObject obj) {
            if (TryEnter(obj)) {
                _enterManualObjects.Add(obj);
                return true;
            }
            return false;
        }

        public bool TryExitManual(ISmartCursorObject obj) {
            return TryExit(obj);
        }

        private bool TryEnter(ISmartCursorObject obj) {
            if (_enterObjects.Contains(obj)) {
                return false;
            }
            _enterObjects.Add(obj);
            return true;
        }

        private bool TryExit(ISmartCursorObject obj) {
            if (!_enterObjects.Contains(obj)) {
                return false;
            }
            _enterObjects.Remove(obj);
            _enterManualObjects.Remove(obj);
            return true;
        }

        private void FilterAllObjects() {
            _enterObjects = FilterObjects(_enterObjects);
            _enterManualObjects = FilterObjects(_enterManualObjects);
            _downObjects = FilterObjects(_downObjects);
        }

        private List<ISmartCursorObject> SortObjects(List<ISmartCursorObject> objects) {
            objects = objects
            .OrderBy(x => _cachedLayerIndexes[x.SmartCursorSettings.Layer])
            .ThenByDescending(x => x.SmartCursorSettings.OrderInLayer)
            .ToList();
            return objects;
        }

        private List<ISmartCursorObject> FilterObjects(List<ISmartCursorObject> objects) {
            List<ISmartCursorObject> objectsToKeep = new();
            foreach (ISmartCursorObject obj in objects) {
                if ((Object)obj != null) {
                    objectsToKeep.Add(obj);
                }
            }
            return objectsToKeep;
        }
    }
}
