using iShape.Collections;
using NUnit.Framework;
using Unity.Collections;

public class CollectionTests {

    [Test]
    public void Test_00() {
        const int n = 10;
        var list = new DynamicArray<int>(n, Allocator.Temp);
        for (int i = 0; i < n; ++i) {
            list.Add(i);
        }

        list.RemoveAt(n - 1);

        var a = list.ToArray(Allocator.Temp);
        int[] c = { 0, 1, 2, 3, 4, 5, 6, 7, 8};
        Assert.AreEqual(a.ToArray(), c);
        a.Dispose();
    }
    
    [Test]
    public void Test_01() {
        const int n = 10;
        var list = new DynamicArray<int>(n, Allocator.Temp);
        for (int i = 0; i < n; ++i) {
            list.Add(i);
        }
        
        list.RemoveAt(0);
        var a = list.ToArray(Allocator.Temp);
        int[] c = { 1, 2, 3, 4, 5, 6, 7, 8, 9};
        Assert.AreEqual(a.ToArray(), c);
        a.Dispose();
    }
    
    [Test]
    public void Test_02() {
        const int n = 10;
        var list = new DynamicArray<int>(n, Allocator.Temp);
        for (int i = 0; i < n; ++i) {
            list.Add(i);
        }

        list.RemoveAt(5);
        var a = list.ToArray(Allocator.Temp);
        int[] c = { 0, 1, 2, 3, 4, 6, 7, 8, 9};
        Assert.AreEqual(a.ToArray(), c);
        a.Dispose();
    }
    
    [Test]
    public void Test_03() {
        const int n = 10;
        var list = new DynamicArray<int>(n, Allocator.Temp);
        for (int i = 0; i < n; ++i) {
            list.Add(i);
        }
        
        for (int i = 1; i < n; ++i) {
            list.RemoveAt(0);
        }
        var a = list.ToArray(Allocator.Temp);
        int[] c = {9};
        Assert.AreEqual(a.ToArray(), c);
        a.Dispose();
    }
}