using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.ComponentModel;
using Hess.Corporate.GHGPortal.Business;

namespace Hess.Corporate.GHGPortal.Web.UI
{
    public abstract class CommonGridViewControl : CommonControl
    {

        #region Constants

        protected const string conAddCommandName = "Add";
        protected const string conCopyCommandName = "Copy";
        protected const string conSortCommandName = "Sort";
        protected const string conRefreshCommandName = "Refresh";
        protected const string conUpdateCommandName = "Update";
        protected const string conClearCommandName = "Clear";
        protected const string conResetCommandName = "Reset";
        protected const string conBusinessObjectName = "currentBusinessObject";
        protected const string conCookieName = "GHGPortalCookie";
        protected const string conSortUpImageUrl = "~/App_Themes/Hess/Images/sortup.gif";
        protected const string conSortDownImageUrl = "~/App_Themes/Hess/Images/sortdown.gif";
        protected GridView _GridView;
        protected Csla.Web.CslaDataSource _DataSource;

        #endregion

        #region Protected Properties

        protected virtual object CurrentBusinessObject
        {
            get
            {
                if ((_DataSource != null))
                {
                    // Use GetBusinessObject to retrieve object from Session or get it from database.
                    System.Type type = System.Type.GetType(string.Format("{0},{1}", _DataSource.TypeName, _DataSource.TypeAssemblyName), true, true);
                    object businessObjects = GetBusinessObject(conBusinessObjectName, type);
                    if (businessObjects == null)
                        businessObjects = System.Activator.CreateInstance(type);

                    //Set default sorting on the business object if exists
                    if (this._GridView.AllowSorting && (businessObjects != null) && businessObjects is IBusinessObjects)
                    {
                        if (!string.IsNullOrEmpty(this.SortExpression) && string.IsNullOrEmpty(((IBusinessObjects)businessObjects).SortExpression))
                        {
                            Sort(ref businessObjects, SortExpression.ToString(), SortDirection);
                        }
                    }
                    return businessObjects;
                }
                return null;
            }
        }

        protected virtual bool ValidateEmptyDataSource
        {
            get { return true; }
        }

        public string SortExpression
        {
            get
            {
                if ((this._GridView != null) && (ViewState[this._GridView.ID + "SortExpression"] != null))
                    return Convert.ToString(ViewState[this._GridView.ID + "SortExpression"]);
                return string.Empty;
            }
            set { ViewState[this._GridView.ID + "SortExpression"] = value; }
        }

        public ListSortDirection SortDirection
        {
            get
            {
                if ((this._GridView != null) && !string.IsNullOrEmpty((string)ViewState[this._GridView.ID + "SortDirection"]))
                    return (ListSortDirection)Enum.Parse(typeof(ListSortDirection), ViewState[this._GridView.ID + "SortDirection"].ToString());

                return System.ComponentModel.ListSortDirection.Ascending;
            }
            set { ViewState[this._GridView.ID + "SortDirection"] = Enum.GetName(typeof(System.ComponentModel.ListSortDirection), value); }
        }

        protected bool OptionsCollapsed
        {
            get
            {
                if (this.Page.Master == null)
                    return true;
                PropertyInfo prop = this.Page.Master.GetType().GetProperty("OptionsCollapsed");
                if ((prop != null))
                    return Convert.ToBoolean(prop.GetValue(this.Page.Master, null));
                return true;
            }
            set
            {
                if (this.Page.Master == null)
                    return;
                PropertyInfo prop = this.Page.Master.GetType().GetProperty("OptionsCollapsed");
                if ((prop != null))
                    prop.SetValue(this.Page.Master, value, null);
            }
        }

        public string StatusMessage
        {
            get
            {
                if ((ViewState["ViewStatusMessage"] != null))
                    return Convert.ToString(ViewState["ViewStatusMessage"]);
                return string.Empty;
            }
            set { ViewState["ViewStatusMessage"] = value; }
        }

        #endregion

        #region Cookie Settings

        protected virtual void LoadSettings()
        {
            HttpCookie cookie = Request.Cookies[conCookieName];
            if (cookie == null)
                return;
            try
            {
                if (!string.IsNullOrEmpty(cookie["OptionsCollapsed"]))
                {
                    this.OptionsCollapsed = bool.Parse(cookie["OptionsCollapsed"]);
                }
                this.SortExpression = cookie["SortExpression"];
                this.SortDirection = (ListSortDirection)Enum.Parse(typeof(ListSortDirection), cookie["SortDirection"].ToString());
            }
            catch
            {
            }
        }

        protected virtual void SaveSettings()
        {
            HttpCookie cookie = Response.Cookies[conCookieName];
            cookie.Values.Clear();
            cookie.Values.Add("Screen", this.GetType().ToString());
            //cookie.Values.Add("Facility", this.GetFacility());
            //cookie.Values.Add("SourceCategory", this.GetSourceCategory());
            cookie.Values.Add("SortDirection", Enum.GetName(typeof(ListSortDirection), this.SortDirection));
            cookie.Expires = DateTime.Today.AddYears(30);
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Load += PageLoad;
            //Initialize grid view and data source variables
            foreach (Control control in this.Controls)
            {
                if (control is System.Web.UI.WebControls.GridView)
                {
                    this._GridView = (System.Web.UI.WebControls.GridView)control;
                    this._GridView.SelectedIndex = -1;
                    this._GridView.EditIndex = -1;
                }
                else if (control is Csla.Web.CslaDataSource)
                {
                    this._DataSource = (Csla.Web.CslaDataSource)control;
                }
            }
            //It is assumed the descendants of this class will always implement a single
            //System.Web.UI.WebControls.GridView and Csla.Web.CslaDataSource components
            if ((this._GridView != null) && (this._DataSource != null))
            {
                //Add event handlers to the grid view and data source components
                this._DataSource.SelectObject += DataSourceSelectObject;
                this._DataSource.UpdateObject += DataSourceUpdateObject;
                this._DataSource.DeleteObject += DataSourceDeleteObject;
                this._GridView.RowDataBound += GridViewRowDataBound;
                this._GridView.RowCommand += GridViewRowCommand;
                this._GridView.RowEditing += GridViewRowEditing;
                this._GridView.RowCancelingEdit += GridViewRowCancelingEdit;
                this._GridView.RowUpdating += GridViewRowUpdating;
                this._GridView.DataBinding += GridViewDataBinding;
                this._GridView.RowCreated += GridViewRowCreated;
            }
        }

        protected bool ViewIsActive
        {
            get
            {
                /*if (string.IsNullOrEmpty(this.Session[CurrentViewId].ToString()))
                    return false;

                return this.Session[CurrentViewId].ToString().Contains(this.GetType().BaseType.Name);*/
                return true;
            }
        }


        #region Event Handlers

        public event EventHandler DataBound;

        protected virtual void PageLoad(object sender, System.EventArgs e)
        {
            if (!this.ViewIsActive)
                return;

            if (!this.Page.IsPostBack)
                LoadSettings();

            SaveSettings();
        }

        protected virtual void DataSourceSelectObject(object sender, Csla.Web.SelectObjectArgs e)
        {
            e.BusinessObject = CurrentBusinessObject;
        }

        protected virtual void DataSourceUpdateObject(object sender, Csla.Web.UpdateObjectArgs e)
        {
            IBusinessObjects businessObjects = (IBusinessObjects)CurrentBusinessObject;
            if ((businessObjects != null))
            {
                IBusinessObject businessObject = (IBusinessObject)businessObjects.Find(e.Keys[this._GridView.DataKeyNames[0]]);
                if ((businessObject != null))
                {
                    //Update business object from the collection of posted values
                    this.UpdateBusinessObject(businessObject, this._GridView.SelectedRow);
                    businessObject.Save();
                    this.GridViewDataBind(false);
                }
            }
        }

        protected virtual void DataSourceDeleteObject(object sender, Csla.Web.DeleteObjectArgs e)
        {
            IBusinessObjects businessObjects = (IBusinessObjects)this.CurrentBusinessObject;
            if ((businessObjects != null))
                e.RowsAffected = Convert.ToInt32(businessObjects.Delete(e.Keys[this._GridView.DataKeyNames[0]]));
        }

        protected virtual void GridViewRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.Header)
            {
                this.SetLinkButton(e.Row, conSortCommandName, null, null);
            }
        }

        protected virtual void GridViewRowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == conAddCommandName || e.CommandName == conCopyCommandName)
            {
                this._GridView.EditIndex = -1;
                IBusinessObjects businessObjects = (IBusinessObjects)this.CurrentBusinessObject;
                if ((businessObjects != null))
                {
                    object businessObject = businessObjects.InsertNew(Int32.Parse(e.CommandArgument.ToString()), e.CommandName == conCopyCommandName);
                    this._GridView.EditIndex = businessObjects.IndexOf(businessObject) - this._GridView.PageIndex * this._GridView.PageSize;
                }
                this.GridViewDataBind(false);
            }
            else if (e.CommandName == conRefreshCommandName)
            {
                this.GridViewDataBind(true);
            }
            else if (e.CommandName == conSortCommandName)
            {
                this.Sort(e.CommandArgument.ToString());
                this.GridViewDataBind(false);
            }
        }

        protected virtual void GridViewRowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            int index = this.CancelNew() - this._GridView.PageIndex * this._GridView.PageSize;
            if (0 <= index && index < e.NewEditIndex)
                e.NewEditIndex -= 1;
        }

        protected virtual void GridViewRowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            this.CancelNew();
        }

        protected virtual void GridViewRowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {
            this._GridView.SelectedIndex = e.RowIndex;
        }

        protected virtual void GridViewDataBinding(object sender, System.EventArgs e)
        {
            if (this.ValidateEmptyDataSource)
                this.CheckIfEmpty();
        }

        protected virtual void GridViewRowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
        }

        #endregion

        #region Protected worker methods

        protected override void SetLinkButton(Control control, string commandName, string commandArgument, string text)
        {
            //base.SetLinkButton(control, commandName, commandArgument, text);
            if ((control != null) && control is LinkButton)
            {
                LinkButton lnkBtn = (LinkButton)control;
                if ((this._GridView != null) && this._GridView.AllowSorting && lnkBtn.CommandName == conSortCommandName)
                {
                    IBusinessObjects businessObjects = (IBusinessObjects)CurrentBusinessObject;
                    if ((businessObjects != null) && lnkBtn.CommandArgument == businessObjects.SortExpression)
                    {
                        Image imgSort = new Image();
                        imgSort.ImageUrl = (businessObjects.SortDirection == System.ComponentModel.ListSortDirection.Ascending ? conSortUpImageUrl : conSortDownImageUrl);
                        lnkBtn.Controls.Add(new LiteralControl(lnkBtn.Text + "&nbsp;"));
                        lnkBtn.Controls.Add(imgSort);
                    }
                    else
                    {
                        lnkBtn.Controls.Clear();
                    }
                }
            }
        }

        protected void SetLabelText(Label label, object value)
        {
            if (label != null && value != null)
                label.Text = (string)value;
        }

        protected void SetLabelAttribute(Label label, string key, string value)
        {
            if (label != null && value != null)
                label.Attributes.Add(key, value);
        }

        protected virtual bool UpdateBusinessObject(object businessObject, Control control)
        {
            bool updated = false;
            PropertyInfo propInfo = null;
            string postedValue = this.Request.Form[control.UniqueID];
            //Get property descriptor for the data control
            if (control is WebControl && !string.IsNullOrEmpty(((WebControl)control).Attributes["dataObjectField"]))
            {
                propInfo = businessObject.GetType().GetProperty(((WebControl)control).Attributes["dataObjectField"]);
            }
            else if (control is DropDownList)
            {
                propInfo = businessObject.GetType().GetProperty(((DropDownList)control).DataValueField);
            }
            //Update object property from dropdown list or textbox controls by matching on their data value fields
            if ((propInfo != null) && (businessObject != null))
            {
                object fieldValue = propInfo.GetValue(businessObject, null);
                if (postedValue == EmptySelectedValue)
                {
                    if (fieldValue != null) { propInfo.SetValue(businessObject, null, null); updated = true; }
                }
                else if (fieldValue == null || postedValue != fieldValue.ToString())
                {
                    if (string.IsNullOrEmpty(postedValue))
                    {
                        propInfo.SetValue(businessObject, null, null);
                    }
                    else if (propInfo.PropertyType.IsEnum)
                    {
                        propInfo.SetValue(businessObject, int.Parse(postedValue), null);
                    }
                    else
                    {
                        propInfo.SetValue(businessObject, Convert.ChangeType(postedValue, propInfo.PropertyType), null);
                    }
                    updated = true;
                }
            }
            //Drill down to the collection of nested controls to get updated values
            foreach (Control childControl in control.Controls)
            {
                updated = this.UpdateBusinessObject(businessObject, childControl) || updated;
            }
            return updated;
        }

        protected void Sort(string sortExpression)
        {
            IBusinessObjects businessObjects = (IBusinessObjects)CurrentBusinessObject;
            if ((businessObjects != null))
            {
                businessObjects.Sort(sortExpression);
                this.SaveSorting(businessObjects);
            }
        }

        protected void Sort(ref object businessObjects, string sortExpression, System.ComponentModel.ListSortDirection direction)
        {
            if ((businessObjects != null) && businessObjects is IBusinessObjects)
            {
                ((IBusinessObjects)businessObjects).Sort(sortExpression, direction);
                this.SaveSorting((IBusinessObjects)businessObjects);
            }
        }

        protected void SaveSorting(IBusinessObjects businessObjects)
        {
            this.SortExpression = ((IBusinessObjects)businessObjects).SortExpression;
            this.SortDirection = ((IBusinessObjects)businessObjects).SortDirection;
            this.SaveSettings();
        }

        protected virtual void GridViewDataBind(bool refresh)
        {
            string oldStatusMessage = this.StatusMessage;
            //Reset the session value to force the business object refresh
            if (refresh)
                Session[conBusinessObjectName] = null;
            //Re-bind the data source to the GridView control
            this._GridView.DataBind();
            //Raise status change event if the status message has changed
            if (this.StatusMessage != oldStatusMessage)
                if (DataBound != null)
                {
                    DataBound(this, new EventArgs());
                }
        }

        protected bool CheckIfEmpty()
        {
            //If data source is empty add a new record and switch grid into edit mode for this record
            if (!this.ValidateEmptyDataSource)
                return false;
            IBusinessObjects businessObject = (IBusinessObjects)CurrentBusinessObject;
            if ((businessObject != null) && businessObject.IsEmpty)
            {
                businessObject.AddNew();
                if ((_GridView != null))
                {
                    this._GridView.SelectedIndex = 0;
                    this._GridView.EditIndex = 0;
                }
                return true;
            }
            return false;
        }

        protected int CancelNew()
        {
            IBusinessObjects businessObject = (IBusinessObjects)this.CurrentBusinessObject;
            if ((businessObject != null))
                return businessObject.CancelNew();
            else
                return -1;
        }


        #endregion

    }
}