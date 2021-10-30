Public Class EvalFunctions
   ' FUNCTION HERE CAN BE USED IN ANY EVALUATION FORMULA
   ' PARAMETERS AND RETURN VALUES CAN BE
   '     DOUBLE (do not use integer !)
   '     DATETIME
   '     BOOLEAN

   Function sin(ByVal v As Double) As Double
      Return Math.Sin(v)
   End Function

   Function now() As DateTime
      Return Microsoft.VisualBasic.Now
   End Function

   Function today() As DateTime
      Return Microsoft.VisualBasic.Today
   End Function

   Function rnd() As Integer
      Microsoft.VisualBasic.Randomize()
      Return CInt(Microsoft.VisualBasic.Rnd() * 100)
   End Function

   Function random() As Integer
      Microsoft.VisualBasic.Randomize()
      Return CInt(Microsoft.VisualBasic.Rnd() * 100)
   End Function

   Dim mMatter As New matter

   Function matter() As matter
      Return mMatter
   End Function

   Function mid(ByVal str As String, ByVal index As Double) As String
      Return Microsoft.VisualBasic.Mid(str, CInt(index))
   End Function

   Function mid(ByVal str As String, ByVal index As Double, ByVal len As Double) As String
      Return Microsoft.VisualBasic.Mid(str, CInt(index), CInt(len))
   End Function

   Function len(ByVal str As String) As Double
      Return Microsoft.VisualBasic.Len(str)
   End Function

   Function trim(ByVal str As String) As String
      Return Microsoft.VisualBasic.Trim(str)
   End Function

   Function ifn(ByVal cond As Boolean, ByVal TrueValue As Double, ByVal FalseValue As Double) As Double
      If cond Then
         Return TrueValue
      Else
         Return FalseValue
      End If
   End Function

   Function ifd(ByVal cond As Boolean, ByVal TrueValue As Date, ByVal FalseValue As Date) As Date
      If cond Then
         Return TrueValue
      Else
         Return FalseValue
      End If
   End Function

   Function ifs(ByVal cond As Boolean, ByVal TrueValue As String, ByVal FalseValue As String) As String
      If cond Then
         Return TrueValue
      Else
         Return FalseValue
      End If
   End Function

   Function Format(ByVal value As Object, ByVal style As String) As String
      Return Microsoft.VisualBasic.Format(value, style)
   End Function

   Function UCase(ByVal value As String) As String
      Return Microsoft.VisualBasic.UCase(value)
   End Function

   Function LCase(ByVal value As String) As String
      Return Microsoft.VisualBasic.LCase(value)
   End Function

   Function WCase(ByVal value As String) As String
      If len(value) = 0 Then Return ""
      Return Microsoft.VisualBasic.UCase(value.Substring(0, 1)) & _
            Microsoft.VisualBasic.LCase(value.Substring(1))
   End Function

   Function [Date](ByVal year As Double, ByVal month As Double, ByVal day As Double) As DateTime
      Return New Date(CInt(year), CInt(month), CInt(day))
   End Function

   Function Int(ByVal value As Double) As Double
      Return Microsoft.VisualBasic.Int(value)
   End Function

   Function Round(ByVal value As Double) As Double
      Return Math.Round(value)
   End Function
End Class

Public Class Matter
   Function DateOpened() As DateTime
      Return #1/2/2003#
   End Function

   Function UnpaidBill() As Double
      Return 123456.78
   End Function

End Class
