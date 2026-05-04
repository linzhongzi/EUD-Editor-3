--[================================[
@Language.ko-KR
@Summary
해당플레이어의 BGM을 [BGMName]로 설정합니다.
@Group
BGM
@param.BGMName.BGM

@Language.zh-CN
@Summary
将相关玩家的背景音乐(BGM) 设置为 [BGMName].
@Group
BGM
@param.BGMName.BGM


@Language.en-US
@Summary
Sets the BGM of the player to [BGMName].
@Group
BGM
@param.BGMName.BGM
]================================]
function SetBGM(BGMName)
	preDefine("import TriggerEditor.BGMPlayerWrapper as bg;")
	mainPreDefine("import TriggerEditor.BGMPlayerWrapper as bg;")
	
	onPluginText("bg.loadSound();")
	beforeText('foreach (cp : EUDLoopPlayer("Human")) {setcurpl(cp); bg.Exec();}')

	--bindex = GetBGMIndex(BGMName)
	 
	echo("bg.SetBGM(" .. GetReturnBGMIndex(BGMName) .. ")")
end

--[================================[
@Language.ko-KR
@Summary
해당플레이어의 BGM을 재생합니다.
@Group
BGM

@Language.zh-CN
@Summary
播放该玩家的背景音乐(BGM).
@Group
BGM


@Language.en-US
@Summary
Plays the BGM of the player.
@Group
BGM
]================================]
function BGMStart()
	preDefine("import TriggerEditor.BGMPlayerWrapper as bg;")

	echo("bg.BGMStart()")
end

--[================================[
@Language.ko-KR
@Summary
해당플레이어의 BGM을 멈춥니다.
@Group
BGM

@Language.zh-CN
@Summary
停止玩家的背景音乐(BGM).
@Group
BGM


@Language.en-US
@Summary
Stops the BGM of the player.
@Group
BGM
]================================]
function BGMStop()
	preDefine("import TriggerEditor.BGMPlayerWrapper as bg;")

	echo("bg.BGMStop()")
end

--[================================[
@Language.ko-KR
@Summary
해당플레이어의 BGM을 재개니다.
@Group
BGM

@Language.zh-CN
@Summary
恢复该玩家的背景音乐(BGM).
@Group
BGM


@Language.en-US
@Summary
Resumes the BGM of the player.
@Group
BGM
]================================]
function BGMResume()
	preDefine("import TriggerEditor.BGMPlayerWrapper as bg;")

	echo("bg.BGMResume()")
end

--[================================[
@Language.ko-KR
@Summary
해당플레이어의 BGM을 일시정지입니다.
@Group
BGM

@Language.zh-CN
@Summary
玩家的背景音乐(BGM) 已暂停。
@Group
BGM

@Language.en-US
@Summary
Pauses the BGM of the player.
@Group
BGM
]================================]
function BGMPause()
	preDefine("import TriggerEditor.BGMPlayerWrapper as bg;")

	echo("bg.BGMPause()")
end
