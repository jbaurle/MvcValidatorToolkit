<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MvcValidatorToolkit.SampleSite.Views.Home.Index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

   <h2>Welcome to my ASP.NET MVC Application!</h2>

	<p>Click on the links below to test different sample HTML forms that are validated by the Validator Toolkit:</p>
	
	<ul>
		<li>
			<%= Html.ActionLink("Sample #1", "Sample1", "Sample")%><br />
			This form shows how to use a simple validation set using localized messages defined in the App_GlobalResources folder.
		</li>
		<li>
			<%= Html.ActionLink("Sample #2", "Sample2", "Sample")%><br />
			This form shows a sample form with different input controls.
		</li>
	</ul>
	
</asp:Content>

