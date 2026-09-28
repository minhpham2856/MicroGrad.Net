using System.Collections.Generic;
using System.Reflection.Emit;

namespace MicroGrad.Net;

internal class Value
{
	public double Data { get; set; }
	public double Grad { get; set; } = 0.0;
	public HashSet<Value> Prev { get; }
	public string OperatorType { get; set; }
	public string? Label { get; set; }

	public Value(double data, string? label = null, string operatorType = "", params Value[] children)
	{
		Data = data;
		OperatorType = operatorType;
		Label = label;
		Prev = new HashSet<Value>(children);
	}

	public Value Add(Value value) => new(this.Data + value.Data, null, "+", this, value);
	public Value Multiply(Value value) => new(this.Data * value.Data, null, "*", this, value);
	public Value Tanh() => new(Math.Tanh(this.Data), null, "tanh", this);

	public override string? ToString() => $"Value(Data = {Data})";
}