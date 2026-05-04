Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Resources
Imports System.Windows

' 程序集的常规信息通过以下特性集
' 控制。要更改与程序集相关的信息，请
' 更改这些特性值。

' 检查程序集特性值。

<Assembly: AssemblyTitle("EUD Editor 3")>
<Assembly: AssemblyDescription("EUD Edit Program")>
<Assembly: AssemblyCompany("")>
<Assembly: AssemblyProduct("EUD Editor 3")>
<Assembly: AssemblyCopyright("Copyright © 2025 맛있는빙수")>
<Assembly: AssemblyTrademark("")>
<Assembly: ComVisible(false)>

' 要开始构建可本地化的应用程序，请在
' .vbproj 文件中的 <PropertyGroup> 内设置 <UICulture>CultureYouAreCodingWith</UICulture>。
' 例如，如果在源文件中使用美国英语，
' 请将 <UICulture> 设置为 "en-US"。然后取消注释下面的
' NeutralResourceLanguage 特性。更新下面行的 "en-US"，
' 使其与项目文件中的 UICulture 设置相匹配。

'<Assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)>


' ThemeInfo 特性指定可以找到任何主题特定资源字典和通用资源字典的位置。
' 第一个参数：主题特定资源字典的位置
' （当页面或应用程序资源图片中没有
' 资源时使用）

' 第二个参数：通用资源字典的位置
' （当页面或应用程序资源图片中的
' 资源字典中没有资源时使用）
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>



' 如果此项目向 COM 公开，则以下 GUID 表示 typelib 的 ID。
<Assembly: Guid("5f564870-11c8-4d50-b537-e6ac98e9dcbe")>

' 程序集的版本信息由以下四个值组成：
'
' 主版本
' 次版本
' 构建号
' 修订号
'
' 可以指定所有值，也可以使用 '*' 自动
' 指定构建号和修订号，如下所示：
' <Assembly: AssemblyVersion("0.19.*")>

<Assembly: AssemblyVersion("0.19.6.2")>
<Assembly: AssemblyFileVersion("0.19.6.2")>
