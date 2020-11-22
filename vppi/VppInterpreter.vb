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
    Private tmpip2 = -1 'temp pointer 3
    Private tmpval = Nothing 'temp value
    Private tmpval1 = Nothing 'temp value 1 (if)
    Private tmpval2 = Nothing 'temp value 2 (if)
    Private canexec = True 'can execute
    Private state = 0 'in "if" statement
    Private starttimer As Integer 'timer value
    Public objects As New Dictionary(Of String, DefineObject) 'defined objects by script (int, string, function)
    Public events As New Dictionary(Of String, String)
    Public didsetup = False
    Public dependencies As New Dictionary(Of String, VppInterpreter)
    Public nf = "main"
    Public cf = ""
    Public slave = False
    Public logfilename = ""
    Public logfile As StreamWriter
    Public readk = ""

    Sub New(fpath As String, Optional _slave As Boolean = False)
        code = File.ReadAllText(fpath)
        logfilename = "\log_" + fpath.Substring(fpath.LastIndexOf("\") + 1) + "_" + DateTime.Now.Hour.ToString + DateTime.Now.Minute.ToString + DateTime.Now.Second.ToString + ".txt"
        slave = _slave
        threadname = fpath
        startinterpret()
    End Sub

    Sub startinterpret()
        ac.Clear()
        For Each i As String In code.Split(vbNewLine)
            ac.Add(i)
        Next
        If slave = False Then
            logfile = File.CreateText(Module1.getapplogsdir() + logfilename)
            logfile.AutoFlush = True
            starttimer = 100
            ticktimer = New Threading.Timer(AddressOf tick, Nothing, 0, starttimer)
            log("[" + DateTime.Now.ToString + "]: Setting up...")
            While True

            End While
        End If
    End Sub

    Function tick(state As Object) As TimerCallback
        ptick()

        ticktimer.Dispose()
        ticktimer = New Threading.Timer(AddressOf tick, Nothing, 0, starttimer)
    End Function

    Public Sub ptick()
        If didsetup = False Then
            setup()
        ElseIf didsetup = True Then
            interpret()
        End If

        For Each i In dependencies
            i.Value.ptick()
        Next
    End Sub

    Sub reset()
        ip = 0
        tmpip = -1
        tmpip1 = -1
        canexec = True
        state = 0
    End Sub

    Sub setup()
        If tmpip < ac.Count Then
            Static steststring As String
            Static sinsset() As String

            Try
                steststring = ac.Item(tmpip)
            Catch ex As ArgumentOutOfRangeException

            End Try

            sinsset = Split(steststring)

            sinsset(0) = ""
            For Each ii In Split(steststring)(0)

                If Asc(ii) = 9 Then

                ElseIf Asc(ii) = 10 Then

                Else
                    sinsset(0) = sinsset(0) + ii
                End If
            Next

            If sinsset(0) = "@ScriptName" Then
                threadname = sinsset(1)
            ElseIf sinsset(0) = "@EntryPoint" Then
                nf = sinsset(1)
            ElseIf sinsset(0) = "@Include" Then
                dependencies.Add(sinsset(1), New VppInterpreter(sinsset(1), True))
                log("[" + DateTime.Now.ToString + "]: Loaded dependency. File path: " + sinsset(1))
            ElseIf sinsset(0) = "@IgnoreErrors" Then
                ignoreerr = True
            ElseIf sinsset(0) = "function" Then
                objects.Add(sinsset(1), New DefineObject("function", tmpip))
                log("[" + DateTime.Now.ToString + "]: Defined function " + sinsset(1) + ", ip: " + tmpip.ToString)
            ElseIf sinsset(0) = "@HandleEvent" Then
                If sinsset(1) = "InputEvent" Then
                    tmpval = "InputEvent"
                End If
            Else

            End If

            tmpip = tmpip + 1
        Else
            reset()
            log("[" + DateTime.Now.ToString + "]: Finished setting up.")
            didsetup = True
        End If
    End Sub

    Private Sub interpret()
        If ip < ac.Count Then
            Static teststring As String
            Static insset() As String

            If canexec Then
                Try
                    teststring = ac.Item(ip)
                Catch ex As ArgumentOutOfRangeException
                    teststring = ""
                End Try

                insset = Split(teststring)
                execfunction(insset, teststring.Length)

                ip = ip + 1
            End If
        Else

        End If
    End Sub

#Region "Parser Fucntions"
    Function execfunction(_insset() As String, strlen As Integer)
        'Parser
        Static insset() As String
        insset = New String(_insset.Length - 1) {}
        tmpip = 0
        For Each i In _insset

            Try
                insset(tmpip) = ""

                For Each ii In i
                    If Asc(ii) = 9 Then

                    ElseIf Asc(ii) = 10 Then

                    Else
                        insset(tmpip) = insset(tmpip) + ii
                    End If
                Next
            Catch oex As ArgumentOutOfRangeException

            Catch iex As IndexOutOfRangeException

            End Try


            tmpip = tmpip + 1
        Next

        log("[" + DateTime.Now.ToString + "]: " + "Interpreting line " + ip.ToString + ". Contents: " + Chr(34) + stringa_to_string(insset) + Chr(34))

        'Function executing
        If cf = nf Then
            If insset(0) = "find" Then

            ElseIf insset(0) = "command" Then
                instruction_interrupt(insset)

            ElseIf insset(0) = "exit" Then
                If state = 1 Then

                Else
                    End
                End If
            ElseIf insset(0) = "define" Then
                instruction_def(insset)
            ElseIf insset(0) = "function" Then

            ElseIf insset(0) = "if" Then
                instruction_if(insset)
            ElseIf insset(0) = "end" Then
                If insset(1) = "if" Then
                    If state = 1 Then
                        state = 0
                    ElseIf state = 2 Then
                        state = 0
                    End If
                End If
            ElseIf insset(0) = "#" Then
                'Comment
            ElseIf insset(0).StartsWith("#") Then
                'Comment
            Else
                If state = 1 Then

                Else
                    If insset(0).Length > 1 Then
                        If objects.ContainsKey(insset(0)) Then
                            If insset(1) = "=" Then
                                'Set variable value
                                If objects(insset(0)).type = "string" Then
                                    setvalue(insset(0), stringa_to_string(insset, 2))
                                End If
                            Else
                                'Execute function
                                tmpip2 = ip - 1
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
                            If ignoreerr = True Then
                                canexec = True
                            Else
                                Console.Read()
                                End
                            End If
                        End If
                    End If
                End If
            End If
        Else
            If insset(0) = "function" Then
                cf = insset(1)
            ElseIf insset(0) = "end" Then
                cf = ""
            ElseIf insset(0) = "#" Then
                'Comment
            ElseIf insset(0).StartsWith("#") Then
                'Comment
            End If
        End If
    End Function

    Sub exceptionmsg(message As String, errcode As String)
        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Exception: [" + errcode + "] at " + threadname + " (" + (ip + 1).ToString + "): " + message)
        Console.WriteLine("Press any key...")
    End Sub

    Sub instruction_wait(ByVal stringval() As String)
        Static parameters As String()
        parameters = parsearguments(stringval, 1)
        If Not parameters Is Nothing Then
            starttimer += Convert.ToDecimal(parameters(0))
        End If
    End Sub

    Sub instruction_if(ByVal stringvalif() As String)
        If state = 1 Then
            Exit Sub
        End If
        Static ifparameters As String()
        ifparameters = parsearguments(stringvalif, 1)
        If gettypefromval(ifparameters(1)) = "string" Then
            tmpval1 = ifparameters(1)
        ElseIf gettypefromval(ifparameters(1)) = "int" Then
            tmpval1 = ifparameters(1)
        ElseIf gettypefromval(ifparameters(1)) = "bool" Then
            tmpval1 = ifparameters(1)
        ElseIf gettypefromval(ifparameters(1)) = "null" Then
            tmpval1 = ifparameters(1)
        Else
            If objects.ContainsKey(ifparameters(1)) Then
                tmpval1 = objects(vppstring_to_string(ifparameters(1))).value
            End If
        End If

        If gettypefromval(ifparameters(2)) = "string" Then
            tmpval2 = ifparameters(2)
        ElseIf gettypefromval(ifparameters(2)) = "int" Then
            tmpval2 = ifparameters(2)
        ElseIf gettypefromval(ifparameters(2)) = "bool" Then
            tmpval2 = ifparameters(2)
        ElseIf gettypefromval(ifparameters(2)) = "null" Then
            tmpval2 = ifparameters(2)
        Else
            If objects.ContainsKey(ifparameters(2)) Then
                tmpval2 = objects(vppstring_to_string(ifparameters(2))).value
            End If
        End If

        If ifparameters(0) = "0" Then
            If tmpval1 = tmpval2 Then
                state = 2
            Else
                state = 1
            End If
        End If
        tmpval1 = Nothing
        tmpval2 = Nothing
    End Sub

    Sub instruction_def(ByVal stringval() As String)
        If state = 1 Then
            Exit Sub
        End If
        tmpval = Nothing
        If stringval(2) = "as" Then
            If gettypefromval(stringval(1)) = "int" Then
                exceptionmsg("Failed to define variable.", "d_0001")
            ElseIf gettypefromval(stringval(1)) = "string" Then
                exceptionmsg("Failed to define variable.", "d_0001")
            ElseIf gettypefromval(stringval(1)) = "bool" Then
                exceptionmsg("Failed to define variable.", "d_0001")
            Else
                If stringval(3) = "string" Then
                    If stringval(4) = "=" Then
                        Try
                            tmpval = stringa_to_string(stringval, 5)
                            log("[" + DateTime.Now.ToString + "]: Value defined (string). vname: " + Chr(34) + stringval(1) + Chr(34) + ",vval: " + Chr(34) + tmpval + Chr(34))
                            objects.Add(stringval(1), New DefineObject("string", tmpval))
                        Catch ex As Exception
                            canexec = False
                            exceptionmsg("Failed to define variable.", "d_0001")
                            If ignoreerr = True Then
                                canexec = True
                            Else
                                Console.Read()
                                End
                            End If
                        End Try
                    End If
                ElseIf stringval(3) = "bool" Then
                    If stringval(4) = "=" Then
                        Try
                            log("[" + DateTime.Now.ToString + "]: Value defined (string). vname: " + Chr(34) + stringval(1) + Chr(34) + ",vval: " + Chr(34) + tmpval + Chr(34))
                            objects.Add(stringval(1), New DefineObject("bool", stringval(5)))
                        Catch ex As Exception
                            canexec = False
                            exceptionmsg("Failed to define variable.", "d_0001")
                            If ignoreerr = True Then
                                canexec = True
                            Else
                                Console.Read()
                                End
                            End If
                        End Try
                    End If
                ElseIf stringval(3) = "int" Then
                    If stringval(4) = "=" Then
                        Try
                            log("[" + DateTime.Now.ToString + "]: Value defined (string). vname: " + Chr(34) + stringval(1) + Chr(34) + ",vval: " + Chr(34) + tmpval + Chr(34))
                            objects.Add(stringval(1), New DefineObject("int", stringval(5)))
                        Catch ex As Exception
                            canexec = False
                            exceptionmsg("Failed to define variable.", "d_0001")
                            If ignoreerr = True Then
                                canexec = True
                            Else
                                Console.Read()
                                End
                            End If
                        End Try
                    End If
                End If
            End If
        End If
    End Sub

    Sub instruction_interrupt(ByVal stringval() As String)
        If state = 1 Then
            Exit Sub
        End If
        Try
            Static parameters As String()
            parameters = parsearguments(stringval, 1)
            log("[" + DateTime.Now.ToString + "]: Command requested. cmdid: " + Chr(34) + parameters(0) + Chr(34) + " args: " + Chr(34) + stringa_to_string(parameters, 1) + Chr(34))
            If parameters(0) = "0x0001" Then
                Console.Title = vppstring_to_string(parameters(1))
            ElseIf parameters(0) = "0x0002" Then
                Console.SetCursorPosition(Convert.ToDecimal(parameters(1)), Convert.ToDecimal(parameters(2)))
            ElseIf parameters(0) = "0x0003" Then
                Console.Clear()
            ElseIf parameters(0) = "0x0004" Then
                If gettypefromval(parameters(1)) = "string" Then
                    Console.Write(vppstring_to_string(parameters(1)))
                Else
                    If objects.ContainsKey(parameters(1)) Then
                        Console.WriteLine(vppstring_to_string(objects(vppstring_to_string(parameters(1))).value))
                    End If
                End If
            ElseIf parameters(0) = "0x0005" Then
                If gettypefromval(parameters(1)) = "string" Then
                    Console.WriteLine(vppstring_to_string(parameters(1)))
                Else
                    If objects.ContainsKey(parameters(1)) Then
                        Console.WriteLine(vppstring_to_string(objects(vppstring_to_string(parameters(1))).value))
                    End If
                End If
            ElseIf parameters(0) = "0x0006" Then
                canexec = False
                Console.Read()
                canexec = True
            ElseIf parameters(0) = "0x0007" Then
                canexec = False
                Console.ReadLine()
                canexec = True
            ElseIf parameters(0) = "0x0008" Then
                canexec = False
                MsgBox(vppstring_to_string(parameters(1)))
                canexec = True
            ElseIf parameters(0) = "0x0009" Then
                Console.OpenStandardOutput()
            ElseIf parameters(0) = "0x000A" Then
                If gettypefromval(parameters(1)) = "int" Then
                    Console.ForegroundColor = Convert.ToDecimal(parameters(1))
                Else
                    If objects.ContainsKey(parameters(1)) Then
                        Console.ForegroundColor = Convert.ToDecimal(objects(parameters(1)).value)
                    End If
                End If
            ElseIf parameters(0) = "0x000B" Then
                If gettypefromval(parameters(1)) = "int" Then
                    Console.BackgroundColor = Convert.ToDecimal(parameters(1))
                Else
                    If objects.ContainsKey(parameters(1)) Then
                        Console.BackgroundColor = Convert.ToDecimal(objects(parameters(1)).value)
                    End If
                End If
            ElseIf parameters(0) = "0x000C" Then
                Beep()
            ElseIf parameters(0) = "0x000D" Then
                If gettypefromval(parameters(1)) = "int" Then
                    ip = Convert.ToDecimal(parameters(1))
                Else
                    If objects.ContainsKey(parameters(1)) Then
                        ip = Convert.ToDecimal(objects(parameters(1)).value)
                    End If
                End If
            ElseIf parameters(0) = "0x000F" Then
                If gettypefromval(parameters(1)) = "string" Then
                    canexec = False
                    exceptionmsg(parameters(1), "g_0004")
                    If ignoreerr = True Then
                        canexec = True
                    Else
                        Console.Read()
                        End
                    End If
                Else
                    If objects.ContainsKey(parameters(1)) Then
                        canexec = False
                        exceptionmsg(parameters(1), vppstring_to_string(objects(vppstring_to_string(parameters(1))).value))
                        If ignoreerr = True Then
                            canexec = True
                        Else
                            Console.Read()
                            End
                        End If
                    End If
                End If
            ElseIf parameters(0) = "0x0010" Then
                If objects.ContainsKey(parameters(1)) Then
                    canexec = False
                    objects(parameters(1)).value = InputBox(vppstring_to_string(parameters(2)))
                    canexec = True
                Else

                End If
            ElseIf parameters(0) = "0x0011" Then
                Try
                    If objects.ContainsKey(parameters(1)) Then
                        If File.Exists(vppstring_to_string(parameters(2))) Then
                            objects(parameters(1)).value = File.ReadAllText(vppstring_to_string(parameters(2)))
                        End If
                    Else

                    End If
                Catch ex As Exception
                    exceptionmsg("Failed to access file.", "p_0001")
                    If ignoreerr = True Then
                        canexec = True
                    Else
                        Console.Read()
                        End
                    End If
                End Try
            Else
                canexec = False
                exceptionmsg("Invalid arguments given: " + Chr(34) + parameters(0) + Chr(34), "c_0001")
                If ignoreerr = True Then
                    canexec = True
                Else
                    Console.Read()
                    End
                End If
            End If
        Catch ex As Exception
            If TypeOf ex Is NullReferenceException Then
                canexec = False
                exceptionmsg("Internal exception: " + ex.Message, "c_0002")
                If ignoreerr = True Then
                    canexec = True
                Else
                    Console.Read()
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

    Function _gettype(value As DefineObject)
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
        ElseIf value.value = "null" Then
            Return "null"
        ElseIf value.value = "null" Then
            Return "null"
        Else
            Return Nothing
        End If
    End Function

    Function gettypefromval(value As Object)
        If value.Contains(Chr(34)) Then
            Return "string"
        ElseIf value = "true" Then
            Return "bool"
        ElseIf value = "false" Then
            Return "bool"
        ElseIf value.Remove(1) = "0" Then
            Return "int"
        ElseIf value.Remove(1) = "1" Then
            Return "int"
        ElseIf value.Remove(1) = "2" Then
            Return "int"
        ElseIf value.Remove(1) = "3" Then
            Return "int"
        ElseIf value.Remove(1) = "4" Then
            Return "int"
        ElseIf value.Remove(1) = "5" Then
            Return "int"
        ElseIf value.Remove(1) = "6" Then
            Return "int"
        ElseIf value.Remove(1) = "7" Then
            Return "int"
        ElseIf value.Remove(1) = "8" Then
            Return "int"
        ElseIf value.Remove(1) = "9" Then
            Return "int"
        ElseIf value = "null" Then
            Return "null"
        ElseIf value = "null" Then
            Return "null"
        Else
            Return Nothing
        End If
    End Function

    Function vppstring_to_string(_value As String) As String
        Static vpps_tmpval
        vpps_tmpval = ""
        tmpval = ""
        For Each i In _value
            If i = Chr(34) Then

            ElseIf i = "\" Then
                If tmpval = "" Then
                    tmpval = "\"
                ElseIf tmpval = "\" Then
                    tmpval = "\\"
                End If
            Else
                If i = "n" Then
                    If tmpval = "\\" Then
                        vpps_tmpval = vpps_tmpval + vbNewLine
                        tmpval = ""
                    Else
                        vpps_tmpval = vpps_tmpval + i
                    End If
                Else
                    vpps_tmpval = vpps_tmpval + i
                End If
            End If
        Next
        tmpval = Nothing
        Return vpps_tmpval
    End Function

    Function string_to_bool(_value As String) As Boolean
        If _value = "true" Then
            Return True
        ElseIf _value = "false" Then
            Return False
        End If
    End Function

    Function bool_to_string(_value As Boolean) As String
        If _value = True Then
            Return "true"
        ElseIf _value = False Then
            Return "false"
        End If
    End Function

    Function stringa_to_string(_value As String(), Optional startip As Integer = 0) As String
        Static tmpval_stra
        tmpval_stra = ""
        tmpip = 0
        For Each i In _value
            If tmpip >= startip Then
                tmpval_stra += i + " "
            End If
            tmpip += 1
        Next
        Return tmpval_stra
    End Function

    Function conditionval()

    End Function

    Sub log(logmsg As String)
        If slave = False Then
            logfile.WriteLine(logmsg)
        End If
    End Sub


#End Region

End Class
