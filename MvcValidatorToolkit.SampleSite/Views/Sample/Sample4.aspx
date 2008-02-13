<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Sample4.aspx.cs" Inherits="MvcValidatorToolkit.SampleSite.Sample.Sample4" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">

	<script type="text/javascript">
		$(function(){
			updateSettingsForSample4aValidationSet($('#sample4aForm').validate({
				errorPlacement:function(error,element){
					error.appendTo(element.parent("td").next("td"));
				},
				rules:{}
			}));
			updateSettingsForSample4bValidationSet($('#sample4bForm').validate({
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

	<h2>Sample Multi-Form </h2>

	<form id="sample4aForm" action="/Sample/Sample4aProcessing" method="post"> 
	<table>
		<tr>
			<td>Field #1</td>
			<td><input type="text" id="field1" name="field1" /></td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td colspan="2"><input id="sb1" type="submit" value="Save" /></td>
			<td>&nbsp;</td>
		</tr>
	</table>
	</form>

	<br />
	
	<form id="sample4bForm" action="/Sample/Sample4bProcessing" method="post"> 
	<table>
		<tr>
			<td>Field #2</td>
			<td><input type="text" id="field2" name="field2" /></td>
			<td>&nbsp;</td>
		</tr>
		<tr>
			<td>&nbsp;</td>
			<td colspan="2"><input id="sb2" type="submit" value="Save" /></td>
			<td>&nbsp;</td>
		</tr>
	</table>
	</form>
	
	<% this.RenderValidationSetScripts(); %>

</asp:Content>

