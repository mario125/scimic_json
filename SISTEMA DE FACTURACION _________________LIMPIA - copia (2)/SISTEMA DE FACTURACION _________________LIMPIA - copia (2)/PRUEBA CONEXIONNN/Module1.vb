Imports System.Data.Odbc
Imports BE = BusinessEntities
Imports System.Security.Cryptography
Imports System.Text
Imports scimic = scimicvb.SCIMIC.Encriptacion
Imports conex = PRUEBA_CONEXIONNN.Form1
Module Module1
    Dim objCPE As New BE.CPE
    Dim objCPE2 As New BE.CPE_RESUMEN_BOLETA
    Dim objCPE_DETALLE2 As BE.CPE_RESUMEN_BOLETA_DETALLE
    Dim objCPE_DETALLE As BE.CPE_DETALLE
    Dim objCPE_BAJA As New BE.CPE_BAJA
    Dim obj As New CPEConfig
    Dim objCPE_BAJA_DETALLE As BE.CPE_BAJA_DETALLE
    Public mi_documento As String
    Public estado_db As String
    Public Function Tipo_doc(id As Integer)
        Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String, tipo_return As Integer
        cmd.Connection = Form1.Connection
        cmd.CommandType = CommandType.Text
        Sql = "select tipo from fe_anula where id=" & id
        cmd.CommandText = Sql
        SqlDR = cmd.ExecuteReader()
        While SqlDR.Read()
            tipo_return = SqlDR("tipo")
        End While
        Return tipo_return
    End Function
    Public Sub En_Bloque(id As Integer)
        Dim dateARC As Date = Format(Form1.fecha, "dd-MM-yyyy")
        Dim dateSIS As Date = DateTime.Now.ToString("dd-MM-yyyy")
        Dim days As Long = DateDiff(DateInterval.Day, dateSIS, dateARC)

        If days < 0 Then

            If days < -15 Then
                MessageBox.Show("vencio no puede emitir")
                End
            Else
                MessageBox.Show("ya vencio, le  quedan " + (15 + (days)).ToString + " dias")
                Dim tipo As Integer = Tipo_doc(id)
                If tipo = 1 Then 'COMUNICACION DE  BAJA (ANULAR FACTURAS)|
                    Anular_Factura(id)
                End If
                If tipo = 2 Then 'RESUMEN DIARIO (ANULAR BOLETAS)
                    Anular_Factura(id)
                End If
                If tipo = 3 Then 'RESUMEN DIARIO (DECLARAR BOLETAS)
                    Resumen_Diario_Boletas(id, 0)
                End If
            End If
        Else
            Dim tipo As Integer = Tipo_doc(id)
            If tipo = 1 Then 'COMUNICACION DE  BAJA (ANULAR FACTURAS)
                Anular_Factura(id)
            End If
            If tipo = 2 Then 'RESUMEN DIARIO (ANULAR BOLETAS)
                Resumen_Diario_Boletas(id, 1)
            End If
            If tipo = 3 Then 'RESUMEN DIARIO (DECLARAR BOLETAS)
                Resumen_Diario_Boletas(id, 0)
            End If
        End If
    End Sub
    Public Sub Enviar_Factura_boleta(id As Integer)
        Dim dateARC As Date = Format(Form1.fecha, "dd-MM-yyyy")
        Dim dateSIS As Date = DateTime.Now.ToString("dd-MM-yyyy")
        Dim days As Long = DateDiff(DateInterval.Day, dateSIS, dateARC)
        If days < 0 Then
            If days < -15 Then
                MessageBox.Show("vencio no puede emitir")
                End
            Else
                MessageBox.Show("ya vencio, le  quedan " + (15 + (days)).ToString + " dias")
                DocFACTURAR(id)
            End If
        Else
            DocFACTURAR(id)
        End If
    End Sub
    Public Sub DocFACTURAR(id As Integer)
        Try
            Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String, Z As Integer
            Dim cmd2 As New OdbcCommand, SqlDR2 As OdbcDataReader, Sql2 As String
            Sql2 = ""
            Dim cmd3 As New OdbcCommand, SqlDR3 As OdbcDataReader, Sql3 As String
            Sql3 = ""
            cmd.Connection = Form1.Connection
            cmd.CommandType = CommandType.Text
            '___________________________DATOS DE EMPRESA EMISORA DE DOCUMENTOS
            Sql = "Select emp_empresa.ruc, emp_empresa.telefono, emp_empresa.empresa, emp_empresa.ubigeo, emp_empresa.direccion, emp_empresa.departamento, emp_empresa.provincia, emp_empresa.distrito, emp_empresa.nombrec, emp_empresa.usuario_sol, emp_empresa.pass_sol, emp_empresa.contra_firma, emp_empresa.logo, emp_empresa.firma, emp_empresa.fe_web FROM emp_empresa WHERE emp_ppal = True"
            cmd.CommandText = Sql
            SqlDR = cmd.ExecuteReader()
            While SqlDR.Read()
                objCPE.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString
                If SqlDR("firma").ToString <> "FIRMABETA.pfx" Then 'prduccion
                    objCPE.FIRMA_EMP = SqlDR("firma").ToString 'pfx
                Else ' beta
                    objCPE.FIRMA_EMP = "beta" 'pfx
                End If
                objCPE.TIPO_DOCUMENTO_EMPRESA = "6" '1=DNI,6=RUC
                objCPE.NOMBRE_COMERCIAL_EMPRESA = SqlDR("nombrec").ToString
                objCPE.CODIGO_UBIGEO_EMPRESA = SqlDR("ubigeo").ToString
                objCPE.DIRECCION_EMPRESA = SqlDR("direccion").ToString
                objCPE.TELEFONO_PRINCIPAL = SqlDR("telefono").ToString
                objCPE.DEPARTAMENTO_EMPRESA = SqlDR("departamento").ToString
                objCPE.PROVINCIA_EMPRESA = SqlDR("provincia").ToString
                objCPE.DISTRITO_EMPRESA = SqlDR("distrito").ToString
                objCPE.CODIGO_PAIS_EMPRESA = "PE"
                objCPE.RAZON_SOCIAL_EMPRESA = SqlDR("empresa").ToString
                objCPE.LOGO_EMP = SqlDR("logo").ToString
                objCPE.WEB_EMP = SqlDR("fe_web").ToString
                objCPE.USUARIO_SOL_EMPRESA = SqlDR("usuario_sol").ToString
                objCPE.PASS_SOL_EMPRESA = scimic.Desencripta(SqlDR("pass_sol").ToString, "MARIO125")
                objCPE.CONTRA_FIRMA = scimic.Desencripta(SqlDR("contra_firma"), "MARIO125")
                objCPE.TIPO_PROCESO = 1 '1=PRODUCCION, 2=HOMOLOGACION, 3=BETA 
            End While
            '_______________________________DATOS DEL DOCUMENTO  A EMITIR_____________________________
            cmd2.Connection = Form1.Connection
            cmd2.CommandType = CommandType.Text
            Sql = "SELECT public.fe_documento.id, public.fe_documento.numero AS serie, public.fe_documento.fecha, public.emp_empresa.ruc, public.emp_empresa.empresa, public.emp_empresa.direccion, public.emp_empresa.provincia, public.cja_documento.listo, public.cja_documento.monto_me, public.cja_documento.igv, public.cja_documento.prod_id, public.fe_tipo_doc.codigo AS tipo_comp, public.cja_moneda.abrev AS moneda, public.emp_tipo_doc.codigo AS tipo_doc, public.cja_documento.obs, public.emp_entidad.nombre AS vendedor, public.cja_moneda.obs AS mon_des, public.cja_documento.monto_exonerado FROM public.fe_documento RIGHT OUTER JOIN public.cja_documento ON(public.fe_documento.venta = public.cja_documento.id) LEFT OUTER JOIN public.emp_empresa ON (public.cja_documento.empresa = public.emp_empresa.id) LEFT OUTER JOIN public.fe_serie ON (public.fe_documento.de_serie = public.fe_serie.id) LEFT OUTER JOIN public.fe_tipo_doc ON (public.fe_serie.tipo_doc = public.fe_tipo_doc.id) LEFT OUTER JOIN public.cja_moneda ON (public.cja_documento.moneda = public.cja_moneda.id) LEFT OUTER JOIN public.emp_tipo_doc ON (public.emp_empresa.tipo_doc = public.emp_tipo_doc.id) LEFT OUTER JOIN public.emp_entidad ON (public.fe_documento.usuario = public.emp_entidad.id) WHERE fe_documento.anulado = false AND cja_documento.id =" & id
            cmd2.CommandText = Sql
            SqlDR2 = cmd2.ExecuteReader()
            While SqlDR2.Read()
                'validamos campos 
                If SqlDR2("tipo_comp").ToString = "01" Then
                    objCPE.NRO_DOCUMENTO_CLIENTE = Val_ruc(SqlDR2("ruc").ToString, SqlDR2("id")) 'CORRECTO
                End If
                If SqlDR2("tipo_comp").ToString = "03" Then
                    objCPE.NRO_DOCUMENTO_CLIENTE = Val_dni(SqlDR2("ruc").ToString, SqlDR2("id")) 'CORRECTO
                End If
                If SqlDR2("ruc").ToString = "00000000" Or (SqlDR2("ruc").ToString).Count = 8 Then ' SI ES DNI= 00000000 NO ES UTIL NOMBRE NI DIRECCION
                    Dim DA33 = Val_data(SqlDR2("serie").ToString, SqlDR2("id"), "SERIE")
                    Dim DA23 = Val_data(SqlDR2("fecha").ToString, SqlDR2("id"), "FECHA")
                    Dim DA39 = Val_data(SqlDR2("ruc").ToString, SqlDR2("id"), "RUC CLIENTE")
                    Dim DA63 = Val_data(SqlDR2("monto_me").ToString, SqlDR2("id"), "MONTO TOTAL")
                    Dim DA73 = Val_data(SqlDR2("igv").ToString, SqlDR2("id"), "IGV")
                    Dim DA83 = Val_data(SqlDR2("prod_id").ToString, SqlDR2("id"), "PRODUCTO")
                    Dim DA93 = Val_data(SqlDR2("tipo_comp").ToString, SqlDR2("id"), "TIPO DE COMPROBANTE")
                    Dim DA130 = Val_data(SqlDR2("moneda").ToString, SqlDR2("id"), "MONEDA")
                    ' Dim DA131 = Val_data(SqlDR2("tipo_doc").ToString, SqlDR2("id"), "TIPO DE DOCUMENTO")
                Else
                    Dim DA = Val_data(SqlDR2("serie").ToString, SqlDR2("id"), "SERIE")
                    Dim DA2 = Val_data(SqlDR2("fecha").ToString, SqlDR2("id"), "FECHA")
                    Dim DA3 = Val_data(SqlDR2("ruc").ToString, SqlDR2("id"), "RUC CLIENTE")
                    Dim DA4 = Val_data(SqlDR2("empresa").ToString, SqlDR2("id"), "NOMBRE EMPRESA")
                    Dim DA5 = Val_data(SqlDR2("direccion").ToString, SqlDR2("id"), "DIRECCION DE EMPRESA")
                    Dim DA6 = Val_data(SqlDR2("monto_me").ToString, SqlDR2("id"), "MONTO TOTAL")
                    Dim DA7 = Val_data(SqlDR2("igv").ToString, SqlDR2("id"), "IGV")
                    Dim DA8 = Val_data(SqlDR2("prod_id").ToString, SqlDR2("id"), "PRODUCTO")
                    Dim DA9 = Val_data(SqlDR2("tipo_comp").ToString, SqlDR2("id"), "TIPO DE COMPROBANTE")
                    Dim DA10 = Val_data(SqlDR2("moneda").ToString, SqlDR2("id"), "MONEDA")
                    'Dim DA11 = Val_data(SqlDR2("tipo_doc").ToString, SqlDR2("id"), "TIPO DE DOCUMENTO")
                End If
                objCPE.TIPO_OPERACION = ""
                objCPE.TOTAL_GRAVADAS = Format(Math.Round(SqlDR2("monto_me").ToString - 0, 2), "#0.00")  ' CORRECTO
                objCPE.SUB_TOTAL = Format(Math.Round(SqlDR2("monto_me").ToString - SqlDR2("igv").ToString, 2), "#0.00") 'CORRECTO
                objCPE.TOTAL_IGV = SqlDR2("igv").ToString  'CORRECTO
                objCPE.TOTAL_ISC = 0
                objCPE.TOTAL_OTR_IMP = 0
                objCPE.TOTAL = SqlDR2("monto_me").ToString  'CORRECTO 
                objCPE.TOTAL_LETRAS = Letras(SqlDR2("monto_me").ToString, SqlDR2("mon_des").ToString) 'CORRECTO
                objCPE.NRO_GUIA_REMISION = ""
                objCPE.COD_GUIA_REMISION = ""
                objCPE.NRO_OTR_COMPROBANTE = ""
                objCPE.OBS_DOC = SqlDR2("obs").ToString 'CORRECTO
                objCPE.NRO_COMPROBANTE = SqlDR2("serie").ToString 'CORRECTO
                objCPE.FECHA_DOCUMENTO = Format(CDate(SqlDR2("fecha").ToString), "yyy-MM-dd") 'CORRECTO
                objCPE.COD_TIPO_DOCUMENTO = SqlDR2("tipo_comp").ToString 'CORRECTO 01=FACTURA, 03=BOLETA, 07=NOTA CREDITO, 08=NOTA DEBITO
                objCPE.COD_MONEDA = SqlDR2("moneda").ToString ' CORRECTO
                objCPE.PLACA_VEHICULO = ""
                '========================DATOS DEL CIENTE==========================
                objCPE.RAZON_SOCIAL_CLIENTE = SqlDR2("empresa").ToString 'CORRECTO
                If SqlDR2("tipo_doc").ToString = "" Then
                    Select Case Len(SqlDR2("ruc").ToString)
                        Case 8
                            objCPE.TIPO_DOCUMENTO_CLIENTE = "1"
                        Case 11
                            objCPE.TIPO_DOCUMENTO_CLIENTE = "6"
                        Case Else
                            Dim DA11 = Val_data(SqlDR2("tipo_doc").ToString, SqlDR2("id"), "TIPO DE DOCUMENTO")

                    End Select
                Else
                    objCPE.TIPO_DOCUMENTO_CLIENTE = SqlDR2("tipo_doc").ToString 'CORRECTO   '1=DNI,6=RUC
                End If


                objCPE.DIRECCION_CLIENTE = SqlDR2("direccion").ToString 'CORRECTO
                If SqlDR2("provincia").ToString <> "" Then
                    objCPE.CIUDAD_CLIENTE = SqlDR2("provincia").ToString '
                Else
                    objCPE.CIUDAD_CLIENTE = "LIMA"
                End If
                objCPE.COD_PAIS_CLIENTE = "PE"
                ''=============================DATOS DE ITEMS  EN VENTA ===========================
                Dim OBJCPE_DETALLE_LIST As New List(Of BusinessEntities.CPE_DETALLE)
                cmd3.Connection = Form1.Connection
                cmd3.CommandType = CommandType.Text
                Dim id2 As Integer
                id2 = SqlDR2("prod_id")
                Sql = "Select stk_traslado_itm.cantidad, stk_traslado_itm.precio, stk_traslado_itm.total, stk_unidad.codigo As unidad, stk_producto.codigo, stk_producto.producto, stk_producto.exonerada, stk_traslado_itm.exonerado FROM stk_traslado_itm LEFT OUTER JOIN stk_unidad On(stk_traslado_itm.medida = stk_unidad.id) LEFT OUTER JOIN stk_producto On (stk_traslado_itm.producto = stk_producto.id) WHERE stk_traslado_itm.cantidad > 0 And stk_traslado_itm.traslado =" & id2
                cmd3.CommandText = Sql
                SqlDR3 = cmd3.ExecuteReader()
                Z = 1
                Dim suma_total As Integer
                suma_total = 0
                While SqlDR3.Read()
                    objCPE_DETALLE = New BusinessEntities.CPE_DETALLE
                    If SqlDR3("exonerado") = True Then
                        objCPE_DETALLE.COD_TIPO_OPERACION = "20"
                    Else
                        objCPE_DETALLE.COD_TIPO_OPERACION = "10"
                    End If
                    Dim TOTAL_X_CANTIDAD = Format(SqlDR3("precio") * SqlDR3("cantidad"), "#0.00 ")
                    Dim IGV2 = Format(TOTAL_X_CANTIDAD - TOTAL_X_CANTIDAD / 1.18, "#0.00 ")
                    Dim PRECIO_X_UNIDAD = Format(TOTAL_X_CANTIDAD / SqlDR3("cantidad"), "#0.00 ")
                    Dim PRECIO_SN_IGV_ = Format((TOTAL_X_CANTIDAD - IGV2), "#0.00 ")
                    objCPE_DETALLE.ITEM = Z
                    objCPE.ITEMS = Z
                    objCPE_DETALLE.UNIDAD_MEDIDA = SqlDR3("unidad").ToString
                    objCPE_DETALLE.CANTIDAD = SqlDR3("cantidad").ToString
                    objCPE_DETALLE.PRECIO = SqlDR3("precio").ToString
                    objCPE_DETALLE.PRECIO_SIN_IMPUESTO = PRECIO_SN_IGV_
                    objCPE_DETALLE.IMPORTE = Format(SqlDR3("cantidad").ToString * (SqlDR3("precio").ToString), "#0.00") 'CON IGV
                    objCPE_DETALLE.SUB_TOTAL = Format(objCPE_DETALLE.IMPORTE / 1.18, "#0.00") 'SIN IGV
                    objCPE_DETALLE.PRECIO_TIPO_CODIGO = "01"
                    objCPE_DETALLE.IGV = Format(objCPE_DETALLE.IMPORTE - objCPE_DETALLE.IMPORTE / 1.18, "#0.00")
                    objCPE_DETALLE.ISC = 0
                    objCPE_DETALLE.CODIGO = SqlDR3("codigo").ToString
                    objCPE_DETALLE.DESCRIPCION = SqlDR3("producto").ToString
                    OBJCPE_DETALLE_LIST.Add(objCPE_DETALLE)
                    Z = Z + 1
                End While
                objCPE.detalle = OBJCPE_DETALLE_LIST
                '======================================RESPUESTA====================================
                Dim dictionaryEnv As New Dictionary(Of String, String)
                dictionaryEnv = obj.Envio(objCPE)

                If dictionaryEnv.Item("TIKET") <> "ERROR DE CONEXION" Then
                    Sql = "UPDATE fe_documento SET(cod_hash,cod_sunat,msg_sunat,emitir) = ('" & dictionaryEnv.Item("HASH") & "','" & dictionaryEnv.Item("HASH") & "','" & dictionaryEnv.Item("TIKET") & "',TRUE) WHERE id=" & SqlDR2("id")
                    RunSQL(Sql)
                    Form1.Notificacion(10, "INFORMACIÓN", "EL DOC. " & objCPE.NRO_COMPROBANTE & " A SIDO ACEPTADA.", ToolTipIcon.Info)
                Else
                    Form1.Notificacion(10, "ERROR".ToString, "DOC. NO ENVIADO.", ToolTipIcon.Error)
                End If
                SqlDR3.Close()
            End While
            SqlDR.Close()
            SqlDR2.Close()
            cmd.Dispose()
            Form1.Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "error")
        End Try
    End Sub
    Public Sub RunSQL(ByVal Sql As String)
        Dim cmd As New OdbcCommand

        Try
            cmd.Connection = Form1.Connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = Sql
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox("Hubo un error" & ex.Message)
        End Try
    End Sub
    Public Function Letras(ByVal numero As String, ByVal tipo As String) As String
        '********Declara variables de tipo cadena************
        Dim palabras = "", entero, dec, flag As String
        entero = ""
        dec = ""
        '********Declara variables de tipo entero***********
        Dim num, x, y As Integer

        flag = "N"

        '**********Número Negativo***********
        If Mid(numero, 1, 1) = "-" Then
            numero = Mid(numero, 2, numero.ToString.Length - 1).ToString
            palabras = "menos "
        End If

        '**********Si tiene ceros a la izquierda*************
        For x = 1 To numero.ToString.Length
            If Mid(numero, 1, 1) = "0" Then
                numero = Trim(Mid(numero, 2, numero.ToString.Length).ToString)
                If Trim(numero.ToString.Length) = 0 Then palabras = ""
            Else
                Exit For
            End If
        Next

        '*********Dividir parte entera y decimal************
        For y = 1 To Len(numero)
            If Mid(numero, y, 1) = "." Then
                flag = "S"
            Else
                If flag = "N" Then
                    entero = entero + Mid(numero, y, 1)
                Else
                    dec = dec + Mid(numero, y, 1)
                End If
            End If
        Next y

        If Len(dec) = 1 Then dec = dec & "0"

        '**********proceso de conversión***********
        flag = "N"

        If Val(numero) <= 999999999 Then
            For y = Len(entero) To 1 Step -1
                num = Len(entero) - (y - 1)
                Select Case y
                    Case 3, 6, 9
                        '**********Asigna las palabras para las centenas***********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" And Mid(entero, num + 2, 1) = "0" Then
                                    palabras = palabras & "cien "
                                Else
                                    palabras = palabras & "ciento "
                                End If
                            Case "2"
                                palabras = palabras & "doscientos "
                            Case "3"
                                palabras = palabras & "trescientos "
                            Case "4"
                                palabras = palabras & "cuatrocientos "
                            Case "5"
                                palabras = palabras & "quinientos "
                            Case "6"
                                palabras = palabras & "seiscientos "
                            Case "7"
                                palabras = palabras & "setecientos "
                            Case "8"
                                palabras = palabras & "ochocientos "
                            Case "9"
                                palabras = palabras & "novecientos "
                        End Select
                    Case 2, 5, 8
                        '*********Asigna las palabras para las decenas************
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    flag = "S"
                                    palabras = palabras & "diez "
                                End If
                                If Mid(entero, num + 1, 1) = "1" Then
                                    flag = "S"
                                    palabras = palabras & "once "
                                End If
                                If Mid(entero, num + 1, 1) = "2" Then
                                    flag = "S"
                                    palabras = palabras & "doce "
                                End If
                                If Mid(entero, num + 1, 1) = "3" Then
                                    flag = "S"
                                    palabras = palabras & "trece "
                                End If
                                If Mid(entero, num + 1, 1) = "4" Then
                                    flag = "S"
                                    palabras = palabras & "catorce "
                                End If
                                If Mid(entero, num + 1, 1) = "5" Then
                                    flag = "S"
                                    palabras = palabras & "quince "
                                End If
                                If Mid(entero, num + 1, 1) > "5" Then
                                    flag = "N"
                                    palabras = palabras & "dieci"
                                End If
                            Case "2"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "veinte "
                                    flag = "S"
                                Else
                                    palabras = palabras & "veinti"
                                    flag = "N"
                                End If
                            Case "3"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "treinta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "treinta y "
                                    flag = "N"
                                End If
                            Case "4"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cuarenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cuarenta y "
                                    flag = "N"
                                End If
                            Case "5"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "cincuenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "cincuenta y "
                                    flag = "N"
                                End If
                            Case "6"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "sesenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "sesenta y "
                                    flag = "N"
                                End If
                            Case "7"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "setenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "setenta y "
                                    flag = "N"
                                End If
                            Case "8"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "ochenta "
                                    flag = "S"
                                Else
                                    palabras = palabras & "ochenta y "
                                    flag = "N"
                                End If
                            Case "9"
                                If Mid(entero, num + 1, 1) = "0" Then
                                    palabras = palabras & "noventa "
                                    flag = "S"
                                Else
                                    palabras = palabras & "noventa y "
                                    flag = "N"
                                End If
                        End Select
                    Case 1, 4, 7
                        '*********Asigna las palabras para las unidades*********
                        Select Case Mid(entero, num, 1)
                            Case "1"
                                If flag = "N" Then
                                    If y = 1 Then
                                        palabras = palabras & "uno "
                                    Else
                                        palabras = palabras & "un "
                                    End If
                                End If
                            Case "2"
                                If flag = "N" Then palabras = palabras & "dos "
                            Case "3"
                                If flag = "N" Then palabras = palabras & "tres "
                            Case "4"
                                If flag = "N" Then palabras = palabras & "cuatro "
                            Case "5"
                                If flag = "N" Then palabras = palabras & "cinco "
                            Case "6"
                                If flag = "N" Then palabras = palabras & "seis "
                            Case "7"
                                If flag = "N" Then palabras = palabras & "siete "
                            Case "8"
                                If flag = "N" Then palabras = palabras & "ocho "
                            Case "9"
                                If flag = "N" Then palabras = palabras & "nueve "
                        End Select
                End Select

                '***********Asigna la palabra mil***************
                If y = 4 Then
                    If Mid(entero, 6, 1) <> "0" Or Mid(entero, 5, 1) <> "0" Or Mid(entero, 4, 1) <> "0" Or
                    (Mid(entero, 6, 1) = "0" And Mid(entero, 5, 1) = "0" And Mid(entero, 4, 1) = "0" And
                    Len(entero) <= 6) Then palabras = palabras & "mil "
                End If

                '**********Asigna la palabra millón*************
                If y = 7 Then
                    If Len(entero) = 7 And Mid(entero, 1, 1) = "1" Then
                        palabras = palabras & "millón "
                    Else
                        palabras = palabras & "millones "
                    End If
                End If
            Next y

            '**********Une la parte entera y la parte decimal*************
            If dec <> "" Then

                Letras = palabras & "con " & dec & "/100 " & tipo


            Else

                Letras = palabras
            End If
        Else
            Letras = ""
        End If
    End Function
    Public Sub Resumen_Diario_Boletas(id As Integer, proceso As Integer)
        Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String, Z As Integer = 0
        Dim cmd2 As New OdbcCommand, Sql2 As String = "", id_anula As String = ""
        Dim OBJCPE_DETALLE_LIST As New List(Of BusinessEntities.CPE_RESUMEN_BOLETA_DETALLE)
        cmd.Connection = Form1.Connection
        cmd.CommandType = CommandType.Text
        Sql = "SELECT emp_empresa1.firma, emp_empresa1.ruc, emp_empresa1.empresa, emp_tipo_doc1.codigo, emp_empresa1.usuario_sol, emp_empresa1.pass_sol, emp_empresa1.contra_firma, public.fe_anula.serie, public.fe_anula.correlativo, public.fe_anula.fecha_ref, public.fe_anula.fecha, public.fe_tipo_doc.codigo AS tipo_comp, public.fe_documento.numero, public.emp_tipo_doc.codigo AS tipo_doc, public.emp_empresa.ruc AS doc_cli, public.cja_moneda.abrev, public.cja_documento.monto_me, public.cja_documento.igv, public.cja_documento.monto_exonerado FROM public.fe_anula_itm LEFT OUTER JOIN public.fe_anula ON(public.fe_anula_itm.anula = public.fe_anula.id) RIGHT OUTER JOIN public.fe_documento ON (public.fe_anula_itm.documento = public.fe_documento.id) RIGHT OUTER JOIN public.cja_documento ON (public.fe_documento.venta = public.cja_documento.id) LEFT OUTER JOIN public.emp_empresa ON (public.cja_documento.empresa = public.emp_empresa.id) LEFT OUTER JOIN public.emp_tipo_doc ON (public.emp_empresa.tipo_doc = public.emp_tipo_doc.id) LEFT OUTER JOIN public.emp_empresa emp_empresa1 ON (public.cja_documento.emp_ppal = emp_empresa1.id) FULL OUTER JOIN public.emp_tipo_doc emp_tipo_doc1 ON (emp_empresa1.tipo_doc = emp_tipo_doc1.id) LEFT OUTER JOIN public.fe_tipo_doc ON (public.cja_documento.fe_doc = public.fe_tipo_doc.id) LEFT OUTER JOIN public.cja_moneda ON (public.cja_documento.moneda = public.cja_moneda.id) WHERE fe_anula.id =  " & id
        cmd.CommandText = Sql
        SqlDR = cmd.ExecuteReader()
        While SqlDR.Read()
            If SqlDR("firma").ToString <> "FIRMABETA.pfx" Then 'prduccion
                objCPE2.FIRMA_EMP = SqlDR("firma").ToString 'pfx
            Else ' beta
                objCPE2.FIRMA_EMP = "beta" 'pfx
            End If
            objCPE2.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString
            objCPE2.RAZON_SOCIAL = SqlDR("empresa").ToString
            objCPE2.TIPO_DOCUMENTO = SqlDR("codigo").ToString '6 ruc 1 dni
            objCPE2.USUARIO_SOL_EMPRESA = SqlDR("usuario_sol").ToString
            objCPE2.PASS_SOL_EMPRESA = scimic.Desencripta(SqlDR("pass_sol").ToString, "MARIO125")
            objCPE2.CONTRA_FIRMA = scimic.Desencripta(SqlDR("contra_firma"), "MARIO125")
            objCPE2.CODIGO = "RC"
            objCPE2.SERIE = SqlDR("serie").ToString.Replace("RC-", "") '"20161029" '-------
            objCPE2.SECUENCIA = SqlDR("correlativo").ToString ' "1"
            objCPE2.FECHA_REFERENCIA = Format(CDate(SqlDR("fecha_ref").ToString), "yyy-MM-dd") '"2016-10-28" '-------
            objCPE2.FECHA_DOCUMENTO = Format(CDate(SqlDR("fecha").ToString), "yyy-MM-dd") '"2016-10-29" '-------
            objCPE2.TIPO_PROCESO = "1"
            Z = Z + 1
            objCPE_DETALLE2 = New BusinessEntities.CPE_RESUMEN_BOLETA_DETALLE
            objCPE_DETALLE2.ITEM = Z
            objCPE_DETALLE2.TIPO_COMPROBANTE = "03" 'SqlDR("tipo_comp").ToString '"03" '03BOLETA 07N.CREDITO 08N.DEBITO
            objCPE_DETALLE2.NRO_COMPROBANTE = SqlDR("numero").ToString '"B001-14"
            objCPE_DETALLE2.TIPO_DOCUMENTO = SqlDR("tipo_doc").ToString ' "1" ' 1dni 6ruc
            objCPE_DETALLE2.NRO_DOCUMENTO = SqlDR("doc_cli").ToString '"44791512" 'DNI
            objCPE_DETALLE2.TIPO_COMPROBANTE_REF = "" '03 boleta
            objCPE_DETALLE2.NRO_COMPROBANTE_REF = "" 'b001-11
            If proceso = 0 Then
                objCPE_DETALLE2.STATU = 1  '1= envioDECLARAR --------------------------
            Else
                objCPE_DETALLE2.STATU = 3  '3 = anulacion--------------------------
            End If
            objCPE_DETALLE2.COD_MONEDA = SqlDR("abrev").ToString '"PEN"
            objCPE_DETALLE2.TOTAL = Format(Val(CDec(SqlDR("monto_me").ToString)), "#0.00") ' 1693.39
            objCPE_DETALLE2.GRAVADA = Format(Val(CDec(SqlDR("monto_me") - SqlDR("igv"))), "#0.00") 'SqlDR("monto_me") - SqlDR("igv")
            objCPE_DETALLE2.ISC = 0
            objCPE_DETALLE2.IGV = SqlDR("igv").ToString
            objCPE_DETALLE2.OTROS = 0
            objCPE_DETALLE2.CARGO_X_ASIGNACION = 1
            objCPE_DETALLE2.MONTO_CARGO_X_ASIG = 0
            objCPE_DETALLE2.EXONERADO = 0
            objCPE_DETALLE2.INAFECTO = 0
            objCPE_DETALLE2.EXPORTACION = 0
            objCPE_DETALLE2.GRATUITAS = 0
            OBJCPE_DETALLE_LIST.Add(objCPE_DETALLE2)
        End While
        objCPE2.detalle = OBJCPE_DETALLE_LIST
        '======================================RESPUESTA====================================
        Dim dictionaryEnv As New Dictionary(Of String, String)
        dictionaryEnv = obj.envioResumen(objCPE2)
        If dictionaryEnv.Item("cod_sunat") = "x" Then
            Form1.Notificacion(10, "INFORMACIÓN", "SUNAT NO RESPONDE VUELVA A  ENVIAR..", ToolTipIcon.Error)
        Else
            Dim mesajito_sunat = dictionaryEnv.Item("msj_sunat").Replace("'", " ").Replace(":", " ").Replace("='", " ").Replace("""", " ").Replace("http //xxx.xxx.xxx/ol-ti-itcpfegem-beta/billService", "PUEDE QUE SU DOCUMENTO NO TENGA ITEMS").Replace("{urn oasis names specification ubl schema xsd Invoice-2}", "").Replace("(in namespace urn oasis names specification ubl schema xsd Invoice-2), but next item should be {urn oasis names specification ubl schema xsd CommonAggregateComponents-2}InvoiceLine", "")
            If dictionaryEnv.Item("cod_sunat") = "1" Then
                Sql = "update fe_anula set(cod_sunat,tck_consulta, cod_hash,msg_sunat)=('" + dictionaryEnv.Item("cod_sunat") + "','" + dictionaryEnv.Item("msj_sunat") + "','" + dictionaryEnv.Item("hash_cpe") + "','" + dictionaryEnv.Item("mensaje") + "') where id =" & id
                RunSQL(Sql)
                Form1.Notificacion(10, "INFORMACIÓN", dictionaryEnv.Item("mensaje"), ToolTipIcon.Info)
                End
            Else
                Sql = "update fe_anula set(cod_sunat,tck_consulta, cod_hash,msg_sunat)=('" + dictionaryEnv.Item("cod_sunat") + "','" + dictionaryEnv.Item("msj_sunat") + "','" + dictionaryEnv.Item("hash_cpe") + "','" + dictionaryEnv.Item("mensaje") + "') where id =" & id
                RunSQL(Sql)
                Form1.Notificacion(10, dictionaryEnv.Item("cod_sunat").ToString, mesajito_sunat.ToString, ToolTipIcon.Error)
                End
            End If
        End If
    End Sub
    Public Sub Anular_Factura(id As Integer)

        Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String, Z As Integer = 0
        Dim cmd2 As New OdbcCommand, SqlDR2 As OdbcDataReader, Sql2 As String = "", id_anula As String = ""
        cmd.Connection = Form1.Connection
        cmd.CommandType = CommandType.Text
        Sql = "SELECT emp_empresa.ruc, emp_empresa.telefono, emp_empresa.empresa, emp_empresa.ubigeo, emp_empresa.direccion, emp_empresa.departamento, emp_empresa.provincia, emp_empresa.distrito, emp_empresa.nombrec, emp_empresa.usuario_sol, emp_empresa.pass_sol, emp_empresa.contra_firma, emp_empresa.logo, emp_empresa.firma, emp_empresa.fe_web, public.emp_tipo_doc.codigo AS tipo_doc FROM emp_empresa FULL OUTER JOIN public.emp_tipo_doc ON(emp_empresa.tipo_doc = public.emp_tipo_doc.id) WHERE emp_ppal = True "
        cmd.CommandText = Sql
        SqlDR = cmd.ExecuteReader()
        While SqlDR.Read()
            If SqlDR("firma").ToString <> "FIRMABETA.pfx" Then 'prduccion
                objCPE_BAJA.FIRMA_EMP = SqlDR("firma").ToString 'pfx
            Else ' beta
                objCPE_BAJA.FIRMA_EMP = "beta" 'pfx
            End If
            objCPE_BAJA.UBIGEO = SqlDR("ubigeo").ToString
            objCPE_BAJA.DEPARTAMENTO = SqlDR("departamento").ToString
            objCPE_BAJA.PROVINCIA = SqlDR("provincia").ToString
            objCPE_BAJA.DISTRITO = SqlDR("distrito").ToString
            objCPE_BAJA.DIRECCION = SqlDR("direccion").ToString
            objCPE_BAJA.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString ' "10447915125"
            objCPE_BAJA.RAZON_SOCIAL = SqlDR("empresa").ToString ' "CREVPERU S.A."
            objCPE_BAJA.TIPO_DOCUMENTO = SqlDR("tipo_doc").ToString '"6"
            objCPE_BAJA.CODIGO = "RA"
            objCPE_BAJA.USUARIO_SOL_EMPRESA = SqlDR("usuario_sol") ' "MODDATOS"
            objCPE_BAJA.PASS_SOL_EMPRESA = scimic.Desencripta(SqlDR("pass_sol").ToString, "MARIO125") '"moddatos"
            objCPE_BAJA.CONTRA_FIRMA = scimic.Desencripta(SqlDR("contra_firma"), "MARIO125") '"123456"
            objCPE_BAJA.TIPO_PROCESO = 1
        End While
        Dim OBJCPE_DETALLE_LIST As New List(Of BusinessEntities.CPE_BAJA_DETALLE)
        cmd2.Connection = Form1.Connection
        cmd2.CommandType = CommandType.Text
        Sql = "SELECT public.fe_anula.serie, public.fe_anula.correlativo, public.fe_anula.fecha_ref, public.fe_anula.fecha, public.fe_tipo_doc.codigo, public.fe_documento.de_num, public.fe_anula_itm.motivo, public.fe_serie.serie AS serie_doc, public.fe_anula.id FROM public.fe_anula RIGHT OUTER JOIN public.fe_anula_itm ON(public.fe_anula.id = public.fe_anula_itm.anula) INNER JOIN public.fe_documento ON(public.fe_anula_itm.documento = public.fe_documento.id) INNER JOIN public.fe_serie ON (public.fe_documento.de_serie = public.fe_serie.id) INNER JOIN public.fe_tipo_doc ON (public.fe_serie.tipo_doc = public.fe_tipo_doc.id) WHERE fe_anula.id =" & id
        cmd2.CommandText = Sql
        SqlDR2 = cmd2.ExecuteReader()
        While SqlDR2.Read()
         objCPE_BAJA_DETALLE = New BusinessEntities.CPE_BAJA_DETALLE
            Z = Z + 1
            id_anula = SqlDR2("id").ToString
            objCPE_BAJA.SERIE = SqlDR2("serie").ToString & "-" & SqlDR2("correlativo").ToString '"20161029"
            objCPE_BAJA.SECUENCIA = SqlDR2("correlativo").ToString 'secuencia.Text por dia 1 ..
            objCPE_BAJA.FECHA_REFERENCIA = Format(CDate(SqlDR2("fecha_ref").ToString), "yyy-MM-dd") ' Format(SqlDR2("fecha_ref").ToString, "yyyy-MM-dd") ' "yyy-MM-dd"
            objCPE_BAJA.FECHA_BAJA = Format(CDate(SqlDR2("fecha").ToString), "yyy-MM-dd") 'Format(SqlDR2("fecha"), "yyyy-MM-dd") ' "yyyy-MM-dd" '
            objCPE_BAJA_DETALLE.ITEM = Z
            objCPE_BAJA_DETALLE.TIPO_COMPROBANTE = SqlDR2("codigo").ToString ' "01"
            objCPE_BAJA_DETALLE.SERIE = SqlDR2("serie_doc").ToString  '"FF11"
            objCPE_BAJA_DETALLE.NUMERO = SqlDR2("de_num").ToString ' "750"
            objCPE_BAJA_DETALLE.DESCRIPCION = SqlDR2("motivo").ToString
            If objCPE_BAJA_DETALLE.DESCRIPCION = "" Then
                Form1.Notificacion(70, "ERROR", "EL DETALLE DE ANULACION, ES NECESARIO..", ToolTipIcon.Error)
                End
            End If

            OBJCPE_DETALLE_LIST.Add(objCPE_BAJA_DETALLE)
        End While

        objCPE_BAJA.detalle = OBJCPE_DETALLE_LIST
        '======================================RESPUESTA====================================
        Dim dictionaryEnv As New Dictionary(Of String, String)
        dictionaryEnv = obj.envioBaja(objCPE_BAJA)

        If dictionaryEnv.Item("TIKET") <> "ERROR DE CONEXION" Then
            Sql = "update fe_anula set(cod_sunat,tck_consulta, cod_hash,msg_sunat)=('" + dictionaryEnv.Item("HASH") + "','" + dictionaryEnv.Item("TIKET") + "','" + dictionaryEnv.Item("HASH") + "','" + dictionaryEnv.Item("TIKET") + "') where id =" & id_anula
            RunSQL(Sql)
            Form1.Notificacion(10, "INFORMACIÓN", "EL DOC. " & objCPE.NRO_COMPROBANTE & " A SIDO ACEPTADA.", ToolTipIcon.Info)
        Else
            Form1.Notificacion(10, "ERROR".ToString, "DOC. NO ENVIADO.", ToolTipIcon.Error)
        End If



        'If dictionaryEnv.Item("cod_sunat") = "x" Then
        '    Form1.Notificacion(10, "INFORMACIÓN", "SUNAT NO RESPONDE VUELVA A  ENVIAR..", ToolTipIcon.Error)
        'Else
        '    Dim mesajito_sunat = dictionaryEnv.Item("msj_sunat").Replace("'", " ").Replace(":", " ").Replace("='", " ").Replace("""", " ").Replace("http //xxx.xxx.xxx/ol-ti-itcpfegem-beta/billService", "PUEDE QUE SU DOCUMENTO NO TENGA ITEMS").Replace("{urn oasis names specification ubl schema xsd Invoice-2}", "").Replace("(in namespace urn oasis names specification ubl schema xsd Invoice-2), but next item should be {urn oasis names specification ubl schema xsd CommonAggregateComponents-2}InvoiceLine", "")
        '    If dictionaryEnv.Item("cod_sunat") = "1" Then
        '        Sql = "update fe_anula set(cod_sunat,tck_consulta, cod_hash,msg_sunat)=('" + dictionaryEnv.Item("cod_sunat") + "','" + dictionaryEnv.Item("msj_sunat") + "','" + dictionaryEnv.Item("hash_cpe") + "','" + dictionaryEnv.Item("mensaje") + "') where id =" & id_anula
        '        RunSQL(Sql)
        '        Fovrm1.Notificacion(10, "INFORMACIÓN", dictionaryEnv.Item("mensaje"), ToolTipIcon.Info)
        '        End
        '    Else
        '        Sql = "update fe_anula set(cod_sunat,tck_consulta, cod_hash,msg_sunat)=('" + dictionaryEnv.Item("cod_sunat") + "','" + dictionaryEnv.Item("msj_sunat") + "','" + dictionaryEnv.Item("hash_cpe") + "','" + dictionaryEnv.Item("mensaje") + "') where id =" & id_anula
        '        RunSQL(Sql)
        '        Form1.Notificacion(10, dictionaryEnv.Item("cod_sunat").ToString, mesajito_sunat.ToString, ToolTipIcon.Error)
        '        End
        '    End If
        'End If
    End Sub
    Public Sub Consultar_Ticket(id As Integer)
        Dim objCPETICKET As New BE.CONSULTA_TICKET
        Dim cmd As New OdbcCommand, SqlDR As OdbcDataReader, Sql As String
        cmd.Connection = Form1.Connection
        cmd.CommandType = CommandType.Text
        Sql = "SELECT DISTINCT public.fe_anula.tck_consulta, public.fe_anula.numero, public.emp_empresa.ruc, public.emp_empresa.usuario_sol, public.emp_empresa.pass_sol,public.fe_anula.id FROM public.fe_anula INNER JOIN public.fe_anula_itm ON(public.fe_anula.id = public.fe_anula_itm.anula) INNER JOIN public.fe_documento ON (public.fe_anula_itm.documento = public.fe_documento.id) INNER JOIN public.cja_documento ON (public.fe_documento.venta = public.cja_documento.id) INNER JOIN public.emp_empresa ON (public.cja_documento.emp_ppal = public.emp_empresa.id) WHERE emp_empresa.emp_ppal is true and fe_anula.id =" & id
        cmd.CommandText = Sql
        SqlDR = cmd.ExecuteReader()
        Dim id_anula As Integer = 0
        While SqlDR.Read()
            objCPETICKET.TIPO_PROCESO = 1
            id_anula = SqlDR("id")
            objCPETICKET.NRO_DOCUMENTO_EMPRESA = SqlDR("ruc").ToString '"20505161051" ' "10447915125"
            objCPETICKET.USUARIO_SOL_EMPRESA = SqlDR("usuario_sol") '"ANDREA01" ' "MODDATOS"
            objCPETICKET.PASS_SOL_EMPRESA = scimic.Desencripta(SqlDR("pass_sol"), "MARIO125") '"moddatos"
            objCPETICKET.TICKET = SqlDR("tck_consulta").ToString
            ' OPTENER TIPO DOC
            objCPETICKET.TIPO_DOCUMENTO = Tipo_RA_RC(SqlDR("numero").ToString)  ' "RA"  ' RA = FACTURA  RC = BOLETAS
            objCPETICKET.NRO_DOCUMENTO = SqlDR("numero").ToString.Replace("RA-", "").Replace("RC-", "") '"20161029-1"
        End While
        '======================================RESPUESTA====================================
        Dim dictionaryEnv As New Dictionary(Of String, String)
        dictionaryEnv = obj.consultaTicket(objCPETICKET)
        Sql = "update fe_anula set(msg_sunat)=('" + dictionaryEnv.Item("msj_sunat").ToString + "') where id =" & id_anula
        RunSQL(Sql)
        Form1.Notificacion(10, "INFORMACIÓN", dictionaryEnv.Item("msj_sunat"), ToolTipIcon.Info)
    End Sub
    Public Function Tipo_RA_RC(TIPO As String)
        Dim TIPO_DOC = Mid(TIPO, 1, 2)
        Dim TIPO_DE_DOC As String = ""
        If TIPO_DOC = "RA" Then ' FACTURA
            TIPO_DE_DOC = "RA"
        End If
        If TIPO_DOC = "RC" Then 'BOLETAS 
            TIPO_DE_DOC = "RC"
        End If
        Return TIPO_DE_DOC
    End Function
    Public Function Val_sum(suma As Integer, total As Integer, id As Integer) As Double

        If suma = total Then
            Return total
        Else
            Dim Sql = "UPDATE fe_documento SET(cod_hash,cod_sunat,msg_sunat) = ('ERROR','----------','LA SUMA DE LOS PRODUCTOS NO ES IGUAL AL TOTAL.') WHERE id=" & id
            RunSQL(Sql)
            Form1.Notificacion(70, "ERROR", "LA SUMA DE LOS PRODUCTOS NO ES IGUAL AL TOTAL.", ToolTipIcon.Error)
            End

        End If

    End Function
    Public Function Val_ruc(Valor As String, id As Integer) As Double

        If Valor.Count = 11 Then
            Return Valor
        Else
            Dim Sql = "UPDATE fe_documento SET(cod_hash,cod_sunat,msg_sunat) = ('ERROR','----------','EL RUC DEL CLIENTE  NO TIENE 11 DIGITOS.') WHERE id=" & id
            RunSQL(Sql)
            Form1.Notificacion(70, "ERROR", "EL RUC DEL CLIENTE  NO TIENE 11 DIGITOS.", ToolTipIcon.Error)
            End

        End If

    End Function
    Public Function Val_dni(Valor As String, id As Integer) As Double

        If Valor.Count = 8 Or Valor.Count = 11 Then
            Return Valor
        Else
            Dim Sql = "UPDATE fe_documento SET(cod_hash,cod_sunat,msg_sunat) = ('ERROR','----------','EL DNI DEL CLIENTE  NO TIENE 8 DIGITOS.') WHERE id=" & id
            RunSQL(Sql)
            Form1.Notificacion(70, "ERROR", "EL DNI DEL CLIENTE  NO TIENE 8 DIGITOS.", ToolTipIcon.Error)
            End

        End If

    End Function
    Public Function Val_ruc_emp(Valor As String, id As Integer) As Double

        If Valor.Count = 11 Then
            Return Valor
        Else
            Dim Sql = "UPDATE fe_documento SET(cod_hash,cod_sunat,msg_sunat) = ('ERROR','----------','EL RUC DE NUESTRA EMPRESA  NO TIENE 11 DIGITOS.') WHERE id=" & id
            RunSQL(Sql)
            Form1.Notificacion(70, "ERROR", "EL RUC DE NUESTRA EMPRESA  NO TIENE 11 DIGITOS.", ToolTipIcon.Error)
            End

        End If

    End Function
    Public Function Val_data(Valor As String, id As Integer, campo As String) As String

        If Valor <> "" Then
            Return Valor
        Else
            Dim Sql = "UPDATE fe_documento SET(cod_hash,cod_sunat,msg_sunat) = ('ERROR','----------','EL CAMPO " & campo & " ES NECESARIO.. ') WHERE id=" & id
            RunSQL(Sql)
            Form1.Notificacion(70, "ERROR", "EL CAMPO " + campo + " ES NECESARIO..", ToolTipIcon.Error)
            End

        End If

    End Function
End Module


