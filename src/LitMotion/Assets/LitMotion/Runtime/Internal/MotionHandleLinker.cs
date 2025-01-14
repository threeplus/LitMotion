using System.Runtime.CompilerServices;
using UnityEngine;

namespace LitMotion
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    internal sealed class MotionHandleLinker : MonoBehaviour
    {
        readonly MinimumList<MotionHandle> handleList = new(8);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Register(MotionHandle handle)
        {
            handleList.Add(handle);
        }

        void OnDestroy()
        {
            var span = handleList.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                ref var handle = ref span[i];
                if (handle.IsActive()) handle.Cancel();
            }
        }
    }
}
