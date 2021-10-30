Public Class Evaluator

   Private mParser As New parser(Me)
   Private mExtraFunctions As Object

   Private Enum eTokenType
      none
      end_of_formula
      operator_plus
      operator_minus
      operator_mul
      operator_div
      operator_percent
      open_parenthesis
      comma
      dot
      close_parenthesis
      operator_ne
      operator_gt
      operator_ge
      operator_eq
      operator_le
      operator_lt
      operator_and
      operator_or
      operator_not
      operator_concat
      value_identifier
      value_true
      value_false
      value_number
      value_string
      open_bracket
      close_bracket
   End Enum

   Private Enum ePriority
      none = 0
      [concat] = 1
      [or] = 2
      [and] = 3
      [not] = 4
      equality = 5
      plusminus = 6
      muldiv = 7
      percent = 8
      unaryminus = 9
   End Enum

   Public Class parserexception
      Inherits Exception

      Friend Sub New(ByVal str As String)
         MyBase.New(str)
      End Sub

   End Class

   Private Class tokenizer
      Private mString As String
      Private mLen As Integer
      Private mPos As Integer
      Private mCurChar As Char
      Public startpos As Integer
      Public type As eTokenType
      Public value As New System.Text.StringBuilder
      Private mParser As parser

      Sub New(ByVal Parser As parser, ByVal str As String)
         mString = str
         mLen = str.Length
         mPos = 0
         mParser = Parser
         NextChar()   ' start the machine
      End Sub

      Sub NextChar()
         If mPos < mLen Then
            mCurChar = mString.Chars(mPos)
            If mCurChar = Chr(147) Or mCurChar = Chr(148) Then
               mCurChar = """"c
            End If
            mPos += 1
         Else
            mCurChar = Nothing
         End If
      End Sub

      Public Function IsOp() As Boolean

         Return mCurChar = "+"c _
            Or mCurChar = "-"c _
            Or mCurChar = "%"c _
            Or mCurChar = "/"c _
            Or mCurChar = "("c _
            Or mCurChar = ")"c _
            Or mCurChar = "."c
      End Function

      Public Sub NextToken()
         startpos = mPos
         value.Length = 0
         type = eTokenType.none
         Do
            Select Case mCurChar
               Case Nothing
                  type = eTokenType.end_of_formula
               Case "0"c To "9"c
                  ParseNumber()
               Case "-"c
                  NextChar()
                  type = eTokenType.operator_minus
               Case "+"c
                  NextChar()
                  type = eTokenType.operator_plus
               Case "*"c
                  NextChar()
                  type = eTokenType.operator_mul
               Case "/"c
                  NextChar()
                  type = eTokenType.operator_div
               Case "%"c
                  NextChar()
                  type = eTokenType.operator_percent
               Case "("c
                  NextChar()
                  type = eTokenType.open_parenthesis
               Case ")"c
                  NextChar()
                  type = eTokenType.close_parenthesis
               Case "<"c
                  NextChar()
                  If mCurChar = "="c Then
                     NextChar()
                     type = eTokenType.operator_le
                  ElseIf mCurChar = ">"c Then
                     NextChar()
                     type = eTokenType.operator_ne
                  Else
                     type = eTokenType.operator_lt
                  End If
               Case ">"c
                  NextChar()
                  If mCurChar = "="c Then
                     NextChar()
                     type = eTokenType.operator_ge
                  Else
                     type = eTokenType.operator_gt
                  End If
               Case ","c
                  NextChar()
                  type = eTokenType.comma
               Case "="c
                  NextChar()
                  type = eTokenType.operator_eq
               Case "."c
                  NextChar()
                  type = eTokenType.dot
               Case "'"c, """"c
                  ParseString(True)
                  type = eTokenType.value_string
               Case "&"c
                  NextChar()
                  type = eTokenType.operator_concat
               Case "["c
                  NextChar()
                  type = eTokenType.open_bracket
               Case "]"c
                  NextChar()
                  type = eTokenType.close_bracket
               Case Chr(0) To " "c
                  ' do nothing
               Case Else
                  ParseIdentifier()
            End Select
            If type <> eTokenType.none Then Exit Do
            NextChar()
         Loop
      End Sub

      Public Sub ParseNumber()
         type = eTokenType.value_number
         While mCurChar >= "0"c And mCurChar <= "9"c
            value.Append(mCurChar)
            NextChar()
         End While
         If mCurChar = "."c Then
            value.Append(mCurChar)
            NextChar()
            While mCurChar >= "0"c And mCurChar <= "9"c
               value.Append(mCurChar)
               NextChar()
            End While
         End If
      End Sub

      Public Sub ParseIdentifier()
         While (mCurChar >= "0"c And mCurChar <= "9"c) _
            Or (mCurChar >= "a"c And mCurChar <= "z"c) _
            Or (mCurChar >= "A"c And mCurChar <= "Z"c) _
            Or (mCurChar >= "A"c And mCurChar <= "Z"c) _
            Or (mCurChar >= Chr(128)) _
            Or (mCurChar = "_"c)
            value.Append(mCurChar)
            NextChar()
         End While
         Select Case value.ToString
            Case "and"
               type = eTokenType.operator_and
            Case "or"
               type = eTokenType.operator_or
            Case "not"
               type = eTokenType.operator_not
            Case "true", "yes"
               type = eTokenType.value_true
            Case "false", "no"
               type = eTokenType.value_false
            Case Else
               type = eTokenType.value_identifier
         End Select
      End Sub

      Public Sub ParseString(ByVal InQuote As Boolean)
         Dim OriginalChar As Char
         If InQuote Then
            OriginalChar = mCurChar
            NextChar()
         End If

         Dim PreviousChar As Char
         Do While mCurChar <> Nothing
            If InQuote AndAlso mCurChar = OriginalChar Then
               NextChar()
               If mCurChar = OriginalChar Then
                  value.Append(mCurChar)
               Else
                  'End of String
                  Exit Sub
               End If
            ElseIf mCurChar = "%"c Then
               NextChar()
               If mCurChar = "["c Then
                  NextChar()
                  Dim SaveValue As System.Text.StringBuilder = value
                  Dim SaveStartPos As Integer = startpos
                  Me.value = New System.Text.StringBuilder
                  Me.NextToken() ' restart the tokenizer for the subExpr
                  Dim subExpr As Object
                  Try
                     subExpr = mParser.ParseExpr(0, ePriority.none)
                     If subExpr Is Nothing Then
                        Me.value.Append("<nothing>")
                     Else
                        Me.value.Append(subExpr.ToString)
                     End If
                  Catch ex As Exception
                     Me.value.Append("<error " & ex.Message & ">")
                  End Try
                  SaveValue.Append(value.ToString)
                  value = SaveValue
                  startpos = SaveStartPos
               Else
                  value.Append("%"c)
               End If
            Else
               value.Append(mCurChar)
               NextChar()
            End If
         Loop
         If InQuote Then
            RaiseError("Incomplete string, missing " & OriginalChar & "; String started")
         End If
      End Sub

      Sub RaiseError(ByVal msg As String, Optional ByVal ex As Exception = Nothing)
         If TypeOf ex Is parserexception Then
            msg &= ". " & ex.Message
         Else
            msg &= " " & " at position " & startpos
            If Not ex Is Nothing Then
               msg &= ". " & ex.Message
            End If
         End If
         Throw New parserexception(msg)
      End Sub

      Sub RaiseUnexpectedToken(Optional ByVal msg As String = Nothing)
         If Len(msg) = 0 Then
            msg = ""
         Else
            msg &= "; "
         End If
         RaiseError(msg & "Unexpected " & type.ToString().Replace("_"c, " "c) & " : " & value.ToString)
      End Sub

      Sub RaiseWrongOperator(ByVal tt As eTokenType, ByVal ValueLeft As Object, ByVal valueRight As Object, Optional ByVal msg As String = Nothing)
         If Len(msg) > 0 Then
            msg.Replace("[op]", tt.GetType.ToString)
            msg &= ". "
         End If
         msg = "Cannot apply the operator " & tt.ToString
         If ValueLeft Is Nothing Then
            msg &= " on nothing"
         Else
            msg &= " on a " & ValueLeft.GetType.ToString()
         End If
         If Not valueRight Is Nothing Then
            msg &= " and a " & valueRight.GetType.ToString()
         End If
         RaiseError(msg)
      End Sub

   End Class

   Private Class parser
      Dim tokenizer As tokenizer
      Private mEvaluator As Evaluator
      'Private mEvalBinder As New EvalBinder
      Private mEvalFunctions As New EvalFunctions

      Sub New(ByVal evaluator As Evaluator)
         mEvaluator = evaluator
      End Sub

      Friend Function ParseExpr(ByVal Acc As Object, ByVal priority As ePriority) As Object
         Dim ValueLeft, valueRight As Object
         'Dim negValue As Integer = 0
         'Dim notValue As Integer = 0
         Do
            Select Case tokenizer.type
               Case eTokenType.operator_minus
                  ' unary minus operator
                  tokenizer.NextToken()
                  ValueLeft = ParseExpr(0, ePriority.unaryminus)
                  If TypeOf ValueLeft Is Double Then
                     ValueLeft = -DirectCast(ValueLeft, Double)
                  Else
                     tokenizer.RaiseWrongOperator(eTokenType.operator_minus, ValueLeft, Nothing, _
                        "You can use [op] only with numbers")
                  End If
                  Exit Do
               Case eTokenType.operator_plus
                  ' unary minus operator
                  tokenizer.NextToken()
               Case eTokenType.operator_not
                  tokenizer.NextToken()
                  ValueLeft = ParseExpr(0, ePriority.not)
                  If TypeOf ValueLeft Is Boolean Then
                     ValueLeft = Not DirectCast(ValueLeft, Boolean)
                  Else
                     tokenizer.RaiseWrongOperator(eTokenType.operator_not, ValueLeft, Nothing, _
                        "You can use [op] only with boolean values")
                  End If
               Case eTokenType.value_identifier
                  ValueLeft = InternalGetVariable()
                  Exit Do
               Case eTokenType.value_true
                  ValueLeft = True
                  tokenizer.NextToken()
                  Exit Do
               Case eTokenType.value_false
                  ValueLeft = False
                  tokenizer.NextToken()
                  Exit Do
               Case eTokenType.value_string
                  ValueLeft = tokenizer.value.ToString
                  tokenizer.NextToken()
                  Exit Do
               Case eTokenType.value_number
                  ValueLeft = Double.Parse(tokenizer.value.ToString)
                  tokenizer.NextToken()
                  Exit Do
               Case eTokenType.open_parenthesis
                  tokenizer.NextToken()
                  ValueLeft = ParseExpr(0, ePriority.none)
                  If tokenizer.type = eTokenType.close_parenthesis Then
                     ' good we eat the end parenthesis and continue ...
                     tokenizer.NextToken()
                     Exit Do
                  Else
                     tokenizer.RaiseUnexpectedToken("End parenthesis not found")
                  End If
               Case Else
                  Exit Do
            End Select
         Loop
         Do
            Dim tt As eTokenType
            tt = tokenizer.type
            Select Case tt
               Case eTokenType.end_of_formula
                  ' end of line
                  Return ValueLeft
               Case eTokenType.value_number
                  tokenizer.RaiseUnexpectedToken("Unexpected number without previous opterator")
                  Exit Function
               Case eTokenType.operator_plus
                  If priority < ePriority.plusminus Then
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.plusminus)
                     If TypeOf ValueLeft Is Double _
                        And TypeOf valueRight Is Double Then
                        ValueLeft = CDbl(ValueLeft) + CDbl(valueRight)
                     ElseIf (TypeOf ValueLeft Is DateTime And TypeOf valueRight Is Double) Then
                        ValueLeft = CDate(ValueLeft).AddDays(CDbl(valueRight))
                     ElseIf (TypeOf ValueLeft Is Double And TypeOf valueRight Is DateTime) Then
                        ValueLeft = CDate(valueRight).AddDays(CDbl(ValueLeft))
                     Else
                        ValueLeft = ValueLeft.ToString & valueRight.ToString
                     End If
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_minus
                  If priority < ePriority.plusminus Then
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.plusminus)
                     If TypeOf ValueLeft Is Double _
                        And TypeOf valueRight Is Double Then
                        ValueLeft = CDbl(ValueLeft) - CDbl(valueRight)
                     ElseIf (TypeOf ValueLeft Is DateTime And TypeOf valueRight Is Double) Then
                        ValueLeft = CDate(valueRight).AddDays(-CDbl(ValueLeft))
                     ElseIf (TypeOf ValueLeft Is DateTime And TypeOf valueRight Is DateTime) Then
                        ValueLeft = CDate(ValueLeft).Subtract(CDate(valueRight)).TotalDays
                     Else
                        tokenizer.RaiseWrongOperator(tt, ValueLeft, valueRight, _
                            "You can use [op] only with numbers or dates")
                     End If
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_concat
                  If priority < ePriority.concat Then
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.concat)
                     ValueLeft = ValueLeft.ToString & valueRight.ToString
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_mul, eTokenType.operator_div
                  If priority < ePriority.muldiv Then
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.muldiv)

                     If TypeOf ValueLeft Is Double _
                        And TypeOf valueRight Is Double Then
                        If tt = eTokenType.operator_mul Then
                           ValueLeft = CDbl(ValueLeft) * CDbl(valueRight)
                        Else
                           ValueLeft = CDbl(ValueLeft) / CDbl(valueRight)
                        End If
                     Else
                        tokenizer.RaiseError("Cannot apply the operator * or / on a " & ValueLeft.GetType.Name & " and " & valueRight.GetType.Name & vbCrLf _
                           & "You can use - only with numbers")
                     End If
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_percent
                  If priority < ePriority.percent Then
                     tokenizer.NextToken()
                     If TypeOf ValueLeft Is Double _
                        And TypeOf Acc Is Double Then
                        ValueLeft = CDbl(Acc) * CDbl(ValueLeft) / 100.0
                     Else
                        Dim ValueLeftString As String

                        If ValueLeft Is Nothing Then
                           ValueLeftString = "nothing"
                        Else
                           ValueLeftString = ValueLeft.GetType.ToString
                        End If
                        tokenizer.RaiseError("Cannot apply the operator + or - on a " & ValueLeftString & " and " & valueRight.GetType.Name & vbCrLf _
                           & "You can use % only with numbers. For example 150 + 20.5% ")
                     End If
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_or
                  If priority < ePriority.or Then
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.or)
                     If TypeOf ValueLeft Is Boolean _
                        And TypeOf valueRight Is Boolean Then
                        ValueLeft = CBool(ValueLeft) Or CBool(valueRight)
                     Else
                        tokenizer.RaiseError("Cannot apply the operator OR on a " & ValueLeft.GetType.Name & " and " & valueRight.GetType.Name & vbCrLf _
                           & "You can use OR only with boolean values")
                     End If
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_and
                  If priority < ePriority.and Then
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.and)
                     If TypeOf ValueLeft Is Boolean _
                        And TypeOf valueRight Is Boolean Then
                        ValueLeft = CBool(ValueLeft) And CBool(valueRight)
                     Else
                        tokenizer.RaiseWrongOperator(tt, ValueLeft, valueRight, _
                           "You can use [op] only with boolean values")
                     End If
                  Else
                     Exit Do
                  End If
               Case eTokenType.operator_ne, eTokenType.operator_gt, eTokenType.operator_ge, eTokenType.operator_eq, eTokenType.operator_le, eTokenType.operator_lt
                  If priority < ePriority.equality Then
                     tt = tokenizer.type
                     tokenizer.NextToken()
                     valueRight = ParseExpr(ValueLeft, ePriority.equality)
                     If TypeOf ValueLeft Is IComparable AndAlso _
                        ValueLeft.GetType.Equals(valueRight.GetType) Then
                        Dim cmp As Integer = CType(ValueLeft, IComparable).CompareTo(valueRight)
                        Select Case tt
                           Case eTokenType.operator_ne
                              ValueLeft = (cmp <> 0)
                           Case eTokenType.operator_lt
                              ValueLeft = (cmp < 0)
                           Case eTokenType.operator_le
                              ValueLeft = (cmp <= 0)
                           Case eTokenType.operator_eq
                              ValueLeft = (cmp = 0)
                           Case eTokenType.operator_ge
                              ValueLeft = (cmp >= 0)
                           Case eTokenType.operator_gt
                              ValueLeft = (cmp > 0)
                        End Select
                     Else
                        tokenizer.RaiseWrongOperator(tt, ValueLeft, valueRight)
                     End If
                  Else
                     Exit Do
                  End If
               Case Else

                  Exit Do
            End Select
         Loop

         Return ValueLeft
      End Function

      Private Function InternalGetVariable() As Object
         ' first check functions
         Dim parameters As New ArrayList  ' parameters... 
         'Dim types As New ArrayList
         Dim valueleft As Object
         Dim CurrentObject As Object
         Do
            Dim func As String = tokenizer.value.ToString
            tokenizer.NextToken()
            parameters.Clear()
            'types.Clear()
            If tokenizer.type = eTokenType.open_parenthesis Then
               tokenizer.NextToken() 'eat the parenthesis
               Do
                  If tokenizer.type = eTokenType.close_parenthesis Then
                     ' good we eat the end parenthesis and continue ...
                     tokenizer.NextToken()
                     Exit Do
                  End If
                  valueleft = ParseExpr(0, ePriority.none)
                  parameters.Add(valueleft)
                  'If valueleft Is Nothing Then
                  'types.Add(Nothing)
                  'Else
                  '   types.Add(valueleft.GetType())
                  'End If
                  If tokenizer.type = eTokenType.close_parenthesis Then
                     ' good we eat the end parenthesis and continue ...
                     tokenizer.NextToken()
                     Exit Do
                  ElseIf tokenizer.type = eTokenType.comma Then
                     tokenizer.NextToken()
                  Else
                     tokenizer.RaiseUnexpectedToken("End parenthesis not found")
                  End If
               Loop
            End If

            Dim mi As System.Reflection.MethodInfo = Nothing
            Dim pi As System.Reflection.PropertyInfo = Nothing
            If CurrentObject Is Nothing Then
               CurrentObject = mEvalFunctions
               mi = CurrentObject.GetType().GetMethod( _
                  func)
               'Reflection.BindingFlags.Public _
               '   Or Reflection.BindingFlags.Instance _
               '   Or Reflection.BindingFlags.IgnoreCase, _
               ' mEvalBinder, _
               '  DirectCast(types.ToArray(GetType(Type)), Type()), _
               '   Nothing)
               If mi Is Nothing AndAlso Not mEvaluator.mExtraFunctions Is Nothing Then
                  CurrentObject = mEvaluator.mExtraFunctions
                  mi = CurrentObject.GetType().GetMethod(func)
                  'mi = CurrentObject.GetType().GetMethod( _
                  '   func, _
                  '   Reflection.BindingFlags.Public _
                  '      Or Reflection.BindingFlags.Instance _
                  '      Or Reflection.BindingFlags.IgnoreCase, _
                  '   mEvalBinder, _
                  '   DirectCast(types.ToArray(GetType(Type)), Type()), _
                  '   Nothing)
               End If
            Else
               mi = CurrentObject.GetType().GetMethod(func, Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance)
               If mi Is Nothing Then
                  pi = CurrentObject.GetType().GetProperty(func, Reflection.BindingFlags.IgnoreCase Or Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public)
               End If
               '                  func, _
               '  Reflection.BindingFlags.Public _
               '     Or Reflection.BindingFlags.Instance _
               '    Or Reflection.BindingFlags.IgnoreCase, _
               ' mEvalBinder, _
               '  DirectCast(types.ToArray(GetType(Type)), Type()), _
               '   Nothing)
            End If

            If Not mi Is Nothing Then
               Try
                  Dim idx As Integer = 0
                  Dim param As Object
                  For Each pri As Reflection.ParameterInfo In mi.GetParameters()
                     If idx < parameters.Count Then
                        param = parameters(idx)
                     Else
                        param = pri.DefaultValue
                        parameters.Add(param)
                     End If
                     idx += 1
                  Next
                  valueleft = mi.Invoke(CurrentObject, System.Reflection.BindingFlags.Default, Nothing, DirectCast(parameters.ToArray(GetType(Object)), Object()), Nothing)
               Catch ex As Exception
                  tokenizer.RaiseError("Error while running function " & func, ex)
               End Try
            ElseIf Not pi Is Nothing Then
               Try
                  Dim idx As Integer = 0
                  Dim param As Object
                  For Each pri As Reflection.ParameterInfo In pi.GetIndexParameters
                     If idx < parameters.Count Then
                        param = parameters(idx)
                     Else
                        param = pri.DefaultValue
                        parameters.Add(param)
                     End If
                     idx += 1
                  Next
                  valueleft = pi.GetValue(CurrentObject, System.Reflection.BindingFlags.Default, Nothing, DirectCast(parameters.ToArray(GetType(Object)), Object()), Nothing)
               Catch ex As Exception
                  tokenizer.RaiseError("Error while running function " & func, ex)
               End Try
            ElseIf parameters.Count = 0 Then
               ' then raise event
               valueleft = Nothing
               Try
                  mEvaluator.RaiseGetVariable(func, valueleft)
               Catch ex As Exception
                  tokenizer.RaiseError("Error while raising event get variable" & func, ex)
               End Try
               If valueleft Is Nothing Then
                  tokenizer.RaiseError("Unknown variable " & func)
               End If
            Else
               tokenizer.RaiseError("Unknown function " & func)
            End If
            If tokenizer.type = eTokenType.dot Then
               ' continue with the current object...
               tokenizer.NextToken()
               CurrentObject = valueleft
            Else
               Exit Do
            End If
         Loop
         Return valueleft
      End Function

      Public Function Eval(ByVal str As String) As Object
         If Len(str) > 0 Then
            tokenizer = New tokenizer(Me, str)
            tokenizer.NextToken()
            Dim res As Object = ParseExpr(Nothing, ePriority.none)
            If tokenizer.type = eTokenType.end_of_formula Then
               Return res
            Else
               tokenizer.RaiseUnexpectedToken()
            End If
         End If
      End Function

      Public Function EvalString(ByVal str As String) As String
         If Len(str) > 0 Then
            tokenizer = New tokenizer(Me, str)
            tokenizer.ParseString(False)
            Return tokenizer.value.ToString
         Else
            Return String.Empty
         End If
      End Function

   End Class

   Public Property ExtraFunctions() As Object
      Get
         Return mExtraFunctions
      End Get
      Set(ByVal Value As Object)
         mExtraFunctions = Value
      End Set
   End Property

   Public Function EvalDouble(ByRef formula As String) As Double
      Dim res As Object = Eval(formula)
      If TypeOf res Is Double Then
         Return CDbl(res)
      Else
         Throw New parserexception("The result is not a number : " & res.ToString)
      End If
   End Function

   Public Function Eval(ByVal str As String) As Object
      Return mParser.Eval(str)
   End Function

   Public Function EvalString(ByVal str As String) As String
      Return mParser.EvalString(str)
   End Function

   Private Sub RaiseGetVariable(ByVal name As String, ByRef value As Object)
      RaiseEvent GetVariable(name, value)
      If TypeOf value Is Single Or TypeOf value Is Integer Then
         value = CDbl(value)
      End If
   End Sub

   Event GetVariable(ByVal name As String, ByRef value As Object)
End Class