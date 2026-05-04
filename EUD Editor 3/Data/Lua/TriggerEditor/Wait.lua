waittime = 0
--[================================[
@Language.ko-KR
@Summary
프레임 [Time]만큼 기다립니다.
@Group
대기하기
@param.Time.Number


@Language.zh-CN
@Summary
等待 [Time] 个帧数。
@Group
等待
@param.Time.Number


@Language.en-US
@Summary
Waits for [Time] frames.
@Group
Wait
@param.Time.Number
The number of frames to wait.
]================================]
function Wait(Time) -- Wait 组/Number/等待 [Time] 个帧数。
	waittime = waittime + Time
	echo("}else if(WaitTimer == " .. waittime .. "){//")
end

--[================================[
@Language.ko-KR
@Summary
대기하기의 시작 부분입니다.
@Group
대기하기


@Language.zh-CN
@Summary
这是等待的开始部分
@Group
等待


@Language.en-US
@Summary
The beginning of the wait block.
@Group
Wait
]================================]
function WaitStart() -- Wait 组//这是等待的开始部分。
	waittime = 0
	echo("{static var WaitTimer = 0;")
end

--[================================[
@Language.ko-KR
@Summary
대기하기 조건 부의 시작 부분입니다.
@Group
대기하기


@Language.zh-CN
@Summary
等待条件部分的开始部分
@Group
等待


@Language.en-US
@Summary
The beginning of the condition part of the wait block.
@Group
Wait
]================================]
function WaitConditionStart() -- Wait 组//等待条件部分的开始部分。
	echo("if (WaitTimer == 0 &&")
end

--[================================[
@Language.ko-KR
@Summary
대기하기 조건 부의 끝 부분입니다.
@Group
대기하기


@Language.zh-CN
@Summary
等待条件部分的结束部分。
@Group
等待


@Language.en-US
@Summary
The end of the condition part of the wait block.
@Group
Wait
]================================]
function WaitConditionEnd() -- Wait 组//等待条件部分的结束部分。
	echo("){WaitTimer = 1;}")
end

--[================================[
@Language.ko-KR
@Summary
대기하기 액션 부의 시작 부분입니다.
@Group
대기하기


@Language.zh-CN
@Summary
这是等待动作部分的开始部分。
@Group
等待


@Language.en-US
@Summary
The beginning of the action part of the wait block.
@Group
Wait
]================================]
function WaitActionStart() -- Wait 组//这是等待动作部分的开始部分。
	echo("if(WaitTimer > 0){if (WaitTimer == 1){")
end

--[================================[
@Language.ko-KR
@Summary
대기하기 액션 부의 끝 부분입니다.
@Group
대기하기


@Language.zh-CN
@Summary
等待动作部分的结束部分。
@Group
等待


@Language.en-US
@Summary
The end of the action part of the wait block.
@Group
Wait
]================================]
function WaitActionEnd() -- Wait 组//等待动作部分的结束部分。
	echo("}WaitTimer += 1;")
end

--[================================[
@Language.ko-KR
@Summary
대기하기의 끝 부분입니다.
@Group
대기하기


@Language.zh-CN
@Summary
等待动作的结束部分。
@Group
等待


@Language.en-US
@Summary
The end of the wait block.
@Group
Wait
]================================]
function WaitEnd() -- Wait 组//等待动作的结束部分。
	echo("if (WaitTimer > " .. waittime .. "){WaitTimer = 0;}}}")
end
