
from expression import Ok,Error, Result


def try_parse_int(s) -> Result[int, str]:
    try:
        value = int(s, 10)
        return Ok(value)
    except ValueError:
        err = Error(f"无法将输入值({s})转成整数")
        return err

def 要求输入的数字大于临界值(a: int, c: int):
    if (a > c):
        return Ok(())
    else :
        err = f"当前数字{a}未大于{c}"
        return Error(err)


def 要求输入的数字小于临界值(a: int, c: int):
    if (a < c):
        return Ok(())
    else :
        err = f"当前数字{a}未小于{c}"
        return Error(err)