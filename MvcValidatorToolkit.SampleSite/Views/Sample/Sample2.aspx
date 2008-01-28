<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Sample2.aspx.cs" Inherits="MvcValidatorToolkit.SampleSite.Sample.Sample2" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">

	<script type="text/javascript">
		$(function(){
			updateSettingsForSample2ValidationSet($('#sample2Form').validate({
				errorContainer:$('#sample2FormSummary'),
				errorLabelContainer:$('#sample2FormSummary ul'),
				wrapper:'li',
				rules:{}
			}));
		});
	</script>

	<style type="text/css">
		label.error, #sample2FormSummary {
			color:red;
		}
	</style> 

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h2>Sample Form #1</h2>

	<p><i><b>NOTE:</b> ...</i></p>
	
	<div id="sample2FormSummary" style="display:none;"><%= Resources.ValidationSet.ValidationSummary_HeaderText %><ul></ul></div>

	<form id="sample2Form" action="/Sample/Sample2Processing" method="post"> 
	<table>
		<tr>
			<td>Username</td>
			<td><input type="text" id="username" name="username" /></td>
		</tr>
		<tr>
			<td>Password</td>
			<td><input type="text" id="password" name="password" /></td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td colspan="2"><input type="submit" value="Login" /></td>
		</tr>
	</table>
	</form>
	
	<% this.RenderValidationSetScripts(); %>

</asp:Content>

