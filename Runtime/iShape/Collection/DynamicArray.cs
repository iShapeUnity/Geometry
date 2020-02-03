using System;
using Unity.Collections;

namespace iShape.Collections {
	
	public struct DynamicArray<T>: IDisposable where T : struct {

		private const int defaultCapacity = 10;
		private readonly Allocator allocator;

		private NativeArray<T> array;

		public NativeSlice<T> slice => array.Slice(0, Count);

		public DynamicArray(Allocator allocator) {
			array = new NativeArray<T>(defaultCapacity, allocator);
			this.Count = 0;
			this.allocator = allocator;
		}
		
		public DynamicArray(int initialCapacity, Allocator allocator) {
			if(initialCapacity > 0) {
				array = new NativeArray<T>(initialCapacity, allocator);
			} else {
				array = new NativeArray<T>(defaultCapacity, allocator);
			}

			this.Count = 0;
			this.allocator = allocator;
		}

		public DynamicArray(NativeArray<T> array, Allocator allocator) {
			this.array = new NativeArray<T>(array, allocator);
			this.allocator = allocator;
			this.Count = array.Length;
		}

		public T this[int index] {
			get => array[index];
			set => array[index] = value;
		}

		public int Count { get; private set; }

		public NativeSlice<T> Slice(int start, int length) {
			return array.Slice(start, length);
		}

		public void Add(T item) {
			EnsureExplicitCapacity(Count + 1); // Increments modCount!!
			array[Count++] = item;
		}

		public void Add(NativeArray<T> items) {
			EnsureExplicitCapacity(Count + items.Length); // Increments modCount!!
			Count = array.Copy(items, Count);
		}

		public void Add(DynamicArray<T> items) {
			EnsureExplicitCapacity(Count + items.Count); // Increments modCount!!
			array.Copy(Count, items.array, 0, items.Count);
			Count += items.Count;
		}

		public void Add(NativeSlice<T> items) {
			EnsureExplicitCapacity(Count + items.Length); // Increments modCount!!
			Count = array.Copy(items, Count);
		}
		
		public void RemoveAt(int index) {
			int next = index + 1;
			if (next == Count) {
				this.RemoveLast();
				return;
			}

			int length = this.Count - next;
			var tail = this.array.Slice(next, length);
			this.array.Slice(index, length).CopyFrom(tail);
			Count -= 1;
		}
		
		public void RemoveLast() {
			if(Count > 0) {
				Count -= 1;
			}
		}

		public void RemoveAll() {
			Count = 0;
		}
		
		public void Exclude(int index) {
			int lastIndex = this.Count - 1;
			if(lastIndex != index) {
				this[index] = this[lastIndex];
			}
			this.RemoveLast();
		}

		public NativeArray<T> ToArray(Allocator allocator) {
			var flushArray = new NativeArray<T>(Count, allocator);
			flushArray.Copy(0, array, 0, Count);

			return flushArray;
		}

		public NativeArray<T> Convert() {
			var flushArray = new NativeArray<T>(Count, allocator);
			flushArray.Copy(0, array, 0, Count);

			Dispose();
			return flushArray;
		}


		public void Dispose() {
			this.array.Dispose();
			this.Count = 0;
		}


		private void EnsureExplicitCapacity(int minCapacity) {
			if(minCapacity - array.Length > 0) {
                this.Grow(minCapacity);
            }
		}

		private void Grow(int minCapacity) {
			// overflow-conscious code
			int oldCapacity = array.Length;
			int newCapacity = oldCapacity + oldCapacity + (oldCapacity >> 1);
            if (newCapacity < minCapacity) {
                newCapacity = minCapacity;
            }

            var oldArray = array;
			array = new NativeArray<T>(newCapacity, allocator);
			array.Copy(oldArray, 0);

			oldArray.Dispose();
		}
	}
}