Imports System.IO
Imports System.Threading
Public Class Form1
    Function ReadLineWithNumberFrom(filePath As String, ByVal lineNumber As Integer) As String
        Using file As New StreamReader(filePath)
            ' Skip all preceding lines: '
            For i As Integer = 1 To lineNumber - 1
                If file.ReadLine() Is Nothing Then
                    Throw New ArgumentOutOfRangeException("lineNumber")
                End If
            Next
            ' Attempt to read the line you're interested in: '
            Dim line As String = file.ReadLine()
            If line Is Nothing Then
                Throw New ArgumentOutOfRangeException("lineNumber")
            End If
            ' Succeded!
            Return line
        End Using
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ConfigLine As String = ReadLineWithNumberFrom(System.Environment.GetEnvironmentVariable("ProgramFiles") + "\VNN6\core\settings.cfg", 3)
        Dim ConfigNumber As Integer = ConfigLine.Substring(ConfigLine.Length - 1)
        If ConfigNumber = 0 Then
            Label4.Text = "正在读取配置文件..."
            Dim ConfigEdit As String = IO.File.ReadAllText(System.Environment.GetEnvironmentVariable("ProgramFiles") + "\VNN6\core\settings.cfg").Replace("onlyusetcp = 0", "onlyusetcp = 1")
            Label4.Text = "正在修改配置文件..."
            IO.File.WriteAllText(System.Environment.GetEnvironmentVariable("ProgramFiles") + "\VNN6\core\settings.cfg", ConfigEdit)
            Label4.Text = "正在重启VNN6(停止中)..."
            Process.Start("net", "stop vnn6csrv").WaitForExit()
            Thread.Sleep(1000)
            Label4.Text = "正在重启VNN6(启动中)..."
            Process.Start("net", "start vnn6csrv").WaitForExit()
            Thread.Sleep(1000)
            Label4.Text = "成功！"
            Label2.Text = "转发"
        ElseIf ConfigNumber = 1 Then
            Label4.Text = "正在读取配置文件..."
            Dim ConfigEdit As String = IO.File.ReadAllText(System.Environment.GetEnvironmentVariable("ProgramFiles") + "\VNN6\core\settings.cfg").Replace("onlyusetcp = 1", "onlyusetcp = 0")
            Label4.Text = "正在修改配置文件..."
            IO.File.WriteAllText(System.Environment.GetEnvironmentVariable("ProgramFiles") + "\VNN6\core\settings.cfg", ConfigEdit)
            Label4.Text = "正在重启VNN6(停止中)..."
            Process.Start("net", "stop vnn6csrv").WaitForExit()
            Thread.Sleep(1000)
            Label4.Text = "正在重启VNN6(启动中)..."
            Process.Start("net", "start vnn6csrv").WaitForExit()
            Thread.Sleep(1000)
            Label4.Text = "成功！"
            Label2.Text = "直连"
        Else
            MsgBox("切换过程中遇到一个问题，请访问我们的网站 http://www.vnn.cn 以获取技术人员帮助。", MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ConfigLine As String = ReadLineWithNumberFrom(System.Environment.GetEnvironmentVariable("ProgramFiles") + "\VNN6\core\settings.cfg", 3)
        Dim ConfigNumber As Integer = ConfigLine.Substring(ConfigLine.Length - 1)
        If ConfigNumber = 0 Then
            Label2.Text = "直连"
        ElseIf ConfigNumber = 1 Then
            Label2.Text = "转发"
        Else
            Label2.Text = "读取错误"
        End If
    End Sub
End Class
