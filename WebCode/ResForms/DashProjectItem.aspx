<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DashProjectItem.aspx.cs" Inherits="DaSH_Researcher_Portal.ResForms.DashProjectItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<script src="https://code.jquery.com/jquery-3.6.0.js"></script>
	<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
	<script>
		$(function () {

		<%--	$("#<%=tb_projTitle.ClientID %>").tooltip();--%>
			var dd = '<%= DashIdent %>';
			if (dd.length == 0) {
				$('#mainContentRow').html("You are not authorised to access this project");
			}


		});


		$(function () {
			$("#datepicker").datepicker({ dateFormat: 'dd-mm-yy' });

			$("#<%=tb_projStartDate.ClientID %>").datepicker({ dateFormat: 'dd-mm-yy', changeYear: true });

			$("#<%=tb_projEndDate.ClientID %>").datepicker({ dateFormat: 'dd-mm-yy', changeYear: true });


		});
	</script>
	<br />
	<br />
	<br />
	<br />





	<div class="row" id="mainContentRow">
		<div class="col-md-12 conboarder">
				<div class="row">
					<div class="col-md-12">

						<asp:Label ID="lbl_Status" runat="server" Text="In Progess (not submitted)" Font-Bold="true" Font-Underline="true" ForeColor="Red"></asp:Label>
						</div>
					</div>


			<div class="row">
				<div class="col-md-2">
					Dash Refrence No:
				</div>
				<div class="col-md-4">
					<label id="l_DashIdent"><%= DashIdent %></label>
				</div>

				<div class="col-md-2">
					Dash Short Title:
				</div>

				<div class="col-md-4">
					<label id="l_DashTitle"><%= ShortTitle %></label>
				</div>
			</div>


			<div class="row">
				<div class="col-md-2">
					Project Title 
				</div>
				<div class="col-md-10">
										<asp:RequiredFieldValidator ID="rfc_title" ControlToValidate="tb_projTitle" ValidationGroup="Proj"
 runat="server" Font-Bold="true" ForeColor="Red" ErrorMessage="Please the titl of your project"></asp:RequiredFieldValidator>
					<asp:TextBox ID="tb_projTitle" placeholder="Project Title" Style="max-width: 1768px;" Enabled="true" runat="server" Width="100%" TextMode="MultiLine" Rows="2" Text='<%#Eval("shortTitle")%>' Title="Project Title" />

				</div>
			</div>
			<br />
			<div class="row">
				<div class="col-md-2">
					Project Description
				</div>
				<div class="col-md-10">
											<asp:RequiredFieldValidator ID="rfc_projDesc" ControlToValidate="tb_proj_Desc" ValidationGroup="Proj"
 runat="server" Font-Bold="true" ForeColor="Red" ErrorMessage="Please a brief description of your project"></asp:RequiredFieldValidator>
					<asp:TextBox ID="tb_proj_Desc" placeholder="Brief Description of Project" Style="max-width: 1768px;" Enabled="true" runat="server" Rows="10" Width="100%" TextMode="MultiLine" Text='<%#Eval("ProjectDescription")%>' Title="Project Description" />

				</div>
			</div>


			<div class="row">
				<div class="col-md-2">
					Cohort Description (keep concise)
				</div>
				<div class="col-md-10">
										<asp:RequiredFieldValidator ID="rfc_Cohort" ControlToValidate="tb_Cohort_Desc" ValidationGroup="Proj"
 runat="server" Font-Bold="true" ForeColor="Red" ErrorMessage="Please a description of your cohort"></asp:RequiredFieldValidator>
					<asp:TextBox ID="tb_Cohort_Desc" Style="max-width: 1768px;" Enabled="true" runat="server" Rows="5" Width="100%" TextMode="MultiLine" Text='<%#Eval("Cohort_Desc")%>' Title="Cohort Description" placeholder="Brief Description of Cohort" />

				</div>
			</div>
			<div class="row">
				<div class="col-md-2">
					Inclusion Criteria (keep concise)
				</div>
				<div class="col-md-10">
										<asp:RequiredFieldValidator ID="rfv_Inclusion" ControlToValidate="tb_InclusionCriteria" ValidationGroup="Proj"
 runat="server"  Font-Bold="true" ForeColor="Red" ErrorMessage="Please a describe your inclusion criteria"></asp:RequiredFieldValidator>
					<asp:TextBox ID="tb_InclusionCriteria" Style="max-width: 1768px;" Enabled="true" runat="server" Rows="2" Width="100%" TextMode="MultiLine" Text='<%#Eval("InclusionCriteria")%>' Title="Inclusion Criteria" />

				</div>
			</div>
			<div class="row">
				<div class="col-md-2">
					Exclusion Criteria (keep concise)
				</div>
				<div class="col-md-10">
												<asp:RequiredFieldValidator ID="rfv_Exclusion" ControlToValidate="tb_ExclusionCriteria" ValidationGroup="Proj"
 runat="server" Font-Bold="true" ForeColor="Red" ErrorMessage="Please a describe your exclusion criteria"></asp:RequiredFieldValidator>
					<asp:TextBox ID="tb_ExclusionCriteria" Style="max-width: 1768px;" Enabled="true" runat="server" Rows="2" Width="100%" TextMode="MultiLine" Text='<%#Eval("ExclusionCriteria")%>' Title="Exclusion Criteria Criteria" />

				</div>
			</div>





			<div class="row">
				<div class="col-md-2">
					Start Date
				</div>
				<div class="col-md-4">

					<asp:TextBox ID="tb_projStartDate" Enabled="true" runat="server" Text='<%#Eval("ProjectStartDate")%>' /><br />
															<asp:RequiredFieldValidator ID="rfv_projStart" ControlToValidate="tb_projStartDate" ValidationGroup="Proj"
 runat="server" Font-Bold="true" ForeColor="Red" ErrorMessage="Please enter a project start date"></asp:RequiredFieldValidator>

				</div>
				<div class="col-md-2">
					End Date
				</div>
				<div class="col-md-4">
					<asp:TextBox ID="tb_projEndDate" Enabled="true" runat="server" Text='<%#Eval("ProjectendDate")%>' /><br />
						<asp:RequiredFieldValidator ID="rfv_projEndDate" ControlToValidate="tb_projEndDate" ValidationGroup="Proj"
 runat="server" Font-Bold="true" ForeColor="Red" ErrorMessage="Please enter a project end date"></asp:RequiredFieldValidator>
				</div>


				<%--		 <p><%= ProjectDescription %></p>--%>
			</div>


			<br />
			<div class="row">
				<div class="col-md-6">



					<div class="col-md-12 conboarder">
						<div class="conboarder">
							File Upload

					
					<asp:FileUpload ID="FileUpload1" runat="server" />

							<asp:Label ID="Label1" runat="server" Font-Names="Verdana,Arial,Helvetica,sans-serif"
								Font-Size="X-Small" Style="position: relative" Text="Description" Width="148px"></asp:Label>
							<asp:TextBox ID="tb_fileDescription" runat="server" Style="position: relative; top: 0px; left: 0px;"
								Width="356px" Font-Names="Verdana,Arial,Helvetica,sans-serif" Font-Size="X-Small"></asp:TextBox><br />
							<br />

							<asp:Button ID="btnUpload" runat="server" Text="Upload Document" OnClick="Upload" />
						</div>


						<br />


						<b>Uploaded Documents</b>
						<asp:GridView ID="gv_files" runat="server" HeaderStyle-BackColor="#8CA5FF" HeaderStyle-ForeColor="White"
							RowStyle-BackColor="#E8EFFF" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
							AutoGenerateColumns="false" Width="90%" CellPadding="2" CellSpacing="2">
							<Columns>
								<asp:BoundField DataField="DocumentName" HeaderText="File Name" />
								<asp:TemplateField ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
											CommandArgument='<%# Eval("DocId") %>'></asp:LinkButton>
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
						<br />


					</div>
				</div>



				<div class="col-md-6">
					<div class="col-md-12 conboarder">

						<div class="conboarder">
							<div class="row">
								<div class="col-md-2">Name :</div>
								<div class="col-md-8">
									<asp:TextBox ID="tb_ResearcherFName" Enabled="true" runat="server" Text='<%#Eval("ResearcherName")%>' />
								</div>
							</div>

							<div class="row">
								<div class="col-md-2">Email :</div>
								<div class="col-md-6">
									<asp:TextBox ID="tb_ResEmail" Enabled="true" runat="server" Text='<%#Eval("Email")%>' />
								</div>
							</div>

							<div class="row">
								<div class="col-md-2">UserName :</div>
								<div class="col-md-6">
									<asp:TextBox ID="tb_ResUserName" Enabled="true" runat="server" Text='<%#Eval("UserName")%>' />
								</div>
							</div>

							<%--	<asp:LinkButton ID="lnkb_AddResearcher" runat="server" Text="Add Researcher" OnClick="AddResearcher"
							CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>--%>

							<asp:Button ID="btnAddResearcher" runat="server" Text="Add Researcher" OnClick="AddResearcher" CommandArgument='<%# Eval("Id") %>' />
						</div>
						<br />


						<b>Project Researchers</b>
						<asp:GridView ID="gv_AdditionalRes" runat="server" HeaderStyle-BackColor="#8CA5FF" HeaderStyle-ForeColor="White"
							RowStyle-BackColor="#E8EFFF" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
							AutoGenerateColumns="false" CellPadding="12" CellSpacing="120" Width="95%">
							<Columns>
								<asp:BoundField DataField="ResearcherName" HeaderText="ResearcherName" />
								<asp:BoundField DataField="Email" HeaderText="Email" />
								<asp:BoundField DataField="UserName" HeaderText="UserName" />
								<asp:BoundField DataField="PMD_ResearcherID" Visible="false" HeaderText="PMD_ResearcherID" />
							</Columns>

							<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
							<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
							<EditRowStyle BackColor="#999999" />
							<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
							<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
							<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
							<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
						</asp:GridView>


						<br />

					</div>

				</div>
				<br />
				&nbsp;
			<hr />
				<div class="row">
					<div class="col-md-2">

						<input type="button" runat="server" value="    Cancel     " onclick="document.location.href = './ResProjects.aspx'" />


					</div>
					<div class="col-md-4">
					</div>
					<div class="col-md-2">
						<input type="button" onclick="document.location.href = './Default.aspx?resVar=<%= ProjectId %>'" value="Project Datasets" />
					</div>

					<div class="col-md-1">
						<asp:Button ID="btnSaveColumn" runat="server" Text="    Save     " OnClick="btnSave_Click"  ValidationGroup="Proj"/>&nbsp;
					</div>

					<div class="col-md-2">
						<asp:Button ID="btnSubmit" runat="server" Text="    Submit For Approval    " OnClick="btnSubmist_Click" />&nbsp;
					</div>

				</div>

			</div>
		</div>

	</div>
</asp:Content>
