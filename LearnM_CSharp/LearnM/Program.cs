var q = from a in InputValue("请输入一个整数a")
        where 要求输入的数大于10(a, 10)
        from b in InputValue("请输入一个整数b")
        where 要求输入的数小于100(b, 100)
        select a + b;
if (q.IsOk)
{
    Console.WriteLine(q.ResultValue);
}
else
{
    Console.WriteLine($"{q.ErrorValue}");
}


FSharpResult<int, string> InputValue(string str)
{
    Console.WriteLine(str);
    var s = Console.ReadLine();
    var a = int.TryParse(s, out var b);
    if (a)
    {
        return b.ToOkResult<int, string>();
    }
    else
    {
        return $"无法将{s}解析成整数".ToErrResult<int, string>();
    }

}

FSharpResult<ValueTuple, string> 要求输入的数大于10(int a, float c)
{
    if (a > c)
    {
        return new ValueTuple().ToOkResult<ValueTuple, string>();
    }
    else
    {
        var err = $"输入的数不大于10(当前={a})";
        var res = err.ToErrResult<ValueTuple, string>();
        return res;
    }

}

FSharpResult<ValueTuple, string> 要求输入的数小于100(int b, float d)
{
    if (b < d)
    {
        return new ValueTuple().ToOkResult<ValueTuple, string>();
    }
    else
    {
        var err = $"输入的数不小于100(当前={b})";
        var res = err.ToErrResult<ValueTuple, string>();
        return res;
    }

}