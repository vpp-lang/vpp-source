'V++ Interpreter
'Made by VMGP Official (2016-2020)
'
'VppInterpreter.vb: Used for interpreting V++ code.


Imports System.Windows.Forms
Imports System.Threading
Imports System.IO

Public Class VppInterpreter
    Public threadname
    Public Class DefineObject
        Public value = Nothing
        Public type = Nothing

        Sub New(Optional _type As String = Nothing, Optional _value As Object = Nothing)
            type = _type
            value = _value
        End Sub
    End Class

    Private ac As New List(Of String) 'code lines
    Private code As String = "" 'raw code
    Private ticktimer As Threading.Timer 'tick timer
    Public ip = 0 'pointer
    Public ignoreerr = False
    Private tmpip = 0 'temp pointer 1
    Private tmpip1 = 0 'temp pointer 2
    Private canexec = True 'can execute
    Private inif = True 'in "if" statement
    Private starttimer As Integer 'timer value
    Private objects As New Dictionary(Of String, DefineObject) 'defined objects by script (int, string, function)
    Public didsetup = False
    Public dependencies As New Dictionary(Of String, VppInterpreter)
    Public nf = "main"
    Public cf = ""

    Sub New(fpath As String)
        code = File.ReadAllText(fpath)
        threadname = fpath
        startinterpret()
    End Sub

    Sub startinterpret()
        ac.Clear()
        For Each i As String In code.Split(vbNewLine)
            ac.Add(i)
        Next
        starttimer = 100
        ticktimer = New Threading.Timer(AddressOf tick, Nothing, 10, Timeout.Infinite)
        While True

        End While
    End Sub

    Function tick(stateInfo As Object) As TimerCallback
        If didsetup = False Then
            setup()
        ElseIf didsetup = True Then
            interpret()
        End If

        'ticktimer.Dispose()
        ticktimer = New Threading.Timer(AddressOf tick, Nothing, 0, starttimer)
    End Function

    Sub reset()
        ip = 0
        tmpip = 0
        tmpip1 = 0
        canexec = True
        inif = False
    End Sub

    Sub setup()
        If tmpip < ac.Count Then
            Static teststring As String
            Static insset() As String

            teststring = ac.Item(tmpip)
            insset = Split(teststring)

            insset(0) = ""
            For Each ii In Split(teststring)(0)

                If Asc(ii) = 9 Then

                ElseIf Asc(ii) = 10 Then

                Else
                    insset(0) = insset(0) + ii
                End If
            Next

            If insset(0) = "@ScriptName" Then
                threadname = insset(1)
            ElseIf insset(0) = "@EntryPoint" Then
                nf = insset(1)
            ElseIf insset(0) = "@Include" Then
                'dependencies.Add(insset(1), )
            ElseIf insset(0) = "@IgnoreErrors" Then
                ignoreerr = True
            ElseIf insset(0) = "@SkipSetup" Then
                tmpip = ac.Count - 1
            ElseIf insset(0) = "function" Then
                objects.Add(insset(1), New DefineObject("function", tmpip))
            Else

            End If

            tmpip = tmpip + 1
        Else
            reset()
            didsetup = True
        End If
    End Sub

    Private Sub interpret()
        If ip < ac.Count Then
            Static teststring As String
            Static insset() As String

            Try
                teststring = ac.Item(ip)
            Catch ex As ArgumentOutOfRangeException
                canexec = False
                MsgBox(".")
            End Try

            insset = Split(teststring)

            If canexec Then
                execfunction(insset, teststring.Length)

                ip = ip + 1
            End If
        End If
    End Sub

#Region "Parser Fucntions"
    Function execfunction(_insset() As String, strlen As Integer)
        'Parser
        Static insset() As String
        insset = New String(_insset.Length - 1) {}
        tmpip = 0
        For Each i In _insset
            insset(tmpip) = ""

            For Each ii In i
                If Asc(ii) = 9 Then

                ElseIf Asc(ii) = 10 Then

                Else
                    insset(tmpip) = insset(tmpip) + ii
                End If
            Next
            tmpip = tmpip + 1
        Next

        'Function executing
        If cf = nf Then
            If insset(0) = "find" Then

            ElseIf insset(0) = "command" Then
                instruction_interrupt(insset)
            ElseIf insset(0) = "wait" Then
                instruction_wait(insset)
            ElseIf insset(0) = "exit" Then
                End
            ElseIf insset(0) = "define" Then
                'do nothing
            ElseIf insset(0) = "goto" Then

            ElseIf insset(0) = "function" Then

            ElseIf insset(0) = "end" Then

            Else
                If insset(0).Length > 1 Then
                    If objects.ContainsKey(insset(0)) Then
                        If insset(1) = "=" Then
                            'Set variable value
                        Else
                            'Execute function
                            ip = objects(insset(0)).value - 1
                            nf = insset(0)
                        End If
                    Else
                        canexec = False
                        Dim insset_string = ""
                        For Each i In insset
                            insset_string = insset_string + i + " "
                        Next
                        exceptionmsg("Syntax error: " + insset_string, "g_0001")
                        Console.Read()
                        If ignoreerr = True Then
                            canexec = True
                        Else
                            End
                        End If
                    End If
                End If
            End If
        Else
            If insset(0) = "function" Then
                cf = insset(1)
            ElseIf insset(0) = "end" Then
                'cf = ""
            End If
        End If
    End Function

    Sub exceptionmsg(message As String, errcode As String)
        'Error codes
        'g_0001 - Syntax error
        'g_0002 - Instance called as a function.
        'c_0001 - Invalid command arguments.
        'g_0002 - Exception.
        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Exception: [" + errcode + "] at " + threadname + " (" + (ip + 1).ToString + "): " + message)
        Console.WriteLine("Press any key...")
        Console.Read()
    End Sub

    Sub instruction_wait(ByVal stringval() As String)
        Static parameters As String()
        parameters = parsearguments(stringval, 1)
        If Not parameters Is Nothing Then
            starttimer += Convert.ToDecimal(parameters(0))
        End If
    End Sub

    Sub instruction_def(ByVal stringval() As String)
        Static parameters As String()
        parameters = parsearguments(stringval, 1)
        If parameters(1) = "as" Then

        End If
    End Sub

    Sub instruction_interrupt(ByVal stringval() As String)
        Try
            Static parameters As String()
            parameters = parsearguments(stringval, 1)
            If parameters(0) = "0x0001" Then
                Console.Title = vppstring_to_string(parameters(1))
            ElseIf parameters(0) = "0x0002" Then
                Console.SetCursorPosition(Convert.ToDecimal(parameters(1)), Convert.ToDecimal(parameters(2)))
            ElseIf parameters(0) = "0x0003" Then
                Console.Clear()
            ElseIf parameters(0) = "0x0003" Then
                Console.CursorVisible = vppstring_to_bool(parameters(1))
            ElseIf parameters(0) = "0x0004" Then
                Console.Write(vppstring_to_string(parameters(1)))
            ElseIf parameters(0) = "0x0005" Then
                Console.WriteLine(vppstring_to_string(parameters(1)))
            ElseIf parameters(0) = "0x0006" Then
                canexec = False
                Console.Read()
                canexec = True
            ElseIf parameters(0) = "0x0007" Then
                canexec = False
                Console.Write(vppstring_to_string(parameters(1)))
                Console.ReadLine()
                canexec = True
            ElseIf parameters(0) = "0x0008" Then
                canexec = False
                MsgBox(vppstring_to_string(parameters(1)))
                canexec = True
            Else
                canexec = False
                exceptionmsg("Invalid arguments given: " + Chr(34) + parameters(0) + Chr(34), "c_0001")
                Console.Read()
                If ignoreerr = True Then
                    canexec = True
                Else
                    End
                End If
            End If
        Catch ex As Exception
            If TypeOf ex Is NullReferenceException Then
                canexec = False
                exceptionmsg("Internal exception: " + ex.Message, "c_0002")
                Console.Read()
                If ignoreerr = True Then
                    canexec = True
                Else
                    End
                End If
            End If
        End Try
    End Sub

    Public Function getvalue(name As String)
        Try
            Return objects(name).value
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "getvalue")
        End Try
    End Function

    Public Sub setvalue(name As String, value As Object)
        Try
            objects(name).value = value
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "setvalue")
        End Try
    End Sub

    Public Function getref(name As String)
        Try
            Return objects(name)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "getvalue")
        End Try
    End Function

    Public Sub setref(name As String, value As DefineObject)
        Try
            objects(name) = value
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "setvalue")
        End Try
    End Sub

    Public Function parsearguments(arguments() As String, startip As Integer)
        tmpip = 0
        Dim val() As String = New String(200) {}
        Dim tempip = 0
        Dim tempip1 = 0
        Dim ignore = False
        If arguments.Length < startip + 1 Then
            Throw New Exception
        End If
        For Each i In arguments
            If tempip >= startip Then
                For Each i1 In i
                    If i1 = "," Then
                        If ignore = True Then
                            val(tempip1) = val(tempip1) + i1
                        Else
                            tempip1 = tempip1 + 1
                        End If
                    ElseIf i1 = "(" Then
                        If ignore = True Then
                            val(tempip1) = val(tempip1) + i1
                        End If
                    ElseIf i1 = ")" Then
                        If ignore = True Then
                            val(tempip1) = val(tempip1) + i1
                        End If
                    ElseIf i1 = Chr(34) Then
                        If ignore = True Then
                            ignore = False
                        ElseIf ignore = False Then
                            ignore = True
                        End If
                        val(tempip1) = val(tempip1) + i1
                    Else
                        val(tempip1) = val(tempip1) + i1
                    End If
                Next
                val(tempip1) = val(tempip1) + " "
            Else

            End If
            tempip = tempip + 1
        Next
        tmpip = tempip
        Return val
    End Function

    Function getvariabletype(value As DefineObject)
        If value.value.Contains(Chr(34)) Then
            Return "string"
        ElseIf value.value = "true" Then
            Return "bool"
        ElseIf value.value = "false" Then
            Return "bool"
        ElseIf value.value.Remove(1) = "0" Then
            Return "int"
        ElseIf value.value.Remove(1) = "1" Then
            Return "int"
        ElseIf value.value.Remove(1) = "2" Then
            Return "int"
        ElseIf value.value.Remove(1) = "3" Then
            Return "int"
        ElseIf value.value.Remove(1) = "4" Then
            Return "int"
        ElseIf value.value.Remove(1) = "5" Then
            Return "int"
        ElseIf value.value.Remove(1) = "6" Then
            Return "int"
        ElseIf value.value.Remove(1) = "7" Then
            Return "int"
        ElseIf value.value.Remove(1) = "8" Then
            Return "int"
        ElseIf value.value.Remove(1) = "9" Then
            Return "int"
        Else
            Return Nothing
        End If
    End Function

    Function vppstring_to_string(_value As String) As String
        Static tmpval
        tmpval = ""
        For Each i In _value
            If i = Chr(34) Then

            Else
                tmpval = tmpval + i
            End If
        Next
        Return tmpval
    End Function

    Function vppstring_to_bool(_value As String) As Boolean
        If _value = "true" Then
            Return True
        ElseIf _value = "false" Then
            Return False
        End If
    End Function

    Function bool_to_vppstring(_value As Boolean) As String
        If _value = True Then
            Return "true"
        ElseIf _value = False Then
            Return "false"
        End If
    End Function

    Function stringa_to_string(_value As String()) As String
        Static tmpval_stra
        tmpval_stra = ""
        For Each i In _value
            tmpval_stra += i + " "
        Next
        Return tmpval_stra
    End Function
#End Region


End Class
