Imports CONEXION = PRUEBA_CONEXIONNN.Form1
Imports BE = BusinessEntities
Imports System.Data.Odbc
Imports DES = scimicvb.SCIMIC.Encriptacion




Public Class ANULAR
    '=====================resumen de boleta=============================
    Dim objCPE As New BE.CPE_RESUMEN_BOLETA
    Dim objCPE_DETALLE As BE.CPE_RESUMEN_BOLETA_DETALLE
    '=====================resumen de boleta=============================
    Dim objCPE_BAJA As New BE.CPE_BAJA
    Dim objCPE_BAJA_DETALLE As BE.CPE_BAJA_DETALLE
    '=====================consulta ticket=============================
    Dim objCPETICKET As New BE.CONSULTA_TICKET
    Dim obj As New CPEConfig
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String, Z As Integer = 0
        Dim cmd2 As New OdbcCommand, SqlDR2 As OdbcDataReader, Sql2 As String = ""

        Form1.Conn()
        cmd.Connection = Form1.Connection
        cmd.CommandType = CommandType.Text
        Sql = "Select emp_empresa.ruc, emp_empresa.telefono, emp_empresa.empresa, emp_empresa.ubigeo, emp_empresa.direccion, emp_empresa.departamento, emp_empresa.provincia, emp_empresa.distrito, emp_empresa.nombrec, emp_empresa.usuario_sol, emp_empresa.pass_sol, emp_empresa.contra_firma, emp_empresa.logo, emp_empresa.firma, emp_empresa.fe_web FROM emp_empresa WHERE emp_ppal = True"
        cmd.CommandText = Sql
        SqlDR = cmd.ExecuteReader()
        'MessageBox.Show(ComboBox1.SelectedItem.ToString)

        If ComboBox1.SelectedIndex <> -1 And secuencia.Text <> "" And motivo.Text <> "" Then




            While SqlDR.Read()
                'NOSOTROS
                objCPE_BAJA.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString

                objCPE_BAJA.FIRMA_EMP = SqlDR("firma").ToString 'pfx


                    objCPE_BAJA.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString ' "10447915125"
                objCPE_BAJA.RAZON_SOCIAL = SqlDR("empresa").ToString ' "CREVPERU S.A."
                objCPE_BAJA.TIPO_DOCUMENTO = "6"
                If ComboBox1.SelectedItem.ToString = "FACTURA" Then

                    objCPE_BAJA.CODIGO = "RA"
                Else

                    objCPE_BAJA.CODIGO = "RC"

                End If

                objCPE_BAJA.SERIE = DateTime.Now.ToString("yyyyMMdd") '"20161029"
                objCPE_BAJA.FECHA_BAJA = DateTime.Now.ToString("yyyy-MM-dd") ' "2016-10-29"
                objCPE_BAJA.SECUENCIA = secuencia.Text
                objCPE_BAJA.USUARIO_SOL_EMPRESA = SqlDR("usuario_sol") ' "MODDATOS"
                objCPE_BAJA.PASS_SOL_EMPRESA = DES.Desencripta(SqlDR("pass_sol"), "MARIO125") '"moddatos"
                objCPE_BAJA.CONTRA_FIRMA = DES.Desencripta(SqlDR("contra_firma"), "MARIO125") '"123456"
                objCPE_BAJA.TIPO_PROCESO = 1
            End While




            Dim OBJCPE_DETALLE_LIST As New List(Of BE.CPE_BAJA_DETALLE)
            'Agregando datos a la lista Comprobante detalle
            objCPE_BAJA_DETALLE = New BusinessEntities.CPE_BAJA_DETALLE

            cmd2.Connection = Form1.Connection
            cmd2.CommandType = CommandType.Text
            Sql = "SELECT public.fe_documento.id, public.fe_documento.fecha, public.fe_tipo_doc.codigo as tipo_comp, public.fe_documento.numero, fe_serie1.serie as serie, public.fe_documento.de_num FROM public.fe_documento INNER JOIN public.fe_serie ON(public.fe_documento.de_serie = public.fe_serie.id) INNER JOIN public.fe_tipo_doc ON (public.fe_serie.tipo_doc = public.fe_tipo_doc.id) INNER JOIN public.fe_serie fe_serie1 ON (public.fe_documento.de_serie = fe_serie1.id) where fe_documento.id =" & iddoc.Text
            cmd2.CommandText = Sql '"Select * from emp_empresa where id=1"
            SqlDR2 = cmd2.ExecuteReader()
            While SqlDR2.Read()
                'EMPRESA
                objCPE_BAJA.FECHA_REFERENCIA = Format(CDate(SqlDR2("fecha").ToString), "yyy-MM-dd") ' "2016-10-28"
                objCPE_BAJA_DETALLE.ITEM = 1   ' auntoincrement
                objCPE_BAJA_DETALLE.TIPO_COMPROBANTE = SqlDR2("tipo_comp").ToString ' "01"
                objCPE_BAJA_DETALLE.SERIE = SqlDR2("serie").ToString  '"FF11"
                objCPE_BAJA_DETALLE.NUMERO = SqlDR2("de_num").ToString ' "750"
                objCPE_BAJA_DETALLE.DESCRIPCION = motivo.Text '"ERROR DE DIGITACION"
            End While


            OBJCPE_DETALLE_LIST.Add(objCPE_BAJA_DETALLE)

            objCPE_BAJA.detalle = OBJCPE_DETALLE_LIST
            '======================================RESPUESTA====================================
            Dim dictionaryEnv As New Dictionary(Of String, String)
            dictionaryEnv = obj.envioBaja(objCPE_BAJA)
            TXTCOD_SUNAT.Text = dictionaryEnv.Item("cod_sunat")
            TXT_MSJ_SUNAT.Text = dictionaryEnv.Item("msj_sunat")
            TXTHASHCPE.Text = dictionaryEnv.Item("hash_cpe")
            TXTHASHCDR.Text = dictionaryEnv.Item("hash_cdr")
            '==============================
            txtticket.Text = dictionaryEnv.Item("msj_sunat")

        Else
            MessageBox.Show("FALTAN DATOS...!!")

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String


        Form1.Conn()
        cmd.Connection = Form1.Connection
        cmd.CommandType = CommandType.Text
        Sql = "SELECT emp_empresa.ruc, emp_empresa.telefono,emp_empresa.empresa, emp_empresa.ubigeo, emp_empresa.direccion, emp_empresa.departamento, emp_empresa.provincia, emp_empresa.distrito, emp_empresa.nombrec, emp_empresa.usuario_sol, emp_empresa.pass_sol, emp_empresa.contra_firma FROM emp_empresa where emp_ppal=true"
        cmd.CommandText = Sql
        SqlDR = cmd.ExecuteReader()
        While SqlDR.Read()
            objCPETICKET.TIPO_PROCESO = 1
            objCPETICKET.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString '"20505161051" ' "10447915125"
            objCPETICKET.USUARIO_SOL_EMPRESA = SqlDR("usuario_sol") '"ANDREA01" ' "MODDATOS"
            'objCPETICKET.PASS_SOL_EMPRESA = SqlDR("pass_sol") ' "SCIMIC2016" '"moddatos"
            objCPETICKET.PASS_SOL_EMPRESA = DES.Desencripta(SqlDR("pass_sol"), "MARIO125") '"moddatos"
            objCPETICKET.TICKET = txtticket.Text
            objCPETICKET.TIPO_DOCUMENTO = "RA"

            objCPETICKET.NRO_DOCUMENTO = numanul.Text '"20161029-1"
        End While




        '======================================RESPUESTA====================================
        Dim dictionaryEnv As New Dictionary(Of String, String)
        dictionaryEnv = obj.consultaTicket(objCPETICKET)
        TXTCOD_SUNAT.Text = dictionaryEnv.Item("cod_sunat")
        TXT_MSJ_SUNAT.Text = dictionaryEnv.Item("msj_sunat")
        TXTHASHCPE.Text = dictionaryEnv.Item("hash_cpe")
        TXTHASHCDR.Text = dictionaryEnv.Item("hash_cdr")
        ' txtticket.Text = ""


    End Sub

    Private Sub ANULAR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        serie.Text = DateTime.Now.ToString("yyyyMMdd")
    End Sub
End Class