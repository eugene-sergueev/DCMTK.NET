﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace DCMTK.Serialization
{ // 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot("file-format", IsNullable=false)]
    public partial class fileformat {
    
        private metaheader metaheaderField;
    
        private dataset datasetField;
    
        /// <remarks/>
        [XmlElement("meta-header")]
        public metaheader metaheader {
            get {
                return this.metaheaderField;
            }
            set {
                this.metaheaderField = value;
            }
        }
    
        /// <remarks/>
        [XmlElement("data-set")]
        public dataset dataset {
            get {
                return this.datasetField;
            }
            set {
                this.datasetField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot("meta-header", IsNullable=false)]
    public partial class metaheader {
    
        private element[] elementField;
    
        private string xferField;
    
        private string nameField;
    
        /// <remarks/>
        [XmlElement("element")]
        public element[] element {
            get {
                return this.elementField;
            }
            set {
                this.elementField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string xfer {
            get {
                return this.xferField;
            }
            set {
                this.xferField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot(IsNullable=false)]
    public partial class element {
    
        private elementBinary binaryField;
    
        private string lenField;
    
        private elementLoaded loadedField;
    
        private string nameField;
    
        private string tagField;
    
        private string vmField;
    
        private string vrField;
    
        private string valueField;
    
        public element() {
            this.binaryField = elementBinary.no;
            this.loadedField = elementLoaded.yes;
        }
    
        /// <remarks/>
        [XmlAttribute()]
        [System.ComponentModel.DefaultValueAttribute(elementBinary.no)]
        public elementBinary binary {
            get {
                return this.binaryField;
            }
            set {
                this.binaryField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string len {
            get {
                return this.lenField;
            }
            set {
                this.lenField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        [System.ComponentModel.DefaultValueAttribute(elementLoaded.yes)]
        public elementLoaded loaded {
            get {
                return this.loadedField;
            }
            set {
                this.loadedField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        public string tag {
            get {
                return this.tagField;
            }
            set {
                this.tagField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string vm {
            get {
                return this.vmField;
            }
            set {
                this.vmField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string vr {
            get {
                return this.vrField;
            }
            set {
                this.vrField = value;
            }
        }
    
        /// <remarks/>
        [XmlText()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [XmlType(AnonymousType=true)]
    public enum elementBinary {
    
        /// <remarks/>
        yes,
    
        /// <remarks/>
        no,
    
        /// <remarks/>
        hidden,
    
        /// <remarks/>
        base64,
    
        /// <remarks/>
        file,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [XmlType(AnonymousType=true)]
    public enum elementLoaded {
    
        /// <remarks/>
        yes,
    
        /// <remarks/>
        no,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot("data-set", IsNullable=false)]
    public partial class dataset {
    
        private object[] itemsField;
    
        private string xferField;
    
        private string nameField;
    
        /// <remarks/>
        [XmlElement("element", typeof(element))]
        [XmlElement("sequence", typeof(sequence))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string xfer {
            get {
                return this.xferField;
            }
            set {
                this.xferField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot(IsNullable=false)]
    public partial class sequence {
    
        private object[] itemsField;
    
        private string cardField;
    
        private string lenField;
    
        private string nameField;
    
        private string tagField;
    
        private string vrField;
    
        /// <remarks/>
        [XmlElement("item", typeof(item))]
        [XmlElement("pixel-item", typeof(pixelitem))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string card {
            get {
                return this.cardField;
            }
            set {
                this.cardField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string len {
            get {
                return this.lenField;
            }
            set {
                this.lenField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        public string tag {
            get {
                return this.tagField;
            }
            set {
                this.tagField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string vr {
            get {
                return this.vrField;
            }
            set {
                this.vrField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot(IsNullable=false)]
    public partial class item {
    
        private object[] itemsField;
    
        private string cardField;
    
        private string lenField;
    
        private string offsetField;
    
        /// <remarks/>
        [XmlElement("element", typeof(element))]
        [XmlElement("sequence", typeof(sequence))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string card {
            get {
                return this.cardField;
            }
            set {
                this.cardField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string len {
            get {
                return this.lenField;
            }
            set {
                this.lenField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string offset {
            get {
                return this.offsetField;
            }
            set {
                this.offsetField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType=true)]
    [XmlRoot("pixel-item", IsNullable=false)]
    public partial class pixelitem {
    
        private string cdataField;
    
        private pixelitemBinary binaryField;
    
        private string lenField;
    
        private pixelitemLoaded loadedField;
    
        public pixelitem() {
            this.binaryField = pixelitemBinary.yes;
            this.loadedField = pixelitemLoaded.yes;
        }
    
        /// <remarks/>
        public string cdata {
            get {
                return this.cdataField;
            }
            set {
                this.cdataField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        [System.ComponentModel.DefaultValueAttribute(pixelitemBinary.yes)]
        public pixelitemBinary binary {
            get {
                return this.binaryField;
            }
            set {
                this.binaryField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute(DataType="NMTOKEN")]
        public string len {
            get {
                return this.lenField;
            }
            set {
                this.lenField = value;
            }
        }
    
        /// <remarks/>
        [XmlAttribute()]
        [System.ComponentModel.DefaultValueAttribute(pixelitemLoaded.yes)]
        public pixelitemLoaded loaded {
            get {
                return this.loadedField;
            }
            set {
                this.loadedField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [XmlType(AnonymousType=true)]
    public enum pixelitemBinary {
    
        /// <remarks/>
        yes,
    
        /// <remarks/>
        hidden,
    
        /// <remarks/>
        base64,
    
        /// <remarks/>
        file,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [XmlType(AnonymousType=true)]
    public enum pixelitemLoaded {
    
        /// <remarks/>
        yes,
    
        /// <remarks/>
        no,
    }
}