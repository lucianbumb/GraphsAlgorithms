namespace GraphsAlgorithms
{
	public class GraphModel
	{
		public readonly List<Node> Nodes;
		public readonly List<Connection> Connections;

		public bool IsUniDirectional { get; set; }

		public GraphModel()
		{
			Nodes = new List<Node>();
			Connections = Nodes.SelectMany(a => a.Connections).ToList();
		}
		

		public void Add(Node node)
		{
			if (Nodes.Any(a => a.Id == node.Id))
			{
				throw new InvalidOperationException($"A node with Id:{node.Id} was already added!");
			}

			Nodes.Add(node);
			UpdateNodeConnections();
		}

		public void AddConnection(Node startNode, Node endNode, int weight)
		{
			Connection item = new Connection(startNode, endNode, weight, IsUniDirectional);

			Connections.Add(item);
			UpdateNodeConnections();
		}
		public void AddConnection(Node startNode, Node endNode)
		{
			Connections.Add(new Connection(startNode, endNode, 0, IsUniDirectional));
			UpdateNodeConnections();
		}

		public List<Connection> GetNodeConnections(Node node)
		{
			if (IsUniDirectional)
			{
				return Connections.Where(a => a.StartNode?.Id == node.Id).ToList();
			}

			return Connections.Where(a => a.StartNode?.Id == node.Id || a.EndNode?.Id == node.Id).ToList();
		}
		private void UpdateNodeConnections()
		{


			foreach (var node in Nodes)
			{
				node.Connections = GetNodeConnections(node);
			}


		}
		public void PrintNodes()
		{
			foreach (var node in Nodes)
			{
				Console.WriteLine(node);
			}
		}
		public void PrintMatrix(bool showWeight = false)
		{
			Console.WriteLine();
			Console.WriteLine("-----------------Start Print Matrix----------------------");
			Console.WriteLine();

			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine($"==> Type: {(showWeight == false ? "Show 1 if connected or 0 if not connected" : "Show Weight if connected or '?' if not connected")}");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine();

			Console.Write("  \t");
			foreach (var node in Nodes)
			{
				Console.Write(node.Name);
				Console.Write("\t");
			}

			Console.WriteLine();
			Console.WriteLine();
			foreach (var node in Nodes)
			{
				Console.Write(node.Name);
				Console.Write("\t");
				var values = new List<int>();

				foreach (var n in Nodes)
				{
					if (n.Id == node.Id)
					{
						values.Add(0);
						continue;
					}

					var connection = node.Connections.FirstOrDefault(x => x.EndNode?.Id == n.Id);

					if (connection != null)
					{
						values.Add(showWeight ? connection.Weight : 1);
					}
					else
					{
						values.Add(0);
					}
				}

				foreach (var value in values)
				{

					if (value == 0)
					{
						var textValue = showWeight ? "?" : "0";
						Console.Write($"{textValue}\t");
					}
					else
					{
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Write($"{value}\t");
						Console.ForegroundColor = ConsoleColor.White;
					}
				}

				Console.WriteLine();
			}

			Console.WriteLine();
			Console.WriteLine("-----------------End Print Matrix----------------------");
			Console.WriteLine();
		}
		public void PrintBreadthFirstSearch(Node startingNode)
		{
			Console.WriteLine();
			Console.WriteLine("--------------Start: Breadth First Search----------------");
			Queue<Node> q = new Queue<Node>();
			q.Enqueue(startingNode);

			var searchIndex = 1;

			while (q.Count > 0)
			{

				var node = q.Dequeue();
				Console.WriteLine($"Search step {searchIndex} : {node.Name}");
				searchIndex++;
				node.Visited = true;
				var nodeConnections = GetNodeConnections(node);
				foreach (var connection in nodeConnections)
				{
					var theOtherEnd = connection.GetTheOtherEnd(node);
					if (theOtherEnd is null) continue;
					if (theOtherEnd.Visited) continue;
					q.Enqueue(theOtherEnd);
				}

			}

			foreach (var node in Nodes)
			{
				node.Visited = false;
			}

			Console.WriteLine("--------------End:  Breadth First Search----------------");
			Console.WriteLine();
		}
		

		
	}





	public class Node
	{
		public Node(int id,string name)
		{
			Id = id;
			Name = name;
			Connections=new List<Connection>();
		}
		public Node(int id)
		{
			Id = id;
			Name = $"x{id}";
			Connections = new List<Connection>();
		}

		public int Id { get; set; }
		public bool Visited { get; set; }
		public int? DistanceValue { get; set; }


		public List<Connection> Connections { get; set; }

		public string Name { get; set; }

		public override string ToString()
		{
			return $"{Name} - [{string.Join(";", Connections)}]";
		}
	}


	public class Connection
	{
		public Connection(Node? startNode, Node? endNode)
		{
			StartNode = startNode;
			EndNode = endNode;
		}

		public Connection(Node? startNode, Node? endNode, int weight) : this(startNode, endNode)
		{
			Weight = weight;
		}

		public Connection(Node? startNode, Node? endNode, int weight, bool isUnidirectional) : this(startNode, endNode, weight)
		{
			IsUnidirectional = isUnidirectional;
		}

		public string Name => $"{StartNode!.Name}";
		public Node? StartNode { get; set; }
		public Node? EndNode { get; set; }
		public int Weight { get; set; }
		public bool IsUnidirectional { get; set; }
		public bool IsVisited { get; set; }

		public Node? GetTheOtherEnd(Node node)
		{
			if (node.Id == StartNode?.Id) return EndNode;
			if (node.Id == EndNode?.Id) return StartNode;

			return null;
		}

		public override string ToString()
		{
			return $"(x{StartNode?.Id}-x{EndNode?.Id})";
		}
	}
}
