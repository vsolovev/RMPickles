﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace RMPickles.Core.TestFrameworks.XUnit.XUnit2 {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class assemblies {
        
        private assembliesAssembly assemblyField;
        
        /// <remarks/>
        public assembliesAssembly assembly {
            get {
                return this.assemblyField;
            }
            set {
                this.assemblyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class assembliesAssembly {
        
        private object errorsField;
        
        private assembliesAssemblyCollection[] collectionField;
        
        private string nameField;
        
        private string environmentField;
        
        private string testframeworkField;
        
        private System.DateTime rundateField;
        
        private System.DateTime runtimeField;
        
        private string configfileField;

        private int totalField;

        private int passedField;

        private int failedField;

        private int skippedField;

        private decimal timeField;

        private int errors1Field;

        /// <remarks/>
        public object errors {
            get {
                return this.errorsField;
            }
            set {
                this.errorsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("collection")]
        public assembliesAssemblyCollection[] collection {
            get {
                return this.collectionField;
            }
            set {
                this.collectionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string environment {
            get {
                return this.environmentField;
            }
            set {
                this.environmentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("test-framework")]
        public string testframework {
            get {
                return this.testframeworkField;
            }
            set {
                this.testframeworkField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("run-date", DataType="date")]
        public System.DateTime rundate {
            get {
                return this.rundateField;
            }
            set {
                this.rundateField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("run-time", DataType="time")]
        public System.DateTime runtime {
            get {
                return this.runtimeField;
            }
            set {
                this.runtimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("config-file")]
        public string configfile {
            get {
                return this.configfileField;
            }
            set {
                this.configfileField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int passed {
            get {
                return this.passedField;
            }
            set {
                this.passedField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int failed {
            get {
                return this.failedField;
            }
            set {
                this.failedField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int skipped {
            get {
                return this.skippedField;
            }
            set {
                this.skippedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal time {
            get {
                return this.timeField;
            }
            set {
                this.timeField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute("errors")]
        public int errors1 {
            get {
                return this.errors1Field;
            }
            set {
                this.errors1Field = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class assembliesAssemblyCollection {
        
        private assembliesAssemblyCollectionTest[] testField;

        private int totalField;

        private int passedField;

        private int failedField;

        private int skippedField;

        private string nameField;
        
        private decimal timeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("test")]
        public assembliesAssemblyCollectionTest[] test {
            get {
                return this.testField;
            }
            set {
                this.testField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int passed {
            get {
                return this.passedField;
            }
            set {
                this.passedField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int failed {
            get {
                return this.failedField;
            }
            set {
                this.failedField = value;
            }
        }
        
        /// <remarks/>
        [XmlAttribute()]
        public int skipped {
            get {
                return this.skippedField;
            }
            set {
                this.skippedField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal time {
            get {
                return this.timeField;
            }
            set {
                this.timeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class assembliesAssemblyCollectionTest {
        
        private assembliesAssemblyCollectionTestTrait[] traitsField;
        
        private string reasonField;
        
        private assembliesAssemblyCollectionTestFailure failureField;
        
        private string nameField;
        
        private string typeField;
        
        private string methodField;
        
        private decimal timeField;
        
        private string resultField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("trait", IsNullable=false)]
        public assembliesAssemblyCollectionTestTrait[] traits {
            get {
                return this.traitsField;
            }
            set {
                this.traitsField = value;
            }
        }
        
        /// <remarks/>
        public string reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
            }
        }
        
        /// <remarks/>
        public assembliesAssemblyCollectionTestFailure failure {
            get {
                return this.failureField;
            }
            set {
                this.failureField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string method {
            get {
                return this.methodField;
            }
            set {
                this.methodField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal time {
            get {
                return this.timeField;
            }
            set {
                this.timeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string result {
            get {
                return this.resultField;
            }
            set {
                this.resultField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class assembliesAssemblyCollectionTestTrait {
        
        private string nameField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class assembliesAssemblyCollectionTestFailure {
        
        private string messageField;
        
        private string stacktraceField;
        
        private string exceptiontypeField;
        
        /// <remarks/>
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("stack-trace")]
        public string stacktrace {
            get {
                return this.stacktraceField;
            }
            set {
                this.stacktraceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute("exception-type")]
        public string exceptiontype {
            get {
                return this.exceptiontypeField;
            }
            set {
                this.exceptiontypeField = value;
            }
        }
    }
}