# EUD Editor 3
<span class="badge-patreon">
<a href="https://www.patreon.com/bing_su" title="Donate to this project using Patreon"><img src="https://img.shields.io/badge/patreon-donate-yellow.svg" alt="Patreon donate button" /></a>
</span>

StarCraft: Remastered UMS Make Tool

星际争霸: 重制版 UMS 制作工具



> The current project is a test version. Save files may not be compatible with the full version.

> 当前版本只是个测试版本。保存的文件可能与完整版不兼容。


## Initial Setting
## 初始化配置
https://github.com/Buizz/EUD-Editor-3/wiki/%EC%B4%88%EA%B8%B0-%EC%84%B8%ED%8C%85


## UserManual
## 用户手册
https://github.com/Buizz/EUD-Editor-3/wiki


## CheckPatch
## 检查补丁
https://github.com/Buizz/EUD-Editor-3/blob/master/EUD%20Editor%203/PatchNote.txt

--------------------------------------------------------------------------------

## Fork 版本说明
Fork 并创建分支:
https://github.com/linzhongzi/EUD-Editor-3/tree/0.19.6
依赖分支:
https://github.com/linzhongzi/BingsuCodeEditor/tree/0.19.6

--------------------------------------------------------------------------------

## Tag 版本
* 0.19.6.0:	首次创建，未修改任何代码与配置。基于 EUD-Editor-3 Master 2025.05.27(8af35b1) 的提交创建分支。
* 0.19.6.1:	统一Nuget包位置。尚未翻译中文为韩文。修正编译错误。

--------------------------------------------------------------------------------

## 网上教程
https://www.bilibili.com/video/BV1SmmGYLEY8/?vd_source=3a5440c76763b1446854d89582d61af9&spm_id_from=333.788.videopod.sections

--------------------------------------------------------------------------------

## 工程目录结构:

```
EUD-Editor-3-0.19.6.1 Source\
├── BingsuCodeEditor\
│   ├── BingsuBlocklyEpsEditor\
│   ├── BingsuCodeEditor\ 
│   ├── BingsuCodeEditorTest\
│   └── NuGet.Config                   ← 指定还原 NuGet 包时的位置为: packages
├── EUD-Editor-3\
│   ├── EUD Editor 3\
│   ├── EUD Editor 3.sln
│   └── NuGet.Config                   ← 指定还原 NuGet 包时的位置为: packages
└── packages\                          ← 所有 NuGet 包统一放此
```

--------------------------------------------------------------------------------

## 导入星际争霸图片时需要的资源文件:

操作:
设置 - Default - Graphics - Remastered

文件:
EUD-Editor-3-0.19.6.1-kor Source\Data\GRPDATA

这个文件是在操作后自动生成的，如果选择 Remastered 时出错，可从其它地方拷贝一个相同文件。 

原代码的换行符如何没有从 Linux 格式转成 Windows 格式，会导致 0.19.6.1 变慢，且选择 Remastered 时会异常。

