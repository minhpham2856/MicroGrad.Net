using MicroGrad.Net;

var a = new Value(2);
var b = new Value(3);

var c = a.Multiply(b);

GraphVisualiser.SaveSvg(c, "graph.svg");