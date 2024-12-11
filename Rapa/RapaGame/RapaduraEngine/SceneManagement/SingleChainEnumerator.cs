#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;

namespace Rapa.RapaGame.RapaduraEngine.SceneManagement;

public struct SingleChainEnumerator<T> : IEnumerator<T>
{
    #region properties

    public T Current => _current!;

    object? IEnumerator.Current
    {
        get
        {
            if (_position == 0 || _position >= _list.Count)
                throw new IndexOutOfRangeException();
            
            return Current;
        }
    }
    
    #endregion

    #region constructors
    
    public SingleChainEnumerator(SingleChainedList<T> list)
    {
        _list = list;
        _node = list.First;
        _current = default;
        _position = 0;
    }
    
    #endregion
    
    #region methodes
    
    public bool MoveNext()
    {
        if (_node is null)
        {
            _position = _list.Count + 1;
            return false;
        }

        ++_position;
        _current = _node.Value;
        _node = _node.Next;
        if (_node == _list.First)
            _node = null;
        
        return true;
    }

    public void Reset()
    {
        _current = default;
        _node = _list.First;
        _position = 0;
    }

    public void Dispose()
    {
    }
    
    #endregion
    
    #region fields
    
    private readonly SingleChainedList<T> _list;
    
    private int _position = -1;
    
    private T? _current;

    private SingleChainNode<T>? _node;

    #endregion
}