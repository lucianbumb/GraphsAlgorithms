namespace GraphsAlgorithms
{
	public class EulerianGraph
	{
		public GraphModel Graph { get; set; } = new GraphModel();

		private const string graphView = 
@"
              x1
             /   \
            x2    x3 -- x7
           /     /  \  /
         x4 -- x5    x6
";

		public EulerianGraph()
		{
			var node1 = new Node(1);
			var node2 = new Node(2);
			var node3 = new Node(3);
			var node4 = new Node(4);
			var node5 = new Node(5);
			var node6 = new Node(6);
			var node7 = new Node(7);

			Graph.IsUniDirectional = false;

			Graph.Add(node1);
			Graph.Add(node2);
			Graph.Add(node3);
			Graph.Add(node4);
			Graph.Add(node5);
			Graph.Add(node6);
			Graph.Add(node7);

			Graph.AddConnection(node1, node2);
			Graph.AddConnection(node1, node3);
			Graph.AddConnection(node2, node4);
			Graph.AddConnection(node3, node6);
			Graph.AddConnection(node3, node5);
			Graph.AddConnection(node3, node7);
			Graph.AddConnection(node4, node5);
			Graph.AddConnection(node6, node7);
		}


		public bool IsEulerian()
		{

			foreach (var node in Graph.Nodes)
			{
				var connections = Graph.GetNodeConnections(node);

				var result = connections.Count % 2 == 0;
				if (result is false) return false;
			}

			return true;
		}

		private void ResetVisitedConnections()
		{
			foreach (var connection in Graph.Connections)
			{
				connection.IsVisited = false;
			}
		}
		

		public void PrintCircuit()
		{

			if (IsEulerian() == false)
			{
				Console.WriteLine("The graph is not Eulerian");
			}

			foreach (var node in Graph.Nodes)
			{
				var circuit = VisitedNodes(new List<Node>(), node);

				var visitedNodes = circuit.Select(a=>a.Id).Distinct();
				if(visitedNodes.Count()!=Graph.Nodes.Count) continue;

				Console.WriteLine(graphView);
				Console.WriteLine();
				Console.WriteLine($"Circuit({string.Join("-", circuit.Select(a => a.Name))})");
				ResetVisitedConnections();
				return;
			}

			Console.WriteLine("No complete circuit was found!");
		}

		private List<Node> VisitedNodes(List<Node> visitedNodes, Node startNode)
		{
			visitedNodes.Add(startNode);

			var nodeConnections = Graph.GetNodeConnections(startNode);

			var destination = nodeConnections
				.Where(a => a.IsVisited == false)
				.Select(a => new { Node = a.StartNode!.Id == startNode.Id ? a.EndNode! : a.StartNode!, Connection = a })
				.MaxBy(a => a.Node.Connections.Count(x => x.IsVisited == false));


			if (destination != null)
			{
				destination.Connection.IsVisited = true;
				VisitedNodes(visitedNodes, destination.Node);
			}

			return visitedNodes;
		}
	}
}
