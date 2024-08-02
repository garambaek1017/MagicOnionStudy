using System.Reflection;

namespace Shared.Util
{
    public class Singleton<T> where T : class
    {
        private static readonly Lazy<T> LazyInstance = new(() =>
        {
            var instanceType = typeof(T);

            var publicConstructors = instanceType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (publicConstructors.Length >= 1)
            {
                throw new MissingMethodException($"This Class Include the Public Constructor : {instanceType.Name}");
            }

            var invokeConstructors = instanceType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null,
                Type.EmptyTypes, null);

            if (invokeConstructors is null)
            {
                throw new MissingMethodException(
                    $"This Class Do Not Include the NonPublic Constructor : {instanceType.Name}");
            }

            return (T)invokeConstructors.Invoke(null);
        }, true);

        public static T Instance => LazyInstance.Value;
    }
}
