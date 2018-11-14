Imports System
Imports System.IO
Imports System.Xml
Imports BE = BusinessEntities
Public Class CrearXML

    Public Function CPE(comprobante As BE.CPE, nomArchivo As String, ruta As String) As Integer
        Try
            Dim xml As String
            Dim doc As New XmlDocument()
            xml = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no'?> 
                    <Invoice xmlns='urn:oasis:names:specification:ubl:schema:xsd:Invoice-2' 
                             xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2'  
                             xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' 
                             xmlns:ccts='urn:un:unece:uncefact:documentation:2' 
                             xmlns:ds='http://www.w3.org/2000/09/xmldsig#' 
                             xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' 
                             xmlns:qdt='urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2' 
                             xmlns:sac='urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1' 
                             xmlns:schemaLocation='urn:oasis:names:specification:ubl:schema:xsd:Invoice-2' 
                             xmlns:udt='urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2' 
                             xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                    <ext:UBLExtensions>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    <sac:AdditionalInformation>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1001</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_GRAVADAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1002</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_INAFECTA & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1003</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_EXONERADAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1004</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_GRATUITAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"

            If comprobante.TOTAL_PERCEPCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2001</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_PERCEPCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_RETENCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2002</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_RETENCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_DETRACCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2003</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_DETRACCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_BONIFICACIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2004</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_BONIFICACIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_DESCUENTO > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2005</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_DESCUENTO & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_LETRAS <> "" Then
                xml = xml & "<sac:AdditionalProperty>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Value>" & comprobante.TOTAL_LETRAS & "</cbc:Value>
                    </sac:AdditionalProperty>"
            End If
            If comprobante.TOTAL_GRATUITAS > 0 Then
                xml = xml & "<sac:AdditionalProperty>
                    <cbc:ID>1002</cbc:ID>
                    <cbc:Value>TRANSFERENCIA GRATUITA DE UN BIEN Y/O SERVICIO PRESTADO GRATUITAMENTE</cbc:Value>
                    </sac:AdditionalProperty>"
            End If

            If comprobante.TIPO_OPERACION <> "" Then
                xml = xml & "<sac:SUNATTransaction>
                         <cbc:ID>" & comprobante.TIPO_OPERACION & "</cbc:ID>
                         </sac:SUNATTransaction>"
            End If

            If comprobante.PLACA_VEHICULO <> "" Then
                xml = xml & "<sac:SUNATCosts>
                        <cac:RoadTransport>
                            <cbc:LicensePlateID>" & comprobante.PLACA_VEHICULO & "</cbc:LicensePlateID>
                        </cac:RoadTransport>
                      </sac:SUNATCosts>"
            End If

            xml = xml & "</sac:AdditionalInformation>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    </ext:UBLExtensions>
                    <cbc:UBLVersionID>2.0</cbc:UBLVersionID>
                    <cbc:CustomizationID>1.0</cbc:CustomizationID>
                    <cbc:ID>" & comprobante.NRO_COMPROBANTE & "</cbc:ID>
                    <cbc:IssueDate>" & comprobante.FECHA_DOCUMENTO & "</cbc:IssueDate>
                    <cbc:InvoiceTypeCode>" & comprobante.COD_TIPO_DOCUMENTO & "</cbc:InvoiceTypeCode>
                    <cbc:DocumentCurrencyCode>" & comprobante.COD_MONEDA & "</cbc:DocumentCurrencyCode>
                    <cac:Signature>
                    <cbc:ID>" & comprobante.NRO_COMPROBANTE & "</cbc:ID>
                    <cac:SignatoryParty>
                    <cac:PartyIdentification>
                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:ID>
                    </cac:PartyIdentification>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.RAZON_SOCIAL_EMPRESA & "]]></cbc:Name>
                    </cac:PartyName>
                    </cac:SignatoryParty>
                    <cac:DigitalSignatureAttachment>
                    <cac:ExternalReference>
                    <cbc:URI>#" & comprobante.NRO_COMPROBANTE & "</cbc:URI>
                    </cac:ExternalReference>
                    </cac:DigitalSignatureAttachment>
                    </cac:Signature>
                    <cac:AccountingSupplierParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO_EMPRESA & "</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.NOMBRE_COMERCIAL_EMPRESA & "]]></cbc:Name>
                    </cac:PartyName>
                    <cac:PostalAddress>
                    <cbc:ID>" & comprobante.CODIGO_UBIGEO_EMPRESA & "</cbc:ID>
                    <cbc:StreetName><![CDATA[" & comprobante.DIRECCION_EMPRESA & "]]></cbc:StreetName>
                    <cbc:CitySubdivisionName/>
                    <cbc:CityName><![CDATA[" & comprobante.DEPARTAMENTO_EMPRESA & "]]></cbc:CityName>
                    <cbc:CountrySubentity><![CDATA[" & comprobante.PROVINCIA_EMPRESA & "]]></cbc:CountrySubentity>
                    <cbc:District><![CDATA[" & comprobante.DISTRITO_EMPRESA & "]]></cbc:District>
                    <cac:Country>
                    <cbc:IdentificationCode>" & comprobante.CODIGO_PAIS_EMPRESA & "</cbc:IdentificationCode>
                    </cac:Country>
                    </cac:PostalAddress>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL_EMPRESA & "]]></cbc:RegistrationName>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingSupplierParty>
                    <cac:AccountingCustomerParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_CLIENTE & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO_CLIENTE & "</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PhysicalLocation>
                    <cbc:Description><![CDATA[" & comprobante.DIRECCION_CLIENTE & "]]></cbc:Description>
                    </cac:PhysicalLocation>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL_CLIENTE & "]]></cbc:RegistrationName>
                    <cac:RegistrationAddress>
                    <cbc:StreetName><![CDATA[" & comprobante.CIUDAD_CLIENTE & "]]></cbc:StreetName>
                    <cac:Country>
                    <cbc:IdentificationCode>" & comprobante.COD_PAIS_CLIENTE & "</cbc:IdentificationCode>
                    </cac:Country>
                    </cac:RegistrationAddress>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingCustomerParty>"

            If comprobante.MONTO_REGU_ANTICIPO > 0 Then
                xml = xml & "<cac:PrepaidPayment>
           	        <cbc:ID schemeID='02'>" & comprobante.NRO_COMPROBANTE_REF_ANT & "</cbc:ID>
                    <cbc:PaidAmount currencyID='" & comprobante.MONEDA_REGU_ANTICIPO & "'>" & comprobante.MONTO_REGU_ANTICIPO & "</cbc:PaidAmount>
                    <cbc:InstructionID schemeID='" & comprobante.TIPO_DOCUMENTO_EMP_REGU_ANT & "'>" & comprobante.NRO_DOCUMENTO_EMP_REGU_ANT & "</cbc:InstructionID>
                    </cac:PrepaidPayment>"
            End If

            xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            If comprobante.TOTAL_ISC > 0 Then
                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            End If
            If comprobante.TOTAL_OTR_IMP > 0 Then
                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_OTR_IMP & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_OTR_IMP & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>9999</cbc:ID>
                    <cbc:Name>OTROS</cbc:Name>
                    <cbc:TaxTypeCode>OTH</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            End If
            xml = xml & "<cac:LegalMonetaryTotal>                  
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL & "</cbc:PayableAmount>
                    </cac:LegalMonetaryTotal>"
            For x As Integer = 0 To comprobante.detalle.Count - 1
                If comprobante.detalle(x).COD_TIPO_OPERACION = "10" Or comprobante.detalle(x).COD_TIPO_OPERACION = "20" Or comprobante.detalle(x).COD_TIPO_OPERACION = "30" Or comprobante.detalle(x).COD_TIPO_OPERACION = "40" Then

                    'AQUI PARA MODIFICAR PRECIO  CON IGV

                    xml = xml & "<cac:InvoiceLine>
                    <cbc:ID>" & comprobante.detalle(x).ITEM & "</cbc:ID>
                    <cbc:InvoicedQuantity unitCode='" & comprobante.detalle(x).UNIDAD_MEDIDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:InvoicedQuantity>
                    <cbc:LineExtensionAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).SUB_TOTAL & "</cbc:LineExtensionAmount>
                    <cac:PricingReference>
                    <cac:AlternativeConditionPrice>
                                                         

                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO & "</cbc:PriceAmount>
                    <cbc:PriceTypeCode>" & comprobante.detalle(x).PRECIO_TIPO_CODIGO & "</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    </cac:PricingReference>
                    <cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TaxExemptionReasonCode>" & comprobante.detalle(x).COD_TIPO_OPERACION & "</cbc:TaxExemptionReasonCode>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                    If comprobante.detalle(x).ISC > 0 Then
                        xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TierRange>02</cbc:TierRange>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
                    End If
                    ' PRECIO  SIN IMPUESTO

                    xml = xml & "<cac:Item>
                    <cbc:Description><![CDATA[" & comprobante.detalle(x).DESCRIPCION & "]]></cbc:Description>
                    <cac:SellersItemIdentification>
                    <cbc:ID><![CDATA[" & comprobante.detalle(x).CODIGO & "]]></cbc:ID>
                    </cac:SellersItemIdentification>
                    </cac:Item>
                    <cac:Price>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO_SIN_IMPUESTO & "</cbc:PriceAmount>
                    </cac:Price>
                    </cac:InvoiceLine>"
                ElseIf comprobante.detalle(x).COD_TIPO_OPERACION = "31" Then 'GRATUITAS
                    xml = xml & "<cac:InvoiceLine>
                    <cbc:ID>" & comprobante.detalle(x).ITEM & "</cbc:ID>
                    <cbc:InvoicedQuantity unitCode='" & comprobante.detalle(x).UNIDAD_MEDIDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:InvoicedQuantity>
                    <cbc:LineExtensionAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IMPORTE & "</cbc:LineExtensionAmount>
                    <cac:PricingReference>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>0</cbc:PriceAmount>
                    <cbc:PriceTypeCode>01</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO & "</cbc:PriceAmount>
                    <cbc:PriceTypeCode>" & comprobante.detalle(x).PRECIO_TIPO_CODIGO & "</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    </cac:PricingReference>
                    <cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TaxExemptionReasonCode>" & comprobante.detalle(x).COD_TIPO_OPERACION & "</cbc:TaxExemptionReasonCode>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                    If comprobante.detalle(x).ISC > 0 Then
                        xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TierRange>02</cbc:TierRange>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
                    End If

                    xml = xml & "<cac:Item>
                    <cbc:Description><![CDATA[" & comprobante.detalle(x).DESCRIPCION & "]]></cbc:Description>
                    <cac:SellersItemIdentification>
                    <cbc:ID><![CDATA[" & comprobante.detalle(x).CODIGO & "]]></cbc:ID>
                    </cac:SellersItemIdentification>
                    </cac:Item>
                    <cac:Price>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO_SIN_IMPUESTO & "</cbc:PriceAmount>
                    </cac:Price>
                    </cac:InvoiceLine>"
                End If
            Next
            xml = xml & "</Invoice>"

            doc.LoadXml(xml)
            doc.Save(ruta & nomArchivo & ".XML")
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function

    Public Function CPEGuiaRemisionRemitente(comprobante As BE.CPE, nomArchivo As String, ruta As String) As Integer
        Try
            Dim xml As String
            Dim doc As New XmlDocument()
            xml = ""

            doc.LoadXml(xml)
            doc.Save(ruta & nomArchivo & ".XML")
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function

    Public Function CPE_NC(comprobante As BE.CPE, nomArchivo As String, ruta As String) As Integer
        Try
            Dim xml As String
            Dim doc As New XmlDocument()
            xml = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no'?>
                <CreditNote xmlns='urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2'
                xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2'
                xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'
                xmlns:ccts='urn:un:unece:uncefact:documentation:2'
                xmlns:ds='http://www.w3.org/2000/09/xmldsig#' 
                xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2'
                xmlns:qdt='urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2'
                xmlns:sac='urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1'
                xmlns:udt='urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2'
                xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                    <ext:UBLExtensions>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    <sac:AdditionalInformation>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1001</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_GRAVADAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1002</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_INAFECTA & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1003</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_EXONERADAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1004</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_GRATUITAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"

            If comprobante.TOTAL_PERCEPCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2001</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_PERCEPCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_RETENCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2002</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_RETENCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_DETRACCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2003</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_DETRACCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_BONIFICACIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2004</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_BONIFICACIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_DESCUENTO > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2005</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_DESCUENTO & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_LETRAS <> "" Then
                xml = xml & "<sac:AdditionalProperty>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Value>" & comprobante.TOTAL_LETRAS & "</cbc:Value>
                    </sac:AdditionalProperty>"
            End If
            If comprobante.TOTAL_GRATUITAS > 0 Then
                xml = xml & "<sac:AdditionalProperty>
                    <cbc:ID>1002</cbc:ID>
                    <cbc:Value>TRANSFERENCIA GRATUITA DE UN BIEN Y/O SERVICIO PRESTADO GRATUITAMENTE</cbc:Value>
                    </sac:AdditionalProperty>"
            End If

            If comprobante.TIPO_OPERACION <> "" Then
                xml = xml & "<sac:SUNATTransaction>
                         <cbc:ID>" & comprobante.TIPO_OPERACION & "</cbc:ID>
                         </sac:SUNATTransaction>"
            End If

            xml = xml & "</sac:AdditionalInformation>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    </ext:UBLExtensions>
                    <cbc:UBLVersionID>2.0</cbc:UBLVersionID>
                    <cbc:CustomizationID>1.0</cbc:CustomizationID>
                    <cbc:ID>" & comprobante.NRO_COMPROBANTE & "</cbc:ID>
                    <cbc:IssueDate>" & comprobante.FECHA_DOCUMENTO & "</cbc:IssueDate>
                    <cbc:DocumentCurrencyCode>" & comprobante.COD_MONEDA & "</cbc:DocumentCurrencyCode>
                    <cac:DiscrepancyResponse>
                    <cbc:ReferenceID>" & comprobante.NRO_DOCUMENTO_MODIFICA & "</cbc:ReferenceID>
                    <cbc:ResponseCode>" & comprobante.COD_TIPO_MOTIVO & "</cbc:ResponseCode>
                    <cbc:Description><![CDATA[" & comprobante.DESCRIPCION_MOTIVO & "]]></cbc:Description>
                    </cac:DiscrepancyResponse>
                    <cac:BillingReference>
                    <cac:InvoiceDocumentReference>
                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_MODIFICA & "</cbc:ID>
                    <cbc:DocumentTypeCode>" & comprobante.TIPO_COMPROBANTE_MODIFICA & "</cbc:DocumentTypeCode>
                    </cac:InvoiceDocumentReference>
                    </cac:BillingReference>
                    <cac:Signature>
                    <cbc:ID>" & comprobante.NRO_COMPROBANTE & "</cbc:ID>
                    <cac:SignatoryParty>
                    <cac:PartyIdentification>
                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:ID>
                    </cac:PartyIdentification>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.RAZON_SOCIAL_EMPRESA & "]]></cbc:Name>
                    </cac:PartyName>
                    </cac:SignatoryParty>
                    <cac:DigitalSignatureAttachment>
                    <cac:ExternalReference>
                    <cbc:URI>#" & comprobante.NRO_COMPROBANTE & "</cbc:URI>
                    </cac:ExternalReference>
                    </cac:DigitalSignatureAttachment>
                    </cac:Signature>
                    <cac:AccountingSupplierParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO_EMPRESA & "</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.NOMBRE_COMERCIAL_EMPRESA & "]]></cbc:Name>
                    </cac:PartyName>
                    <cac:PostalAddress>
                    <cbc:ID>" & comprobante.CODIGO_UBIGEO_EMPRESA & "</cbc:ID>
                    <cbc:StreetName><![CDATA[" & comprobante.DIRECCION_EMPRESA & "]]></cbc:StreetName>
                    <cbc:CitySubdivisionName/>
                    <cbc:CityName><![CDATA[" & comprobante.DEPARTAMENTO_EMPRESA & "]]></cbc:CityName>
                    <cbc:CountrySubentity><![CDATA[" & comprobante.PROVINCIA_EMPRESA & "]]></cbc:CountrySubentity>
                    <cbc:District><![CDATA[" & comprobante.DISTRITO_EMPRESA & "]]></cbc:District>
                    <cac:Country>
                    <cbc:IdentificationCode>" & comprobante.CODIGO_PAIS_EMPRESA & "</cbc:IdentificationCode>
                    </cac:Country>
                    </cac:PostalAddress>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL_EMPRESA & "]]></cbc:RegistrationName>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingSupplierParty>
                    <cac:AccountingCustomerParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_CLIENTE & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO_CLIENTE & "</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PhysicalLocation>
                    <cbc:Description><![CDATA[" & comprobante.DIRECCION_CLIENTE & "]]></cbc:Description>
                    </cac:PhysicalLocation>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL_CLIENTE & "]]></cbc:RegistrationName>
                    <cac:RegistrationAddress>
                    <cbc:StreetName><![CDATA[" & comprobante.CIUDAD_CLIENTE & "]]></cbc:StreetName>
                    <cac:Country>
                    <cbc:IdentificationCode>" & comprobante.COD_PAIS_CLIENTE & "</cbc:IdentificationCode>
                    </cac:Country>
                    </cac:RegistrationAddress>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingCustomerParty>"

            If comprobante.MONTO_REGU_ANTICIPO > 0 Then
                xml = xml & "<cac:PrepaidPayment>
           	        <cbc:ID schemeID='02'>" & comprobante.NRO_COMPROBANTE_REF_ANT & "</cbc:ID>
                    <cbc:PaidAmount currencyID='" & comprobante.MONEDA_REGU_ANTICIPO & "'>" & comprobante.MONTO_REGU_ANTICIPO & "</cbc:PaidAmount>
                    <cbc:InstructionID schemeID='" & comprobante.TIPO_DOCUMENTO_EMP_REGU_ANT & "'>" & comprobante.NRO_DOCUMENTO_EMP_REGU_ANT & "</cbc:InstructionID>
                    </cac:PrepaidPayment>"
            End If

            xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            If comprobante.TOTAL_ISC > 0 Then
                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            End If
            If comprobante.TOTAL_OTR_IMP > 0 Then
                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_OTR_IMP & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_OTR_IMP & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>9999</cbc:ID>
                    <cbc:Name>OTROS</cbc:Name>
                    <cbc:TaxTypeCode>OTH</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            End If
            xml = xml & "<cac:LegalMonetaryTotal>                  
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL & "</cbc:PayableAmount>
                    </cac:LegalMonetaryTotal>"
            For x As Integer = 0 To comprobante.detalle.Count - 1
                If comprobante.detalle(x).COD_TIPO_OPERACION = "10" Or comprobante.detalle(x).COD_TIPO_OPERACION = "20" Or comprobante.detalle(x).COD_TIPO_OPERACION = "30" Or comprobante.detalle(x).COD_TIPO_OPERACION = "40" Then
                    xml = xml & "<cac:CreditNoteLine>
                    <cbc:ID>" & comprobante.detalle(x).ITEM & "</cbc:ID>
                    <cbc:CreditedQuantity unitCode='" & comprobante.detalle(x).UNIDAD_MEDIDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:CreditedQuantity>
                    <cbc:LineExtensionAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IMPORTE & "</cbc:LineExtensionAmount>
                    <cac:PricingReference>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO & "</cbc:PriceAmount>
                    <cbc:PriceTypeCode>" & comprobante.detalle(x).PRECIO_TIPO_CODIGO & "</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    </cac:PricingReference>
                    <cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TaxExemptionReasonCode>" & comprobante.detalle(x).COD_TIPO_OPERACION & "</cbc:TaxExemptionReasonCode>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                    If comprobante.detalle(x).ISC > 0 Then
                        xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TierRange>02</cbc:TierRange>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
                    End If

                    xml = xml & "<cac:Item>
                    <cbc:Description><![CDATA[" & comprobante.detalle(x).DESCRIPCION & "]]></cbc:Description>
                    <cac:SellersItemIdentification>
                    <cbc:ID><![CDATA[" & comprobante.detalle(x).CODIGO & "]]></cbc:ID>
                    </cac:SellersItemIdentification>
                    </cac:Item>
                    <cac:Price>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO_SIN_IMPUESTO & "</cbc:PriceAmount>
                    </cac:Price>
                    </cac:CreditNoteLine>"
                ElseIf comprobante.detalle(x).COD_TIPO_OPERACION = "31" Then 'GRATUITAS
                    xml = xml & "<cac:InvoiceLine>
                    <cbc:ID>" & comprobante.detalle(x).ITEM & "</cbc:ID>
                    <cbc:CreditedQuantity unitCode='" & comprobante.detalle(x).UNIDAD_MEDIDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:CreditedQuantity>
                    <cbc:LineExtensionAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IMPORTE & "</cbc:LineExtensionAmount>
                    <cac:PricingReference>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>0</cbc:PriceAmount>
                    <cbc:PriceTypeCode>01</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO & "</cbc:PriceAmount>
                    <cbc:PriceTypeCode>" & comprobante.detalle(x).PRECIO_TIPO_CODIGO & "</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    </cac:PricingReference>
                    <cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TaxExemptionReasonCode>" & comprobante.detalle(x).COD_TIPO_OPERACION & "</cbc:TaxExemptionReasonCode>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                    If comprobante.detalle(x).ISC > 0 Then
                        xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TierRange>02</cbc:TierRange>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
                    End If

                    xml = xml & "<cac:Item>
                    <cbc:Description><![CDATA[" & comprobante.detalle(x).DESCRIPCION & "]]></cbc:Description>
                    <cac:SellersItemIdentification>
                    <cbc:ID><![CDATA[" & comprobante.detalle(x).CODIGO & "]]></cbc:ID>
                    </cac:SellersItemIdentification>
                    </cac:Item>
                    <cac:Price>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO_SIN_IMPUESTO & "</cbc:PriceAmount>
                    </cac:Price>
                    </cac:CreditNoteLine>"
                End If
            Next
            xml = xml & "</CreditNote>"

            doc.LoadXml(xml)
            doc.Save(ruta & nomArchivo & ".XML")
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function

    Public Function CPE_ND(comprobante As BE.CPE, nomArchivo As String, ruta As String) As Integer
        Try
            Dim xml As String
            Dim doc As New XmlDocument()
            xml = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no'?><DebitNote
                    xmlns='urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2'
                    xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2'
                    xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2'
                    xmlns:ccts='urn:un:unece:uncefact:documentation:2'
                    xmlns:ds='http://www.w3.org/2000/09/xmldsig#'
                    xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2'
                    xmlns:qdt='urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2'
                    xmlns:sac='urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1'
                    xmlns:udt='urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2'
                    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                    <ext:UBLExtensions>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    <sac:AdditionalInformation>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1001</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_GRAVADAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1002</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_INAFECTA & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1003</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_EXONERADAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>
                    <sac:AdditionalMonetaryTotal>
                    <cbc:ID>1004</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_GRATUITAS & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"

            If comprobante.TOTAL_PERCEPCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2001</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_PERCEPCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_RETENCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2002</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_RETENCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_DETRACCIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2003</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_DETRACCIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_BONIFICACIONES > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2004</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_BONIFICACIONES & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_DESCUENTO > 0 Then
                xml = xml & "<sac:AdditionalMonetaryTotal>
                    <cbc:ID>2005</cbc:ID>
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_DESCUENTO & "</cbc:PayableAmount>
                    </sac:AdditionalMonetaryTotal>"
            End If

            If comprobante.TOTAL_LETRAS <> "" Then
                xml = xml & "<sac:AdditionalProperty>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Value>" & comprobante.TOTAL_LETRAS & "</cbc:Value>
                    </sac:AdditionalProperty>"
            End If
            If comprobante.TOTAL_GRATUITAS > 0 Then
                xml = xml & "<sac:AdditionalProperty>
                    <cbc:ID>1002</cbc:ID>
                    <cbc:Value>TRANSFERENCIA GRATUITA DE UN BIEN Y/O SERVICIO PRESTADO GRATUITAMENTE</cbc:Value>
                    </sac:AdditionalProperty>"
            End If

            If comprobante.TIPO_OPERACION <> "" Then
                xml = xml & "<sac:SUNATTransaction>
                         <cbc:ID>" & comprobante.TIPO_OPERACION & "</cbc:ID>
                         </sac:SUNATTransaction>"
            End If

            xml = xml & "</sac:AdditionalInformation>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    </ext:UBLExtensions>
                    <cbc:UBLVersionID>2.0</cbc:UBLVersionID>
                    <cbc:CustomizationID>1.0</cbc:CustomizationID>
                    <cbc:ID>" & comprobante.NRO_COMPROBANTE & "</cbc:ID>
                    <cbc:IssueDate>" & comprobante.FECHA_DOCUMENTO & "</cbc:IssueDate>
                    <cbc:DocumentCurrencyCode>" & comprobante.COD_MONEDA & "</cbc:DocumentCurrencyCode>
                    <cac:DiscrepancyResponse>
                    <cbc:ReferenceID>" & comprobante.NRO_COMPROBANTE & "</cbc:ReferenceID>
                    <cbc:ResponseCode>" & comprobante.COD_TIPO_MOTIVO & "</cbc:ResponseCode>
                    <cbc:Description><![CDATA[" & comprobante.DESCRIPCION_MOTIVO & "]]></cbc:Description>
                    </cac:DiscrepancyResponse>
                    <cac:BillingReference>
                    <cac:InvoiceDocumentReference>
                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_MODIFICA & "</cbc:ID>
                    <cbc:DocumentTypeCode>" & comprobante.TIPO_COMPROBANTE_MODIFICA & "</cbc:DocumentTypeCode>
                    </cac:InvoiceDocumentReference>
                    </cac:BillingReference>
                    <cac:Signature>
                    <cbc:ID>" & comprobante.NRO_COMPROBANTE & "</cbc:ID>
                    <cac:SignatoryParty>
                    <cac:PartyIdentification>
                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:ID>
                    </cac:PartyIdentification>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.RAZON_SOCIAL_EMPRESA & "]]></cbc:Name>
                    </cac:PartyName>
                    </cac:SignatoryParty>
                    <cac:DigitalSignatureAttachment>
                    <cac:ExternalReference>
                    <cbc:URI>#" & comprobante.NRO_COMPROBANTE & "</cbc:URI>
                    </cac:ExternalReference>
                    </cac:DigitalSignatureAttachment>
                    </cac:Signature>
                    <cac:AccountingSupplierParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO_EMPRESA & "</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.NOMBRE_COMERCIAL_EMPRESA & "]]></cbc:Name>
                    </cac:PartyName>
                    <cac:PostalAddress>
                    <cbc:ID>" & comprobante.CODIGO_UBIGEO_EMPRESA & "</cbc:ID>
                    <cbc:StreetName><![CDATA[" & comprobante.DIRECCION_EMPRESA & "]]></cbc:StreetName>
                    <cbc:CitySubdivisionName/>
                    <cbc:CityName><![CDATA[" & comprobante.DEPARTAMENTO_EMPRESA & "]]></cbc:CityName>
                    <cbc:CountrySubentity><![CDATA[" & comprobante.PROVINCIA_EMPRESA & "]]></cbc:CountrySubentity>
                    <cbc:District><![CDATA[" & comprobante.DISTRITO_EMPRESA & "]]></cbc:District>
                    <cac:Country>
                    <cbc:IdentificationCode>" & comprobante.CODIGO_PAIS_EMPRESA & "</cbc:IdentificationCode>
                    </cac:Country>
                    </cac:PostalAddress>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL_EMPRESA & "]]></cbc:RegistrationName>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingSupplierParty>
                    <cac:AccountingCustomerParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_CLIENTE & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO_CLIENTE & "</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PhysicalLocation>
                    <cbc:Description><![CDATA[" & comprobante.DIRECCION_CLIENTE & "]]></cbc:Description>
                    </cac:PhysicalLocation>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL_CLIENTE & "]]></cbc:RegistrationName>
                    <cac:RegistrationAddress>
                    <cbc:StreetName><![CDATA[" & comprobante.CIUDAD_CLIENTE & "]]></cbc:StreetName>
                    <cac:Country>
                    <cbc:IdentificationCode>" & comprobante.COD_PAIS_CLIENTE & "</cbc:IdentificationCode>
                    </cac:Country>
                    </cac:RegistrationAddress>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingCustomerParty>"

            If comprobante.MONTO_REGU_ANTICIPO > 0 Then
                xml = xml & "<cac:PrepaidPayment>
           	        <cbc:ID schemeID='02'>" & comprobante.NRO_COMPROBANTE_REF_ANT & "</cbc:ID>
                    <cbc:PaidAmount currencyID='" & comprobante.MONEDA_REGU_ANTICIPO & "'>" & comprobante.MONTO_REGU_ANTICIPO & "</cbc:PaidAmount>
                    <cbc:InstructionID schemeID='" & comprobante.TIPO_DOCUMENTO_EMP_REGU_ANT & "'>" & comprobante.NRO_DOCUMENTO_EMP_REGU_ANT & "</cbc:InstructionID>
                    </cac:PrepaidPayment>"
            End If

            xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            If comprobante.TOTAL_ISC > 0 Then
                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            End If
            If comprobante.TOTAL_OTR_IMP > 0 Then
                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_OTR_IMP & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL_OTR_IMP & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>9999</cbc:ID>
                    <cbc:Name>OTROS</cbc:Name>
                    <cbc:TaxTypeCode>OTH</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
            End If
            xml = xml & "<cac:RequestedMonetaryTotal>                  
                    <cbc:PayableAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.TOTAL & "</cbc:PayableAmount>
                    </cac:RequestedMonetaryTotal>"
            For x As Integer = 0 To comprobante.detalle.Count - 1
                If comprobante.detalle(x).COD_TIPO_OPERACION = "10" Or comprobante.detalle(x).COD_TIPO_OPERACION = "20" Or comprobante.detalle(x).COD_TIPO_OPERACION = "30" Or comprobante.detalle(x).COD_TIPO_OPERACION = "40" Then
                    xml = xml & "<cac:DebitNoteLine>
                    <cbc:ID>" & comprobante.detalle(x).ITEM & "</cbc:ID>
                    <cbc:DebitedQuantity unitCode='" & comprobante.detalle(x).UNIDAD_MEDIDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:DebitedQuantity>
                    <cbc:LineExtensionAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IMPORTE & "</cbc:LineExtensionAmount>
                    <cac:PricingReference>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO & "</cbc:PriceAmount>
                    <cbc:PriceTypeCode>" & comprobante.detalle(x).PRECIO_TIPO_CODIGO & "</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    </cac:PricingReference>
                    <cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TaxExemptionReasonCode>" & comprobante.detalle(x).COD_TIPO_OPERACION & "</cbc:TaxExemptionReasonCode>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                    If comprobante.detalle(x).ISC > 0 Then
                        xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TierRange>02</cbc:TierRange>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
                    End If

                    xml = xml & "<cac:Item>
                    <cbc:Description><![CDATA[" & comprobante.detalle(x).DESCRIPCION & "]]></cbc:Description>
                    <cac:SellersItemIdentification>
                    <cbc:ID><![CDATA[" & comprobante.detalle(x).CODIGO & "]]></cbc:ID>
                    </cac:SellersItemIdentification>
                    </cac:Item>
                    <cac:Price>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO_SIN_IMPUESTO & "</cbc:PriceAmount>
                    </cac:Price>
                    </cac:DebitNoteLine>"
                ElseIf comprobante.detalle(x).COD_TIPO_OPERACION = "31" Then 'GRATUITAS
                    xml = xml & "<cac:DebitNoteLine>
                    <cbc:ID>" & comprobante.detalle(x).ITEM & "</cbc:ID>
                    <cbc:DebitedQuantity unitCode='" & comprobante.detalle(x).UNIDAD_MEDIDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:DebitedQuantity>
                    <cbc:LineExtensionAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).CANTIDAD & "</cbc:LineExtensionAmount>
                    <cac:PricingReference>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>0</cbc:PriceAmount>
                    <cbc:PriceTypeCode>01</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    <cac:AlternativeConditionPrice>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IMPORTE & "</cbc:PriceAmount>
                    <cbc:PriceTypeCode>" & comprobante.detalle(x).PRECIO_TIPO_CODIGO & "</cbc:PriceTypeCode>
                    </cac:AlternativeConditionPrice>
                    </cac:PricingReference>
                    <cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TaxExemptionReasonCode>" & comprobante.detalle(x).COD_TIPO_OPERACION & "</cbc:TaxExemptionReasonCode>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                    If comprobante.detalle(x).ISC > 0 Then
                        xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cbc:TierRange>02</cbc:TierRange>
                    <cac:TaxScheme>
                    <cbc:ID>2000</cbc:ID>
                    <cbc:Name>ISC</cbc:Name>
                    <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"
                    End If

                    xml = xml & "<cac:Item>
                    <cbc:Description><![CDATA[" & comprobante.detalle(x).DESCRIPCION & "]]></cbc:Description>
                    <cac:SellersItemIdentification>
                    <cbc:ID><![CDATA[" & comprobante.detalle(x).CODIGO & "]]></cbc:ID>
                    </cac:SellersItemIdentification>
                    </cac:Item>
                    <cac:Price>
                    <cbc:PriceAmount currencyID='" & comprobante.COD_MONEDA & "'>" & comprobante.detalle(x).PRECIO_SIN_IMPUESTO & "</cbc:PriceAmount>
                    </cac:Price>
                    </cac:DebitNoteLine>"
                End If
            Next
            xml = xml & "</DebitNote>"

            doc.LoadXml(xml)
            doc.Save(ruta & nomArchivo & ".XML")
        Catch ex As Exception
            Return 0
        End Try
        Return 1
    End Function

    Public Function ResumenBoleta(comprobante As BE.CPE_RESUMEN_BOLETA, nomArchivo As String, ruta As String)
        'BOLETAS--------------------------------
        Try
            Dim xml As String
            Dim doc As New XmlDocument()
            xml = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no'?>
                    <SummaryDocuments xmlns='urn:sunat:names:specification:ubl:peru:schema:xsd:SummaryDocuments-1' 
                    xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2' 
                    xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' 
                    xmlns:ds='http://www.w3.org/2000/09/xmldsig#' 
                    xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' 
                    xmlns:qdt='urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2' 
                    xmlns:sac='urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1' 
                    xmlns:udt='urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2'>
                    <ext:UBLExtensions>
                    <ext:UBLExtension>
                    <ext:ExtensionContent>
                    </ext:ExtensionContent>
                    </ext:UBLExtension>
                    </ext:UBLExtensions>
                    <cbc:UBLVersionID>2.0</cbc:UBLVersionID>
                    <cbc:CustomizationID>1.1</cbc:CustomizationID>
                    <cbc:ID>" & comprobante.CODIGO & "-" & comprobante.SERIE & "-" & comprobante.SECUENCIA & "</cbc:ID>
                    <cbc:ReferenceDate>" & comprobante.FECHA_REFERENCIA & "</cbc:ReferenceDate>
                    <cbc:IssueDate>" & comprobante.FECHA_DOCUMENTO & "</cbc:IssueDate>
                    <cac:Signature>
                    <cbc:ID>" & comprobante.CODIGO & "-" & comprobante.SERIE & "-" & comprobante.SECUENCIA & "</cbc:ID>
                    <cac:SignatoryParty>
                    <cac:PartyIdentification>
                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:ID>
                    </cac:PartyIdentification>
                    <cac:PartyName>
                    <cbc:Name><![CDATA[" & comprobante.RAZON_SOCIAL & "]]></cbc:Name>
                    </cac:PartyName>
                    </cac:SignatoryParty>
                    <cac:DigitalSignatureAttachment>
                    <cac:ExternalReference>
                    <cbc:URI>" & comprobante.CODIGO & "-" & comprobante.SERIE & "-" & comprobante.SECUENCIA & "</cbc:URI>
                    </cac:ExternalReference>
                    </cac:DigitalSignatureAttachment>
                    </cac:Signature>
                    <cac:AccountingSupplierParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>6</cbc:AdditionalAccountID>
                    <cac:Party>
                    <cac:PartyLegalEntity>
                    <cbc:RegistrationName><![CDATA[" & comprobante.RAZON_SOCIAL & "]]></cbc:RegistrationName>
                    </cac:PartyLegalEntity>
                    </cac:Party>
                    </cac:AccountingSupplierParty>"
            For x As Integer = 0 To comprobante.detalle.Count - 1
                xml = xml & "<sac:SummaryDocumentsLine>
                    <cbc:LineID>" & comprobante.detalle(x).ITEM & "</cbc:LineID>
                    <cbc:DocumentTypeCode>" & comprobante.detalle(x).TIPO_COMPROBANTE & "</cbc:DocumentTypeCode>
                    <cbc:ID>" & comprobante.detalle(x).NRO_COMPROBANTE & "</cbc:ID>
                    <cac:AccountingCustomerParty>
                    <cbc:CustomerAssignedAccountID>" & comprobante.detalle(x).NRO_DOCUMENTO & "</cbc:CustomerAssignedAccountID>
                    <cbc:AdditionalAccountID>" & comprobante.detalle(x).TIPO_DOCUMENTO & "</cbc:AdditionalAccountID>
                    </cac:AccountingCustomerParty>"
                If (comprobante.detalle(x).TIPO_COMPROBANTE = "07" Or comprobante.detalle(x).TIPO_COMPROBANTE = "08") Then
                    xml = xml & "<cac:BillingReference>
			                        <cac:InvoiceDocumentReference>
				                        <cbc:ID>" & comprobante.detalle(x).NRO_COMPROBANTE_REF & "</cbc:ID>
				                        <cbc:DocumentTypeCode>" & comprobante.detalle(x).TIPO_COMPROBANTE_REF & "</cbc:DocumentTypeCode>
			                        </cac:InvoiceDocumentReference>
		                        </cac:BillingReference>"
                End If
                xml = xml & "<cac:Status>
                    <cbc:ConditionCode>" & comprobante.detalle(x).STATU & "</cbc:ConditionCode>
                    </cac:Status>
                    <sac:TotalAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).TOTAL & "</sac:TotalAmount>
                    <sac:BillingPayment>
                    <cbc:PaidAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).GRAVADA & "</cbc:PaidAmount>
                    <cbc:InstructionID>01</cbc:InstructionID>
                    </sac:BillingPayment>"
                If (comprobante.detalle(x).EXONERADO > 0) Then
                    xml = xml & "<sac:BillingPayment>
                    <cbc:PaidAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).EXONERADO & "</cbc:PaidAmount>
                    <cbc:InstructionID>02</cbc:InstructionID>
                    </sac:BillingPayment>"
                End If

                If (comprobante.detalle(x).INAFECTO > 0) Then
                    xml = xml & "<sac:BillingPayment>
                    <cbc:PaidAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).INAFECTO & "</cbc:PaidAmount>
                    <cbc:InstructionID>03</cbc:InstructionID>
                    </sac:BillingPayment>"
                End If

                If (comprobante.detalle(x).EXPORTACION > 0) Then
                    xml = xml & "<sac:BillingPayment>
                    <cbc:PaidAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).EXPORTACION & "</cbc:PaidAmount>
                    <cbc:InstructionID>04</cbc:InstructionID>
                    </sac:BillingPayment>"
                End If

                If (comprobante.detalle(x).GRATUITAS > 0) Then
                    xml = xml & "<sac:BillingPayment>
                    <cbc:PaidAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).GRATUITAS & "</cbc:PaidAmount>
                    <cbc:InstructionID>05</cbc:InstructionID>
                    </sac:BillingPayment>"
                End If

                If (comprobante.detalle(x).MONTO_CARGO_X_ASIG > 0) Then
                    xml = xml & "<cac:AllowanceCharge>"
                    If (comprobante.detalle(x).CARGO_X_ASIGNACION = 1) Then
                        xml = xml & "<cbc:ChargeIndicator>true</cbc:ChargeIndicator>"
                    Else
                        xml = xml & "<cbc:ChargeIndicator>false</cbc:ChargeIndicator>"
                    End If
                    xml = xml & "<cbc:Amount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).MONTO_CARGO_X_ASIG & "</cbc:Amount>
                    </cac:AllowanceCharge>"
                End If

                If (comprobante.detalle(x).ISC > 0) Then
                    xml = xml & "<cac:TaxTotal>
			        <cbc:TaxAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
			        <cac:TaxSubtotal>
                        <cbc:TaxAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).ISC & "</cbc:TaxAmount>
				        <cac:TaxCategory>
                            <cac:TaxScheme>
                                <cbc:ID>2000</cbc:ID>
                                <cbc:Name>ISC</cbc:Name>
                                <cbc:TaxTypeCode>EXC</cbc:TaxTypeCode>
                            </cac:TaxScheme>
				        </cac:TaxCategory>
                    </cac:TaxSubtotal>
		        </cac:TaxTotal>"
                End If

                xml = xml & "<cac:TaxTotal>
                    <cbc:TaxAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxSubtotal>
                    <cbc:TaxAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).IGV & "</cbc:TaxAmount>
                    <cac:TaxCategory>
                    <cac:TaxScheme>
                    <cbc:ID>1000</cbc:ID>
                    <cbc:Name>IGV</cbc:Name>
                    <cbc:TaxTypeCode>VAT</cbc:TaxTypeCode>
                    </cac:TaxScheme>
                    </cac:TaxCategory>
                    </cac:TaxSubtotal>
                    </cac:TaxTotal>"

                If (comprobante.detalle(x).OTROS > 0) Then
                    xml = xml & "<cac:TaxTotal>
			            <cbc:TaxAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).OTROS & "</cbc:TaxAmount>
			            <cac:TaxSubtotal>
                            <cbc:TaxAmount currencyID='" & comprobante.detalle(x).COD_MONEDA & "'>" & comprobante.detalle(x).OTROS & "</cbc:TaxAmount>
				            <cac:TaxCategory>
                                <cac:TaxScheme>
                                    <cbc:ID>9999</cbc:ID>
                                    <cbc:Name>OTROS</cbc:Name>
                                    <cbc:TaxTypeCode>OTH</cbc:TaxTypeCode>
                                </cac:TaxScheme>
				            </cac:TaxCategory>
                        </cac:TaxSubtotal>
		            </cac:TaxTotal>"
                End If

                xml = xml & "</sac:SummaryDocumentsLine>"
            Next
            xml = xml & "</SummaryDocuments>"
            doc.LoadXml(xml)
            doc.Save(ruta & nomArchivo & ".XML")
        Catch ex As Exception
            Return 0
        End Try
        Return 0
    End Function

    Public Function ResumenBaja(comprobante As BE.CPE_BAJA, nomArchivo As String, ruta As String)
        '---------------------FACTURA
        Try
            Dim xml As String
            Dim doc As New XmlDocument()
            xml = "<?xml version='1.0' encoding='ISO-8859-1' standalone='no'?>
                    <VoidedDocuments xmlns='urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1' 
                    xmlns:cac='urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2' 
                    xmlns:cbc='urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2' 
                    xmlns:ds='http://www.w3.org/2000/09/xmldsig#' xmlns:ext='urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2' 
                    xmlns:sac='urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1' 
                    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
	                    <ext:UBLExtensions>
		                    <ext:UBLExtension>
			                    <ext:ExtensionContent>
			                    </ext:ExtensionContent>
		                    </ext:UBLExtension>
	                    </ext:UBLExtensions>
	                    <cbc:UBLVersionID>2.0</cbc:UBLVersionID>
	                    <cbc:CustomizationID>1.0</cbc:CustomizationID>
	                    <cbc:ID>" & comprobante.CODIGO & "-" & comprobante.SERIE & "-" & comprobante.SECUENCIA & "</cbc:ID>
	                    <cbc:ReferenceDate>" & comprobante.FECHA_REFERENCIA & "</cbc:ReferenceDate>
	                    <cbc:IssueDate>" & comprobante.FECHA_BAJA & "</cbc:IssueDate>
	                    <cac:Signature>
		                    <cbc:ID>" & comprobante.CODIGO & "-" & comprobante.SERIE & "-" & comprobante.SECUENCIA & "</cbc:ID>
		                    <cac:SignatoryParty>
			                    <cac:PartyIdentification>
				                    <cbc:ID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:ID>
			                    </cac:PartyIdentification>
			                    <cac:PartyName>
				                    <cbc:Name>" & comprobante.RAZON_SOCIAL & "</cbc:Name>
			                    </cac:PartyName>
		                    </cac:SignatoryParty>
		                    <cac:DigitalSignatureAttachment>
			                    <cac:ExternalReference>
				                    <cbc:URI>" & comprobante.CODIGO & "-" & comprobante.SERIE & "-" & comprobante.SECUENCIA & "</cbc:URI>
			                    </cac:ExternalReference>
		                    </cac:DigitalSignatureAttachment>
	                    </cac:Signature>
	                    <cac:AccountingSupplierParty>
		                    <cbc:CustomerAssignedAccountID>" & comprobante.NRO_DOCUMENTO_EMPRESA & "</cbc:CustomerAssignedAccountID>
		                    <cbc:AdditionalAccountID>" & comprobante.TIPO_DOCUMENTO & "</cbc:AdditionalAccountID>
		                    <cac:Party>
			                    <cac:PartyLegalEntity>
				                    <cbc:RegistrationName>" & comprobante.RAZON_SOCIAL & "</cbc:RegistrationName>
			                    </cac:PartyLegalEntity>
		                    </cac:Party>
	                    </cac:AccountingSupplierParty>"
            For x As Integer = 0 To comprobante.detalle.Count - 1
                xml = xml & "<sac:VoidedDocumentsLine>
		                     <cbc:LineID>" & comprobante.detalle(x).ITEM & "</cbc:LineID>
		                     <cbc:DocumentTypeCode>" & comprobante.detalle(x).TIPO_COMPROBANTE & "</cbc:DocumentTypeCode>
		                     <sac:DocumentSerialID>" & comprobante.detalle(x).SERIE & "</sac:DocumentSerialID>
		                     <sac:DocumentNumberID>" & comprobante.detalle(x).NUMERO & "</sac:DocumentNumberID>
		                     <sac:VoidReasonDescription>" & comprobante.detalle(x).DESCRIPCION & "</sac:VoidReasonDescription>
	                         </sac:VoidedDocumentsLine>"
            Next
            xml = xml & "</VoidedDocuments>"
            doc.LoadXml(xml)
            doc.Save(ruta & nomArchivo & ".XML")
        Catch ex As Exception
            Return 0
        End Try
        Return 0
    End Function
End Class
