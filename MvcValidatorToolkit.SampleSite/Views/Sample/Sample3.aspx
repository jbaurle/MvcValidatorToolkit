<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Sample3.aspx.cs" Inherits="MvcValidatorToolkit.SampleSite.Sample.Sample3" %>
<%@ Import Namespace="MvcValidatorToolkit.SampleSite" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">

	<script type="text/javascript">
		$(function(){
			updateSettingsForSample3ValidationSet($('#sample3Form').validate({
				errorPlacement:function(error,element){
					error.appendTo(element.parent("td").next("td"));
				},
				rules:{}
			}));
		});
	</script>

	<style type="text/css">
		label.error {
			color:red;
		}
	</style> 

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h2>Sample Form #3</h2>

	<p><i><b>NOTE:</b> The form is displaying the error messages next to the field using the errorPlacement option 
		of the jQuery validation plugin with strongly typed ViewData.</i></p>
	
	<form id="sample3Form" action="/Sample/Sample3Processing" method="post"> 
	<table>
		<tr>
			<td>Username</td>
			<td><input type="text" id="username" name="username" /></td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>Password</td>
			<td><input type="text" id="password" name="password" /></td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>Password again</td>
			<td><input type="text" id="password2" name="password2" /></td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>Gender</td>
			<td>
				<input type="radio" name="gender" value="M" /> Male<br />
				<input type="radio" name="gender" value="F" /> Female
			</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>Membership</td>
			<td>
				<select id="membership" name="membership">
					<option value="">Please select...</option>
					<% foreach (MembershipData m in ViewData.MembershipList)
        { %>
					<option value="<%= m.Code %>"><%= m.Name %></option>
					<% } %>
				</select>
			</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td><input type="checkbox" id="Checkbox1" name="termsOfService" /> I accept the terms of service</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td colspan="2"><input type="submit" value="Save" /></td>
			<td>&nbsp;</td>
		</tr>
	</table>
	</form>
	
	<% this.RenderValidationSetScripts(); %>

</asp:Content>

