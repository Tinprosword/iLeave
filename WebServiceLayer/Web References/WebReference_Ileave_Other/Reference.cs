﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// 此源代码是由 Microsoft.VSDesigner 4.0.30319.42000 版自动生成。
// 
#pragma warning disable 1591

namespace WebServiceLayer.WebReference_Ileave_Other {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ILeave_OtherSoap", Namespace="http://tempuri.org/")]
    public partial class ILeave_Other : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback Test_GetListOperationCompleted;
        
        private System.Threading.SendOrPostCallback Announce_GetAnnouncementByFirstEidOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public ILeave_Other() {
            this.Url = global::WebServiceLayer.Properties.Settings.Default.WebServiceLayer_WebReference_Ileave_Other_ILeave_Other;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event Test_GetListCompletedEventHandler Test_GetListCompleted;
        
        /// <remarks/>
        public event Announce_GetAnnouncementByFirstEidCompletedEventHandler Announce_GetAnnouncementByFirstEidCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Test_GetList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] Test_GetList() {
            object[] results = this.Invoke("Test_GetList", new object[0]);
            return ((string[])(results[0]));
        }
        
        /// <remarks/>
        public void Test_GetListAsync() {
            this.Test_GetListAsync(null);
        }
        
        /// <remarks/>
        public void Test_GetListAsync(object userState) {
            if ((this.Test_GetListOperationCompleted == null)) {
                this.Test_GetListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTest_GetListOperationCompleted);
            }
            this.InvokeAsync("Test_GetList", new object[0], this.Test_GetListOperationCompleted, userState);
        }
        
        private void OnTest_GetListOperationCompleted(object arg) {
            if ((this.Test_GetListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Test_GetListCompleted(this, new Test_GetListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Announce_GetAnnouncementByFirstEid", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public t_Announcement[] Announce_GetAnnouncementByFirstEid(int firstEID) {
            object[] results = this.Invoke("Announce_GetAnnouncementByFirstEid", new object[] {
                        firstEID});
            return ((t_Announcement[])(results[0]));
        }
        
        /// <remarks/>
        public void Announce_GetAnnouncementByFirstEidAsync(int firstEID) {
            this.Announce_GetAnnouncementByFirstEidAsync(firstEID, null);
        }
        
        /// <remarks/>
        public void Announce_GetAnnouncementByFirstEidAsync(int firstEID, object userState) {
            if ((this.Announce_GetAnnouncementByFirstEidOperationCompleted == null)) {
                this.Announce_GetAnnouncementByFirstEidOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAnnounce_GetAnnouncementByFirstEidOperationCompleted);
            }
            this.InvokeAsync("Announce_GetAnnouncementByFirstEid", new object[] {
                        firstEID}, this.Announce_GetAnnouncementByFirstEidOperationCompleted, userState);
        }
        
        private void OnAnnounce_GetAnnouncementByFirstEidOperationCompleted(object arg) {
            if ((this.Announce_GetAnnouncementByFirstEidCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Announce_GetAnnouncementByFirstEidCompleted(this, new Announce_GetAnnouncementByFirstEidCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class t_Announcement {
        
        private int idField;
        
        private int typeIDField;
        
        private string subjectField;
        
        private string contentField;
        
        private System.DateTime validDateFromField;
        
        private System.DateTime validDateToField;
        
        private int sortSeqField;
        
        private bool isOnTopField;
        
        private byte statusField;
        
        private System.DateTime createDateField;
        
        private int createUserField;
        
        private System.DateTime modifiedDateField;
        
        private int modifiedUserField;
        
        /// <remarks/>
        public int ID {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public int TypeID {
            get {
                return this.typeIDField;
            }
            set {
                this.typeIDField = value;
            }
        }
        
        /// <remarks/>
        public string Subject {
            get {
                return this.subjectField;
            }
            set {
                this.subjectField = value;
            }
        }
        
        /// <remarks/>
        public string Content {
            get {
                return this.contentField;
            }
            set {
                this.contentField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ValidDateFrom {
            get {
                return this.validDateFromField;
            }
            set {
                this.validDateFromField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ValidDateTo {
            get {
                return this.validDateToField;
            }
            set {
                this.validDateToField = value;
            }
        }
        
        /// <remarks/>
        public int SortSeq {
            get {
                return this.sortSeqField;
            }
            set {
                this.sortSeqField = value;
            }
        }
        
        /// <remarks/>
        public bool IsOnTop {
            get {
                return this.isOnTopField;
            }
            set {
                this.isOnTopField = value;
            }
        }
        
        /// <remarks/>
        public byte Status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime CreateDate {
            get {
                return this.createDateField;
            }
            set {
                this.createDateField = value;
            }
        }
        
        /// <remarks/>
        public int CreateUser {
            get {
                return this.createUserField;
            }
            set {
                this.createUserField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime ModifiedDate {
            get {
                return this.modifiedDateField;
            }
            set {
                this.modifiedDateField = value;
            }
        }
        
        /// <remarks/>
        public int ModifiedUser {
            get {
                return this.modifiedUserField;
            }
            set {
                this.modifiedUserField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void Test_GetListCompletedEventHandler(object sender, Test_GetListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Test_GetListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Test_GetListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    public delegate void Announce_GetAnnouncementByFirstEidCompletedEventHandler(object sender, Announce_GetAnnouncementByFirstEidCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1055.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Announce_GetAnnouncementByFirstEidCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Announce_GetAnnouncementByFirstEidCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public t_Announcement[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((t_Announcement[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591