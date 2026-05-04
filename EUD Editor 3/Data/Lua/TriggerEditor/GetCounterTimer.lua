--[================================[
@Language.ko-KR
@Summary
현재 카운트 타이머를 반환합니다.
@Group
시스템

@Language.zh-CN
@Summary
返回当前计数定时器。
@Group
系统


@Language.en-US
@Summary
Returns the current count timer.
@Group
System
]================================]
function GetCounterTime() -- General 组//返回当前计数定时器。
	echo("dwread(0x58D6F4)")
end

--[================================[
@Language.ko-KR
@Summary
현재 카운트 타이머 주소를 반환합니다.
@Group
시스템

@Language.zh-CN
@Summary
返回当前计数定时器地址。
@Group
系统


@Language.en-US
@Summary
Returns the current count timer address.
@Group
System
]================================]
function CounterTimeOffset() -- General 组//返回当前计数定时器地址。
	echo("0x58D6F4")
end
