<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="DaSH_Researcher_Portal.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%: Title %>.</h2>
	<h3>About Data Safe Haven</h3>
	

      <link rel="stylesheet" href="//code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-3.6.0.js"></script>
  <script src="https://code.jquery.com/ui/1.13.2/jquery-ui.js"></script>
     <script type="text/javascript">


			 $(function () {
				 $("#tabs").tabs();
			 });


	 </script>
	<div id="tabs">
  <ul>
    <li><a href="#tabs-1">About DaSH</a></li>
    <li><a href="#tabs-2">For Researchers: Our Services</a></li>
    <li><a href="#tabs-3">Add more</a></li>
  </ul>
  <div id="tabs-1">
    
Using NHS electronic patient records for research is an important means of understanding the factors that influence health and disease over the course of life. Scottish Safe Havens enable the secure processing and linking of health and social data for the Scottish population when it is not practicable to obtain consent from individual patients.
 

The Grampian Data Safe Haven was opened in 2012 by NHS Grampian and the University of Aberdeen’s School of Medicine, Medical Sciences and Nutrition to provide a pioneering data centre based on National guidance to improve the safe handling of linked data sets for research.

In 2015, Grampian DaSH signed up to deliver best practice in Information Security and Governance based on the Charter for Safe Havens in Scotland. In 2017 DaSH became a Scottish Government Accredited Safe Haven and is part of the Scottish Federated Safe Haven Network. DaSH undertakes an annual ISO 27001 Certification to ensure that our organisation, people, processes and technology all meet international information security best practice. DaSH processes data according to the NHS Grampian Privacy Notice and NHS Grampian are the Data Controllers of all NHS data accessed by DaSH.  Ensuring the protection of individuals’ personal data is our primary responsibility and we continually assess and improve our processes to ensure that we minimise any risks to data protection.

DaSH provides a secure setting for data linkage and data hosting projects accessed through a Virtual Private Network (VPN). Each research project within the Safe Haven has a dedicated secure folder where the research team can safely analyse their data without the ability to copy, print, or transfer data outside the Safe Haven.
   
  </div>
  <div id="tabs-2">
   
<b>For Researchers: Our Services</b><br />
The Grampian Data Safe Haven offers bespoke data storage, processing and linkage services based on the individual needs of your research project. The DaSH team works with researchers to provide detailed project planning and data management support whilst ensuring adherence to the highest standards of security and governance and protecting patient confidentiality. Each project is supported by a Research Coordinator and Data Analyst to ensure projects adhere to permissions and outputs meet the project specification and comply with information security regulations. If you require non-DaSH held datasets, we can assist with obtaining the relevant approvals, as well as coordinating the request with the data custodians and linkage between datasets.

The sections below provide further information about the services we offer, though each project will be provided with tailored components depending on your requirements. You can also read more about working with DaSH and your project's lifecycle and how to initiate a project with DaSH. If you have an idea for a data project, contact us at dash@abdn.ac.uk to discuss your requirements.
       </div>
  <div id="tabs-3">
  and so on...  </div>
</div>
    

         






</asp:Content>








