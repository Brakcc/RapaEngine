#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public class SingleChainedList<T> : ICollection<T>, ICollection, IReadOnlyCollection<T>
{
    #region properties

    /// <summary>
    /// First element of the List 
    /// </summary>
    public SingleChainNode<T>? First => _head;
    
    /// <summary>
    /// Last element of the List
    /// </summary>
    public SingleChainNode<T>? Last => _tail;

    public int Count { get; private set; }

    bool ICollection<T>.IsReadOnly => false;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => this;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            
            var node = _head;
            var i = 0;
            
            while (i < index)
            {
                i++;
                node = node!.Next;
            }
            
            return node!.Value;
        }
        set => ReplaceAt(index, value);
    }
    
    #endregion
    
    #region constructors
    
    public SingleChainedList()
    {
        _head = null;
        _tail = null;
        Count = 0;
    }

    public SingleChainedList(IEnumerable<T> collection)
    {
        if (collection == null)
        {
            throw new ArgumentNullException(nameof(collection));
        }
        
        foreach (var item in collection)
        {
            AddLast(item);
        }
    }
        
    #endregion
    
    #region methodes

    private SingleChainNode<T>? FindNode(T value, out int i)
    {
        i = 0;
        
        if (Count <= 0 && value is null)
            return null;
        
        var n = _head;
        var c = EqualityComparer<T>.Default;

        while (i < Count)
        {
            if (c.Equals(n!.Value, value))
                return n;
            
            n = n.Next;
            i++;
        }
        
        return null;
    }

    private SingleChainNode<T>? FindNodeAt(int i)
    {
        if (i < 0 || i >= Count)
            throw new IndexOutOfRangeException();
        
        var n = _head;
        var j = 0;
        
        while (j < i)
        {
            n = n!.Next;
            j++;
        }
        
        return n;
    }

    public void Add(T value) => AddLast(value);

    private void AddLast(T value)
    {
        var node = new SingleChainNode<T>(value, this);

        if (_head == null)
        {
            _head = node;
            _tail = node;
            _head.Next = _tail;
        }
        
        else
        {
            _tail!.Next = node;
            _tail = node;
        }
        
        Count++;
    }

    public void AddAt(T value, int i)
    {
        if (i < 0 || i >= Count)
            throw new IndexOutOfRangeException();

        var n = new SingleChainNode<T>(value, this);
        
        if (i == 0)
        {
            n.Next = _head;
            _head = n;
            Count++;
            return;
        }
        
        var b = FindNodeAt(i - 1);
        n.Next = b!.Next;
        b.Next = n;
        Count++;
    }

    private void ReplaceAt(int i, T value)
    {
        if (i < 0 || i >= Count)
            throw new IndexOutOfRangeException();

        var n = new SingleChainNode<T>(value, this);
        
        if (Count == 1)
        {
            _head = n;
            _tail = n;
            _head.Next = _tail;
            return;
        }

        if (i == 0)
        {
            n.Next = _head!.Next;
            _head.OnEmpty();
            _head = n;
        }
        else
        {
            var b = FindNodeAt(i - 1);
            n.Next = b!.Next!.Next;
            b.Next.OnEmpty();
            b.Next = n;
        }
    }

    public bool Remove(T value)
    {
        if (Count <= 0 || value is null)
            throw new IndexOutOfRangeException();
        
        var n = FindNode(value, out var i);

        if (n is null)
            return false;
        
        if (Count == 1)
        {
            _head!.OnEmpty();
            _tail!.OnEmpty();
            _head = null;
            _tail = null;
            Count--;
            return true;
        }
        
        if (i == 0)
        {
            _head!.OnEmpty();
            _head = null;
            Count--;
            return true;
        }
            
        var b = FindNodeAt(i - 1);
        
        if (i == Count - 1)
        {
            n.OnEmpty();
            b!.Next = null;
        }
        else
        {
            b!.Next = n.Next;
            n.OnEmpty();
        }
        
        Count--;
        return true;
    }

    public void RemoveAt(int i)
    {
        if (Count <= 0 || i >= Count)
            throw new IndexOutOfRangeException();

        if (Count == 1)
        {
            _head!.OnEmpty();
            _tail!.OnEmpty();
            _head = null;
            _tail = null;
            Count--;
            return;
        }

        if (i == 0)
        {
            _head!.OnEmpty();
            _head = null;
            Count--;
            return;
        }
        
        var b = FindNodeAt(i - 1);

        if (i == Count - 1)
        {
            b!.Next!.OnEmpty();
            b.Next = null;
        }
        else
        {
            b!.Next = b.Next!.Next;
        }
        
        Count--;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new SingleChainEnumerator<T>(this);
    }
    
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public void Clear()
    {
        var node = _head;

        while (node is not null)
        {
            var temp = node;
            node = node.Next;
            temp.OnEmpty();
        }
        
        _head = null;
        _tail = null;
    }

    public bool Contains(T item) => FindNode(item, out _) is not null;

    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array is null)
            throw new ArgumentNullException(nameof(array));
        
        if (arrayIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));

        if (array.Length - arrayIndex < Count)
            throw new ArgumentException(null);
        
        var node = _head;
        
        if (node is null)
            return;

        while (node is not null)
        {
            array[arrayIndex++] = node.Value;
            node = node.Next;
        }
    }
    
    public void CopyTo(Array array, int index)
    {
        if (array is null)
            throw new ArgumentNullException(nameof(array));
        
        if (array.Rank != 1)
            throw new ArgumentException(null, nameof(array));
        
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));
        
        if (array.Length - index < Count)
            throw new ArgumentException(null, nameof(array));
        
        var tArray = array as T[];

        if (tArray is not null)
        {
            CopyTo(tArray, index);
        }
        else
        {
            if (array is not object[] objArray)
            {
                throw new ArgumentException(null, nameof(array));
            }
            
            var node = _head;

            try
            {
                if (node is null)
                    return;
                
                while (node is not null)
                {
                    objArray[index++] = node!.Value!;
                    node = node.Next;
                }
            }
            catch (ArrayTypeMismatchException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
    
    #endregion
    
    #region fields
    
    private SingleChainNode<T>? _tail;
    
    private SingleChainNode<T>? _head;

    #endregion
}