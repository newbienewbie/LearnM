

// Console.WriteLine("请输入整数a:");
// var aStr = Console.ReadLine();
// var a = int.TryParse(aStr,out var a);
// Console.WriteLine("请输入一个整数b:");
// var bStr = Console.ReadLine();
// var b = int.Parse(bStr);

// Console.WriteLine($"结果={a+b}");

//输入一个整数a
//var aStr = from a in inputValue("请输入一个整数");
using Itminus.FSharpExtensions;
using System.Reflection.Metadata.Ecma335;

 var q = from a in InputValue ("请输入一个整数a")
    where 要求输入的数大于10 (a,10)
    from b in InputValue ("请输入一个整数b")
    where 要求输入的数小于100(b,100)
    select a+b;
if(q.IsOk)
{
    Console.WriteLine(q.ResultValue);
}
else
{
    Console.WriteLine($"请输入错误信息{q.ErrorValue}");
}


 FSharpResult<int ,string>  InputValue(string str)
 {
    Console.WriteLine(str);
    var s = Console.ReadLine();
    var a = int.TryParse(s,out var b);
     if(a)
     {
        return b.ToOkResult<int,string>();
     }
     else
     {
        return str.ToErrResult<int,string>();
     }

 }

 FSharpResult<ValueTuple ,string>  要求输入的数大于10(int a,float c)
 {
    if(a>c)
    {
         return new ValueTuple().ToOkResult<ValueTuple, string>();
    }
    else
    {
        var err = $"{InputValue}输入的数不大于10";
        var res =err.ToErrResult<ValueTuple, string>();
        return res;
    }

 }

 FSharpResult<ValueTuple,string> 要求输入的数小于100(int b,float d)
 {
    if(b<d)
    {
        return new ValueTuple().ToOkResult<ValueTuple,string>();
    }
    else 
    {
        var err =$"{InputValue}输入的数不小于100";
        var res = err.ToErrResult<ValueTuple ,string>();
        return res;
    }

 }