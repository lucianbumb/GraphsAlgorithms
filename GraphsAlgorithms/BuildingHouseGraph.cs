namespace GraphsAlgorithms
{
	public class BuildingHouseGraph
	{
		public GraphModel Graph { get; set; }=new GraphModel();

		public BuildingHouseGraph()
		{
			var node1 = new Node(1,"Create house bluePrint");
			var node2 = new Node(2,"Build house foundation");
			var node3 = new Node(3,"Build house level 1");
			var node4 = new Node(4,"Build house level 2");
			var node5 = new Node(5,"Build house garage");
			var node6 = new Node(6,"Paint house");
			var node7 = new Node(7,"Paint garage");

			Graph.IsUniDirectional = true;

			Graph.Add(node1);
			Graph.Add(node2);
			Graph.Add(node3);
			Graph.Add(node4);
			Graph.Add(node5);
			Graph.Add(node6);
			Graph.Add(node7);

			Graph.AddConnection(node1, node2,10);
			Graph.AddConnection(node2, node3,5);
			Graph.AddConnection(node3, node4,6);
			Graph.AddConnection(node2, node5,7);
			Graph.AddConnection(node5, node7,4);
			Graph.AddConnection(node4, node6,9);
			Graph.AddConnection(node4, node5,6);
		}

		public void PrintHouseExecution()
		{
			var bkGraph = new BellmanKalabaGraph(this.Graph);

			var firstOrDefault = Graph.Nodes.FirstOrDefault(a=>a.Id==1);
			if (firstOrDefault == null)
			{
				Console.WriteLine("Graph has no starting node");
				return;
			}
			var result= bkGraph.PrintBellmanKalabaDistancePathMinValue(firstOrDefault);

			foreach (var iterationNodeDistance in result.OrderBy(a=>a.Value))
			{
				Console.WriteLine($"{iterationNodeDistance.Node.Id}.{iterationNodeDistance.Node.Name}");
			}

		}
	}
}
