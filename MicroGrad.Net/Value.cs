using System.Collections.Generic;

namespace MicroGrad.Net;

public class Value
{
	public double Data { get; set; }
	public double Grad { get; set; } = 0.0;
	public HashSet<Value> Prev { get; }
	public string OperatorType { get; set; }

	public Value(double data, string operatorType = "", params Value[] children)
	{
		Data = data;
		Prev = new HashSet<Value>(children);
	}

	public Value Add(Value value) => new(this.Data + value.Data, "+", this, value);
	public Value Multiply(Value value) => new(this.Data * value.Data, "*", this, value);

	public override string? ToString() => $"Value(Data = {Data})";
}