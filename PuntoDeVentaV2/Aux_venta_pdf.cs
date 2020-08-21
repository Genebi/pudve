
using System.Xml.Schema;
using System.Xml.Serialization;



public partial class ComprobanteVenta
{
    private ComprobanteEmisorVenta emisorField;

    private ComprobanteReceptorVenta receptorField;

    private ComprobanteConceptoVenta[] conceptosField;

    private string versionField;

    private string serieField;

    private string folioField;

    private string fechaField;

    private string formaPagoField;

    private bool formaPagoFieldSpecified;
    
    private decimal subTotalField;

    private decimal descuentoField;

    private bool descuentoFieldSpecified;
    
    private decimal totalField;

    private string lugarExpedicionField;

    private decimal anticipoField;





    #region Código auxiliar para la generación del PDF

    public string monedacon_letra
    {
        get
        {
            PuntoDeVentaV2.Moneda_conletra moneda_letra = new PuntoDeVentaV2.Moneda_conletra();

            return moneda_letra.Convertir(Total.ToString("#.00"), true);
        }
    }

    public string imagen
    {
        get
        {
            PuntoDeVentaV2.Carga_logo cargar_imag = new PuntoDeVentaV2.Carga_logo();

            return cargar_imag.cargar_imagen();
        }
    }
    #endregion 




    public ComprobanteVenta()
    {
        this.versionField = "3.3";
    }

    /// <remarks/>
    public ComprobanteEmisorVenta Emisor
    {
        get
        {
            return this.emisorField;
        }
        set
        {
            this.emisorField = value;
        }
    }

    /// <remarks/>
    public ComprobanteReceptorVenta Receptor
    {
        get
        {
            return this.receptorField;
        }
        set
        {
            this.receptorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Concepto", IsNullable = false)]
    public ComprobanteConceptoVenta[] Conceptos
    {
        get
        {
            return this.conceptosField;
        }
        set
        {
            this.conceptosField = value;
        }
    }
    

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Version
    {
        get
        {
            return this.versionField;
        }
        set
        {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Serie
    {
        get
        {
            return this.serieField;
        }
        set
        {
            this.serieField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Folio
    {
        get
        {
            return this.folioField;
        }
        set
        {
            this.folioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Fecha
    {
        get
        {
            return this.fechaField;
        }
        set
        {
            this.fechaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string FormaPago
    {
        get
        {
            return this.formaPagoField;
        }
        set
        {
            formaPagoFieldSpecified = true;
            this.formaPagoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool FormaPagoSpecified
    {
        get
        {
            return this.formaPagoFieldSpecified;
        }
        set
        {
            this.formaPagoFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal SubTotal
    {
        get
        {
            return this.subTotalField;
        }
        set
        {
            this.subTotalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Descuento
    {
        get
        {
            return this.descuentoField;
        }
        set
        {
            descuentoFieldSpecified = true;
            this.descuentoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DescuentoSpecified
    {
        get
        {
            return this.descuentoFieldSpecified;
        }
        set
        {
            this.descuentoFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Total
    {
        get
        {
            return this.totalField;
        }
        set
        {
            this.totalField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string LugarExpedicion
    {
        get
        {
            return this.lugarExpedicionField;
        }
        set
        {
            this.lugarExpedicionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Anticipo
    {
        get
        {
            return this.anticipoField;
        }
        set
        {
            this.anticipoField = value;
        }
    }
}



public partial class ComprobanteEmisorVenta
{
    private string rfcField;

    private string nombreField;

    private string regimenFiscalField;


    public string Rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }

    public string Nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    public string RegimenFiscal
    {
        get
        {
            return this.regimenFiscalField;
        }
        set
        {
            this.regimenFiscalField = value;
        }
    }

    public string DomicilioEmisor { get; set; }

    public string Correo { get; set; }

    public string Telefono { get; set; }
}

public partial class ComprobanteReceptorVenta
{
    private string rfcField;

    private string nombreField;
    

    public string Rfc
    {
        get
        {
            return this.rfcField;
        }
        set
        {
            this.rfcField = value;
        }
    }
    
    public string Nombre
    {
        get
        {
            return this.nombreField;
        }
        set
        {
            this.nombreField = value;
        }
    }

    public string DomicilioReceptor { get; set; }

    public string Correo { get; set; }

    public string Telefono { get; set; }
}



public partial class ComprobanteConceptoVenta
{

    private ComprobanteConceptoImpuestosVenta impuestosField;

    private decimal cantidadField;
    
    private string descripcionField;

    private decimal valorUnitarioField;

    private decimal importeField;

    private decimal descuentoField;

    private bool descuentoFieldSpecified;

    private string porcentajeDescuentoField;

    /// <remarks/>
    public ComprobanteConceptoImpuestosVenta Impuestos
    {
        get
        {
            return this.impuestosField;
        }
        set
        {
            this.impuestosField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Cantidad
    {
        get
        {
            return this.cantidadField;
        }
        set
        {
            this.cantidadField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Descripcion
    {
        get
        {
            return this.descripcionField;
        }
        set
        {
            this.descripcionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal ValorUnitario
    {
        get
        {
            return this.valorUnitarioField;
        }
        set
        {
            this.valorUnitarioField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            this.importeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Descuento
    {
        get
        {
            return this.descuentoField;
        }
        set
        {
            descuentoFieldSpecified = true;
            this.descuentoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool DescuentoSpecified
    {
        get
        {
            return this.descuentoFieldSpecified;
        }
        set
        {
            this.descuentoFieldSpecified = value;
        }
    }

    public string PorcentajeDescuento
    {
        get
        {
            return this.porcentajeDescuentoField;
        }
        set
        {
            this.porcentajeDescuentoField = value;
        }
    }
}

public partial class ComprobanteConceptoImpuestosVenta
{

    private ComprobanteConceptoImpuestosTrasladoVenta[] trasladosField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Traslado", IsNullable = false)]
    public ComprobanteConceptoImpuestosTrasladoVenta[] Traslados
    {
        get
        {
            return this.trasladosField;
        }
        set
        {
            this.trasladosField = value;
        }
    }
}

public partial class ComprobanteConceptoImpuestosTrasladoVenta
{

    private decimal baseField;

    private string impuestoField;

    private string tipoFactorField;

    private decimal tasaOCuotaField;

    private bool tasaOCuotaFieldSpecified;

    private decimal importeField;

    private bool importeFieldSpecified;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Base
    {
        get
        {
            return this.baseField;
        }
        set
        {
            this.baseField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Impuesto
    {
        get
        {
            return this.impuestoField;
        }
        set
        {
            this.impuestoField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string TipoFactor
    {
        get
        {
            return this.tipoFactorField;
        }
        set
        {
            this.tipoFactorField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal TasaOCuota
    {
        get
        {
            return this.tasaOCuotaField;
        }
        set
        {
            tasaOCuotaFieldSpecified = true;
            this.tasaOCuotaField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool TasaOCuotaSpecified
    {
        get
        {
            return this.tasaOCuotaFieldSpecified;
        }
        set
        {
            this.tasaOCuotaFieldSpecified = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Importe
    {
        get
        {
            return this.importeField;
        }
        set
        {
            importeFieldSpecified = true;
            this.importeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool ImporteSpecified
    {
        get
        {
            return this.importeFieldSpecified;
        }
        set
        {
            this.importeFieldSpecified = value;
        }
    }
}