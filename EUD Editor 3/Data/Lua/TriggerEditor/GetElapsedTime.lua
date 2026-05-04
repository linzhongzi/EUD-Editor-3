--[================================[
@Language.ko-KR
@Summary
게임 진행 시간을 반환합니다.
@Group
시스템


@Language.zh-CN
@Summary
返回游戏时间进度。
@Group
系统


@Language.en-US
@Summary
Returns the game elapsed time.
@Group
System
]================================]
function GetElapsedTime() -- General 组//返回游戏时间进度。
	echo("dwread(0x58D6F8)")
end

--[================================[
@Language.ko-KR
@Summary
게임 진행 시간 주소를 반환합니다.
@Group
시스템


@Language.zh-CN
@Summary
返回游戏时间进度地址。
@Group
系统


@Language.en-US
@Summary
Returns the game elapsed time address.
@Group
System
]================================]
function ElapsedTimeOffset() -- General 组//返回游戏时间进度地址。
	echo("0x58D6F8")
end
