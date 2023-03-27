using System;
using Itminus.FSharpExtensions;
using Microsoft.FSharp.Core;

public static class Program{
    public static void Main()
    {
        //按要求输入a、b的值
        var result = 
            from a in Input($"请输入整数a的值")
            where 要求输入的值大于10(a, 10)
            from b in Input($"请输入整数b的值")
            where 要求输入的值小于100(b,100)
            select a  + b;
        if(result.IsOk){
            Console.WriteLine(result.ResultValue);
        }
        else{
            Console.WriteLine($"{result.ErrorValue}");
        }

        FSharpResult<int ,string> Input(string str){
            Console.WriteLine(str);
            var input = Console.ReadLine();
            if(!int.TryParse(input,out var value)){
                return $"输入的{input}必须为一个整数".ToErrResult<int,string>();
            }
            return value.ToOkResult<int,string>();
        }
        FSharpResult<ValueTuple,string> 要求输入的值大于10(int value, int criticalLow)
        {
            if(value < criticalLow){
                return $"输入a的值小于10".ToErrResult<ValueTuple, string>();
            }
            return new ValueTuple().ToOkResult<ValueTuple, string>();
        }
        FSharpResult<ValueTuple, string> 要求输入的值小于100(int value, int criticalHigh)
        {
            if(value > criticalHigh){
                return $"输入b的值大于100".ToErrResult<ValueTuple, string>();
            }
            return new ValueTuple().ToOkResult<ValueTuple, string>();
        }

    }
}

