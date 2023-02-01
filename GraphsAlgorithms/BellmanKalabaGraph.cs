namespace GraphsAlgorithms
{
	public class BellmanKalabaGraph
	{
		public GraphModel Graph { get; set; } = new();

		public BellmanKalabaGraph(GraphModel graph)
		{
			Graph=graph;
		}

		public BellmanKalabaGraph()
		{
			var node1 = new Node(1);
			var node2 = new Node(2);
			var node3 = new Node(3);
			var node4 = new Node(4);
			var node5 = new Node(5);
			var node6 = new Node(6);

			Graph.IsUniDirectional = true;

			Graph.Add(node1);
			Graph.Add(node2);
			Graph.Add(node3);
			Graph.Add(node4);
			Graph.Add(node5);
			Graph.Add(node6);
			Graph.AddConnection(node1, node2, 10);
			Graph.AddConnection(node1, node6, 8);
			Graph.AddConnection(node2, node4, 2);
			Graph.AddConnection(node4, node3, -2);
			Graph.AddConnection(node5, node4, -1);
			Graph.AddConnection(node6, node5, 1);
			Graph.AddConnection(node5, node2, -4);
		}

		public List<IterationNodeDistance> PrintBellmanKalabaDistancePathMinValue(Node startingNode,bool printResults=false)
		{
			//max number of iterations is number of nodes - 1
			var iterations = Graph.Nodes.Count - 1;

			var iterationsResult = new Dictionary<int, List<IterationNodeDistance>>();
			startingNode.DistanceValue = 0;

			try
			{
				for (var i = 0; i < iterations; i++)
				{
					var nodes = new List<IterationNodeDistance>();

					foreach (var node in Graph.Nodes)
					{
						var hasPreviousValues = iterationsResult.TryGetValue(i - 1, out var previousValues);
						if (hasPreviousValues)
						{
							var value = previousValues?.FirstOrDefault(a => a.Node.Id == node.Id);
							nodes.Add(new IterationNodeDistance(node,value?.Value));
						}
						else
						{
							nodes.Add(new IterationNodeDistance(node,null));

						}
					}


					foreach (var node in nodes)
					{
						if (node.Node.Id == startingNode.Id)
						{
							node.Value = 0;
						}

						if(node.Value is null) continue;
						foreach (var connection in node.Node.Connections)
						{

								var item = nodes.FirstOrDefault(a => a.Node.Id == connection.EndNode!.Id);
								if(item is null) continue;

								if (item.Value == null || node.Value + connection.Weight < item.Value)
								{
									item.Value= connection.Weight+node.Value;
								}
							
						}
					}

					iterationsResult.Add(i, nodes);
					
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			if (printResults)
			{
				Console.WriteLine("====Bellman Kalaba Min Path Value=====");
				foreach (var value in iterationsResult)
				{
					Console.WriteLine($"Iteration: {value.Key} => {string.Join("\t", value.Value.Select(a => $"{a.Node.Name}({a.Value})"))}");
				}
			}

			return iterationsResult.Last().Value;
		}

		public void PrintBellmanKalabaDistancePathMaxValue(Node startingNode)
		{
			//max number of iterations is number of nodes - 1
			var iterations = Graph.Nodes.Count - 1;

			var iterationsResult = new Dictionary<int, List<IterationNodeDistance>>();
			startingNode.DistanceValue = 0;

			try
			{
				for (var i = 0; i < iterations; i++)
				{
					var nodes = new List<IterationNodeDistance>();

					foreach (var node in Graph.Nodes)
					{
						var hasPreviousValues = iterationsResult.TryGetValue(i - 1, out var previousValues);
						if (hasPreviousValues)
						{
							var value = previousValues?.FirstOrDefault(a => a.Node.Id == node.Id);
							nodes.Add(new IterationNodeDistance(node,value?.Value));
						}
						else
						{
							nodes.Add(new IterationNodeDistance(node,null));

						}
					}


					foreach (var node in nodes)
					{
						if (node.Node.Id == startingNode.Id)
						{
							node.Value = 0;
						}

						if(node.Value is null) continue;
						foreach (var connection in node.Node.Connections)
						{

							var item = nodes.FirstOrDefault(a => a.Node.Id == connection.EndNode!.Id);
							if(item is null) continue;

							if (item.Value == null || node.Value + connection.Weight > item.Value)
							{
								item.Value= connection.Weight+node.Value;
							}
							
						}
					}

					iterationsResult.Add(i, nodes);
					
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}

			Console.WriteLine("====Bellman Kalaba Max Path Value=====");
			foreach (var value in iterationsResult)
			{
				Console.WriteLine($"Iteration: {value.Key} => {string.Join("\t", value.Value.Select(a => $"{a.Node.Name}({a.Value})"))}");
			}
		}

		public class IterationNodeDistance
		{
			public IterationNodeDistance(Node Node, int? Value)
			{
				this.Node = Node;
				this.Value = Value;
			}

			public Node Node { get; set; }
			public int? Value { get; set; }
		}
	}
}
