using MicroGrad.Net;
using Rubjerg.Graphviz;

public class GraphVisualiser
{
	// Recursively collects every Value node and the edges between parent/child Values
	private static (HashSet<Value> Nodes, HashSet<(Value From, Value To)> Edges) Trace(Value root)
	{
		var nodes = new HashSet<Value>();
		var edges = new HashSet<(Value From, Value To)>();

		void Build(Value value)
		{
			// Add returns false if this node was already visited
			if (!nodes.Add(value)) return;

			// Each child points toward the current Value
			foreach (var child in value.Prev)
			{
				edges.Add((child, value));
				Build(child);
			}
		}

		Build(root);
		return (nodes, edges);
	}

	// Generates a unique node ID from the Value's hash code.
	private static string GetId(Value value) => $"v_{value.GetHashCode()}";

	public static void SaveSvg(Value root, string filePath)
	{
		var (nodes, edges) = Trace(root);

		RootGraph graph = RootGraph.CreateNew(GraphType.Directed, "MicroGrad");

		// Lay the graph out from left to right
		graph.SetAttribute("rankdir", "LR");

		foreach (var value in nodes)
		{
			string id = GetId(value);

			// Create the Value node
			Node node = graph.GetOrAddNode(id);
			node.SetAttribute("shape", "record");
			node.SetAttribute("label", $"{{{value.Label} | data {value.Data:F4}| grad {value.Grad:F4}}}");

			// If this Value came from an operation, create an operation node
			if (!string.IsNullOrEmpty(value.OperatorType))
			{
				Node operationNode = graph.GetOrAddNode(id + value.OperatorType);
				operationNode.SetAttribute("label", value.OperatorType);

				// Operation -> resulting Value
				graph.GetOrAddEdge(operationNode, node);
			}
		}

		foreach (var (from, to) in edges)
		{
			string fromId = GetId(from);
			string toId = GetId(to);
			string operationId = toId + to.OperatorType;

			Node fromNode = graph.GetNode(fromId)!;
			Node operationNode = graph.GetNode(operationId)!;

			// Child Value -> operation
			graph.GetOrAddEdge(fromNode, operationNode);
		}

		graph.ToSvgFile(filePath);
	}
}