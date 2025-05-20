using UnityEngine;

namespace FinnSchuuring.Utilities {
    public static class TransformExtensions {
        public static void SetPositionX(this Transform transform, float x) {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        public static void SetPositionY(this Transform transform, float y) {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public static void SetPositionZ(this Transform transform, float z) {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        public static void ResetPosition(this Transform transform) {
            transform.position = Vector3.zero;
        }

        public static void SetLocalPositionX(this Transform transform, float x) {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        public static void SetLocalPositionY(this Transform transform, float y) {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        public static void SetLocalPositionZ(this Transform transform, float z) {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }

        public static void ResetLocalPosition(this Transform transform) {
            transform.localPosition = Vector3.zero;
        }

        public static float Distance(this Transform transform, Transform other) {
            return Vector3.Distance(transform.position, other.position);
        }

        public static float Distance(this Transform transform, Vector3 position) {
            return Vector3.Distance(transform.position, position);
        }

        public static void SetRotation(this Transform transform, Vector3 euler) {
            transform.rotation = Quaternion.Euler(euler);
        }

        public static void SetRotationX(this Transform transform, float x) {
            transform.rotation = Quaternion.Euler(new Vector3(x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
        }

        public static void SetRotationY(this Transform transform, float y) {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z));
        }

        public static void SetRotationZ(this Transform transform, float z) {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, z));
        }

        public static void SetLocalRotationX(this Transform transform, float x) {
            transform.localRotation = Quaternion.Euler(new Vector3(x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z));
        }

        public static void SetLocalRotationY(this Transform transform, float y) {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, y, transform.localRotation.eulerAngles.z));
        }

        public static void SetLocalRotationZ(this Transform transform, float z) {
            transform.localRotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, z));
        }

        public static void ResetRotation(this Transform transform) {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        public static void SetLocalRotation(this Transform transform, Vector3 euler) {
            transform.localRotation = Quaternion.Euler(euler);
        }

        public static void ResetLocalRotation(this Transform transform) {
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
    }
}