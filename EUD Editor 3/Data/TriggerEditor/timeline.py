from eudplib import *


def Timeline(loopn):
    """
    这是一个根据时间进行某种操作的函数。

    for t in Timeline(60):
    内容

    这意味着每个触发器循环 t是 0, 1, 2, 3, ..., 58, 59, 0, 1,...
    到当它发生变化时，内容就会被执行。
    我在制作导弹图案时经常使用它。

    由于用于 foreach 的函数尚不能用 eps 编写，所以我用 .py 创建了函数。
    """
    v = EUDVariable(0)
    t = EUDVariable()
    t << 0
    if EUDWhile()(t == 0):
        vt = EUDVariable()  # Temporary variable
        vt << v
        yield vt
        EUDSetContinuePoint()
        v += 1
        t << 1
    EUDEndWhile()
    Trigger(v == loopn, v.SetNumber(0))


def pyrange(start, stop=None, step=1):
    if stop is None:
        start, stop = 0, start

    for i in range(start, stop, step):
        yield i
