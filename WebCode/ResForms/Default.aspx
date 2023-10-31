<%@ Page Title="Project Datasets" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DaSH_Researcher_Portal.ResForms.Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<script src="https://code.jquery.com/jquery-3.6.0.js"></script>
	<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
	<script type="text/javascript">


		function off(varName) {
			var overlay = "overlay_" + varName;
			togglePopup(varName);
			document.getElementById(overlay).style.display = "none";
		}

		// Function to show and hide the popup
		function togglePopup(varName) {
			var overlay = "overlay_" + varName;
			document.getElementById(overlay).style.display = "block";
			var contentTog = '#about_' + varName;
			$(contentTog).toggle();
		}


		function divDescOnload(info, varName) {
			if (info.length > 0) {
				document.getElementById(varName).style.display = "block";
			}
		}


		function resctrictionClick(varName, clientID, divToHide) {

			var hType = "h_type_" + varName;
			var typeVal = document.getElementById(hType);

			//alert(divToHide);

			var Cbox = '#' + clientID;

			//if (typeVal.value == 'date' || typeVal.value == 'datetime2') {
			var resctrictionDisp = "#" + divToHide


			//$('#divToHide').toggle()

			if ($(Cbox).is(':checked')) {
				$(resctrictionDisp).show();
			}
			else {
				$(resctrictionDisp).hide();
			}

			$('[id*=tb_resctrictionMinDate]').datepicker({
				dateFormat: 'dd/mm/yy'
			});
			$('[id*=tb_resctrictionMaxDate]').datepicker({
				dateFormat: 'dd/mm/yy'
			});
			//	}

			if (typeVal.value == 'int') {

				//	alert(clientID);
			}
		}

	</script>


	<%--<%@ Reference Control="../UserControls/WebUserControl1.ascx" %>
	--%>



	<br />
	<br />
	<br />
	<%--	<br />
	<br />
	<br />

	<button type="button" class="btn btn-lg btn-danger" data-toggle="popover" title="Popover title" data-content="And here's some amazing content. It's very engaging. Right?">Click to toggle popover</button>--%>


	<br />
	<%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
	<div style="vertical-align: top;">

		<table style="width: 100%; vertical-align: top;">

			<tr>
				<%--<td style="width: 20%;"></td>--%>

				<td style="vertical-align: top;">

					<div class="col-md-12 px-1 conboarder">
						<asp:Panel runat="server" GroupingText="Dataset Selection" ID="pnl_Datasets">
							<table border="0" width="99%">
								<asp:Repeater ID="rptDataSets" runat="server">
									<ItemTemplate>

										<tr style="border-bottom: 1px solid antiquewhite;">

											<td>
												<asp:HiddenField ID="h_DataSetID" runat="server" Value='<%#Eval("DatasetId") %>' />
												&nbsp;<asp:CheckBox ID="chkDataSet" Style="float: left;" runat="server" Checked='<%#Eval("dsSelected")%>' OnClick="HighlightSelectedRow($(this));" />
											</td>

											<td>
												<div style="float: left;">
													<img src="../Resources/Images/info.jpg" hidden id='img_<%#Eval("DS_Name") %>'
														onload="divDescOnload('<%#Eval("Description")%>','img_<%#Eval("DS_Name") %>')"
														onclick="togglePopup('<%#Eval("DS_Name") %>')" style="width: 15px; height: 15px; float: right;" alt="variable info" />

													<b><%#Eval("DS_Name")%></b>
												</div>
												<div class="overlay" id='overlay_<%#Eval("DS_Name") %>'>
													<div class='content' id='about_<%#Eval("DS_Name") %>' hidden>

														<div onclick="off('<%#Eval("DS_Name") %>')" class="close-btn">× </div>

														<h3><%#Eval("DS_Name") %> </h3>
														<%#Eval("Description") %>
														<br />

														<a href='<%#Eval("webURL") %>' target="_blank"><%#Eval("webURL") %></a>
													</div>
												</div>
												<asp:Literal ID="DS_ID" runat="server" Text='<%#Eval("DatasetID") %>' Visible="false"></asp:Literal>
											</td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</table>
							<br />
							&nbsp;<asp:Button ID="btnSave_Datasets" runat="server" Text="     Save  Dataset Selection   " CssClass="button150 waves-effect waves-light" OnClick="btnSaveDataSets_Click" /><br />

							<%--
							<table border="1" cellspacing="2" cellpadding="2" style="width: 500px;">

								<tr>
									<td>
										<asp:CheckBoxList ID="cbl_Datasets" runat="server"></asp:CheckBoxList>
										&nbsp;<asp:Button ID="btnSave_Datasets" runat="server" Text="     Save  Dataset Selection   " CssClass="button150 waves-effect waves-light" OnClick="btnSaveDataSets_Click" /><br />
									</td>
								</tr>
							</table>--%>
						</asp:Panel>
					</div>
				</td>
			</tr>
		</table>
		<hr />
		<table style="width: 100%;">
			<tr border="1">

				<%--			<td style="width: 20%; vertical-align: top;">
					<%--<asp:Panel runat="server" GroupingText="To Do/Consider" ID="Panel1">
						<ul>
							<li>auto select linkage Vars (e.g. CHI)</li>
							<li>Colour rows red:non release.  Orange:partial release (req. manipulation e.g. DOB to 01MMYYYY) Green full release </li>
							<li>DB cat NHS variables for above + those that don't need to be displayed (e.g. create date)</li>
							<li>resctriction Flags (if int,date, ICDs etc add rang inputs) - text box to outline (keep short to keep concise? 50 words?) </li>
							<li>
							New Projects (upload docs? PID etc?)</lili>
						<li>Projects/Researcher(s) relationship </li>
							<li>Admin Portal to set to read only Etc. - link to PMD status? </li>
							<li>Developer Portal to set comments per var? outline functions or code snippets maybe?</li>
							<li>download pdf of project text?</li>
							<li>REQUEST WEB SPACE</li>
							<li>logins required for outside abdn - need new membership DB (built into razor investigate)</li>
							<li>Auto generate SQL code from DS/Vars - SQL SP (save to table(?) and download as txt(?))</li>
							<li>AUDIT AUDIT AUDIT - who does what .. prob. just last person to insert/update + date would suffice</li>
							<li>use DS alias to display - rollover with info or link to 'about DS' page?</li>
							<li>tidy and MOVE Data processing to Actions (if not going Razor)</li>
							<li>Request versioning</li>
							<li>User datasets?? - do we get them to upload the DSName and vars? (associate ONLY with that project??) - possible issue with parsing file as they will all be different (even if we send template will they hack it??)</li>
							<li>Accessibility (mobile device setup req. bootstrap)</li>


						</ul>
					</asp:Panel>
				</td>--%>
				<td style="vertical-align: top;">

					<div class="col-md-12 px-1 conboarder">
						<asp:Panel runat="server" GroupingText="Dataset Selection" ID="pnl_dsVars">
							<table border="0" style="width: 99%">

								<tr>
									<td>Please Select Dataset to View/Edit Requested Variables.<br />
										<asp:DropDownList ID="ddl_ResDatasets" runat="server" OnSelectedIndexChanged="ddl_ResDatasets_SelectedIndexChanged" AutoPostBack="true">
											<asp:ListItem Selected="True" Value="">--Please Select--</asp:ListItem>
										</asp:DropDownList>

									</td>
									<td>
										<asp:Panel runat="server" ID="pnl_DownloadSpecBtn" BorderWidth="0px" Width="99%">

											<asp:Button ID="btn_DownloadSpec" runat="server" Text=" Download Spec Sheet " CssClass="button150 waves-effect waves-light" AutoPostBack="true" Visible="true" OnClick="DsVars_To_Excel" />


											<asp:Button ID="Button1" runat="server" Text=" Download CSV Sheet " CssClass="button150 waves-effect waves-light" AutoPostBack="true" Visible="true" OnClick="DsVars_To_CSV" />
										</asp:Panel>
									</td>

								</tr>
								<tr>
									<td style="width: 99%" colspan="2">
										<hr />
									</td>
								</tr>

								<tr>
									<td colspan="2">
										<asp:Panel runat="server" ID="pnl_varlist" BorderWidth="0px" Width="99%">
											<%--<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>--%>
											<asp:Panel runat="server" ID="pnl_custVars" BorderWidth="0px" Width="99%">

												
												<asp:HiddenField ID="h_ProjectDatasetID" runat="server" value='<%= ProjectDatasetID %>' />
												<div class="col-md-12 conboarder">
													CUSTOM VARIABLES SELECTION
													<hr />
													<table border="0" width="99%" cellspacing="1" cellpadding="1">
														<tr>
															<td >Output Name :</td>
															<td colspan="4" style="height: 23px">
																<asp:TextBox ID="tb_CustomVarName" Enabled="true" runat="server" Text='<%#Eval("custVarName")%>' /></td>
														</tr>
														<tr>
															<td>Field 1 :</td>
															<td>
																<asp:DropDownList ID="dl_CustField_1" runat="server" DataTextField="VarName" DataValueField="DS_VariableID" AppendDataBoundItems="true">
																	<asp:ListItem Selected="True" Value="">--Please Select--</asp:ListItem>
																</asp:DropDownList></td>
															<td>Field 2  :</td>
															<td>
																<asp:DropDownList ID="dl_CustField_2" runat="server" DataTextField="VarName" DataValueField="DS_VariableID" AppendDataBoundItems="true">
																	<asp:ListItem Selected="True" Value="">--Please Select--</asp:ListItem>
																</asp:DropDownList></td>
														</tr>

														<tr>
															<td>
																Description:</td>
															<td colspan="3">
															
																		<asp:TextBox ID="tb_CustVarDescrition" Style="max-width: 1768px;" Enabled="true" runat="server" Rows="2" Width="100%" TextMode="MultiLine" Text='<%#Eval("custVar_Description")%>' Title="Custom variable how to calculate" />																
															</td>
														</tr>													
													</table>


													<%--	<asp:LinkButton ID="lnkb_AddResearcher" runat="server" Text="Add Researcher" OnClick="AddResearcher"
							CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>--%>

													<asp:Button ID="btn_AddCustomerVariable" runat="server" Text="Add Custom Variable" OnClick="btn_AddCustVar_Click" CommandArgument='<%# Eval("Id") %>' /><br />
													<br />
													<asp:GridView ID="gv_CustomVariable" runat="server" HeaderStyle-BackColor="#8CA5FF" HeaderStyle-ForeColor="White"
														RowStyle-BackColor="#E8EFFF" AlternatingRowStyle-BackColor="White" AlternatingRowStyle-ForeColor="#000"
														AutoGenerateColumns="false" Width="99%" CellPadding="2" CellSpacing="2">
														<Columns>
															<asp:BoundField DataField="Output_VarName" HeaderText="Output Variable Name" />
															<asp:BoundField DataField="DS_Variable1_VarName" HeaderText="Var To Use1" />
															<asp:BoundField DataField="DS_Variable2_VarName" HeaderText="Var To Use2" />
															<asp:BoundField DataField="Researcher_Comments" HeaderText="Description of usage" />
														</Columns>
														<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
														<EditRowStyle BackColor="#999999" />
														<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
														<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
														<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
														<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
														<EmptyDataTemplate>
															No Custom variables defined.
														</EmptyDataTemplate>
													</asp:GridView>

												</div>
												<br />
											</asp:Panel>

											<br />
											<br />

											<svg width="15" height="15">
												<rect width="15" height="15" style="fill: mistyrose; stroke-width: 3; stroke: rgb(0,0,0)" />
											</svg>
											Unavailable For Release


										<div id="rptTable">
											<table cellspacing="1" cellpadding="1" style="width: 100%; border-collapse: collapse;" title="Dataset Selections">
												<tr style="background-color: #1C4392; color: #FFFFFF; height: 30px;">
													<%--<td>Project datasetID</td>--%>
													<td>&nbsp;Variable Name</td>
													<td>Apply Condition</td>
													<td>Output Variable Name</td>
													<td style="border-right: 2px solid #1C4392;"><b>Include in Output</b></td>
												</tr>
												<asp:Repeater ID="rptDataSetDetails" runat="server" OnItemDataBound="rptDataSetDetails_ItemDataBound">
													<ItemTemplate>
														<tr style="border-bottom: 1px solid antiquewhite; padding: 1px;" id="tr" runat="server">
															<td>
																<div style="float: left;">
																	<img src="../Resources/Images/info.jpg" hidden id='img_<%#Eval("VarName") %>'
																		onload="divDescOnload('<%#Eval("VarDescription")%>','img_<%#Eval("VarName") %>')"
																		onclick="togglePopup('<%#Eval("VarName") %>')" style="width: 20px; height: 20px; float: right;" alt="variable info" title='<%#Eval("VarDescription") %>' />

																	<%#Eval("VarName")%> [<%#Eval("DataType") %>]
																</div>

																<div class="overlay" id='overlay_<%#Eval("VarName") %>'>
																	<div class='content' id='about_<%#Eval("VarName") %>' hidden>
																		<%-- <div onclick="togglePopup('<%#Eval("VarName") %>')" class="close-btn">  × </div>--%>
																		<div onclick="off('<%#Eval("VarName") %>')" class="close-btn">× </div>
																		<h3><%#Eval("VarName") %> </h3>
																		<%#Eval("VarDescription") %>
																		<a href="http:www.abdn.ac.uk" target="_blank">linky</a>
																	</div>
																</div>

																<asp:Literal ID="DS_VariableID" runat="server" Text='<%#Eval("DS_VariableID") %>' Visible="false"></asp:Literal>

																<input type="hidden" id="h_type_<%#Eval("VarName") %>" value='<%#Eval("DataType") %>' />
																<!--	<asp:Label ID="lbl_DataType" runat="server" Text='<%#Bind("DataType") %>'></asp:Label>-->
																<asp:HiddenField ID="h_DataType" runat="server" Value='<%#Bind("DataType") %>'></asp:HiddenField>
																<asp:HiddenField ID="h_ProjDataSetID" runat="server" Value='<%#Eval("Project_Dataset_Id") %>' />
																<asp:HiddenField ID="h_ReseacherVariableID" runat="server" Value='<%#Eval("Researcher_VariableID") %>' />
																<asp:HiddenField ID="h_VarName" runat="server" Value='<%#Eval("VarName") %>' />
																<asp:HiddenField ID="h_Releaseable" runat="server" Value='<%#Eval("Can_Release") %>' />

															</td>
															<td>

																<%--		
															<asp:CheckBox ID="cbIsresctriction" runat="server"  Checked='<%#Eval("Isresctriction").ToString().Equals("1") %>'  onclick='<%# String.Format("javascript:return resctrictionClick(\"{0}\")", Eval("VarName").ToString())  %>'/>
																--%>

																<asp:CheckBox ID="cbIsresctriction" runat="server" Checked='<%#Eval("IsRestriction").ToString().Equals("1") %>' />

																<asp:Panel ID="pnl_restictionDate" runat="server">
																	<%-- <div onclick="togglePopup('<%#Eval("VarName") %>')" class="close-btn">  × </div>--%>		
																	Start Date:
																	<asp:TextBox ID='tb_resctrictionMinDate' Enabled="true" runat="server" Text='<%#Eval("IsRestrictionMinRange")%>' Title="Start Date" /><br />
																	End Date  :
																	<asp:TextBox ID='tb_resctrictionMaxDate' Enabled="true" runat="server" Text='<%#Eval("IsRestrictionMaxRange")%>' Title="End Date" />
																</asp:Panel>

																<asp:Panel ID="pnl_restrictionInt" runat="server">
																	<%-- <div onclick="togglePopup('<%#Eval("VarName") %>')" class="close-btn">  × </div>--%>																						
																	Min : 
																	<asp:TextBox ID='tb_resctrictionMin_INT' Enabled="true" runat="server" Text='<%#Eval("IsRestrictionMinRange")%>' Title="Min Number" /><br />
																	Max :
																	<asp:TextBox ID='tb_resctrictionMax_INT' Enabled="true" runat="server" Text='<%#Eval("IsRestrictionMaxRange")%>' Title="Max Number" />
																</asp:Panel>
															</td>
															<td>
																<asp:TextBox ID="output_VarName" Enabled="true" runat="server" Text='<%#Eval("output_VarName")%>' ToolTip='<%#Eval("VarDescription") %>' />
															</td>
															<td style="border-left: 2px solid #1C4392; border-right: 2px solid #1C4392; text-align:center;">
																<asp:CheckBox ID="chkSelect" runat="server" Checked='<%#Eval("VarSelected").ToString().Equals("1") %>' OnClick="HighlightSelectedRow($(this));" />
																<%--															Releaseable <%#Eval("Can_Release").ToString().Equals("1") %>--%>
															</td>
														</tr>
													</ItemTemplate>
												</asp:Repeater>
												<tr>
													<td colspan="4">
														<hr />
													</td>
												</tr>

												<tr style="background-color: #1C4392; border-right: 2px solid #1C4392;">
													<td colspan="4" style="text-align: right">&nbsp;
													<b><asp:Button ID="btnSaveColumn" runat="server" Text="     Save   Variables   " OnClick="btnSaveVariables_Click" /></b>&nbsp;
													&nbsp;&nbsp;
															
													<input type="button" onclick="document.location.href = './DashProjectItem.aspx?resVar=<%= ProjectId %>'" value="Cancel" />
														
													</td>
												</tr>
											</table>
										</div>
										</asp:Panel>
									</td>
								</tr>
							</table>
						</asp:Panel>

					</div>
				</td>
			</tr>
			<tr>
				<td style="width: 20%;"></td>
			</tr>
		</table>

	</div>

</asp:Content>

