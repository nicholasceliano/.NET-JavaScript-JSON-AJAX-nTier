﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18047
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.18047.
// 
#pragma warning disable 1591

namespace GHGPortal.Web.BulkOrderMove {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="CommonWebServiceSoap", Namespace="http://stt2.ihess.com/PhysDealManager/WebServices/")]
    public partial class CommonWebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetResolutionProceduresOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetProcessStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGoodsMovementDetailsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetGoodsMovementPostingStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetBulkOrderGoodsMovementPostingStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteAllBulkOrdersOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteBulkOrdersOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public CommonWebService() {
            this.Url = global::GHGPortal.Web.Properties.Settings.Default.GHGPortal_Web_BulkOrderMove_CommonWebService;
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
        public event GetResolutionProceduresCompletedEventHandler GetResolutionProceduresCompleted;
        
        /// <remarks/>
        public event GetProcessStatusCompletedEventHandler GetProcessStatusCompleted;
        
        /// <remarks/>
        public event GetGoodsMovementDetailsCompletedEventHandler GetGoodsMovementDetailsCompleted;
        
        /// <remarks/>
        public event GetGoodsMovementPostingStatusCompletedEventHandler GetGoodsMovementPostingStatusCompleted;
        
        /// <remarks/>
        public event GetBulkOrderGoodsMovementPostingStatusCompletedEventHandler GetBulkOrderGoodsMovementPostingStatusCompleted;
        
        /// <remarks/>
        public event DeleteAllBulkOrdersCompletedEventHandler DeleteAllBulkOrdersCompleted;
        
        /// <remarks/>
        public event DeleteBulkOrdersCompletedEventHandler DeleteBulkOrdersCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/GetResolutionProcedures", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetResolutionProcedures(string contextKey) {
            object[] results = this.Invoke("GetResolutionProcedures", new object[] {
                        contextKey});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetResolutionProceduresAsync(string contextKey) {
            this.GetResolutionProceduresAsync(contextKey, null);
        }
        
        /// <remarks/>
        public void GetResolutionProceduresAsync(string contextKey, object userState) {
            if ((this.GetResolutionProceduresOperationCompleted == null)) {
                this.GetResolutionProceduresOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetResolutionProceduresOperationCompleted);
            }
            this.InvokeAsync("GetResolutionProcedures", new object[] {
                        contextKey}, this.GetResolutionProceduresOperationCompleted, userState);
        }
        
        private void OnGetResolutionProceduresOperationCompleted(object arg) {
            if ((this.GetResolutionProceduresCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetResolutionProceduresCompleted(this, new GetResolutionProceduresCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/GetProcessStatus", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetProcessStatus(string processCode) {
            object[] results = this.Invoke("GetProcessStatus", new object[] {
                        processCode});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetProcessStatusAsync(string processCode) {
            this.GetProcessStatusAsync(processCode, null);
        }
        
        /// <remarks/>
        public void GetProcessStatusAsync(string processCode, object userState) {
            if ((this.GetProcessStatusOperationCompleted == null)) {
                this.GetProcessStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetProcessStatusOperationCompleted);
            }
            this.InvokeAsync("GetProcessStatus", new object[] {
                        processCode}, this.GetProcessStatusOperationCompleted, userState);
        }
        
        private void OnGetProcessStatusOperationCompleted(object arg) {
            if ((this.GetProcessStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetProcessStatusCompleted(this, new GetProcessStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/GetGoodsMovementDetails", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetGoodsMovementDetails(string contextKey) {
            object[] results = this.Invoke("GetGoodsMovementDetails", new object[] {
                        contextKey});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetGoodsMovementDetailsAsync(string contextKey) {
            this.GetGoodsMovementDetailsAsync(contextKey, null);
        }
        
        /// <remarks/>
        public void GetGoodsMovementDetailsAsync(string contextKey, object userState) {
            if ((this.GetGoodsMovementDetailsOperationCompleted == null)) {
                this.GetGoodsMovementDetailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGoodsMovementDetailsOperationCompleted);
            }
            this.InvokeAsync("GetGoodsMovementDetails", new object[] {
                        contextKey}, this.GetGoodsMovementDetailsOperationCompleted, userState);
        }
        
        private void OnGetGoodsMovementDetailsOperationCompleted(object arg) {
            if ((this.GetGoodsMovementDetailsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGoodsMovementDetailsCompleted(this, new GetGoodsMovementDetailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/GetGoodsMovementPostingStatus", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GetGoodsMovementPostingStatus(ref SAPBulkOrder[] sapBulkOrders, string errorMessage) {
            object[] results = this.Invoke("GetGoodsMovementPostingStatus", new object[] {
                        sapBulkOrders,
                        errorMessage});
            sapBulkOrders = ((SAPBulkOrder[])(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void GetGoodsMovementPostingStatusAsync(SAPBulkOrder[] sapBulkOrders, string errorMessage) {
            this.GetGoodsMovementPostingStatusAsync(sapBulkOrders, errorMessage, null);
        }
        
        /// <remarks/>
        public void GetGoodsMovementPostingStatusAsync(SAPBulkOrder[] sapBulkOrders, string errorMessage, object userState) {
            if ((this.GetGoodsMovementPostingStatusOperationCompleted == null)) {
                this.GetGoodsMovementPostingStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetGoodsMovementPostingStatusOperationCompleted);
            }
            this.InvokeAsync("GetGoodsMovementPostingStatus", new object[] {
                        sapBulkOrders,
                        errorMessage}, this.GetGoodsMovementPostingStatusOperationCompleted, userState);
        }
        
        private void OnGetGoodsMovementPostingStatusOperationCompleted(object arg) {
            if ((this.GetGoodsMovementPostingStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetGoodsMovementPostingStatusCompleted(this, new GetGoodsMovementPostingStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/GetBulkOrderGoodsMovementPostin" +
            "gStatus", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool GetBulkOrderGoodsMovementPostingStatus(string bulkOrderNum) {
            object[] results = this.Invoke("GetBulkOrderGoodsMovementPostingStatus", new object[] {
                        bulkOrderNum});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void GetBulkOrderGoodsMovementPostingStatusAsync(string bulkOrderNum) {
            this.GetBulkOrderGoodsMovementPostingStatusAsync(bulkOrderNum, null);
        }
        
        /// <remarks/>
        public void GetBulkOrderGoodsMovementPostingStatusAsync(string bulkOrderNum, object userState) {
            if ((this.GetBulkOrderGoodsMovementPostingStatusOperationCompleted == null)) {
                this.GetBulkOrderGoodsMovementPostingStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetBulkOrderGoodsMovementPostingStatusOperationCompleted);
            }
            this.InvokeAsync("GetBulkOrderGoodsMovementPostingStatus", new object[] {
                        bulkOrderNum}, this.GetBulkOrderGoodsMovementPostingStatusOperationCompleted, userState);
        }
        
        private void OnGetBulkOrderGoodsMovementPostingStatusOperationCompleted(object arg) {
            if ((this.GetBulkOrderGoodsMovementPostingStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetBulkOrderGoodsMovementPostingStatusCompleted(this, new GetBulkOrderGoodsMovementPostingStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/DeleteAllBulkOrders", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BulkOrderDeleteStatus[] DeleteAllBulkOrders(string moveTransNum) {
            object[] results = this.Invoke("DeleteAllBulkOrders", new object[] {
                        moveTransNum});
            return ((BulkOrderDeleteStatus[])(results[0]));
        }
        
        /// <remarks/>
        public void DeleteAllBulkOrdersAsync(string moveTransNum) {
            this.DeleteAllBulkOrdersAsync(moveTransNum, null);
        }
        
        /// <remarks/>
        public void DeleteAllBulkOrdersAsync(string moveTransNum, object userState) {
            if ((this.DeleteAllBulkOrdersOperationCompleted == null)) {
                this.DeleteAllBulkOrdersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteAllBulkOrdersOperationCompleted);
            }
            this.InvokeAsync("DeleteAllBulkOrders", new object[] {
                        moveTransNum}, this.DeleteAllBulkOrdersOperationCompleted, userState);
        }
        
        private void OnDeleteAllBulkOrdersOperationCompleted(object arg) {
            if ((this.DeleteAllBulkOrdersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteAllBulkOrdersCompleted(this, new DeleteAllBulkOrdersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://stt2.ihess.com/PhysDealManager/WebServices/DeleteBulkOrders", RequestNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", ResponseNamespace="http://stt2.ihess.com/PhysDealManager/WebServices/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public BulkOrderDeleteStatus[] DeleteBulkOrders(string moveTransNum, int[] segmentIds) {
            object[] results = this.Invoke("DeleteBulkOrders", new object[] {
                        moveTransNum,
                        segmentIds});
            return ((BulkOrderDeleteStatus[])(results[0]));
        }
        
        /// <remarks/>
        public void DeleteBulkOrdersAsync(string moveTransNum, int[] segmentIds) {
            this.DeleteBulkOrdersAsync(moveTransNum, segmentIds, null);
        }
        
        /// <remarks/>
        public void DeleteBulkOrdersAsync(string moveTransNum, int[] segmentIds, object userState) {
            if ((this.DeleteBulkOrdersOperationCompleted == null)) {
                this.DeleteBulkOrdersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteBulkOrdersOperationCompleted);
            }
            this.InvokeAsync("DeleteBulkOrders", new object[] {
                        moveTransNum,
                        segmentIds}, this.DeleteBulkOrdersOperationCompleted, userState);
        }
        
        private void OnDeleteBulkOrdersOperationCompleted(object arg) {
            if ((this.DeleteBulkOrdersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteBulkOrdersCompleted(this, new DeleteBulkOrdersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18047")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://stt2.ihess.com/PhysDealManager/WebServices/")]
    public partial class SAPBulkOrder {
        
        private string bulkOrderNumField;
        
        private string bulkOrderTypeCodeField;
        
        private string supplyProductCodeField;
        
        private bool goodsMovementIsPostedField;
        
        /// <remarks/>
        public string BulkOrderNum {
            get {
                return this.bulkOrderNumField;
            }
            set {
                this.bulkOrderNumField = value;
            }
        }
        
        /// <remarks/>
        public string BulkOrderTypeCode {
            get {
                return this.bulkOrderTypeCodeField;
            }
            set {
                this.bulkOrderTypeCodeField = value;
            }
        }
        
        /// <remarks/>
        public string SupplyProductCode {
            get {
                return this.supplyProductCodeField;
            }
            set {
                this.supplyProductCodeField = value;
            }
        }
        
        /// <remarks/>
        public bool GoodsMovementIsPosted {
            get {
                return this.goodsMovementIsPostedField;
            }
            set {
                this.goodsMovementIsPostedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.18047")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://stt2.ihess.com/PhysDealManager/WebServices/")]
    public partial class BulkOrderDeleteStatus {
        
        private string moveTransNumField;
        
        private int segmentIdField;
        
        private string[] errorsField;
        
        /// <remarks/>
        public string MoveTransNum {
            get {
                return this.moveTransNumField;
            }
            set {
                this.moveTransNumField = value;
            }
        }
        
        /// <remarks/>
        public int SegmentId {
            get {
                return this.segmentIdField;
            }
            set {
                this.segmentIdField = value;
            }
        }
        
        /// <remarks/>
        public string[] Errors {
            get {
                return this.errorsField;
            }
            set {
                this.errorsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetResolutionProceduresCompletedEventHandler(object sender, GetResolutionProceduresCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetResolutionProceduresCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetResolutionProceduresCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetProcessStatusCompletedEventHandler(object sender, GetProcessStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetProcessStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetProcessStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetGoodsMovementDetailsCompletedEventHandler(object sender, GetGoodsMovementDetailsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGoodsMovementDetailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGoodsMovementDetailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetGoodsMovementPostingStatusCompletedEventHandler(object sender, GetGoodsMovementPostingStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetGoodsMovementPostingStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetGoodsMovementPostingStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public SAPBulkOrder[] sapBulkOrders {
            get {
                this.RaiseExceptionIfNecessary();
                return ((SAPBulkOrder[])(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void GetBulkOrderGoodsMovementPostingStatusCompletedEventHandler(object sender, GetBulkOrderGoodsMovementPostingStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetBulkOrderGoodsMovementPostingStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetBulkOrderGoodsMovementPostingStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void DeleteAllBulkOrdersCompletedEventHandler(object sender, DeleteAllBulkOrdersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteAllBulkOrdersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteAllBulkOrdersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BulkOrderDeleteStatus[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BulkOrderDeleteStatus[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    public delegate void DeleteBulkOrdersCompletedEventHandler(object sender, DeleteBulkOrdersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.17929")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteBulkOrdersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteBulkOrdersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public BulkOrderDeleteStatus[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((BulkOrderDeleteStatus[])(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591