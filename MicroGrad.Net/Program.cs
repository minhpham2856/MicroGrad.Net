using MicroGrad.Net;

var a = new Value(2); a.Label = "a";
var b = new Value(-3); b.Label = "b";
var c = new Value(10); c.Label = "c";
var d = a.Add(b); d.Label = "d";
var e = d.Multiply(c); e.Label = "e";

var f = new Value(5); f.Label = "f";
var L = d.Multiply(f); L.Label = "L";


GraphVisualiser.SaveSvg(L, "graph.svg");