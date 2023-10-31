<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResProjects.aspx.cs" Inherits="DaSH_Researcher_Portal.ResForms.ResProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<script>
		$(function () {


			$("lb_Title").tooltip();
<%--		//	$("#<%=lb_Title.ClientID %>").tooltip();--%>
		});
	</script>
	<br />
	<br />
	<br />
	<div class="row">
		<div class="col-md-4 conboarder">
			<h2>Researcher Portal</h2>
			<a class="btn btn-default" href='DashProjectItem.aspx?resVar=AddNew'>New Project &raquo;</a>
		</div>
		<div class="col-md-1"></div>
		<div class="col-md-7 conboarder">
			<h2></h2>
		</div>
	</div>
	&nbsp;
	<div class="row conboarder">

<asp:GridView ID="gvProjects" runat="server" CellPadding="4" EmptyDataText="No DaSH project created"
	Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="Small" ForeColor="#333333"
	GridLines="None" Style="position: relative" Width="97%" AutoGenerateColumns="false">
	<EmptyDataTemplate>
		No DaSH project to display
	</EmptyDataTemplate>
	<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
	<Columns>
		<asp:TemplateField HeaderText="RefNo" InsertVisible="False" Visible="true" SortExpression="DashIdent">
			<ItemTemplate>
				<asp:Label ID='lb_RefNo' runat="server" Text='<%# Bind("DashIdent") %>' Visible="True"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Dash Title" InsertVisible="False" Visible="true" SortExpression="ProjectTitle">
			<ItemTemplate>
				<asp:Label ID='lb_Title' runat="server" Text='<%# Bind("ProjectTitle") %>' Visible="True"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Proposed Start Date" InsertVisible="False" Visible="true" SortExpression="ProjectStartDate">
			<ItemTemplate>
				<asp:Label ID='lb_ProjectStartDate' runat="server" Text='<%# Bind("ProjectStartDate", "{0:dd/MM/yyyy}") %>' Visible="True"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Proposed End Date" InsertVisible="False" Visible="true" SortExpression="ProjectStartDate">
			<ItemTemplate>
				<asp:Label ID='lb_ProjectStartDate' runat="server" Text='<%# Bind("ProjectEndDate", "{0:dd/MM/yyyy}") %>' Visible="True"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="" InsertVisible="False" Visible="true" SortExpression="ProjectStartDate">
			<ItemTemplate>
				<a class="btn btn-default" href='DashProjectItem.aspx?resVar=<%#Eval("ProjectID") %>'>Go >></a>
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



	</div>

</asp:Content>
