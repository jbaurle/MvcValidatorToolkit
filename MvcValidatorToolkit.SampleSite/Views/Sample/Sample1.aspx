<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Sample1.aspx.cs" Inherits="MvcValidatorToolkit.SampleSite.Sample.Sample1" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">

	<script type="text/javascript">
		$(function(){
			updateSettingsForSample1ValidationSet($('#sample1Form').validate({
				errorContainer:$('#sample1FormSummary'),
				errorLabelContainer:$('#sample1FormSummary ul'),
				wrapper:'li',
				rules:{}
			}));
		});
	</script>

	<style type="text/css">
		label.error, #sample1FormSummary {
			color:red;
		}
	</style> 

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

	<h2>Sample Form #1</h2>

	<p><i><b>NOTE:</b> To change the language of the validator messages, just click one of the following links: 
		<a href="/Sample/Sample1">English</a> | <a href="/Sample/Sample1/?lc=de">German</a></i></p>
	
	<div id="sample1FormSummary" style="display:none;"><%= Resources.ValidationSet.ValidationSummary_HeaderText %><ul></ul></div>

	<form id="sample1Form" action="/Sample/Sample1Processing" method="post"> 
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

