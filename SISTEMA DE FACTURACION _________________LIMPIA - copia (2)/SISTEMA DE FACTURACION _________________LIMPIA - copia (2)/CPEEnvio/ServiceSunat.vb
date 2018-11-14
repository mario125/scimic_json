Imports System.IO
Imports System.Xml
Public Class ServiceSunat

    Public Function Envio(ruc As String, usu_sol As String, contra_sol As String, nombre_archivo As String, rutaArchivo As String, url As String, hash_cpe As String) As Dictionary(Of String, String)
        Dim dictionary As Dictionary(Of String, String)
        Try
            Dim doc As New XmlDocument()
            Dim strCDR As String
            Dim strSOAP As String
            Dim rutaCompleta As String = rutaArchivo & nombre_archivo
            Comprimir(nombre_archivo, rutaArchivo)
            Dim rutaCdr As String = rutaArchivo & "R-" & nombre_archivo & ".ZIP"
            Dim NomFichierZIP As String = System.IO.Path.GetFileName(rutaCompleta & ".ZIP")
            Dim data As Byte() = System.IO.File.ReadAllBytes(rutaCompleta & ".ZIP")

            strSOAP = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' " &
                    "xmlns:ser='http://service.sunat.gob.pe' " &
                    "xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'> " &
                    "<soapenv:Header> " &
                    "<wsse:Security> " &
                    "<wsse:UsernameToken> " &
                    "<wsse:Username>" & ruc & usu_sol & "</wsse:Username> " &
                    "<wsse:Password>" & contra_sol & "</wsse:Password> " &
                    "</wsse:UsernameToken> " &
                    "</wsse:Security> " &
                    "</soapenv:Header> " &
                    "<soapenv:Body> " &
                    "<ser:sendBill> " &
                    "<fileName>" & NomFichierZIP & "</fileName> " &
                    "<contentFile>" & Convert.ToBase64String(data) & "</contentFile> " &
                    "</ser:sendBill> " &
                    "</soapenv:Body> " &
                    "</soapenv:Envelope>"

            Dim returned_value As String
            Dim strPostData As String
            Dim objRequest As Object

            strPostData = strSOAP
            'objRequest = CreateObject("MSXML2.XMLHTTP.3.0")
            objRequest = CreateObject("MSXML2.ServerXMLHTTP")
            With objRequest
                .Open("POST", url, False)
                .setRequestHeader("Content-Type", "application/xml")
                .setRequestBody()
                .send(strPostData)
                returned_value = .responseText
            End With
            doc.LoadXml(returned_value)

            '=======================validando respuesta========================
            Dim Lst As XmlNodeList = doc.SelectNodes("//faultcode")
            If Lst.Count() > 0 Then
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "0")
                dictionary.Add("mensaje", "ERROR AL ENVIAR A LA SUNAT")
                dictionary.Add("cod_sunat", doc.SelectSingleNode("//faultcode").InnerText.Replace("soap-env:Client.", ""))
                dictionary.Add("msj_sunat", doc.SelectSingleNode("//faultstring").InnerText)
                dictionary.Add("hash_cdr", "")
                dictionary.Add("hash_cpe", hash_cpe)
            Else
                strCDR = doc.SelectSingleNode("//applicationResponse").InnerText
                Dim byteCDR As Byte() = Convert.FromBase64String(strCDR)
                Dim s As IO.FileStream
                s = IO.File.Open(rutaCdr, IO.FileMode.Append)
                s.Write(byteCDR, 0, byteCDR.Length)
                s.Close()

                '===============descomprimo el xml=============
                Descomprimir(rutaArchivo, "R-" & nombre_archivo)
                '================================================================
                Dim xmlCDR As New XmlDocument()
                Dim rutaxmlCDR = rutaArchivo & "R-" & nombre_archivo & ".XML"
                xmlCDR.Load(rutaxmlCDR)

                '=======================nombre de espacios para obtener los valores del xml======================
                Dim nsmgr As New XmlNamespaceManager(doc.NameTable)
                nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
                Dim nsmgrSing As New XmlNamespaceManager(doc.NameTable)
                nsmgrSing.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")

                '========================asignamos valores de retorno======================
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "1")
                dictionary.Add("mensaje", "COMPROBANTE ENVIADO CORRECTAMENTE")
                dictionary.Add("cod_sunat", xmlCDR.SelectSingleNode("//cbc:ResponseCode", nsmgr).InnerText)
                dictionary.Add("msj_sunat", xmlCDR.SelectSingleNode("//cbc:Description", nsmgr).InnerText.ToUpper)
                dictionary.Add("hash_cdr", xmlCDR.SelectSingleNode("//ds:DigestValue", nsmgrSing).InnerText)
                dictionary.Add("hash_cpe", hash_cpe)
                '=============eliminas el archivo comprimido que enviamos===============
                File.Delete(rutaCompleta & ".ZIP")
            End If

            'https://stackoverflow.com/questions/16889895/c-sharp-xmldocument-selectnodes-is-not-working
        Catch ex As Exception
            MsgBox(ex.Message)
            dictionary = New Dictionary(Of String, String)
            dictionary.Add("flg_rta", "0")
            dictionary.Add("mensaje", "ERROR AL CONECTARSE A LA SUNAT: " & ex.Message)
            dictionary.Add("cod_sunat", "")
            dictionary.Add("msj_sunat", "")
            dictionary.Add("hash_cdr", "")
            dictionary.Add("hash_cpe", hash_cpe)
        End Try
        Return dictionary
    End Function

    Public Function EnvioResumen(ruc As String, usu_sol As String, contra_sol As String, nombre_archivo As String, rutaArchivo As String, url As String, hash_cpe As String) As Dictionary(Of String, String)
        Dim dictionary As Dictionary(Of String, String)
        Try
            Dim doc As New XmlDocument()
            Dim ticket As String
            Dim strSOAP As String
            Dim rutaCompleta As String = rutaArchivo & nombre_archivo
            Comprimir(nombre_archivo, rutaArchivo)
            Dim rutaCdr As String = rutaArchivo & "R-" & nombre_archivo & ".ZIP"
            Dim NomFichierZIP As String = System.IO.Path.GetFileName(rutaCompleta & ".ZIP")
            Dim data As Byte() = System.IO.File.ReadAllBytes(rutaCompleta & ".ZIP")

            strSOAP = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' " &
                    "xmlns:ser='http://service.sunat.gob.pe' " &
                    "xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'> " &
                    "<soapenv:Header> " &
                    "<wsse:Security> " &
                    "<wsse:UsernameToken> " &
                    "<wsse:Username>" & ruc & usu_sol & "</wsse:Username> " &
                    "<wsse:Password>" & contra_sol & "</wsse:Password> " &
                    "</wsse:UsernameToken> " &
                    "</wsse:Security> " &
                    "</soapenv:Header> " &
                    "<soapenv:Body> " &
                    "<ser:sendSummary> " &
                    "<fileName>" & NomFichierZIP & "</fileName> " &
                    "<contentFile>" & Convert.ToBase64String(data) & "</contentFile> " &
                    "</ser:sendSummary> " &
                    "</soapenv:Body> " &
                    "</soapenv:Envelope>"


            Dim returned_value As String
            Dim strPostData As String
            Dim objRequest As Object

            strPostData = strSOAP
            'objRequest = CreateObject("MSXML2.XMLHTTP.3.0")
            objRequest = CreateObject("MSXML2.ServerXMLHTTP")
            With objRequest
                .Open("POST", url, False)
                .setRequestHeader("Content-Type", "application/xml")
                .send(strPostData)
                returned_value = .responseText
            End With
            doc.LoadXml(returned_value)

            '=======================validando respuesta========================
            Dim Lst As XmlNodeList = doc.SelectNodes("//faultcode")
            If Lst.Count() > 0 Then
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "0")
                dictionary.Add("mensaje", "ERROR AL ENVIAR A LA SUNAT")
                dictionary.Add("cod_sunat", doc.SelectSingleNode("//faultcode").InnerText.Replace("soap-env:Client.", ""))
                dictionary.Add("msj_sunat", doc.SelectSingleNode("//faultstring").InnerText)
                dictionary.Add("hash_cdr", "")
                dictionary.Add("hash_cpe", hash_cpe)
            Else
                ticket = doc.SelectSingleNode("//ticket").InnerText
                '========================asignamos valores de retorno======================
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "1")
                dictionary.Add("mensaje", "COMPROBANTE ENVIADO CORRECTAMENTE")
                dictionary.Add("cod_sunat", "1")
                dictionary.Add("msj_sunat", ticket)
                dictionary.Add("hash_cdr", "")
                dictionary.Add("hash_cpe", hash_cpe)
                '=============eliminas el archivo comprimido que enviamos===============
                File.Delete(rutaCompleta & ".ZIP")
            End If

            'https://stackoverflow.com/questions/16889895/c-sharp-xmldocument-selectnodes-is-not-working
        Catch ex As Exception
            dictionary = New Dictionary(Of String, String)
            dictionary.Add("flg_rta", "0")
            dictionary.Add("mensaje", "ERROR AL CONECTARSE A LA SUNAT: " & ex.Message)
            dictionary.Add("cod_sunat", "x")
            dictionary.Add("msj_sunat", "")
            dictionary.Add("hash_cdr", "")
            dictionary.Add("hash_cpe", hash_cpe)
        End Try
        Return dictionary
    End Function
    Public Function getStatusFactura(ruc As String, usu_sol As String, contra_sol As String, url As String, ruc_emisor As String, tipo_comprobante As String, serie As String, numero As String) As Dictionary(Of String, String)
        Dim dictionary As Dictionary(Of String, String)
        Try
            Dim doc As New XmlDocument()
            Dim strSOAP As String

            strSOAP = "<soapenv:Envelope xmlns:ser='http://service.sunat.gob.pe' " &
                        "xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' " &
                        "xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'> " &
                        "<soapenv:Header> " &
                        "<wsse:Security> " &
                        "<wsse:UsernameToken> " &
                        "<wsse:Username>" & ruc & usu_sol & "</wsse:Username> " &
                        "<wsse:Password>" & contra_sol & "</wsse:Password> " &
                        "</wsse:UsernameToken> " &
                        "</wsse:Security> " &
                        "</soapenv:Header> " &
                        "<soapenv:Body> " &
                        "<ser:getStatus> " &
                        "<rucComprobante>" & ruc_emisor & "</rucComprobante> " &
                        "<tipoComprobante>" & tipo_comprobante & "</tipoComprobante> " &
                        "<serieComprobante>" & serie & "</serieComprobante> " &
                        "<numeroComprobante>" & numero & "</numeroComprobante> " &
                        "</ser:getStatus> " &
                        "</soapenv:Body> " &
                        "</soapenv:Envelope>"

            Dim returned_value As String
            Dim strPostData As String
            Dim objRequest As Object

            strPostData = strSOAP
            'objRequest = CreateObject("MSXML2.XMLHTTP.3.0")
            objRequest = CreateObject("MSXML2.ServerXMLHTTP")
            With objRequest
                .Open("POST", url, False)
                .setRequestHeader("Content-Type", "text/xml")
                .send(strPostData)
                returned_value = .responseText
            End With
            doc.LoadXml(returned_value)

            '=======================validando respuesta========================
            Dim Lst As XmlNodeList = doc.SelectNodes("//faultcode")
            If Lst.Count() > 0 Then
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "0")
                dictionary.Add("mensaje", "ERROR AL CONSULTAR EN LA SUNAT")
                dictionary.Add("cod_sunat", doc.SelectSingleNode("//faultcode").InnerText.Replace("ns0:", ""))
                dictionary.Add("msj_sunat", doc.SelectSingleNode("//faultstring").InnerText)
                dictionary.Add("hash_cdr", "")
                dictionary.Add("hash_cpe", "")
            Else
                Dim statuCode As String
                Dim statuMensaje As String

                statuCode = doc.SelectSingleNode("//statusCode").InnerText
                statuMensaje = doc.SelectSingleNode("//statusMessage").InnerText
                '========================asignamos valores de retorno======================
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "1")
                dictionary.Add("mensaje", "COMPROBANTE CONSULTADO CORRECTAMENTE")
                dictionary.Add("cod_sunat", statuCode)
                dictionary.Add("msj_sunat", statuMensaje)
                dictionary.Add("hash_cdr", "")
                dictionary.Add("hash_cpe", "")
            End If

            'https://stackoverflow.com/questions/16889895/c-sharp-xmldocument-selectnodes-is-not-working
        Catch ex As Exception
            dictionary = New Dictionary(Of String, String)
            dictionary.Add("flg_rta", "0")
            dictionary.Add("mensaje", "ERROR AL CONECTARSE A LA SUNAT: " & ex.Message)
            dictionary.Add("cod_sunat", "")
            dictionary.Add("msj_sunat", "")
            dictionary.Add("hash_cdr", "")
            dictionary.Add("hash_cpe", "")
        End Try
        Return dictionary
    End Function
    Public Function ConsultaTicket(ruc As String, usu_sol As String, contra_sol As String, nombre_archivo As String, rutaArchivo As String, url As String, hash_cdr As String, ticket As String) As Dictionary(Of String, String)
        Dim dictionary As Dictionary(Of String, String)
        Try
            Dim doc As New XmlDocument()
            Dim strCDR As String
            Dim strSOAP As String

            Dim rutaCdr As String = rutaArchivo & "R-" & nombre_archivo & ".ZIP"


            strSOAP = "<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' " &
                    "xmlns:ser='http://service.sunat.gob.pe' " &
                    "xmlns:wsse='http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd'> " &
                    "<soapenv:Header> " &
                    "<wsse:Security> " &
                    "<wsse:UsernameToken> " &
                    "<wsse:Username>" & ruc & usu_sol & "</wsse:Username> " &
                    "<wsse:Password>" & contra_sol & "</wsse:Password> " &
                    "</wsse:UsernameToken> " &
                    "</wsse:Security> " &
                    "</soapenv:Header> " &
                    "<soapenv:Body> " &
                    "<ser:getStatus> " &
                    "<ticket>" & ticket & "</ticket> " &
                    "</ser:getStatus>" &
                    "</soapenv:Body> " &
                    "</soapenv:Envelope>"


            Dim returned_value As String
            Dim strPostData As String
            Dim objRequest As Object

            strPostData = strSOAP
            'objRequest = CreateObject("MSXML2.XMLHTTP.3.0")
            objRequest = CreateObject("MSXML2.ServerXMLHTTP")
            With objRequest
                .Open("POST", url, False)
                .setRequestHeader("Content-Type", "application/xml")
                .send(strPostData)
                returned_value = .responseText
            End With
            doc.LoadXml(returned_value)

            '=======================validando respuesta========================
            Dim Lst As XmlNodeList = doc.SelectNodes("//faultcode")
            If Lst.Count() > 0 Then
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "0")
                dictionary.Add("mensaje", "ERROR AL ENVIAR A LA SUNAT")
                dictionary.Add("cod_sunat", doc.SelectSingleNode("//faultcode").InnerText.Replace("soap-env:Client.", ""))
                dictionary.Add("msj_sunat", doc.SelectSingleNode("//faultstring").InnerText)
                dictionary.Add("hash_cdr", "")
                dictionary.Add("hash_cpe", "")
            Else
                strCDR = doc.SelectSingleNode("//content").InnerText
                Dim byteCDR As Byte() = Convert.FromBase64String(strCDR)
                Dim s As IO.FileStream
                s = IO.File.Open(rutaCdr, IO.FileMode.Append)
                s.Write(byteCDR, 0, byteCDR.Length)
                s.Close()

                '===============descomprimo el xml=============
                Descomprimir(rutaArchivo, "R-" & nombre_archivo)
                '================================================================
                Dim xmlCDR As New XmlDocument()
                Dim rutaxmlCDR = rutaArchivo & "R-" & nombre_archivo & ".XML"
                xmlCDR.Load(rutaxmlCDR)

                '=======================nombre de espacios para obtener los valores del xml======================
                Dim nsmgr As New XmlNamespaceManager(doc.NameTable)
                nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")
                Dim nsmgrSing As New XmlNamespaceManager(doc.NameTable)
                nsmgrSing.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#")

                '========================asignamos valores de retorno======================
                dictionary = New Dictionary(Of String, String)
                dictionary.Add("flg_rta", "1")
                dictionary.Add("mensaje", "COMPROBANTE ENVIADO CORRECTAMENTE")
                dictionary.Add("cod_sunat", xmlCDR.SelectSingleNode("//cbc:ResponseCode", nsmgr).InnerText)
                dictionary.Add("msj_sunat", xmlCDR.SelectSingleNode("//cbc:Description", nsmgr).InnerText.ToUpper)
                dictionary.Add("hash_cdr", xmlCDR.SelectSingleNode("//ds:DigestValue", nsmgrSing).InnerText)
                dictionary.Add("hash_cpe", "")
                '=============eliminas el archivo comprimido que enviamos===============

            End If


        Catch ex As Exception
            dictionary = New Dictionary(Of String, String)
            dictionary.Add("flg_rta", "0")
            dictionary.Add("mensaje", "ERROR AL CONECTARSE A LA SUNAT: " & ex.Message)
            dictionary.Add("cod_sunat", "")
            dictionary.Add("msj_sunat", "")
            dictionary.Add("hash_cdr", "")
            dictionary.Add("hash_cpe", "")
        End Try
        Return dictionary
    End Function

End Class
