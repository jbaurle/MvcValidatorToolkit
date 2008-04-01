<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MvcValidatorToolkit.SampleSite.Views.Home.Index" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

   <h2>Welcome to my ASP.NET MVC Application!</h2>

	<p>Click on the links below to test different sample HTML forms that are validated by the Validator Toolkit:</p>
	
	<ul>
		<li>
			<%= Html.ActionLink("Sample #1", "Sample1", "Sample")%><br />
			This page shows how to use a simple validation set using localized messages defined in the App_GlobalResources folder.
		</li>
		<li>
			<%= Html.ActionLink("Sample #2", "Sample2", "Sample")%><br />
			This page shows a sample form with different input controls.
		</li>
		<li>
			<%= Html.ActionLink("Sample #3", "Sample3", "Sample")%><br />
			This page shows a sample form with different input controls and strongly typed view data.
		</li>

		<li>
			<%= Html.ActionLink("Sample Multi-Form", "Sample4", "Sample")%><br />
			This page shows a sample with tow different forms.
		</li>
	</ul>
	
</asp:Content>

