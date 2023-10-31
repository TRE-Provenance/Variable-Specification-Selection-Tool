<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DaSH_Researcher_Portal.Account.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style type="text/css">
		.auto-style1 {
			height: 17px;
		}
	</style>
</head>
<body>

	<form id="form1" runat="server">
		<div style="position: relative; width: 300px; height: 300px; z-index: 15; top: 40%; left: 40%; margin: -150px 0 0 -150px;">
		</div>

		<table width="95%">
			<tr>
				<td style="width: 30%">&nbsp;</td>
				<td align="center">
					<asp:Label runat="server" ID="lbl_mobileWarning" Font-Bold="true" Font-Size="Medium" ForeColor="Red" Visible="true" Text="Quick login - needs Security && membership/Custom login setup "></asp:Label>
				</td>
				<td style="width: 30%">&nbsp;</td>
			</tr>
			<tr>
				<td align="center" colspan="3" valign="middle">


					<table border="0" cellpadding="1" cellspacing="1" width="70%">
						<tr>
							<td align="center" colspan="2"
								style="color: #E8D194; background-color: #5D7B9D; font-size: 0.9em; font-weight: bold;" class="auto-style1">Log In</td>
						</tr>
						<tr>
							<td align="right" style="width: 40%">
								<asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
            Name:</asp:Label>
							</td>
							<td>
								<asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" Width="90%"></asp:TextBox>
								<asp:RequiredFieldValidator ID="UserNameRequired" runat="server"
									ControlToValidate="UserName" ErrorMessage="User Name is required."
									ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="right">
								<asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
							</td>
							<td>
								<asp:TextBox ID="Password" runat="server" Font-Size="0.8em"  Width="90%"></asp:TextBox>
								<asp:RequiredFieldValidator ID="PasswordRequired" runat="server"
									ControlToValidate="Password" ErrorMessage="Password is required."
									ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
							</td>
						</tr>
						<tr>
							<td align="center" colspan="2" style="color: Red;">
								<asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
							</td>
						</tr>
						<tr>
							<td align="right" colspan="2">
								<asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF"
									BorderColor="#706538" BorderStyle="Solid" BorderWidth="1px" CommandName="Login"
									Font-Names="Verdana" Font-Size="0.8em" ForeColor="#706538" Text="Log In"
									ValidationGroup="Login1" OnClick="LoginButton_Click" />

								<br />
								<asp:Button ID="btn_addDemoUser" runat="server" BackColor="#FFFBFF"
									BorderColor="#706538" BorderStyle="Solid" BorderWidth="1px" CommandName="Login"
									Font-Names="Verdana" Font-Size="0.8em" ForeColor="#706538" Text="Add Demo User**"
									ValidationGroup="Login1" OnClick="btn_addDemoUser_Click" />

								<br />

							</td>
						</tr>
					</table>
					**PLEASE NOTE: The Login/Registration section is for a 'user' to be linked to a testing/demo account and not to be taken as a secure process - passwords are stored UNENCRYPTED and no security is wrapped into this process.  






        <%--    <asp:Login ID="Login2" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" BorderPadding="6"
                BorderStyle="Solid"  BorderWidth="1px" Font-Names="Verdana" Font-Size="Small" Width="35%"
                ForeColor="#333333" RememberMeText="Remember me" DisplayRememberMe="False" OnLoggedIn="Login2_LoggedIn" >
                <TitleTextStyle  Font-Bold="True"  Font-Size="0.9em" />
                <LayoutTemplate>
                    <table border="0" cellpadding="1" cellspacing="1" width="98%">
                        <tr>
                            <td align="center" valign="middle">
                                <table border="0" cellpadding="1" cellspacing="1" width="98%">
                                    <tr>
                                        <td align="center" colspan="2" 
                                            style="color:#E8D194;background-color:#5D7B9D;font-size:0.9em;font-weight:bold;" class="auto-style1">
                                            Log In</td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="width:40%">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User 
                                            Name:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="UserName" runat="server" Font-Size="0.8em" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                                ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="Password" runat="server" Font-Size="0.8em" TextMode="Password" Width="90%"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                ControlToValidate="Password" ErrorMessage="Password is required." 
                                                ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" BackColor="#FFFBFF" 
                                                BorderColor="#706538" BorderStyle="Solid" BorderWidth="1px" CommandName="Login" 
                                                Font-Names="Verdana" Font-Size="0.8em" ForeColor="#706538" Text="Log In" 
                                                ValidationGroup="Login1" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                <TextBoxStyle Font-Size="0.8em" />
                <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284775" />
            </asp:Login>--%>

				</td>
			</tr>
		</table>
	</form>
</body>
</html>
