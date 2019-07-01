/*Author: Troy Mateo
 * Date: 06/30/2019
 * Program demonstrating basic graph functions implementing adjacency list
 */

using System;
using System.Collections.Generic;
using System.Collections;

namespace CSC395_HW5
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph myGraph = new Graph();

            myGraph.AddVertex("Alice");
            myGraph.AddVertex("Bob");
            myGraph.AddVertex("Karl");

            myGraph.PrintVertices();
            Console.WriteLine();
            myGraph.PrintEdges();

            myGraph.AddEdge("Alice", "Karl");
            myGraph.AddEdge("Alice", "Bob");
            myGraph.AddEdge("Bob", "Alice");
            myGraph.AddEdge("Bob", "Karl");
            myGraph.AddEdge("Karl", "Bob");

            Console.WriteLine();
            myGraph.PrintEdges();

            myGraph.RemoveEdge("Karl", "Bob");
            Console.WriteLine();
            myGraph.PrintEdges();

            myGraph.RemoveVertex("Alice");
            Console.WriteLine();
            myGraph.PrintEdges();
        }
    }

    public class Graph
    {

        //Data
        //Graphs made of vertices and edges
        class Vertex
        {
            public string mLabel { get; set; }

            public Vertex(string pLabel)
            {
                mLabel = pLabel;
            }
        }

        List<Vertex> vertices;
        LinkedList<Vertex>[] edges;
        int MAX_SIZE = 20;

        //ctor
        public Graph()
        {
            vertices = new List<Vertex>();
            edges = new LinkedList<Vertex>[MAX_SIZE];

        }

        //Methods 

        //Print Vertices
        public void PrintVertices()
        {
            Console.WriteLine("Printing the vertices...");
            Console.WriteLine();
            //Goes through list of vertices and prints out each one
            foreach (var vert in vertices)
            {
                Console.Write(vert.mLabel + "\t");
            }
        }

        //Print edges
        public void PrintEdges()
        {
            Console.WriteLine("Printing the edges...");


            for (int i = 0; i < vertices.Count; i++)
            {
                if (edges[i] != null && edges[i].First != null)
                {
                    //Print all rows
                    Console.WriteLine(" "); //Starts a new line
                    LinkedListNode<Vertex> current = edges[i].First;

                    while (current != null)
                    {
                        Console.Write(current.Value.mLabel + "\t");
                        current = current.Next;
                    }
                }
            }
        }


        //AddVertex
        public void AddVertex(string pLabel)
        {
            vertices.Add(new Vertex(pLabel));
        }
        
        //AddEdge from A to B

        public void AddEdge(string labelA, string labelB)
        {
            int edgeFrom = -1, edgeTo = -1;

            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].mLabel.Equals(labelA))
                {
                    edgeFrom = i;
                }
                if (vertices[i].mLabel.Equals(labelB))
                {
                    edgeTo = i;
                }
            }

            //Verticies don't exist
            if (edgeFrom == -1 || edgeTo == -1)
            {
                return;
            }
            else if (edges[edgeFrom] == null)
            {
                edges[edgeFrom] = new LinkedList<Vertex>(); //Directed edge added
                edges[edgeFrom].AddFirst(vertices[edgeTo]);
            }
            else
            {
                edges[edgeFrom].AddLast(vertices[edgeTo]);
            }
        }

        //RemoveVertex
        public void RemoveVertex(string pLabel)
        {
            //First find the  position of pLabel in vertices
            int vertexToRemove = -1;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].mLabel.Equals(pLabel))
                {
                    vertexToRemove = i; //Found pLabel
                    break;
                }
            }

            //If the label is not found
            if (vertexToRemove == -1)
            {
                return;
            }
            else
            {
                vertices.RemoveAt(vertexToRemove);
            }

            edges[vertexToRemove] = null; //Removes edges all from that row

            //Removes all edges linked to the removed vertex
            for (int j = 0; j < vertices.Count; j++)
            {
                if (edges[j] != null && edges[j].First != null)
                {
                    LinkedListNode<Vertex> current = edges[j].First;

                    if (edges[j].First.Value.mLabel.Equals(pLabel))
                    {
                        edges[j].RemoveFirst();
                    }
                    else
                    {
                        while (current != null)
                        {
                            if (current.Value.mLabel.Equals(pLabel))
                            {
                                edges[j].Remove(current);
                            }
                        }
                    }
                }
            }


        }

        //RemoveEdge
        public void RemoveEdge(string labelA, string labelB)
        {
            int edgeFrom = -1, edgeTo = -1;

            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].mLabel.Equals(labelA))
                {
                    edgeFrom = i;
                }
                if (vertices[i].mLabel.Equals(labelB))
                {
                    edgeTo = i;
                }
            }

            //Verticies don't exist
            if (edgeFrom == -1 || edgeTo == -1)
            {
                return;
            }
            else
            {
                edges[edgeFrom].Remove(vertices[edgeTo]);
            }
        }


    }
}
