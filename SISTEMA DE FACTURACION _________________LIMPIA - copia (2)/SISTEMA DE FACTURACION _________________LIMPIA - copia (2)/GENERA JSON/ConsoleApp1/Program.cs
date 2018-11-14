using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    class Program
    {
        public class Invoice
        {
            public string operacion { get; set; }
            public int tipo_de_comprobante { get; set; }
            public string serie { get; set; }
            public int numero { get; set; }
            public int sunat_transaction { get; set; }
            public int cliente_tipo_de_documento { get; set; }
            public string cliente_numero_de_documento { get; set; }
            public string cliente_denominacion { get; set; }
            public string cliente_direccion { get; set; }
            public string cliente_email { get; set; }
            public string cliente_email_1 { get; set; }
            public string cliente_email_2 { get; set; }
            public DateTime fecha_de_emision { get; set; }
            public DateTime fecha_de_vencimiento { get; set; }
            public int moneda { get; set; }
            public dynamic tipo_de_cambio { get; set; } //? MAKES NATURAL TYPES NULLABLE
            public double porcentaje_de_igv { get; set; }
            public dynamic descuento_global { get; set; }
            public dynamic total_descuento { get; set; }
            public dynamic total_anticipo { get; set; }
            public dynamic total_gravada { get; set; }
            public dynamic total_inafecta { get; set; }
            public dynamic total_exonerada { get; set; }
            public double total_igv { get; set; }
            public dynamic total_gratuita { get; set; }
            public dynamic total_otros_cargos { get; set; }
            public double total { get; set; }
            public dynamic percepcion_tipo { get; set; }
            public dynamic percepcion_base_imponible { get; set; }
            public dynamic total_percepcion { get; set; }
            public dynamic total_incluido_percepcion { get; set; }
            public bool detraccion { get; set; }
            public string observaciones { get; set; }
            public dynamic documento_que_se_modifica_tipo { get; set; }
            public string documento_que_se_modifica_serie { get; set; }
            public dynamic documento_que_se_modifica_numero { get; set; }
            public dynamic tipo_de_nota_de_credito { get; set; }
            public dynamic tipo_de_nota_de_debito { get; set; }
            public bool enviar_automaticamente_a_la_sunat { get; set; }
            public bool enviar_automaticamente_al_cliente { get; set; }
            public string codigo_unico { get; set; }
            public string condiciones_de_pago { get; set; }
            public string medio_de_pago { get; set; }
            public string placa_vehiculo { get; set; }
            public string orden_compra_servicio { get; set; }
            public string tabla_personalizada_codigo { get; set; }
            public string formato_de_pdf { get; set; }
            public List<Items> items { get; set; }
         
        }
        public class Items
        {
            public string unidad_de_medida { get; set; }
            public string codigo { get; set; }
            public string descripcion { get; set; }
            public double cantidad { get; set; }
            public double valor_unitario { get; set; }
            public double precio_unitario { get; set; }
            public dynamic descuento { get; set; }
            public double subtotal { get; set; }
            public int tipo_de_igv { get; set; }
            public double igv { get; set; }
            public double total { get; set; }
            public bool anticipo_regularizacion { get; set; }
            public dynamic anticipo_comprobante_serie { get; set; }
            public dynamic anticipo_comprobante_numero { get; set; }
        }


        static void Main(string[] args)
        {
            List<string> listAccounts = new List<string>();
            
            var x = new
            {
                _D = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2",
                _S = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2",
                _B = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2",
                _E = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2",
                
                 Invoice = new[]
                            {
                                new
                                {
                                    UBLVersionID = new []
                                    {
                                        new {
                                                IdentifierContent = "2.1"//Versión del UBL
                                               
                                            }
                                    } ,
                                    CustomizationID = new []
                                    {
                                        new {
                                                IdentifierContent = "2.0"//Versión de la estructura del documento
                                               
                                            }
                                    } ,
                                    ProfileID = new []
                                    {
                                        new {
                                                  IdentifierContent= "0101",//Código de tipo de operación Según Catalogo 51 de Sunat
                                                  IdentificationSchemeAgencyNameText= "PE:SUNAT",
                                                  IdentificationSchemeUniformResourceIdentifier= "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo17",
                                                  IdentificationSchemeNameText= "SUNAT:Identificador de Tipo de Operación"
                                            }
                                    } ,
                                    ID = new []
                                    {
                                        new {
                                                  IdentifierContent= "B001-1"//Serie y número del comprobante FORMATO: B### - NNNNNNNN (Serie: 4 dígitos, Numero de comprobante: Max. 8 dígitos.).
                                                  
                                            }
                                    } ,
                                    IssueDate = new []
                                    {
                                        new {
                                                  DateContent= "2018-10-11"//  "Fecha de emisión FORMATO: yyyy-mm-dd                                       "__comment__9": "Fecha de emisión",
                                                  
                                            }
                                    } ,
                                    IssueTime = new []
                                    {
                                        new
                                        {
                                            DateTimeContent= "16:01:53"//Hora de emisión FORMATO: hh-mm-ss.0z
                                              
                                        }
                                    },
                                    InvoiceTypeCode = new []
                                    {
                                        new
                                        {
                                              CodeContent= "03",//Código de tipo de documento – Según Catalogo 01 de Sunat
                                              CodeListSchemeUniformResourceIdentifier= "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo01",
                                              CodeListNameText= "SUNAT:Identificador de Tipo de Documento",
                                              CodeListAgencyNameText= "PE:SUNAT"

                                        }
                                    },
                                    Note = new []
                                    {
                                        new
                                        {
                                             TextContent= "CIENTO DIECIOCHO Y 00/100",//Monto en letras
                                             LanguageLocaleIdentifier= "1000"//Código de leyenda – Según catalogo 52 de Sunat
                                            
                                        }
                                    },
                                    DocumentCurrencyCode = new []
                                    {
                                        new
                                        {
                                              CodeContent= "PEN",//Tipo de moneda en la cual se emite la boleta electrónica
                                              CodeListNameText= "Currency",
                                              CodeListAgencyNameText= "United Nations Economic Commission for Europe",
                                              CodeListIdentifier= "ISO 4217 Alpha"
                                        }
                                    },
                                    LineCountNumeric = new []
                                    {
                                        new
                                        {
                                            NumericContent= 1//Cantidad de ítems de la boleta
                                        }
                                    },
                                    Signature = new []
                                    {
                                        new
                                        {
                                            ID = new []
                                            { new
                                                {
                                                    IdentifierContent= "IDSignature"
                                                }
                                            },
                                            SignatoryParty = new []
                                            {
                                                new
                                                {
                                                    PartyIdentification = new []
                                                    {
                                                        new
                                                        {
                                                            ID = new []
                                                            {
                                                                new
                                                                {
                                                                    IdentifierContent= "20505161051"//RUC de la empresa que esta emitiendo el documento
                                                                    
                                                                }
                                                            }
                                                        }
                                                    },
                                                    PartyName = new []
                                                    {
                                                        new
                                                        {
                                                            Name = new []
                                                            {
                                                                new
                                                                {
                                                                    TextContent= "SCIMIC TECHNOLOGIES"// Nombre de la Razón Social que emite el documento

                                                                }
                                                            }
                                                        }
                                                    }

                                                }
                                            },
                                            DigitalSignatureAttachment = new []
                                            {
                                                new
                                                {
                                                    ExternalReference = new []
                                                    {
                                                        new
                                                        {
                                                            URI = new []
                                                            {
                                                                new
                                                                {
                                                                     TextContent= "IDSignature"
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                    },
                                    AccountingSupplierParty = new []
                                    {
                                        new
                                        {
                                            PartyName = new []
                                            {
                                                new
                                                {
                                                    Name = new []
                                                    {
                                                        new
                                                        {
                                                            TextContent= "SCIMIC TECHNOLOGIES"//Nombre Comercial del emisor
                                                        }
                                                    }

                                                }
                                            },
                                            PartyTaxScheme = new []
                                            {
                                                new
                                                {
                                                    RegistrationName = new []
                                                    {
                                                        new
                                                        {
                                                             TextContent= "EFACT S.A.C."//Nombre o razón social del emisor
                                                        }
                                                    },
                                                    CompanyID = new []
                                                    {
                                                        new
                                                        {
                                                              IdentifierContent= "20505161051",// Número de RUC del emisor
                                                              IdentificationSchemeIdentifier= "6",//Tipo de Documento de Identidad del Emisor
                                                              IdentificationSchemeNameText= "SUNAT:Identificador de Documento de Identidad",
                                                              IdentificationSchemeAgencyNameText= "PE:SUNAT",
                                                              IdentificationSchemeUniformResourceIdentifier= "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"

                                                        }
                                                    },
                                                    RegistrationAddress = new []
                                                    {
                                                        new
                                                        {
                                                            AddressTypeCode = new []
                                                            {
                                                                new
                                                                {
                                                                    CodeContent= "0001"//Domicilio fiscal o de local anexo del emisor - Código del domicilio fiscal o de local anexo del emisor
                                                                }
                                                            },
                                                            TaxScheme = new []
                                                            {
                                                                new
                                                                {
                                                                    ID= new []
                                                                    {
                                                                        new
                                                                        {
                                                                            IdentifierContent= "-"
                                                                        }
                                                                    }

                                                                }
                                                            }

                                                        }
                                                    }

                                                }
                                            }

                                        }
                                    },
                                    AccountingCustomerParty = new []
                                    {
                                        new
                                        {
                                            Party = new []
                                            {
                                                new
                                                {
                                                    PartyTaxScheme = new []
                                                    {
                                                        new
                                                        {
                                                            RegistrationName = new[]
                                                            {
                                                                new
                                                                {
                                                                    TextContent= "Films"//Nombre o razón social del adquirente o usuario
                                                                }
                                                            },
                                                            CompanyID = new []
                                                            {
                                                                new
                                                                {
                                                                      IdentifierContent= "70471447",//Número de adquirente o usuario
                                                                      IdentificationSchemeIdentifier= "1",//Tipo de Documento de Identidad del Emisor
                                                                      IdentificationSchemeNameText= "SUNAT:Identificador de Documento de Identidad",
                                                                      IdentificationSchemeAgencyNameText= "PE:SUNAT",
                                                                      IdentificationSchemeUniformResourceIdentifier= "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo06"


                                                                }
                                                            },
                                                            RegistrationAddress = new []
                                                            {
                                                                new
                                                                {
                                                                    AddressTypeCode = new []
                                                                    {
                                                                        new
                                                                        {
                                                                            CodeContent= "0001"//Inicio de AddressTypeCode
                                                                        }
                                                                    }

                                                                }
                                                            },
                                                            TaxScheme = new []
                                                            {
                                                                new
                                                                {
                                                                    ID = new []
                                                                    {
                                                                        new
                                                                        {
                                                                            IdentifierContent= "-"
                                                                        }
                                                                    }

                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                    },
                                    TaxTotal = new []
                                    {
                                        new
                                        {
                                            TaxAmount = new []
                                            {
                                                new
                                                {
                                                    AmountContent= "18.00",//Monto total del impuesto
                                                    AmountCurrencyIdentifier= "PEN" // Código de tipo de moneda del monto total del impuesto
                                                }
                                            },
                                            TaxSubtotal = new []
                                            {
                                                new
                                                {
                                                    TaxableAmount = new []
                                                    {
                                                        new
                                                        {
                                                            AmountContent= "100.00",//Monto las operaciones gravadas/exoneradas/inafectas del impuesto
                                                            AmountCurrencyIdentifier= "PEN" // Código de tipo de moneda del monto de las operaciones gravadas/exoneradas/inafectas del impuesto
                                                        }
                                                    },
                                                    TaxAmount = new []
                                                    {
                                                        new
                                                        {
                                                            AmountContent= "18.00", // Monto total del impuesto
                                                            AmountCurrencyIdentifier= "PEN" // Código de tipo de moneda del monto total del impuesto

                                                        }
                                                    },
                                                    TaxCategory = new []
                                                    {
                                                        new
                                                        {
                                                            ID = new []
                                                            {
                                                                new
                                                                {

                                                                      IdentifierContent= "S",//ID de la categoría de impuestos
                                                                      IdentificationSchemeIdentifier= "UN/ECE 5305",
                                                                      IdentificationSchemeNameText= "Tax Category Identifier",
                                                                      IdentificationSchemeAgencyNameText= "United Nations Economic Commission for Europe"
                                                                }
                                                            },
                                                            TaxScheme = new []
                                                            {
                                                                new
                                                                {
                                                                    ID= new []
                                                                    {
                                                                        new
                                                                        {

                                                                          IdentifierContent= "1000",//Código de tributo
                                                                          IdentificationSchemeIdentifier= "UN/ECE 5153",
                                                                          IdentificationSchemeAgencyIdentifier= "6"
                                                                        }
                                                                    },
                                                                    Name= new []
                                                                    {
                                                                        new
                                                                        {
                                                                            TextContent= "IGV"//Nombre de tributo
                                                                        }
                                                                    },
                                                                    TaxTypeCode = new []
                                                                    {
                                                                        new
                                                                        {
                                                                            CodeContent= "VAT"//Código internacional tributo
                                                                        }
                                                                    }

                                                                }
                                                            }


                                                        }
                                                    }

                                                }

                                            }

                                        }
                                    },
                                    LegalMonetaryTotal = new []
                                    {
                                        new
                                        {
                                            LineExtensionAmount = new []
                                            {
                                                new
                                                {
                                                    AmountContent= "100.00",//Total valor de venta
                                                    AmountCurrencyIdentifier= "PEN"
                                                }
                                            },
                                            TaxInclusiveAmount = new []
                                            {
                                                new
                                                {
                                                   AmountContent= "118.00",// Total precio de venta (incluye impuestos)
                                                   AmountCurrencyIdentifier= "PEN"
                                                }
                                            },
                                            PayableAmount= new []
                                            {
                                                new
                                                {
                                                    AmountContent= "118.00",// Importe total de la venta, cesión en uso o del servicio prestado
                                                    AmountCurrencyIdentifier= "PEN"
                                                }
                                            }
                                        }
                                    },                                  
                                    InvoiceLine = new []
                                    {

                                        new // lista de items  1 a mas
                                        {
                                            //ID = new []
                                            //{
                                            //    new
                                            //    {
                                            //        IdentifierContent= 1//Número de orden del Ítem
                                            //    }
                                            //},
                                            //InvoicedQuantity= new []
                                            //{
                                            //    new
                                            //    {
                                            //         QuantityContent= 1,//Cantidad de unidades del ítem
                                            //         QuantityUnitCode= "CMT",//Código de unidad de medida del ítem según catalogo 03
                                            //         QuantityUnitCodeListAgencyNameText= "United Nations Economic Commission for Europe",
                                            //         QuantityUnitCodeListIdentifier= "UN/ECE rec 20"
                                            //    }
                                            //},
                                            //LineExtensionAmount= new []
                                            //{
                                            //    new
                                            //    {
                                            //        AmountContent= "100.00",//Valor de venta del ítem SIN IGV
                                            //        AmountCurrencyIdentifier= "PEN"//Código de tipo de moneda del valor de venta del ítem
                                            //    }
                                            //},
                                            //PricingReference= new []
                                            //{
                                            //    new
                                            //    {
                                            //        AlternativeConditionPrice = new []
                                            //        {
                                            //            new
                                            //            {
                                            //                PriceAmount = new []
                                            //                {
                                            //                    new
                                            //                    {
                                            //                        AmountContent= "118.00",//Precio de venta unitario/ Valor referencial unitario en operaciones no onerosas CON IGV
                                            //                        AmountCurrencyIdentifier= "PEN"// Código de tipo de moneda del precio de venta unitario o valor referencial unitario
                                            //                    }
                                            //                },
                                            //                PriceTypeCode = new []
                                            //                {
                                            //                    new
                                            //                    {
                                            //                          CodeContent= "01", // Código de tipo de precio
                                            //                          CodeListNameText= "SUNAT:Indicador de Tipo de Precio",
                                            //                          CodeListAgencyNameText= "PE:SUNAT",
                                            //                          CodeListUniformResourceIdentifier= "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo16"

                                            //                    }
                                            //                }


                                            //            }
                                            //        }

                                            //    }
                                            //},
                                            //TaxTotal= new []
                                            //{
                                            //    new
                                            //    {
                                            //        TaxAmount = new []
                                            //        {
                                            //            new
                                            //            {
                                            //               AmountContent= "18.00",//Monto de tributo del ítem
                                            //               AmountCurrencyIdentifier= "PEN"
                                            //            }
                                            //        },
                                            //        TaxSubtotal = new []
                                            //        {
                                            //            new
                                            //            {
                                            //                TaxableAmount = new []
                                            //                {
                                            //                    new
                                            //                    {
                                            //                        AmountContent= "118.00",//Monto de la operación
                                            //                        AmountCurrencyIdentifier= "PEN"
                                            //                    }
                                            //                },
                                            //                TaxAmount= new []
                                            //                {
                                            //                    new
                                            //                    {
                                            //                        AmountContent= "18.00", // Monto de tributo del ítem
                                            //                        AmountCurrencyIdentifier= "PEN"
                                            //                    }
                                            //                },
                                            //                TaxCategory= new []
                                            //                {
                                            //                    new
                                            //                    {
                                            //                        ID = new []
                                            //                        {
                                            //                            new
                                            //                            {
                                            //                                  IdentifierContent= "S",//Código de Categoría de impuestos
                                            //                                  IdentificationSchemeIdentifier= "UN/ECE 5305",
                                            //                                  IdentificationSchemeNameText="Tax Category Identifier",
                                            //                                  IdentificationSchemeAgencyNameText="United Nations Economic Commission for Europe"
                                            //                            }
                                            //                        },
                                            //                        TaxExemptionReasonCode= new []
                                            //                        {
                                            //                            new
                                            //                            {
                                            //                              CodeContent= "10",//Código de tipo de afectación del IGV
                                            //                              CodeListAgencyNameText= "PE:SUNAT",
                                            //                              CodeListNameText= "SUNAT:Codigo de Tipo de Afectación del IGV",
                                            //                              CodeListUniformResourceIdentifier= "urn:pe:gob:sunat:cpe:see:gem:catalogos:catalogo07"
                                            //                            }
                                            //                        },
                                            //                        TaxScheme= new []
                                            //                        {
                                            //                            new
                                            //                            {
                                            //                                ID= new []
                                            //                                {
                                            //                                    new
                                            //                                    {
                                            //                                          IdentifierContent= "1000",//Código internacional tributo
                                            //                                          IdentificationSchemeIdentifier= "UN/ECE 5153",
                                            //                                          IdentificationSchemeAgencyIdentifier= "6"
                                            //                                    }

                                            //                                },
                                            //                                Name= new []
                                            //                                {
                                            //                                    new
                                            //                                    {
                                            //                                        TextContent= "IGV"//Nombre de tributo
                                            //                                    }
                                            //                                },
                                            //                                TaxTypeCode = new []
                                            //                                {
                                            //                                    new
                                            //                                    {
                                            //                                        CodeContent= "VAT"//Código del tributo
                                            //                                    }
                                            //                                }

                                            //                            }
                                            //                        }
                                            //                    }
                                            //                }

                                            //            }
                                            //        }

                                            //    }
                                            //},
                                            //Item= new []
                                            //{
                                            //    new
                                            //    {
                                            //        Description = new []
                                            //        {
                                            //            new
                                            //            {
                                            //                TextContent= "DESC"//Descripción detallada del servicio prestado, bien vendido o cedido en uso, indicando las características
                                            //            }
                                            //        },
                                            //        SellersItemIdentification= new []
                                            //        {
                                            //            new
                                            //            {
                                            //                ID = new []
                                            //                {
                                            //                    new
                                            //                    {
                                            //                        IdentifierContent= "COD"//Código de producto del ítem
                                            //                    }
                                            //                }

                                            //            }
                                            //        }

                                            //    }
                                            //},
                                            //Price= new []
                                            //{
                                            //    new
                                            //    {
                                            //        PriceAmount = new []
                                            //        {
                                            //            new
                                            //            {
                                            //                AmountContent= "100.00",//Valor unitario del ítem
                                            //                AmountCurrencyIdentifier= "PEN"//Código de tipo de moneda del valor unitario del ítem
                                            //            }
                                            //        }

                                            //    }
                                            //}
                                        },
                                       
                                    }
                                }
                            }
                
            };
            

            JavaScriptSerializer js = new JavaScriptSerializer(); //system.web.extension assembly....
            string outputJSON = js.Serialize(x);
            js.DeserializeObject("");

            

            string outputJSON2 = js.Serialize(outputJSON);
            File.WriteAllText("ss.json", outputJSON2);
            

        }
    }
}
