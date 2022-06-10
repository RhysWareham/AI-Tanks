using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Composites can have one or more children
//Sequences
//Selectors
//Parallels

public abstract class Composite : Node
{
    public List<Node> childNodes { get; private set; }
    protected int currentIndex = 0;

    protected Composite(Tank ownerTank) : base(ownerTank)
    {
        currentIndex = 0;
        childNodes = new List<Node>();
    }

    /// <summary>
    /// Function to add new child node to list
    /// </summary>
    /// <param name="newChild"></param>
    public void AddChild(Node newChild)
    {
        childNodes.Add(newChild);
    }

    /// <summary>
    /// Function to reset node index to 0
    /// </summary>
    protected void Reset()
    {
        currentIndex = 0;
    }


}