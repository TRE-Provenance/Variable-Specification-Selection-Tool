<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DaSH_Researcher_Portal._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<%--    <div class="jumbotron">--%>
	<br />
	<br />
	<br />
	<div class="row">
		<div class="col-md-12  px-1 conboarder">
			<img src="Resources/Images/DaSH_logolong2_rdax_850x637_opt.jpg" style="border-radius: 25px; margin: 5px; float: left;" />

			The Grampian Data Safe Haven (DaSH) is a secure, virtual healthcare data analysis and storage centre established by the University of Aberdeen and NHS Grampian to allow for the secure processing and linking of health data for the Grampian and Scottish population when it is not practicable to obtain consent from individual patients.

DaSH technology ensures unconsented healthcare, social data and other types of sensitive data are accessible for research and clinical purposes whilst protecting individuals’ privacy. Our ethos is built on working with clinicians, researchers and industry partners to improve health and social care by providing a safe and secure environment and enabling cutting-edge research.

Are you a researcher planning a research project linking one or more datasets? Learn more about our services or contact us at dash@abdn.ac.uk.
		</div>

			<div class="col-md-12">
				&nbsp;

			</div>


		<%--  <div class="row" >--%>
		<div class="col-md-5 px-1 conboarder" >
			<h2>Researcher Portal</h2>
			<p>
				<%--                <a class="btn btn-default" href="ResForms/Default.aspx">Select Datasets &raquo;</a>--%>
				<a class="btn btn-default" href="ResForms/ResProjects.aspx">My DaSH &raquo;</a>
			</p>
		</div>
		<div class="col-md-0">
		</div>
		<div class="col-md-6 conboarderRight" >
			<img src="resources/images/dash.jpg" alt="University of Aberdeen" style="border-radius: 25px; margin: 5px; float: left; width: 382px;">
			<p style="text-align: justify; font-size: 25px;">
				<h2>DaSH</h2>
				The Grampian Data Safe Haven offers bespoke data storage, processing and linkage services based on the individual needs of your research project. The DaSH team works with researchers to provide detailed project planning and data management support whilst ensuring adherence to the highest standards of security and governance and protecting patient confidentiality. Each project is supported by a Research Coordinator and Data Analyst to ensure projects adhere to permissions and outputs meet the project specification and comply with information security regulations. If you require non-DaSH held datasets, we can assist with obtaining the relevant approvals, as well as coordinating the request with the data custodians and linkage between datasets. The sections below provide further information about the services we offer, though each project will be provided with tailored components depending on your requirements. You can also read more about working with DaSH and your project's lifecycle and how to initiate a project with DaSH. 
            If you have an idea for a data project, contact us at dash@abdn.ac.uk to discuss your requirements.
			</p>
		</div>

	</div>


</asp:Content>
