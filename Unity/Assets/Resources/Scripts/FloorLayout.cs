using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

public class FloorLayout
{
    private List<List<Node>> nodes;
    private List<Edge> edges;

    private RectInt size;
    private Point startRoomLocation;
    private RangeInt numRooms;

    protected int numberOfRooms;
    public int NumberOfRooms { get => numberOfRooms;}

    public FloorLayout(RectInt size, Point startRoom, RangeInt numRooms)
    {
        this.numRooms = numRooms;
        this.size = size;
        startRoomLocation = startRoom;
        Generate();
    }

    private void Populate()
    {
        nodes = new List<List<Node>>();
        edges = new List<Edge>();

        for (int x = 0; x < size.width; x++)
        {
            nodes.Add(new List<Node>());
            for (int y = 0; y < size.height; y++)
            {
                Node n = new Node(new Point(x,y));
                nodes[x].Add(n);
                if (n.hasSpaceLeft())
                {
                    Node other = nodes[x - 1][y];
                    Edge e = new Edge(n, other);
                    other.Right = e;
                    n.Left = e;
                    edges.Add(e);
                }
                if (n.hasSpaceUp())
                {
                    Node other = nodes[x][y - 1];
                    Edge e = new Edge(n, other);
                    other.Down = e;
                    n.Up = e;
                    edges.Add(e);
                }
            }
        }
       
    }

    private void SetWeights()
    {
        foreach (Edge e in edges)
        {
            e.weight = Random.Range(0, 100);
        }
    }
    
    private void DetermineLayout()
    {

        Node startRoom = nodes[startRoomLocation.X][startRoomLocation.Y];
        startRoom.Included = true;
        SortedSet<Edge> potentialEdges = new SortedSet<Edge>(new Edge.CompareByWeight());
        potentialEdges.UnionWith(startRoom.AllEdges);
        int rooms = Random.Range(numRooms.start, numRooms.end);
        numberOfRooms = rooms;
        for (int i = 1; i < rooms; i++)
        {
            Edge nextEdge = potentialEdges.Max;
            if (!nextEdge.Included)
            {
                if (nextEdge.A.Included && nextEdge.B.Included)
                {
                    float chanceOfNewDoor = ((4 - nextEdge.A.NumDoors) * 0.25f) * ((4 - nextEdge.B.NumDoors) * 0.25f);
                    Debug.Log("Could add edge with probability " + chanceOfNewDoor);
                    if (Random.value <= chanceOfNewDoor)
                    {
                        nextEdge.Included = true;
                    }
                }
                else if (nextEdge.A.Included)
                {
                    Node newRoom = nextEdge.B;
                    newRoom.Included = true;
                    nextEdge.Included = true;
                    potentialEdges.UnionWith(newRoom.AllEdges);
                }
                else if (nextEdge.B.Included)
                {
                    Node newRoom = nextEdge.A;
                    newRoom.Included = true;
                    nextEdge.Included = true;
                    potentialEdges.UnionWith(newRoom.AllEdges);
                }
                else
                {
                    throw new System.InvalidOperationException("What the actual fuck just happened");
                }
            }
            potentialEdges.Remove(potentialEdges.Max);
        }
    }
    private void Generate()
    {
        Populate();
        SetWeights();
        DetermineLayout();
    }

    private void verifyRoomsSize(List<List<Room>> rooms)
    {
        if(rooms.Count != nodes.Count)
        {
            throw new System.ArgumentException("supplied rooms array is not the same size as layout");
        }
        for(int i=0; i<rooms.Count; i++){
            if (rooms[i].Count != nodes[i].Count)
            {
                throw new System.ArgumentException("supplied rooms array is not the same size as layout");
            }
        }
    }

    private bool notNullAndIncluded(Edge e)
    {
        return e != null && e.Included;
    }

    public void Apply(List<List<Room>> rooms)
    {
        verifyRoomsSize(rooms);
        for(int x = 0; x < size.width; x++)
        {
            for(int y = 0; y < size.height; y++)
            {
                Node currentNode = nodes[x][y];
                Room currentRoom = rooms[x][y];
                if (currentNode.Included)
                {
                    if (notNullAndIncluded(currentNode.Right))
                    {
                        currentRoom.neighbours.Add(rooms[x + 1][y]);
                    }
                    if (notNullAndIncluded(currentNode.Left))
                    {
                        currentRoom.neighbours.Add(rooms[x - 1][y]);
                    }
                    if (notNullAndIncluded(currentNode.Up))
                    {
                        currentRoom.neighbours.Add(rooms[x][y - 1]);
                    }
                    if (notNullAndIncluded(currentNode.Down))
                    {
                        currentRoom.neighbours.Add(rooms[x][y + 1]);
                    }
                    currentRoom.Filter = new Filter(notNullAndIncluded(currentNode.Up), 
                                                    notNullAndIncluded(currentNode.Down), 
                                                    notNullAndIncluded(currentNode.Left), 
                                                    notNullAndIncluded(currentNode.Right));
                }
            }
        }
    }

    //NOTE: output is mirrored along the x=y line
    public override string ToString()
    {
        string res = "";
        foreach(List<Node> l in nodes)
        {
            string l1 = "";
            string l2 = "";
            foreach(Node n in l)
            {
                if (n.Included)
                {
                    l1 += "██";
                    if (notNullAndIncluded(n.Down))
                    {
                        l1 += "═";
                    }
                    else
                    {
                        l1 += " ";
                    }

                    if(notNullAndIncluded(n.Right))
                    {
                        l2 += " ║ ";
                    }
                    else
                    {
                        l2 += "   ";
                    }
                }
                else
                {
                    l1 += "** ";
                    l2 += "   ";
                }
            }
            res += l1 + "\n" + l2 + "\n";
        }
        return res;
    }

    private class Node
    {
        Point location;

        public Edge Left;
        public Edge Right;
        public Edge Up;
        public Edge Down;

        public bool Included = false;

        public Node(Point location)
        {
            this.location = location;
        }
        public bool hasSpaceLeft()
        {
            return location.X - 1 >= 0;
        }
        public bool hasSpaceUp()
        {
            return location.Y - 1 >= 0;
        }

        private void addIfNotNull(List<Edge> list, Edge e)
        {
            if (e != null)
            {
                list.Add(e);
            }
        }
        public List<Edge> AllEdges {
            get {
                List<Edge> l = new List<Edge>();
                addIfNotNull(l, Left);
                addIfNotNull(l, Right);
                addIfNotNull(l, Up);
                addIfNotNull(l, Down);
                return l;
            }
        }
        public int NumDoors {
            get {
                int count = 0;
                foreach (Edge e in AllEdges)
                {
                    if (e.Included)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
    }
    private class Edge
    {
        public int weight = 0;
        public bool Included = false;

        public Node A { get; }
        public Node B { get; }

        public Edge(Node a, Node b)
        {
            this.A = a;
            this.B = b;
        }

        public class CompareByWeight:IComparer<Edge>
        {
            public int Compare(Edge a, Edge b)
            {
                return a.weight - b.weight;
            }
        }
    }
}
