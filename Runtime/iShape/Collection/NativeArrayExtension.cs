using Unity.Collections;

namespace iShape.Collections {
    
    public static class NativeArrayExtension {

        internal static void Copy<T>(this NativeArray<T> dest, int destIdx, NativeArray<T> src, int srcIdx, int count) where T : struct {
            dest.Slice(destIdx, count).CopyFrom(src.Slice(srcIdx, count));
        }

        internal static void Copy<T>(this NativeArray<T> dest, int destIdx, NativeSlice<T> src, int srcIdx, int count) where T : struct {
            dest.Slice(destIdx, count).CopyFrom(src.Slice(srcIdx, count));
        }

        internal static int Copy<T>(this NativeArray<T> dest, NativeArray<T> src, int offset) where T : struct {
            int length = src.Length;
            dest.Slice(offset, length).CopyFrom(src);
            return length + offset;
        }

        internal static int Copy<T>(this NativeArray<T> dest, NativeSlice<T> src, int offset) where T : struct {
            int length = src.Length;
            dest.Slice(offset, length).CopyFrom(src);
            return length + offset;
        }

    }
}