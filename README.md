# MvcValidatorToolkit

**This project is not maintained anymore.**

The Validator Toolkit provides a set of validators for the ASP.NET MVC framework to validate HTML forms on the client and server-side using validation sets.

More documentation take a look at the article on CodeProject.com: [http://www.codeproject.com/KB/aspnet/MvcValidatorToolkit.aspx](http://www.codeproject.com/KB/aspnet/MvcValidatorToolkit.aspx)

See also my homepage on Parago.de:  [http://www.parago.de](http://www.parago.de)

![](Home_Screenshot1.png)

More [Screenshots](Screenshots) here on this page.

Basically, you will create a validation set class which derives from ValidationSet base class:

{{
public class LoginValidationSet : ValidationSet
{
	string Username = "";
	string Password = "";

	protected override ValidatorCollection GetValidators()
	{
		return new ValidatorCollection
		(
			new ValidateElement("username") { Required = true, MinLength = 5, MaxLength = 10 },
			new ValidateElement("password") { Required = true, MinLength = 3, MaxLength = 10 }
		);
	}

	protected override void OnValidate()
	{
		if(Username == "Bill" && Password == "Jobs")
			throw new ValidatorException("username", "The username/password combination is not valid");
	}
}
}}
Then, you will attach it to the view and the HTML form processing action using the ValidationSetAttribute:

{{
public void Login()
{
	RenderView("Login");
}

[ValidationSet(typeof(LoginValidationSet))](ValidationSet(typeof(LoginValidationSet)))
public void Authenticate()
{
	if(this.ValidateForm())
		RenderView("Ok");
	else
		RenderView("Login");
}

...

[ValidationSet(typeof(LoginValidationSet))](ValidationSet(typeof(LoginValidationSet)))
public partial class LoginView : ViewPage
{
}
}}
Then, you add the following script and methods to your view:

{{
<script type="text/javascript">
	$(function(){
		updateSettingsForLoginValidationSet($('#loginForm').validate({rules:{} }));
	});
</script>

...

<form id="loginForm" action="/Authenticate" method="post"> 
...
</form>

<% this.RenderValidationSetScripts(); %>
}}
This all to validate the login HTML form on the client and server-side. 

More documentation take a look at the article on CodeProject.com: [http://www.codeproject.com/KB/aspnet/MvcValidatorToolkit.aspx](http://www.codeproject.com/KB/aspnet/MvcValidatorToolkit.aspx)

Please see the source code with the included sample site for more examples. You will also find a multi-form example showing how to use the toolkit in conjunction with multiple forms on one HTML page (view).

**Author**

Jürgen Bäurle

[http://jbaurle.wordpress.com](http://jbaurle.wordpress.com) (Blog)
[http://www.parago.de/jbaurle](http://www.parago.de/jbaurle) (Homepage)
