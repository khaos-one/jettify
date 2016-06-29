namespace Jettify.Extensions {
    /// <summary>
    /// Provides extension methods of <see cref="object"/> objects.
    /// </summary>
    public static class ObjectExtensions {
        public static T To<T>(this object obj) {
            return (T) obj;
        }

        public static T As<T>(this object obj)
            where T : class {
            return obj as T;
        }
    }
}
