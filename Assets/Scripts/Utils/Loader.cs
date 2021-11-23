using UnityEngine;

namespace Utils {
    public class Loader {

        public static T Load<T>(string path) where T : UnityEngine.Object {
            return Resources.Load<T>(path);
        }

    }
}