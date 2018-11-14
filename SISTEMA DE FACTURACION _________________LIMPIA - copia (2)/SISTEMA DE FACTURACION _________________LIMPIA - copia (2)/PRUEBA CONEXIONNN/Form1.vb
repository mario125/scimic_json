Imports System.Data.Odbc
Imports scimic = scimicvb.SCIMIC.Encriptacion

Public Class Form1
    Public Connection As OdbcConnection
    Dim id2 As String = "" 'ID_COCUMENTO
    Dim doc As String = "" '0 = NINGUNA ACCION 1= ANULAR DOCUMENTO
    Dim conex As String = "" ' CONEXION DB
    Dim arg(4)
    Dim con As String
    Public fecha As DateTime
    Public ruc As String
    Dim otro As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            'con = "Driver={PostgreSQL UNICODE(x64)};Server=localhost;Port=5432;Database=unicenta2; Uid=postgres;Pwd=root;"
            'fecha = "29/08/2018"
            'ruc = "20505161051"

            'Connection = New OdbcConnection(con)
            Connection = New OdbcConnection("Driver={PostgreSQL UNICODE};Server=192.168.1.159;Port=5434;Database=scimic; Uid=postgres;Pwd=Scimic?Developer?479;")
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
            End If
        Catch ex As Exception
            MsgBox("Error en la conexión de DB")



        End Try
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            'con = "Driver={PostgreSQL UNICODE(x64)};Server=localhost;Port=5432;Database=unicenta2; Uid=postgres;Pwd=root;"
            'fecha = "29/08/2018"
            'ruc = "20505161051"

            'Connection = New OdbcConnection(con)
            Connection = New OdbcConnection("Driver={PostgreSQL UNICODE(x64)};Server=192.168.1.159;Port=5434;Database=scimic; Uid=postgres;Pwd=Scimic?Developer?479;")
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
            End If
        Catch ex As Exception
            MsgBox("Error en la conexión de DB")



        End Try
    End Sub
    Public Sub Conn()
        Dim fileReader As String
        Dim des As String
        Dim bol As String
        Dim RUTA As String
        bol = ""
        RUTA = "Cx64.dll"
        Try
            Dim rutaFicheroINI As String
            rutaFicheroINI = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "")
            fileReader = My.Computer.FileSystem.ReadAllText(rutaFicheroINI & "\" & RUTA)
            des = scimic.Desencripta(fileReader, "MARIO125")
            Dim delimiter As Char = ","
            Dim substrings() As String = des.Split(delimiter)
            con = ""
            ruc = ""
            otro = ""
            For Each substring In substrings
                con = substrings(0).ToString
                fecha = substrings(1).ToString
                ruc = substrings(2).ToString
                otro = substrings(3).ToString
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
            End
        End Try
        Try
            'Connection = New OdbcConnection(con)
            'Connection = New OdbcConnection("Driver={PostgreSQL UNICODE};Server=190.187.129.25;Port=5432;Database=agil_camal; Uid=postgres;Pwd=Scimic?Developer?479;")
            Connection = New OdbcConnection("Driver={PostgreSQL UNICODE};Server=localhost;Port=5434;Database=scimic; Uid=postgres;Pwd=Scimic?Developer?479;")
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
            End If
        Catch ex As Exception
            MsgBox("Error en la conexión de DB=" & con)
            End
        End Try
        ' _____________________________________ enviar datos ____________
        'Try
        '    arg = Environment.GetCommandLineArgs()
        '    Dim value As String
        '    value = arg(1).ToString
        '    Dim delimiter As Char = ","
        '    Dim substrings() As String = value.Split(delimiter)
        '    Dim contar As Int16
        '    contar = 0
        '    For Each substring In substrings
        '        id2 = substrings(0)
        '        doc = substrings(1)
        '    Next
        '    ' MessageBox.Show("ID2=" + id2 + ", DOC =" + doc)
        '    If doc = "1" Then ' DECLARAR EN BLOQUE
        '        '    MessageBox.Show("en bloque" + id2)
        En_Bloque(21144)
        '        End
        '    End If
        '    If doc = "2" Then 'DECLARAR FACTURA BOLETA(correcto)
        '        '   MessageBox.Show("factura o boleta--->" + id2)
        '        Enviar_Factura_boleta(id2)
        '        End
        '    End If
        '    If doc = "3" Then 'CONSULTAR TICKET 
        '        '  MessageBox.Show("consultar tiket" + id2)
        '        Consultar_Ticket(id2)
        '        End
        '    End If

        '    End
        '    If doc = "4" Then 'nota
        '        '  MessageBox.Show("consultar tiket" + id2)
        '        NOTAS(id2)
        '        End
        '    End If

        '    End
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message & " PARÁMETROS VACIOS..")
        '    End
        'End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Notificacion(1, "scimic", "Conectando con sunat ...", ToolTipIcon.Info)
        Conn()
    End Sub
    Public Sub Notificacion(time As Int64, tex1 As String, tex2 As String, tipo As String)
        NOTIFI.ShowBalloonTip(time, tex1, tex2, tipo)
    End Sub
End Class
