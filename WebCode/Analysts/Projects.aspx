<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Projects.aspx.cs" Inherits="DaSH_Researcher_Portal.Analysts.Projects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


	<br /><br /><br /><br />


		<asp:GridView ID="gv_Projects" runat="server" HeaderStyle-BackColor="#8CA5FF" HeaderStyle-ForeColor="White"
					RowStyle-BackColor="#E8EFFF" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
					AutoGenerateColumns="false" Width="90%" CellPadding="2" CellSpacing="2">
					<Columns>
						<asp:BoundField DataField="ProjectTitle" HeaderText="Project Title" />
						<asp:TemplateField ItemStyle-HorizontalAlign="Center">
							<ItemTemplate>
								<asp:LinkButton ID="lnkDownload" runat="server" Text="Download Spec Sheet" OnClick="DsVars_To_Excel"
									CommandArgument='<%# Eval("ProjectId") %>'></asp:LinkButton><br />

									<asp:LinkButton ID="LinkButton1" runat="server" Text="Download CSV Sheet (single file per DataSet)" OnClick="DsVars_To_CSV"
								CommandArgument='<%# Eval("ProjectId") %>'></asp:LinkButton><br />

									<asp:LinkButton ID="LinkButton2" runat="server" Text="Download CSV Sheet (combined)" OnClick="dsVars_To_Single_CSV"
CommandArgument='<%# Eval("ProjectId") %>'></asp:LinkButton><br />

									<asp:LinkButton ID="lnkBtn_JSon" runat="server" Text="Download Json " OnClick="lnkBtn_JSon_Click"
CommandArgument='<%# Eval("ProjectId") %>'></asp:LinkButton>

							</ItemTemplate>
						</asp:TemplateField>
					</Columns>
					<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
					<EditRowStyle BackColor="#999999" />
					<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
					<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
					<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
					<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
				</asp:GridView>






									

</asp:Content>
