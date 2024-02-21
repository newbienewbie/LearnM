# LearnM

这是一个如何使用`LINQ`组合业务代码的教学项目。

其基本理念源自[Railway Oriented Programming](https://fsharpforfunandprofit.com/rop/)，我个人十分喜欢`F#`，也在我独立编写的几个实际项目中用`F#`编写了业务核心逻辑，效果拔群。
不过对于我目前的团队成员来说，让他们学习`F#`，还不太现实。在小城市，连靠谱的`C#`高手都招不到，何况是`F#`？

所以，这里使用`C#`的`LINQ`来做一个简化版的`ROP`，虽然写起来不如`F#`干爽，但是好歹算是能用的土枪土炮了。

## LINQ 初步

让我们先熟悉要用到的`LINQ`子句：

- `from a in A`: 表示 **从 A 中获取 a**。
- `where`: 表示 **条件筛选**。
- `let`: 类似于于数学里的表述 **令 ... 等于 ...**
- `select`: 表示一个 **投影操作**。


## 使用`LINQ`组合业务

我们可以使用`LINQ`来组合业务，实现业务的串联和短路。
```c#
var q =
    // 查找缓存槽位
    from cacheSlot in this._slotRepo.FindByCodeAsync(cacheSlotCode)
        .MapNullableToResult<RingSlot, IErr>(() => throw new Exception($"无法找到缓存槽!（槽号={cacheSlotCode.Value}）"))
    where MustAlreadyBound(cacheSlot)
    // 校验来料载具是否正确
    from validation in this._vecBiz.ValidateAsync(input.ConveyorId, input.LocationIndex, 1, input.InputVectorIndex)
        .SelectError(e => new IErr.ValidationError(e) as IErr)
    let slot = validation.Item2.First()
    // 执行物料转移
    from x in cacheSlot.TransferTo(slot)
        .SelectError(e => new IErr.TransError(e) as IErr)
        .ToTask()
    let payload = this._serde.deserialize(slot.Payload!)
    let ok = new SwapDescription(slot.Code, cacheSlotCode, payload)
    // 保存数据库
    from s in this._conveyorRepo.SaveChangesAsync()
            .WithOkResult<SwapDescription, IErr>(ok)
    select s;
var r = await q;
```

## 基本原理

我们知道，程序员编写的`LINQ`查询，总是会被`C#`编译器翻译成等价的方法调用。举个例子，比如`select`子句会被翻译成`.Select()`方法。我们只需要实现对应的方法，既可以让`C#`编译器自动翻译这里的`LINQ`。

### `from`串联

多个`from`子句串联，会被`C#`编译器翻译成`.SelectMany`方法调用。比如：
```c#
from a in A  
from b in B  
select p;
```
会被翻译成
```c#
A.SelectMany(a => B , (a ,b) => p )
```
`from`这种串联形式天然适合表达业务的串联关系。

### where 筛选

对于集合，我们可以使用`where`子句来筛选子集。`where`子句会被`C#`编译器翻译成`.Where()` 方法调用。

由于`where`关键字十分清晰地表达了我们的意图，我们也可以用它来拦截`Result<TOk,TErr>`的错误分支。不过与针对集合的条件筛选不同，这里针对是`Result<TOk,TErr>`，我们无法根据`false`自动给出错误实例，所以这里`where`接受的函数类型是`Result<UNIT, TErr>`


这种代码风格有更好的可读性和可组合性：

- 更好的可读性: 每一个`from`子句都直接对应一个子任务；每个`where`子句都对应一个拦截条件。理解业务逻辑只需要关注`from`+`where`的串联即可。
- 更好的可组合性: 这种风格的代码，要求编写者把步骤分解成独立地函数，函数和函数之间可以根据业务需要，插入更多地拦截代码和其它业务。

## API

### 核心方法：

- `res.SelectOk(mapOk)` 把`ok`丢给`mapOk`。`mapOk`返回`TOk`类型(而非是`Result<TOk,TErr>`)。
- `res.SelectError(mapErr)` 对`Error`进行映射。`mapErr`返回`TErr2`类型，而非`Result<TOk, TErr>`类型
- `res.Bind(fn)`: 把`ok`丢给`fn`。`fn`返回`Result<TOk2, TErr>`
- `res.SelectMany(select, proj)`: 当`res`是`ok1`的时候，把`ok1`丢给`select(ok1)`，如果新的结果还是`Ok`(记作`ok2`)，再对结果进行投影`proj(ok2)`

### 链式调用API

- `.ToOkResult<TOk, TErr>()`
- `.ToErrResult<TOk, TErr>()`

- `.MapNullableToResult<>`: 把`TOk?`变成一个`Result<TOk, TErr>`
- `.WithOkResult<>`: 把`Task`变成一个`Task<Result<TOk, TErr>>`
- `res.ToTask()` : 把`FSharpResult<TOk, TError>`转成`Task<Result<TOk, TError>>`

- `fnList.ExecuteOneByOne<TOk, TError>()`：如果我们有一个动作列表，其中每个都是`Func<Result<TOk, TError>`。依次执行这些动作，结果是`Result<IList<TOk>, TError>`。
