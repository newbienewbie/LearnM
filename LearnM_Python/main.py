from expression import effect
import proc

@effect.result[float, str]()
def run():
    a = input("请输入数字a:")
    a = yield from proc.try_parse_int(a)
    yield from proc.要求输入的数字大于临界值(a,10)

    b = input("请输入数字b:")
    b = yield from proc.try_parse_int(b)
    yield from proc.要求输入的数字小于临界值(b,100)

    return a + b 

    

res = run()
print(res)