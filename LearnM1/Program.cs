// System.Console.WriteLine("请输入整数a：");
// var aStr = Console.ReadLine();

// if(int.TryParse(aStr,out var a){
//     Console.Write($"无法解析{aStar}");
//     return ;
// }

// Console.WriteLine("请输入整数b:");
// var b = int.Parse(bStr);
// var bStr = Console.ReadLine();
// Console.WriteLine($"结果= {a+b}");



// //提示用户输入a
// from a in  

using System;
using Itminus.FSharpExtensions;
using Microsoft.FSharp.Core;

public static class Program{
    public static void Main()
    {
        //按要求输入a、b的值
        var result = 
            from a in Input($"请输入整数a的值")
            where 要求输入的值小于10(a, 10)
            from b in Input($"请输入整数b的值")
            where 要求输入的值大于100(b,100)
            select a  +b;
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
        FSharpResult<ValueTuple,string> 要求输入的值小于10(int value, int criticalLow)
        {
            Console.WriteLine("请输入一个整数：");
            if(value < criticalLow){
                Console.WriteLine($"要求输入的值小于{criticalLow}");
                return $"要求输入的值小于10".ToErrResult<ValueTuple, string>();
            }
            return new ValueTuple().ToOkResult<ValueTuple, string>();
        }
        FSharpResult<ValueTuple, string> 要求输入的值大于100(int value, int criticalHigh)
        {
            Console.WriteLine("请输入一个整数：");
            if(value > criticalHigh){
                Console.WriteLine($"输入的值大于{criticalHigh}");
                return $"输入的值大于100".ToErrResult<ValueTuple, string>();
            }
            return new ValueTuple().ToOkResult<ValueTuple, string>();
        }

    }
}

