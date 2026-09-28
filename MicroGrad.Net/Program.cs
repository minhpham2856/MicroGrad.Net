using MicroGrad.Net;

internal class Program
{
	private static void Main(string[] args)
	{
		var x1 = new Value(2, "x1");
		var x2 = new Value(0, "x2");

		var w1 = new Value(-3, "w1");
		var w2 = new Value(1, "w2");

		var bias = new Value(6.7, "b");

		// y = Wx + b => y = w1 * x1 + w2 * x2 + b
		var w1x1 = w1.Multiply(x1); w1x1.Label = "w1x1";
		var w2x2 = w2.Multiply(x2); w2x2.Label = "w2x2";

		var Wx = w1x1.Add(w2x2); Wx.Label = "Wx";
		var y = Wx.Add(bias); y.Label = "y";

		var a = y.Tanh(); a.Label = "a";

		Graph(a);
	}

	private static void Graph(Value node) => GraphVisualiser.CreateSvg(node, "graph.svg");
}