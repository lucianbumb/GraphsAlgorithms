using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphsAlgorithms
{
	public static class SampleData
	{
		public const string GraphView = @"
              x1
             /  \
            x2   x3
           / \     \
         x4  x5    x6
";

		public static GraphModel GetGraphModel()
		{
			var node1 = new Node(1);
			var node2 = new Node(2);
			var node3 = new Node(3);
			var node4 = new Node(4);
			var node5 = new Node(5);
			var node6 = new Node(6);




			var graph = new GraphModel();

			graph.IsUniDirectional = false;

			graph.Add(node1);
			graph.Add(node2);
			graph.Add(node3);
			graph.Add(node4);
			graph.Add(node5);
			graph.Add(node6);

			graph.AddConnection(node1,node2);
			graph.AddConnection(node1,node3);
			graph.AddConnection(node2,node4);
			graph.AddConnection(node2,node5);
			graph.AddConnection(node3,node6);

			return graph;
		}
	}

    
}
