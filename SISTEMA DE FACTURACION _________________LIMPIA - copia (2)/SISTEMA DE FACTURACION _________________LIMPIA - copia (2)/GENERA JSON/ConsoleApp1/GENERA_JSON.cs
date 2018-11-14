using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using BusinessEntities;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using BusinessEntities;
using businessEntities;

namespace ConsoleApp1
{
   public  class GENERA_JSON
    {
        //_________________________________datos de funciones_____________________________
        public string rutaRAIZ = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "");
        public string TOKEN ;
        public string TIKET;
        public string HASH;
        public string XML;
        public string RUC_EMISOR;
        public string SERIE;
        //________________________________URL___________________________________
        public string Documento_URL = "https://ose.efact.pe/api-efact-ose/v1/document";
        public string Token_URL = "https://ose.efact.pe/api-efact-ose/oauth/token";
        public string Cdr_URL = "https://ose.efact.pe/api-efact-ose/v1/cdr/";
        public string xml_URL = "https://ose.efact.pe/api-efact-ose/v1/xml/";
        //_________________________________datos de usuario____________________________
        const string userName = "20505161051";
        const string password = "c4792ca2681ca4bb762400ff12892f78199a382c10b7249e2af6936aa6c234dd";       
        const string authorization = "Y2xpZW50OnNlY3JldA==";
        XmlDocument xml = new XmlDocument();
        public IDictionary<string, string> NOTA(CPE_BAJA da, string RUTA, string NOMBRE)
        {
            var diccionario = new Dictionary<string, string>();
            string json = @"{
        '_D' : 'urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2',
        '_S' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2',
        '_B' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2',
        '_E' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2',
        'CreditNote' : [ 
            {
                'UBLVersionID' : [ 
                    {
                        'IdentifierContent' : '2.1'
                    }
                ],
                'CustomizationID' : [ 
                    {
                        'IdentifierContent' : '2.0'
                    }
                ],
                'ID' : [ 
                    {
                        'IdentifierContent' : 'FC01-0040'
                    }
                ],
                'IssueDate' : [ 
                    {
                        'DateContent' : '2018-06-20'
                    }
                ],
                'IssueTime' : [ 
                    {
                        'DateTimeContent' : '17:25:28'
                    }
                ],
                'Note' : [ 
                    {
                        'TextContent' : 'QUINIENTOS NOVENTA Y CINCO Y 90/100 SOLES',
                        'LanguageLocaleIdentifier' : '1000'
                    }
                ],
                'DocumentCurrencyCode' : [ 
                    {
                        'CodeContent' : 'PEN'
                    }
                ],
                'DiscrepancyResponse' : [ 
                    {
                        'ReferenceID' : [ 
                            {
                                'IdentifierContent' : 'FF01-00023'
                            }
                        ],
                        'ResponseCode' : [ 
                            {
                                'CodeContent' : '08'
                            }
                        ],
                        'Description' : [ 
                            {
                                'TextContent' : 'Bonificación'
                            }
                        ]
                    }
                ],
                'BillingReference' : [ 
                    {
                        'InvoiceDocumentReference' : [ 
                            {
                                'ID' : [ 
                                    {
                                        'IdentifierContent' : 'FF01-00023'
                                    }
                                ],
                                'DocumentTypeCode' : [ 
                                    {
                                        'CodeContent' : '01'
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'Signature' : [ 
                    {
                        'ID' : [ 
                            {
                                'IdentifierContent' : 'IDSignature'
                            }
                        ],
                        'SignatoryParty' : [ 
                            {
                                'PartyIdentification' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '20101049711'
                                            }
                                        ]
                                    }
                                ],
                                'PartyName' : [ 
                                    {
                                        'Name' : [ 
                                            {
                                                'TextContent' : 'CONTRATISTAS S.A.C.'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'DigitalSignatureAttachment' : [ 
                            {
                                'ExternalReference' : [ 
                                    {
                                        'URI' : [ 
                                            {
                                                'TextContent' : 'IDSignature'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'AccountingSupplierParty' : [ 
                    {
                        'Party' : [ 
                            {
                                'PartyName' : [ 
                                    {
                                        'Name' : [ 
                                            {
                                                'TextContent' : 'CONTRATISTAS S.A.C.'
                                            }
                                        ]
                                    }
                                ],
                                'PostalAddress' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '150122'
                                            }
                                        ],
                                        'StreetName' : [ 
                                            {
                                                'TextContent' : 'Av. Los Dominicos 155'
                                            }
                                        ],
                                        'CityName' : [ 
                                            {
                                                'TextContent' : 'LIMA'
                                            }
                                        ],
                                        'CountrySubentity' : [ 
                                            {
                                                'TextContent' : 'LIMA'
                                            }
                                        ],
                                        'District' : [ 
                                            {
                                                'TextContent' : 'MIRAFLORES'
                                            }
                                        ],
                                        'Country' : [ 
                                            {
                                                'IdentificationCode' : [ 
                                                    {
                                                        'IdentifierContent' : 'PE'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ],
                                'PartyTaxScheme' : [ 
                                    {
                                        'RegistrationName' : [ 
                                            {
                                                'TextContent' : 'CONTRATISTAS S.A.C.'
                                            }
                                        ],
                                        'CompanyID' : [ 
                                            {
                                                'IdentifierContent' : '20101049711',
                                                'IdentificationSchemeNameText' : 'SUNAT:Identificador de Documento de Identidad',
                                                'IdentificationSchemeAgencyNameText' : 'PE:SUNAT',
                                                'IdentificationSchemeUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06',
                                                'IdentificationSchemeIdentifier' : '6'
                                            }
                                        ],
                                        'RegistrationAddress' : [ 
                                            {
                                                'AddressTypeCode' : [ 
                                                    {
                                                        'CodeContent' : '0001'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'AccountingCustomerParty' : [ 
                    {
                        'Party' : [ 
                            {
                                'PostalAddress' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '150122'
                                            }
                                        ],
                                        'StreetName' : [ 
                                            {
                                                'TextContent' : 'AV. LARCO 1301 1602 '
                                            }
                                        ],
                                        'CityName' : [ 
                                            {
                                                'TextContent' : 'LIMA'
                                            }
                                        ],
                                        'CountrySubentity' : [ 
                                            {
                                                'TextContent' : 'LIMA'
                                            }
                                        ],
                                        'District' : [ 
                                            {
                                                'TextContent' : 'MIRAFLORES'
                                            }
                                        ],
                                        'Country' : [ 
                                            {
                                                'IdentificationCode' : [ 
                                                    {
                                                        'IdentifierContent' : 'PE'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ],
                                'PartyTaxScheme' : [ 
                                    {
                                        'RegistrationName' : [ 
                                            {
                                                'TextContent' : 'EFACT S.A.C.'
                                            }
                                        ],
                                        'CompanyID' : [ 
                                            {
                                                'IdentifierContent' : '20551093035',
                                                'IdentificationSchemeNameText' : 'SUNAT:Identificador de Documento de Identidad',
                                                'IdentificationSchemeAgencyNameText' : 'PE:SUNAT',
                                                'IdentificationSchemeUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06',
                                                'IdentificationSchemeIdentifier' : '6'
                                            }
                                        ],
                                        'RegistrationAddress' : [ 
                                            {
                                                'AddressTypeCode' : [ 
                                                    {
                                                        'CodeContent' : '0001'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ],
                                'Contact' : [ 
                                    {
                                        'ElectronicMail' : [ 
                                            {
                                                'TextContent' : 'fmontes@efact.pe'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
               	'TaxTotal' : [ 
                    {
                        'TaxAmount' : [ 
                            {
                                'AmountContent' : '130.50',
                                'AmountCurrencyIdentifier' : 'PEN'
                            }
                        ],
                        'TaxSubtotal' : [ 
                            {
                                'TaxableAmount' : [ 
                                    {
                                        'AmountContent' : '725.00',
                                        'AmountCurrencyIdentifier' : 'PEN'
                                    }
                                ],
                                'TaxAmount' : [ 
                                    {
                                        'AmountContent' : '130.50',
                                        'AmountCurrencyIdentifier' : 'PEN'
                                    }
                                ],
                                'TaxCategory' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : 'S',
                                                'IdentificationSchemeIdentifier' : 'UN/ECE 5305',
                                                'IdentificationSchemeNameText' : 'Tax Category Identifier',
                                                'IdentificationSchemeAgencyNameText' : 'United Nations Economic Commission for Europe'
                                            }
                                        ],
                                        'Percent' : [ 
                                            {
                                                'NumericContent' : 18
                                            }
                                        ],
                                        'TaxScheme' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : '1000',
                                                        'IdentificationSchemeIdentifier' : 'UN/ECE 5153',
                                                        'IdentificationSchemeAgencyIdentifier' : '6'
                                                    }
                                                ],
                                                'Name' : [ 
                                                    {
                                                        'TextContent' : 'IGV'
                                                    }
                                                ],
                                                'TaxTypeCode' : [ 
                                                    {
                                                        'CodeContent' : 'VAT'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'LegalMonetaryTotal' : [ 
                    {
                        'LineExtensionAmount' : [ 
                            {
                                'AmountContent' : '725.00',
                                'AmountCurrencyIdentifier' : 'PEN'
                            }
                        ],
                        'TaxInclusiveAmount' : [ 
                            {
                                'AmountContent' : '855.50',
                                'AmountCurrencyIdentifier' : 'PEN'
                            }
                        ],
                        'PayableAmount' : [ 
                            {
                                'AmountContent' : '855.50',
                                'AmountCurrencyIdentifier' : 'PEN'
                            }
                        ]
                    }
                ],
                'CreditNoteLine' : [ 
                    {
                        'ID' : [ 
                            {
                                'IdentifierContent' : 1
                            }
                        ],
                        'Note' : [ 
                            {
                                'TextContent' : 'Unidad'
                            }
                        ],
                        'CreditedQuantity' : [ 
                            {
                                'QuantityContent' : '1',
                                'QuantityUnitCode' : 'ZZ',
                                'QuantityUnitCodeListIdentifier' : 'UN/ECE rec 20',
                                'QuantityUnitCodeListAgencyNameText' : 'United Nations Economic Commission for Europe'
                            }
                        ],
                        'LineExtensionAmount' : [ 
                            {
                                'AmountContent' : '100.00',
                                'AmountCurrencyIdentifier' : 'PEN'
                            }
                        ],
                        'PricingReference' : [ 
                            {
                                'AlternativeConditionPrice' : [ 
                                    {
                                        'PriceAmount' : [ 
                                            {
                                                'AmountContent' : '118.00',
                                                'AmountCurrencyIdentifier' : 'PEN'
                                            }
                                        ],
                                        'PriceTypeCode' : [ 
                                            {
                                                'CodeContent' : '01'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'TaxTotal' : [ 
                            {
                                'TaxAmount' : [ 
                                    {
                                        'AmountContent' : '18.00',
                                        'AmountCurrencyIdentifier' : 'PEN'
                                    }
                                ],
                                'TaxSubtotal' : [ 
                                    {
                                        'TaxableAmount' : [ 
                                            {
                                                'AmountContent' : '100.00',
                                                'AmountCurrencyIdentifier' : 'PEN'
                                            }
                                        ],
                                        'TaxAmount' : [ 
                                            {
                                                'AmountContent' : '18.00',
                                                'AmountCurrencyIdentifier' : 'PEN'
                                            }
                                        ],
                                        'TaxCategory' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : 'S',
                                                        'IdentificationSchemeIdentifier' : 'UN/ECE 5305',
                                                        'IdentificationSchemeNameText' : 'Tax Category Identifier',
                                                        'IdentificationSchemeAgencyNameText' : 'United Nations Economic Commission for Europe'
                                                    }
                                                ],
                                                'TaxExemptionReasonCode' : [ 
                                                    {
                                                        'CodeContent' : '10',
                                                        'CodeListAgencyNameText' : 'PE:SUNAT',
                                                        'CodeListNameText' : 'SUNAT:Codigo de Tipo de Afectación del IGV',
                                                        'CodeListUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07'
                                                    }
                                                ],
                                                'TaxScheme' : [ 
                                                    {
                                                        'ID' : [ 
                                                            {
                                                                'IdentifierContent' : '1000',
                                                                'IdentificationSchemeIdentifier' : 'UN/ECE 5153',
                                                                'IdentificationSchemeAgencyIdentifier' : '6'
                                                            }
                                                        ],
                                                        'Name' : [ 
                                                            {
                                                                'TextContent' : 'IGV'
                                                            }
                                                        ],
                                                        'TaxTypeCode' : [ 
                                                            {
                                                                'CodeContent' : 'VAT'
                                                            }
                                                        ]
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'Item' : [ 
                            {
                                'Description' : [ 
                                    {
                                        'TextContent' : 'Descripcion 1'
                                    }
                                ],
                                'SellersItemIdentification' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : 'Codigo'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'Price' : [ 
                            {
                                'PriceAmount' : [ 
                                    {
                                        'AmountCurrencyIdentifier' : 'PEN',
                                        'AmountContent' : '100.0000'
                                    }
                                ]
                            }
                        ]
                    }, 
                    {
                        'ID' : [ 
                            {
                                'IdentifierContent' : 2
                            }
                        ],
                        'Note' : [ 
                            {
                                'TextContent' : 'Unidad'
                            }
                        ],
                        'CreditedQuantity' : [ 
                            {
                                'QuantityContent' : '5',
                                'QuantityUnitCode' : 'ZZ',
                                'QuantityUnitCodeListIdentifier' : 'UN/ECE rec 20',
                                'QuantityUnitCodeListAgencyNameText' : 'United Nations Economic Commission for Europe'
                            }
                        ],
                        'LineExtensionAmount' : [ 
                            {
                                'AmountContent' : '625.00',
                                'AmountCurrencyIdentifier' : 'PEN'
                            }
                        ],
                        'PricingReference' : [ 
                            {
                                'AlternativeConditionPrice' : [ 
                                    {
                                        'PriceAmount' : [ 
                                            {
                                                'AmountContent' : '147.50',
                                                'AmountCurrencyIdentifier' : 'PEN'
                                            }
                                        ],
                                        'PriceTypeCode' : [ 
                                            {
                                                'CodeContent' : '01'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'TaxTotal' : [ 
                            {
                                'TaxAmount' : [ 
                                    {
                                        'AmountContent' : '112.50',
                                        'AmountCurrencyIdentifier' : 'PEN'
                                    }
                                ],
                                'TaxSubtotal' : [ 
                                    {
                                        'TaxableAmount' : [ 
                                            {
                                                'AmountContent' : '625.00',
                                                'AmountCurrencyIdentifier' : 'PEN'
                                            }
                                        ],
                                        'TaxAmount' : [ 
                                            {
                                                'AmountContent' : '112.50',
                                                'AmountCurrencyIdentifier' : 'PEN'
                                            }
                                        ],
                                        'TaxCategory' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : 'S',
                                                        'IdentificationSchemeIdentifier' : 'UN/ECE 5305',
                                                        'IdentificationSchemeNameText' : 'Tax Category Identifier',
                                                        'IdentificationSchemeAgencyNameText' : 'United Nations Economic Commission for Europe'
                                                    }
                                                ],
                                                'TaxExemptionReasonCode' : [ 
                                                    {
                                                        'CodeContent' : '10',
                                                        'CodeListAgencyNameText' : 'PE:SUNAT',
                                                        'CodeListNameText' : 'SUNAT:Codigo de Tipo de Afectación del IGV',
                                                        'CodeListUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07'
                                                    }
                                                ],
                                                'TaxScheme' : [ 
                                                    {
                                                        'ID' : [ 
                                                            {
                                                                'IdentifierContent' : '1000',
                                                                'IdentificationSchemeIdentifier' : 'UN/ECE 5153',
                                                                'IdentificationSchemeAgencyIdentifier' : '6'
                                                            }
                                                        ],
                                                        'Name' : [ 
                                                            {
                                                                'TextContent' : 'IGV'
                                                            }
                                                        ],
                                                        'TaxTypeCode' : [ 
                                                            {
                                                                'CodeContent' : 'VAT'
                                                            }
                                                        ]
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'Item' : [ 
                            {
                                'Description' : [ 
                                    {
                                        'TextContent' : 'Descripción 2'
                                    }
                                ],
                                'SellersItemIdentification' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : 'Codigo'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'Price' : [ 
                            {
                                'PriceAmount' : [ 
                                    {
                                        'AmountCurrencyIdentifier' : 'PEN',
                                        'AmountContent' : '125.0000'
                                    }
                                ]
                            }
                        ]
                    }
                ]
            }
        ]
    }
";
            return diccionario;
        }

        
          
            

            public IDictionary<string, string> DOC_BAJA(CPE_BAJA da, string RUTA, string NOMBRE)
        {
            var countQuery = new Dictionary<string, string>();


            string json = @"{
        '_D' : 'urn:sunat:names:specification:ubl:peru:schema:xsd:VoidedDocuments-1',
        '_S' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2',
        '_B' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2',
        '_E' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2',
        '_SUNAT' : 'urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1',
        'VoidedDocuments' : [ 
            {
                'UBLVersionID' : [ 
                    {
                        'IdentifierContent' : '2.0'
                    }
                ],
                'CustomizationID' : [ 
                    {
                        'IdentifierContent' : '1.0'
                    }
                ],
                'ID' : [ 
                    {
                        'IdentifierContent' : '"; json = json + da.SERIE + @"'
                    }
                ],
                'ReferenceDate' : [ 
                    {
                        'DateContent' : '"; json = json + da.FECHA_REFERENCIA + @"'
                    }
                ],
                'IssueDate' : [ 
                    {
                        'DateContent' : '"; json = json + da.FECHA_BAJA + @"'
                    }
                ],
                'Signature' : [ 
                    {
                        'ID' : [ 
                            {
                                'IdentifierContent' : 'IDSignature'
                            }
                        ],
                        'SignatoryParty' : [ 
                            {
                                'PartyIdentification' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.NRO_DOCUMENTO_EMPRESA  + @"'
                                            }
                                        ]
                                    }
                                ],
                                'PartyName' : [ 
                                    {
                                        'Name' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.RAZON_SOCIAL  + @"'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'DigitalSignatureAttachment' : [ 
                            {
                                'ExternalReference' : [ 
                                    {
                                        'URI' : [ 
                                            {
                                                'TextContent' : 'IDSignature'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'AccountingSupplierParty' : [ 
                    {
                        'CustomerAssignedAccountID' : [ 
                            {
                                'IdentifierContent' : '"; json = json + da.NRO_DOCUMENTO_EMPRESA + @"'
                            }
                        ],
                        'AdditionalAccountID' : [ 
                            {
                                'IdentifierContent' : '"; json = json + da.TIPO_DOCUMENTO + @"'
                            }
                        ],
                        'Party' : [ 
                            {
                                'PostalAddress' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.UBIGEO + @"'
                                            }
                                        ],
                                        'StreetName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DIRECCION + @"'
                                            }
                                        ],
                                        'CityName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DEPARTAMENTO + @"'
                                            }
                                        ],
                                        'CountrySubentity' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.PROVINCIA + @"'
                                            }
                                        ],
                                        'District' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DISTRITO + @"'
                                            }
                                        ],
                                        'Country' : [ 
                                            {
                                                'IdentificationCode' : [ 
                                                    {
                                                        'CodeContent' : 'PE'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ],
                                'PartyLegalEntity' : [ 
                                    {
                                        'RegistrationName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.RAZON_SOCIAL + @"'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'VoidedDocumentsLine' : [ 
                    {";
            for (int i = 0; i <= da.detalle.Count() - 1; i++)
            {
                json = json + @"

                        'LineID' : [ 
                            {
                                'IdentifierContent' : 1
                            }
                        ],
                        'DocumentTypeCode' : [ 
                            {
                                'CodeContent' : '"; json = json + da.detalle[i].TIPO_COMPROBANTE + @"'
                            }
                        ],
                        'DocumentSerialID' : [ 
                            {
                                'TextContent' : '"; json = json + da.detalle[i].SERIE + @"'
                            }
                        ],
                        'DocumentNumberID' : [ 
                            {
                                'TextContent' : '"; json = json + da.detalle[i].NUMERO + @"'
                            }
                        ],
                        'VoidReasonDescription' : [ 
                            {
                                'TextContent' : '"; json = json + da.detalle[i].DESCRIPCION + @"'
                            }
                        ]";

            }
            json = json + @"

                    }
                ]
            }
        ]
    }
";

            JObject rss = JObject.Parse(json);
            Console.WriteLine(rss.ToString());
            JavaScriptSerializer js = new JavaScriptSerializer(); //system.web.extension assembly....
            string outputJSON = js.Serialize(rss.ToString());
            string outputJSON2 = js.Serialize(outputJSON);
            File.WriteAllText(RUTA + NOMBRE, rss.ToString());

            try
            {
                // Iniciar una tarea: llamar a una función asíncrona en este ejemplo
                Task<string> callTask = Task.Run(() => GetAPIToken(userName, password, Token_URL));
                // Espere a que termine
                callTask.Wait();
                // Obtener el resultado
                TOKEN = callTask.Result;

                Task<string> callTask2 = Task.Run(() => postDoucumento(TOKEN, RUTA + NOMBRE, Documento_URL, NOMBRE));
                // Espere a que termine
                callTask2.Wait();
                // Obtener el resultado
                TIKET = callTask2.Result;
                Task<string> callTask3 = Task.Run(() => GetCDR_ANULA(TIKET, TOKEN, Cdr_URL));           // Espere a que termine
                callTask3.Wait();
                //// Obtener el resultado
                HASH = callTask3.Result;

                countQuery.Add("TIKET", TIKET);
                countQuery.Add("HASH", HASH);


            }
            catch (Exception ex)  //Exceptions here or in the function will be caught here
            {
                countQuery.Add("TIKET", "ERROR DE CONEXION");
                countQuery.Add("HASH", "ERROR DE CONEXION");
            }

            return countQuery;
        }
        public IDictionary<string, string> JSON_GENERA(CPE da, string RUTA, string NOMBRE)
        {
            var countQuery = new Dictionary<string, string>();
            
            RUC_EMISOR = da.NRO_DOCUMENTO_EMPRESA;
            SERIE = da.NRO_COMPROBANTE;
            //double T_GRAVADA =Convert.ToDouble( da.TOTAL_GRAVADAS);            
            //string T_GRABADA2 = T_GRAVADA.ToString("0.00", CultureInfo.InvariantCulture);
           

            string json = @"{
        '_D' : 'urn:oasis:names:specification:ubl:schema:xsd:Invoice-2',
        '_S' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2',
        '_B' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2',
        '_E' : 'urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2',
        'Invoice' : [ 
            {
                'UBLVersionID' : [ 
                    {
                        'IdentifierContent' : '2.1'
                    }
                ],
                'CustomizationID' : [ 
                    {
                        'IdentifierContent' : '2.0'
                    }
                ],
                'ProfileID' : [ 
                    {
                        'IdentifierContent' : '"; json = json + 1010 + @"',
                        'IdentificationSchemeNameText' : 'SUNAT:Identificador de Tipo de Operación',
                        'IdentificationSchemeAgencyNameText' : 'PE:SUNAT',
                        'IdentificationSchemeUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo17'
                    }
                ],
                'ID' : [ 
                    {
                        'IdentifierContent' : '"; json = json + da.NRO_COMPROBANTE + @"'
                    }
                ],
                'IssueDate' : [ 
                    {
                        'DateContent' : '"; json = json + da.FECHA_DOCUMENTO + @"'
                    }
                ],
                'IssueTime' : [ 
                    {
                        'DateTimeContent' : '"; json = json + DateTime.Now.ToString("hh:mm:ss") + @"'
                    }
                ],
                'InvoiceTypeCode' : [ 
                    {
                        'CodeContent' : '"; json = json + da.COD_TIPO_DOCUMENTO + @"',
                        'CodeListSchemeUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01',
                        'CodeListNameText' : 'SUNAT:Identificador de Tipo de Documento',
                        'CodeListAgencyNameText' : 'PE:SUNAT'
                    }
                ],
                'Note' : [ 
                    {
                        'TextContent' : '"; json = json +da.TOTAL_LETRAS + @"',
                        'LanguageLocaleIdentifier' : '1000'
                    }
                ],
                'DocumentCurrencyCode' : [ 
                    {
                        'CodeContent' : '"; json =json + da.COD_MONEDA + @"',
                        'CodeListIdentifier' : 'ISO 4217 Alpha',
                        'CodeListNameText' : 'Currency',
                        'CodeListAgencyNameText' : 'United Nations Economic Commission for Europe'
                    }
                ],
                'LineCountNumeric' : [ 
                    {
                        'NumericContent' : "; json = json + da.ITEMS + @"
                    }
                ],
                'Signature' : [ 
                    {
                        'ID' : [ 
                            {
                                'IdentifierContent' : 'IDSignature'
                            }
                        ],
                        'SignatoryParty' : [ 
                            {
                                'PartyIdentification' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.NRO_DOCUMENTO_EMPRESA + @"'
                                            }
                                        ]
                                    }
                                ],
                                'PartyName' : [ 
                                    {
                                        'Name' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.RAZON_SOCIAL_EMPRESA + @"'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'DigitalSignatureAttachment' : [ 
                            {
                                'ExternalReference' : [ 
                                    {
                                        'URI' : [ 
                                            {
                                                'TextContent' : 'IDSignature'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],

                'AccountingSupplierParty' : [ 
                    {
                        'Party' : [ 
                            {
                                'PartyName' : [ 
                                    {
                                        'Name' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.NOMBRE_COMERCIAL_EMPRESA  + @"'
                                            }
                                        ]
                                    }
                                ],
                                'PostalAddress' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.CODIGO_UBIGEO_EMPRESA + @"'
                                            }
                                        ],
                                        'StreetName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DIRECCION_EMPRESA + @"'
                                            }
                                        ],
                                        'CityName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DEPARTAMENTO_EMPRESA + @"'
                                            }
                                        ],
                                        'CountrySubentity' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.PROVINCIA_EMPRESA + @"'
                                            }
                                        ],
                                        'District' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DISTRITO_EMPRESA + @"'
                                            }
                                        ],
                                        'Country' : [ 
                                            {
                                                'IdentificationCode' : [ 
                                                    {
                                                        'IdentifierContent' : '"; json = json + da.CODIGO_PAIS_EMPRESA + @"'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ],
                                'PartyTaxScheme' : [ 
                                    {
                                        'RegistrationName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.RAZON_SOCIAL_EMPRESA + @"'
                                            }
                                        ],
                                        'CompanyID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.NRO_DOCUMENTO_EMPRESA + @"',
                                                'IdentificationSchemeIdentifier' : '"; json = json + da.TIPO_DOCUMENTO_EMPRESA + @"',
                                                'IdentificationSchemeNameText' : 'SUNAT:Identificador de Documento de Identidad',
                                                'IdentificationSchemeAgencyNameText' : 'PE:SUNAT',
                                                'IdentificationSchemeUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06'
                                            }
                                        ],
                                        'RegistrationAddress' : [ 
                                            {
                                                'AddressTypeCode' : [ 
                                                    {
                                                        'CodeContent' : '0001'
                                                    }
                                                ]
                                            }
                                        ],
                                        'TaxScheme' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : '-'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'AccountingCustomerParty' : [ 
                    {
                        'Party' : [ 
                            {
                                'PostalAddress' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : ''
                                            }
                                        ],
                                        'StreetName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.DIRECCION_CLIENTE + @"'
                                            }
                                        ]
                                    }
                                ],
                                'PartyTaxScheme' : [ 
                                    {
                                        'RegistrationName' : [ 
                                            {
                                                'TextContent' : '"; json = json + da.RAZON_SOCIAL_CLIENTE + @"'
                                            }
                                        ],
                                        'CompanyID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.NRO_DOCUMENTO_CLIENTE + @"',
                                                'IdentificationSchemeIdentifier' : '"; json = json + da.TIPO_DOCUMENTO_CLIENTE + @"',
                                                'IdentificationSchemeNameText' : 'SUNAT:Identificador de Documento de Identidad',
                                                'IdentificationSchemeAgencyNameText' : 'PE:SUNAT',
                                                'IdentificationSchemeUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06'
                                            }
                                        ],
                                        'RegistrationAddress' : [ 
                                            {
                                                'AddressTypeCode' : [ 
                                                    {
                                                        'CodeContent' : '0001'
                                                    }
                                                ]
                                            }
                                        ],
                                        'TaxScheme' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : '-'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ],
                                'Contact' : [ 
                                    {
                                        'ElectronicMail' : [ 
                                            {
                                                'TextContent' : ''
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
               
                'TaxTotal' : [ 
                    {
                        'TaxAmount' : [ 
                            {
                                'AmountContent' : '"; json = json + da.TOTAL_IGV + @"',
                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                            }
                        ],
                        'TaxSubtotal' : [ 
                            {
                                'TaxableAmount' : [ 
                                    {
                                        'AmountContent' : '"; json = json + da.SUB_TOTAL + @"',
                                        'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                                    }
                                ],
                                'TaxAmount' : [ 
                                    {
                                        'AmountContent' : '"; json = json + da.TOTAL_IGV + @"',
                                        'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                                    }
                                ],
                                'TaxCategory' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : 'S',
                                                'IdentificationSchemeIdentifier' : 'UN/ECE 5305',
                                                'IdentificationSchemeNameText' : 'Tax Category Identifier',
                                                'IdentificationSchemeAgencyNameText' : 'United Nations Economic Commission for Europe'
                                            }
                                        ],
                                        'Percent' : [ 
                                            {
                                                'NumericContent' : 18
                                            }
                                        ],
                                        'TaxScheme' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : '1000',
                                                        'IdentificationSchemeIdentifier' : 'UN/ECE 5153',
                                                        'IdentificationSchemeAgencyIdentifier' : '6'
                                                    }
                                                ],
                                                'Name' : [ 
                                                    {
                                                        'TextContent' : 'IGV'
                                                    }
                                                ],
                                                'TaxTypeCode' : [ 
                                                    {
                                                        'CodeContent' : 'VAT'
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ],
                'LegalMonetaryTotal' : [ 				
                    {
                        'LineExtensionAmount' : [ 
                            {
                                'AmountContent' : '"; json = json + da.TOTAL + @"',
                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                            }
                        ],
                        'TaxInclusiveAmount' : [ 
                            {
                                'AmountContent' : '"; json = json + da.TOTAL_GRAVADAS + @"',
                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                            }
                        ],
                        'PayableAmount' : [ 
                            {
                                'AmountContent' : '"; json = json + da.TOTAL_GRAVADAS + @"',
                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                            }
                        ]
                    }
                ],
                
				
				
				
				
				'InvoiceLine' : [ ";

            for (int i = 0; i <= da.detalle.Count()-1; i++)
            {
                
               

                json = json + @" {
                        'ID' : [ 
                            {
                                'IdentifierContent' : ";json=json+ da.detalle[i].ITEM + @"
                            }
                        ],
                        'Note' : [ 
                            {
                                'TextContent' : '"; json = json + da.detalle[i].UNIDAD_MEDIDA + @"'
                            }
                        ],
                        'InvoicedQuantity' : [ 
                            {
                                'QuantityContent' : '"; json = json + da.detalle[i].CANTIDAD + @"',
                                'QuantityUnitCode' : '"; json = json + da.detalle[i].UNIDAD_MEDIDA + @"',
                                'QuantityUnitCodeListIdentifier' : 'UN/ECE rec 20',
                                'QuantityUnitCodeListAgencyNameText' : 'United Nations Economic Commission for Europe'
                            }
                        ],
                        'LineExtensionAmount' : [ 
                            {
                                'AmountContent' : '"; json = json + da.detalle[i].PRECIO_SIN_IMPUESTO + @"',
                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                            }
                        ],
                        'PricingReference' : [ 
                            {
                                'AlternativeConditionPrice' : [ 
                                    {
                                        'PriceAmount' : [ 
                                            {
                                                'AmountContent' : '"; json = json + da.detalle[i].SUB_TOTAL + @"',
                                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                                            }
                                        ],
                                        'PriceTypeCode' : [ 
                                            {
                                                'CodeContent' : '01',
                                                'CodeListNameText' : 'SUNAT:Indicador de Tipo de Precio',
                                                'CodeListAgencyNameText' : 'PE:SUNAT',
                                                'CodeListUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'TaxTotal' : [ 
                            {
                                'TaxAmount' : [ 
                                    {
                                        'AmountContent' : '"; json = json + da.detalle[i].IGV + @"',
                                        'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                                    }
                                ],
                                'TaxSubtotal' : [ 
                                    {
                                        'TaxableAmount' : [ 
                                            {
                                                'AmountContent' : '"; json = json + da.detalle[i].IMPORTE + @"',
                                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                                            }
                                        ],
                                        'TaxAmount' : [ 
                                            {
                                                'AmountContent' : '"; json = json + da.detalle[i].IGV + @"',
                                                'AmountCurrencyIdentifier' : '"; json = json + da.COD_MONEDA + @"'
                                            }
                                        ],
                                        'TaxCategory' : [ 
                                            {
                                                'ID' : [ 
                                                    {
                                                        'IdentifierContent' : 'S',
                                                        'IdentificationSchemeIdentifier' : 'UN/ECE 5305',
                                                        'IdentificationSchemeNameText' : 'Tax Category Identifier',
                                                        'IdentificationSchemeAgencyNameText' : 'United Nations Economic Commission for Europe'
                                                    }
                                                ],
                                                'TaxExemptionReasonCode' : [ 
                                                    {
                                                        'CodeContent' : '"; json = json + da.detalle[i].COD_TIPO_OPERACION + @"',
                                                        'CodeListAgencyNameText' : 'PE:SUNAT',
                                                        'CodeListNameText' : 'SUNAT:Codigo de Tipo de Afectación del IGV',
                                                        'CodeListUniformResourceIdentifier' : 'urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07'
                                                    }
                                                ],
                                                'TaxScheme' : [ 
                                                    {
                                                        'ID' : [ 
                                                            {
                                                                'IdentifierContent' : '1000',
                                                                'IdentificationSchemeIdentifier' : 'UN/ECE 5153',
                                                                'IdentificationSchemeAgencyIdentifier' : '6'
                                                            }
                                                        ],
                                                        'Name' : [ 
                                                            {
                                                                'TextContent' : 'IGV'
                                                            }
                                                        ],
                                                        'TaxTypeCode' : [ 
                                                            {
                                                                'CodeContent' : 'VAT'
                                                            }
                                                        ]
                                                    }
                                                ]
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'Item' : [ 
                            {
                                'Description' : [ 
                                    {
                                        'TextContent' : '"; json = json + da.detalle[i].DESCRIPCION + @"'
                                    }
                                ],
                                'SellersItemIdentification' : [ 
                                    {
                                        'ID' : [ 
                                            {
                                                'IdentifierContent' : '"; json = json + da.detalle[i].CODIGO + @"'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ],
                        'Price' : [ 
                            {
                                'PriceAmount' : [ 
                                    {
                                        'AmountContent' : '"; json = json + da.detalle[i].PRECIO + @"',
                                        'AmountCurrencyIdentifier' : 'PEN'
                                    }
                                ]
                            }
                        ]
                    },";
            }

            json = json + @"]
            }
        ]
    }
";

            JObject rss = JObject.Parse(json);
            Console.WriteLine(rss.ToString());
            JavaScriptSerializer js = new JavaScriptSerializer(); //system.web.extension assembly....
            string outputJSON = js.Serialize(rss.ToString());
            string outputJSON2 = js.Serialize(outputJSON);
            File.WriteAllText(RUTA+NOMBRE, rss.ToString());

            try
            {
                // Iniciar una tarea: llamar a una función asíncrona en este ejemplo
                Task<string> callTask = Task.Run(() => GetAPIToken(userName,password,Token_URL));
                // Espere a que termine
                callTask.Wait();
                // Obtener el resultado
                TOKEN = callTask.Result;
                
                Task<string> callTask2 = Task.Run(() => postDoucumento(TOKEN, RUTA + NOMBRE, Documento_URL, NOMBRE));
                // Espere a que termine
                callTask2.Wait();
                // Obtener el resultado
                TIKET= callTask2.Result;
                Task<string> callTask3 = Task.Run(() => GetCDR(TIKET, TOKEN, Cdr_URL));
                // Espere a que termine
                callTask3.Wait();
                // Obtener el resultado
                HASH = callTask3.Result;

                Task<string> callTask4 = Task.Run(() => GetXML(TIKET, TOKEN, xml_URL));
                // Espere a que termine
                callTask4.Wait();
                // Obtener el resultado
                XML = callTask4.Result;

                countQuery.Add("TIKET", TIKET);
                countQuery.Add("HASH", HASH);

                
            }
            catch (Exception ex)  //Exceptions here or in the function will be caught here
            {
                countQuery.Add("TIKET", "ERROR DE CONEXION");
                countQuery.Add("HASH", "ERROR DE CONEXION");
            }

            return countQuery;
        }
        public async Task<string> GetXML(string TIKET, string TOKEN, string xml_URL)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //SETUP CLIENTE
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TOKEN);

                    HttpResponseMessage response = await client.GetAsync(xml_URL + TIKET);
                    string responseString = await response.Content.ReadAsStringAsync();

                    // Create the XmlDocument.
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(responseString);
                    
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    // Save the document to a file and auto-indent the output.
                    XmlWriter writer = XmlWriter.Create(rutaRAIZ + "CPE\\CDR\\"+ RUC_EMISOR + "-" + SERIE + ".XML", settings);
                    doc.Save(writer);
                    return responseString.ToString();

                }
                catch (Exception e)
                {
                    return e.Message;
                }


            }
        }
        public async Task<string> GetCDR(string TIKET, string TOKEN, string crd_URL)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //SETUP CLIENTE
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TOKEN);

                    HttpResponseMessage response = await client.GetAsync(Cdr_URL + TIKET);
                    string responseString = await response.Content.ReadAsStringAsync();

                    // Create the XmlDocument.
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(responseString);

                    //Se obtiene el nodo del XML
                    string SERIE = "", TIPO = "", HASH = "";
                    XmlNodeList elemList = doc.GetElementsByTagName("ReferenceID");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        SERIE = (elemList[i].InnerXml);
                    }

                    XmlNodeList elemList2 = doc.GetElementsByTagName("DocumentHash");
                    for (int i = 0; i < elemList2.Count; i++)
                    {
                        HASH = (elemList2[i].InnerXml);
                    }
                    XmlNodeList elemList3 = doc.GetElementsByTagName("DocumentTypeCode");
                    for (int i = 0; i < elemList3.Count; i++)
                    {
                        TIPO = (elemList3[i].InnerXml);
                    }

                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    // Save the document to a file and auto-indent the output.
                    XmlWriter writer = XmlWriter.Create(rutaRAIZ + "CPE\\CDR\\" + "R-" + RUC_EMISOR + "-" + TIPO + "-" + SERIE + ".XML", settings);
                    doc.Save(writer);
                    return HASH.ToString();

                }
                catch(Exception e)
                {
                    return e.Message;
                }
                

            }
        }
        public async Task<string> GetCDR_ANULA(string TIKET, string TOKEN, string crd_URL)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    //SETUP CLIENTE
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TOKEN);

                    HttpResponseMessage response = await client.GetAsync(Cdr_URL + TIKET);
                    string responseString = await response.Content.ReadAsStringAsync();
                    string DESCRIPCION = "", TIPO = "", HASH = "";
                    // Create the XmlDocument.
                    if (responseString =="{\"code\":\"2323\",\"description\":\"Existe documento ya informado anteriormente en una comunicación de baja.\"}")
                    {
                        DESCRIPCION ="eldocumento ya informado anteriormente en una comunicación de baja.";
                    }
                    else
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(responseString);

                        //Se obtiene el nodo del XML
                       
                        XmlNodeList elemList = doc.GetElementsByTagName("Description");
                        for (int i = 0; i < elemList.Count; i++)
                        {
                            DESCRIPCION = (elemList[i].InnerXml);
                        }
                        XmlNodeList elemList3 = doc.GetElementsByTagName("ReferenceID");
                        for (int i = 0; i < elemList3.Count; i++)
                        {
                            TIPO = (elemList3[i].InnerXml);
                        }

                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        // Save the document to a file and auto-indent the output.
                        XmlWriter writer = XmlWriter.Create(rutaRAIZ + "CPE\\ANULACION\\" + "R-" + TIPO + ".XML", settings);
                        doc.Save(writer);
                    }
                    
                    return DESCRIPCION .ToString();

                }
                catch (Exception e)
                {
                    return e.Message;
                }


            }
        }
        public async Task<string> postDoucumento(string token, string  DocRuta, string Documento_URL, string NameDoc)
        {
            using (var client = new HttpClient())
            {
                //SETUP CLIENTE
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);              
                var method = new MultipartFormDataContent();
                try
                {
                    var streamContent = new StreamContent(File.Open(DocRuta, FileMode.Open));
                    method.Add(streamContent, "file",NameDoc);
                    HttpResponseMessage response = await client.PostAsync(Documento_URL, method);
                    var responseString = await response.Content.ReadAsStringAsync();
                    //return responseString;

                    Tiket to = new Tiket();
                    //var numero = jObject[0]["access_token"].ToString();
                    //Console.WriteLine(numero);
                    //string TOKEN = await response.Content.ReadAsStringAsync();
                    to = JsonConvert.DeserializeObject<Tiket>(responseString);
                    TIKET = to.description;
                    return TIKET.ToString();
                }
                catch (Exception e)
                {
                                 
                    return e.Message;
                }
                

            }
        }
        static public async Task<string> GetAPIToken(string userName, string password, string token_URL)
        {
            using (var client = new HttpClient())
            {
                //SETUP CLIENTE
               
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //SETUP LOGIN DATA
                var formContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", userName),
                    new KeyValuePair<string, string>("password", password),
                });
                //ENVIAR REQUEST
                HttpResponseMessage response = await client.PostAsync(token_URL, formContent);
                
                //OBTENER EL ACCESO TOKEN DEL RESPONSE BODY
                //var responseJson = await response.Content.ReadAsStringAsync();
                //var jObject = JObject.Parse(responseJson);
                Token token = new Token();
                //var numero = jObject[0]["access_token"].ToString();
                //Console.WriteLine(numero);
                string TOKEN = await response.Content.ReadAsStringAsync();
                token = JsonConvert.DeserializeObject<Token>(TOKEN);
                TOKEN = token.access_token;
                return TOKEN.ToString();
            }
        }
        public class Token { public string access_token { get; set; } }
        public class Tiket { public string description { get; set; } }
        static void Main(string[] args)
        {
            //reposte r = new reposte();
            //r.Show();

        }

    }
}
