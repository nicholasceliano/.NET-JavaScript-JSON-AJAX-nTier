using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Reflection;

namespace Hess.Corporate.GHGPortal.Web.UI
{
	public abstract class CommonControl : System.Web.UI.UserControl
	{
		 
		#region Constants

		public string DateFormatString = "MM/dd/yyyy"; //date.ToString(), String.Format("{ }", dt)
		public string VolumeFormatString = "#,##0.00 ;(#,##0.00)";
		public string VolumeFormatWithoutDecimalString = "#,##0 ;(#,##0)";
		public string PriceFormatString = "#,##0.00## ;(#,##0.00##)";
		public string RateFormatString = "#,##0.000000####";
		public const string CurrentViewId = "currentViewId";
		protected const string EmptySelectedValue = "NA";
		protected const string __EVENTTARGET = "__EVENTTARGET";
		protected const string __EVENTARGUMENT = "__EVENTARGUMENT";

		#endregion

		#region Get Business Object

		protected object GetBusinessObject(System.Type type)
		{
			return GetBusinessObject(type.Name, type, null);
		}

		protected object GetBusinessObject(System.Type type, string name)
		{
			return GetBusinessObject(name, type, null);
		}

		protected object GetBusinessObject(System.Type type, params object[] parameters)
		{
			if (parameters == null)
			{
				return GetBusinessObject(type);
			}
			string name = type.Name;
			foreach (object param in parameters)
			{
				name += param.ToString();
			}
			return GetBusinessObject(name, type, parameters);
		}

		protected object GetBusinessObject(string name, System.Type type, params object[] parameters)
		{
			object businessObjects = Session[name];
			if (businessObjects == null || (!object.ReferenceEquals(businessObjects.GetType(), type)) )
			{
				foreach (MethodInfo method in type.GetMethods())        // Looking for a method which returns Business Object of type pased in parm
				{   
					if (object.ReferenceEquals(method.ReturnType, type) && !method.IsConstructor && method.IsStatic)
					{
						bool methodMatch = false;                       // Check if parameters match.
						ParameterInfo[] methodParameters = method.GetParameters();
						if (parameters == null)                         //    no parameters
						{
							methodMatch = methodParameters.Length == 0;
						}
						else if (parameters.Length == methodParameters.Length)
						{
							methodMatch = true;                         //    compare parameters.
							for (int i = 0; i <= parameters.Length - 1; i++)
							{
								if (parameters[i] == null)
								{
									methodMatch = methodMatch && methodParameters[i].IsOptional;
								}
								else
								{
									methodMatch = methodMatch && object.ReferenceEquals(parameters[i].GetType(), methodParameters[i].ParameterType);
								}
							}
						}
						if (methodMatch)                                // Execute it, if found. It will return Business Object with data. Save it in Session storage
						{
							try
							{
								businessObjects = method.Invoke(null, parameters);
							}
							catch
							{
							}
							Session[name] = businessObjects;            // Save in Session storage
							break; 
						}
					}
				}
			}
			return businessObjects;
		}


		//protected void SetLabelText(Label label, object value)
		//{
		//    if ((label != null) && (value != null))
		//        label.Text = (string)value;
		//}

	  /*  protected void SetImage(Image image, Hess.Corporate.GHGPortal.Configuration.SystemType systemType)
		{
			this.SetImage(image, Hess.Corporate.GHGPortal.AppConfiguration.Current.Systems(systemType));
		}

		protected void SetImage(Image image, Hess.Corporate.GHGPortal.Configuration.System system)
		{
			if ((system != null))
			{
				string systemTypeName = Enum.GetName(typeof(Hess.Corporate.GHGPortal.Configuration.SystemType), system.SystemType);
				this.SetImage(image, system.ImageURL, systemTypeName, systemTypeName);
			}
		}*/

		protected void SetImage(Image image, string imageURL, string alternateText, string toolTip)
		{
			if (image == null)
				return;
			if (!string.IsNullOrEmpty(imageURL))
				image.ImageUrl = imageURL;
			if (!string.IsNullOrEmpty(alternateText))
				image.AlternateText = alternateText;
			if (!string.IsNullOrEmpty(toolTip))
				image.ToolTip = toolTip;
		}

		protected void SetTextBox(TextBox textBox, object value)
		{
			if ((textBox != null) && (value != null))
				textBox.Text = (string)value;
		}

		protected void SetTextBox(TextBox textBox, object value, bool enabled)
		{
			this.SetTextBox(textBox, value);
			if ((textBox != null))
				textBox.Enabled = enabled;
		}
		
		#endregion
		
		protected void SetLinkButton(Control control, string commandArgument)
		{
			SetLinkButton(control, null, commandArgument, null);
		}

		protected virtual void SetLinkButton(Control control, string commandName, string commandArgument, string text)
		{
			if ((control != null))
			{
				foreach (Control childControl in control.Controls)
				{
					this.SetLinkButton(childControl, commandName, commandArgument, text);
				}
				if (control is LinkButton)
				{
					LinkButton lnkBtn = (LinkButton)control;
					if (!string.IsNullOrEmpty(commandArgument))
						lnkBtn.CommandArgument = commandArgument;
					if (!string.IsNullOrEmpty(commandName))
						lnkBtn.CommandName = commandName;
					if (!string.IsNullOrEmpty(text))
						lnkBtn.Text = text;
				}
			}
		}

	}

}