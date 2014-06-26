<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientEdit.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.ClientEdit" %>

<%@ Import Namespace="System.Web.Optimization" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">

    <div id="content">

        <div class="row">
            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-6">
                <h1 class="page-title txt-color-blueDark">
                    <i class="fa fa-bar-chart-o fa-fw "></i>
                    Remote Monitoring
							<span>> 
								Client Monitoring
                            </span>
                    <span>> 
								Setup
                    </span>
                </h1>
            </div>
            <div class="col-xs-12 col-sm-5 col-md-5 col-lg-8">
                <%--                        <ul id="sparks" class="">
                            <li class="sparks-info">
                                <h5>My Income <span class="txt-color-blue">$47,171</span></h5>
                                <div class="sparkline txt-color-blue hidden-mobile hidden-md hidden-sm">
                                    1300, 1877, 2500, 2577, 2000, 2100, 3000, 2700, 3631, 2471, 2700, 3631, 2471
                                </div>
                            </li>
                            <li class="sparks-info">
                                <h5>Site Traffic <span class="txt-color-purple"><i class="fa fa-arrow-circle-up" data-rel="bootstrap-tooltip" title="Increased"></i>&nbsp;45%</span></h5>
                                <div class="sparkline txt-color-purple hidden-mobile hidden-md hidden-sm">
                                    110,150,300,130,400,240,220,310,220,300, 270, 210
                                </div>
                            </li>
                            <li class="sparks-info">
                                <h5>Site Orders <span class="txt-color-greenDark"><i class="fa fa-shopping-cart"></i>&nbsp;2447</span></h5>
                                <div class="sparkline txt-color-greenDark hidden-mobile hidden-md hidden-sm">
                                    110,150,300,130,400,240,220,310,220,300, 270, 210
                                </div>
                            </li>
                        </ul>--%>
            </div>
        </div>

        <section id="widget-grid" class="">

            <div class="row">

                <!-- NEW COL START -->
                <article class="col-sm-12 col-md-12 col-lg-12">

                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget" id="wid-id-0" data-widget-colorbutton="false" data-widget-sortable="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-custombutton="false">
                        <!-- widget options:
								usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
				
								data-widget-colorbutton="false"
								data-widget-editbutton="false"
								data-widget-togglebutton="false"
								data-widget-deletebutton="false"
								data-widget-fullscreenbutton="false"
								data-widget-custombutton="false"
								data-widget-collapsed="true"
								data-widget-sortable="false"
				
								-->
                        <header>
                            <span class="widget-icon"><i class="fa fa-edit"></i></span>
                            <h2>Client Monitoring Setup</h2>

                        </header>

                        <!-- widget div-->
                        <div>


                            <!-- widget edit box -->
                            <div class="jarviswidget-editbox">
                                <!-- This area used as dropdown edit box -->

                            </div>
                            <!-- end widget edit box -->

                            <!-- widget content -->
                            <div class="widget-body no-padding">

                                <form id="smartForm" class="smart-form" method="post">

                                    <fieldset>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Client Code</label>
                                                <label class="input">
                                                    <input type="text" id="txtClientCode" name="txtClientCode" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Client Type</label>
                                                <label class="select">
                                                    <select id="ddlClientTypeID" class="input-sm">
                                                        <option value="1">Kiosk</option>
                                                        <option value="2">Agent</option>
                                                        <option value="3">Server</option>
                                                    </select>
                                                    <i></i>
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>
                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-4">
                                                <label class="toggle">
                                                    <input type="checkbox" id="cbxUseLocalInfo">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Use Local Info</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-4">
                                                    <label class="label">Local Info - IP Address</label>
                                                    <label class="input">
                                                        <input type="text" id="txtIPAddress" name="txtIPAddress" class="input-sm">
                                                    </label>
                                                    <div class="note">
                                                        <strong>Required Field</strong>
                                                    </div>
                                                    <label class="label">Local Info - Location ID</label>
                                                    <label class="select">
                                                        <select id="ddlLocation" name="ddlLocation" class="input-sm">
                                                        </select>
                                                        <i></i>
                                                    </label>
                                                </section>

                                                <section class="col col-4">
                                                <label class="label">Reference Client</label>
                                                <label class="select">
                                                    <select id="ddlReferenceClient" name="ddlReferenceClient" class="input-sm">
                                                    </select>
                                                    <i></i>
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>

                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-4">
                                                <label class="toggle">
                                                    <input type="checkbox" id="cbxHasMonitoringAgent" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Has Monitoring Agent</span></label>
                                            </section>

                                        </div>
                                        
                                        <div class="row">
                                            <section class="col col-4">
                                                <label class="toggle">
                                                    <input type="checkbox" id="cbxActiveList" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Active List</span></label>
                                            </section>

                                            <section class="col col-4">
                                                <label class="toggle">
                                                    <input type="checkbox" id="cbxEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>
                                        </div>
                                        
 
                                        <div class="row">
                                            <section class="col col-4">
                                                <label class="label">Effective Date</label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <input type="text" class="input-sm" name="txtEffectiveDate" id="txtEffectiveDate" placeholder="Effective Date"/>
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>
                                            </section>

                                            <section class="col col-4">
                                                <label class="label">Expired Date</label>
                                                <label class="input">
                                                    <i class="icon-append fa fa-calendar"></i>
                                                    <input type="text" class="input-sm" name="txtExpiredDate" id="txtExpiredDate" placeholder="Expired Date"/>
                                                </label>
                                            </section>
                                        </div>

                                    </fieldset>

                                    <footer>
                                        <button type="submit" class="btn btn-primary" style="float: left;" ID="btnSubmit" onclick="update();">Submit</button>
                                        <button type="button" class="btn btn-default" style="float: left;" onclick="window.history.go(-1); return false;">
                                            Back
                                        </button>
                                        <input type="hidden" id="id1" />
                                        <input type="hidden" id="m" />
                                        <input type="hidden" id="currentClientCode" />
                                    </footer>
                                </form>

                            </div>
                            <!-- end widget content -->

                        </div>
                        <!-- end widget div -->

                    </div>
                    <!-- end widget -->

                </article>
                <!-- END COL -->

            </div>
            
            
            
            <div class="row" id="divClientMonitoring" style="display: none;">

                <!-- NEW WIDGET START -->
                <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget jarviswidget-color-blueDark" id="client-monitoring-1"
                        data-widget-togglebutton="false"
                        data-widget-colorbutton="false"
                        data-widget-editbutton="false"
                        data-widget-deletebutton="false"
                        data-widget-fullscreenbutton="false"
                        data-widget-sortable="false">
                        <!-- widget options:
								usage: <div class="jarviswidget" id="wid-id-0" data-widget-editbutton="false">
				
								data-widget-colorbutton="false"
								data-widget-editbutton="false"
								data-widget-togglebutton="false"
								data-widget-deletebutton="false"
								data-widget-fullscreenbutton="false"
								data-widget-custombutton="false"
								data-widget-collapsed="true"
								data-widget-sortable="false"
				
								-->
                        <header>
                            <span class="widget-icon"><i class="fa fa-table"></i></span>
                            <h2>Client Monitoring Profile</h2>
                        </header>

                        <!-- widget div-->
                        <div>

                            <!-- widget edit box -->
                            <div class="jarviswidget-editbox">
                                <!-- This area used as dropdown edit box -->

                            </div>
                            <!-- end widget edit box -->

                            <!-- widget content -->
                            <div class="widget-body no-padding">
                                <div class="widget-body-toolbar">
                                </div>

                                <table id="dt_basic" class="table table-striped table-hover">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th>Monitoring Profile</th>
                                            <th>Effective Date</th>
                                            <th></th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>

                            </div>
                            <!-- end widget content -->
                        </div>
                        <!-- end widget div -->

                    </div>
                    <!-- end widget -->

                </article>
                <!-- WIDGET END -->

            </div>

            <!-- end row -->
        </section>
        
        <div class="row form-horizontal" id="divAddClientMonitoring" style="display: none;">
            <div class="col-xs-12">
                <div class="form-group">
                    <div class="col-md-8">
                        <a data-toggle="modal" class="btn btn-primary hidden-mobile" href="#myModal">Add New</a>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Modal -->
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog">
	        <div class="modal-dialog">
		        <div class="modal-content">
			        <div class="modal-header">
				        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
					        &times;
				        </button>
				        <h4 class="modal-title">
					        Add New Client Monitoring Profile
				        </h4>
			        </div>
			        <div class="modal-body no-padding">

				        <form id="monitoringprofile-form" class="smart-form">

							        <fieldset>
								        <section>
									        <div class="row">
										        <label class="label col col-3">Monitoring Profile</label>
										        <div class="col col-9">
                                                    <label class="select">
                                                        <select id="ddlMonitoringProfile" name="ddlMonitoringProfile" class="input-sm">
                                                        </select>
                                                        <i></i>
                                                    </label>
										            <div class="note">
                                                        <strong>Required Field</strong>
											        </div>
									            </div>
									        </div>
								        </section>

								        <section>
									        <div class="row">
										        <label class="label col col-3">Effective Date</label>
										        <div class="col col-9">
                                                    <label class="input">
                                                        <i class="icon-append fa fa-calendar"></i>
                                                        <input type="text" class="input-sm" readonly name="txtProfileEffectiveDate" id="txtProfileEffectiveDate" placeholder="Effective Date"/>
                                                    </label>
											        <div class="note">
                                                        <strong>Required Field</strong>
											        </div>
										        </div>
									        </div>
								        </section>

							        </fieldset>
							
							        <footer>
							            <button type="submit" class="btn btn-primary" ID="btnSubmitMonitoring" onclick="newMonitoring();">Submit</button>
								        <button type="button" class="btn btn-default" data-dismiss="modal">
									        Cancel
								        </button>

							        </footer>
						        </form>						
						

			        </div>

		        </div><!-- /.modal-content -->
	        </div><!-- /.modal-dialog -->
        </div><!-- /.modal -->

        
        

    </div>


    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->

    <%: Scripts.Render("~/bundles/defaultJs") %>
    <%: Scripts.Render("~/bundles/datatableJs") %>
    <%: Scripts.Render("~/bundles/myJs") %>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- PAGE RELATED PLUGIN(S) 
		<script src="..."></script>-->

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!
        initialData();

        $(document).ready(function() {

            var fullPath = "<%=HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/"))%>/ClientList.aspx";
            if (!$('nav li:has(a[href="' + fullPath + '"])').hasClass("active")) {

                $('nav').find('li.active:first').removeClass('active');

                //var index = window.location.pathname.lastIndexOf("/") + 1;
                //var filename = window.location.pathname.substr(index);

                var _obj = $('ul li a[href="' + fullPath + '"]');
                _obj.parent().addClass('active');

                $('nav li:has(a[href="' + fullPath + '"])').addClass("active");

                $('nav').find("li.active").each(function() {
                    $(this).parents("ul").slideDown(0);
                    $(this).parents("ul").parent("li").find("b:first").html('<em class="fa fa-collapse-o"></em>');
                    $(this).parents("ul").parent("li").addClass("open");
                });
            }


            pageSetUp();
            drawBreadCrumb();

            // PAGE RELATED SCRIPTS

            var response;
            $.validator.addMethod("uniqueClientCode", function(value, element) {
                    if (value == $('#currentClientCode').val()) return true;
                    $.ajax({
                        "type": "POST",
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/ExistingClientCode/",
                        "data": "{'clientCode' : '" + value + "', 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") +"}",
                        success: function(ret) {

                            if ($('#currentClientCode').val() == value && $('#m').val() == 'e') return true;

                            if (ret.isSuccess) {
                                response = (ret.aaData == 0) ? true : false;
                            } else {
                                response = false;
                            }
                            return true;
                        }
                    });
                    return response;
                },
                "Client code is taken already"
            );

            $.validator.addMethod("UseLocalInfo", function(value) {
                    if (!$('#cbxUseLocalInfo').is(':checked')) return true;
                    if (value.trim() == '') return false;
                    return true;
                },
                "Please enter IP Address"
            );

            $.validator.addMethod("NotUseLocalInfo", function (value) {
                    if ($('#cbxUseLocalInfo').is(':checked')) return true;
                    if (value.trim() == '') return false;
                    return true;
                },
                "Please enter IP Address"
            );

            $.validator.addMethod('IP4Checker', function(value) {
                if (!$('#cbxUseLocalInfo').is(':checked')) return true;
                var ip = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
                return value.match(ip);
            }, 'Invalid IP address');

            var $checkoutForm = $('#smartForm').validate({
                // Rules for form validation
                rules: {
                    txtClientCode: {
                        required: true,
                        uniqueClientCode: true
                    },
                    ddlReferenceClient: {
                        NotUseLocalInfo: true
                    },
                    txtEffectiveDate: {
                        required: true,
                        dateISO: true
                    },
                    txtExpiredDate: {
                        dateISO: true
                    },
                    txtIPAddress: {
                        UseLocalInfo: true,
                        IP4Checker: true
                    },
                    ddlLocation: {
                        UseLocalInfo: true
                    },
                    ddlMonitoringProfile: {
                        required: true,
                    },
                },
                messages: {
                    txtClientCode: {
                        required: 'Please enter client code',
                        uniqueClientCode: 'This client code is taken already'
                    },
                    ddlReferenceClient: {
                        NotUseLocalInfo: 'Please select reference client'
                    },
                    txtEffectiveDate: {
                        required: 'Please enter effective date',
                        dateISO: 'Please enter effective date in DATE format'
                    },
                    txtExpiredDate: {
                        dateISO: 'Please enter expired date in DATE format'
                    },
                    txtIPAddress: {
                        UseLocalInfo: 'Please enter IP address',
                        IP4Checker: "Invalid IP address"
                    },
                    ddlLocation: {
                        UseLocalInfo: 'Please select location'
                    },
                    ddlMonitoringProfile: {
                        required: 'Please select monitoring profile',
                    },
                },

                errorPlacement: function(error, element) {
                    error.insertAfter(element.parent());
                }

            });

            $('#monitoringprofile-form').validate({
                // Rules for form validation
                rules: {
                    ddlMonitoringProfile: {
                        required: true,
                    },
                },
                messages: {
                    ddlMonitoringProfile: {
                        required: 'Please select monitoring profile',
                    },
                },

                errorPlacement: function(error, element) {
                    error.insertAfter(element.parent());
                }
            });

            $('#txtEffectiveDate').datepicker({
                dateFormat: 'yy-mm-dd',
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                maxDate: new Date(2099, 12, 0),
                changeMonth: true,
                changeYear: true,
                onSelect: function(selectedDate) {
                    $('#txtExpiredDate').datepicker('option', 'minDate', selectedDate);
                },
                onClose: function(selectedDate) {
                    $('#txtExpiredDate').datepicker('option', 'minDate', selectedDate);
                }
            });

            $('#txtExpiredDate').datepicker({
                dateFormat: 'yy-mm-dd',
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                maxDate: new Date(2099, 12, 0),
                changeMonth: true,
                changeYear: true,
                onSelect: function(selectedDate) {
                    $('#txtEffectiveDate').datepicker('option', 'maxDate', selectedDate);
                },
                onClose: function(selectedDate) {
                    if (selectedDate == '') selectedDate = new Date(2099, 12, 0);
                    $('#txtEffectiveDate').datepicker('option', 'maxDate', selectedDate);
                }

            });

            $('#txtProfileEffectiveDate').datepicker({
                dateFormat: 'yy-mm-dd',
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                maxDate: new Date(2099, 12, 0),
                changeMonth: true,
                changeYear: true,
            });



            $("#txtEffectiveDate").datepicker('setDate', new Date());
            $('#txtExpiredDate').datepicker('option', 'minDate', new Date());
            $("#txtProfileEffectiveDate").datepicker('setDate', new Date());

            if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {

                $('#divClientMonitoring').show();
                $('#divAddClientMonitoring').show();

                Pace.restart();
                $.ajax({
                    "type": "POST",
                    "dataType": 'json',
                    "contentType": "application/json; charset=utf-8",
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/GetClient/",
                    "data": "{'id' : " + <%=Request["id1"]%> + ", 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                    "success": function(ret) {

                        if (ret.status == "1") {

                            var myData = JSON.parse(ret.data);

                            $('#txtClientCode').val(myData.ClientCode);
                            $('#currentClientCode').val(myData.ClientCode);

                            $('#ddlClientTypeID').val(myData.ClientTypeId);


                            $('#cbxUseLocalInfo').attr('checked', myData.UseLocalInfo);
                            $('#ddlReferenceClient').val(myData.ReferenceClientId);
                            $('#txtIPAddress').val(myData.IpAddress);
                            $('#ddlLocation').val(myData.LocationId);

                            $('#cbxHasMonitoringAgent').attr('checked', myData.HasMonitoringAgent);
                            $('#cbxActiveList').attr('checked', myData.ActiveList);
                            $('#cbxEnable').attr('checked', myData.Enable);

                            if (myData.EffectiveDate != null && myData.EffectiveDate != '') {
                                var eff = new Date(myData.EffectiveDate.substr(0, 10));
                                $("#txtEffectiveDate").datepicker('setDate', eff);
                                $('#txtExpiredDate').datepicker('option', 'minDate', eff);

                            }

                            if (myData.ExpiredDate != null && myData.ExpiredDate != '') {
                                var exp = new Date(myData.ExpiredDate.substr(0, 10));
                                $("#txtExpiredDate").datepicker('setDate', exp);
                                $('#txtEffectiveDate').datepicker('option', 'maxDate', exp);
                            }

                            $('#id1').val(myData.ClientId);
                            $('#m').val("e");


                        } else if (ret.status == "-1") {

                            try {
                                $.smallBox({
                                    title: "Access Denied!",
                                    content: "<i class='fa fa-clock-o'></i> <i>No access rights to complete this operation.</i>",
                                    color: "#C46A69",
                                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                    timeout: 4000
                                });

                            } catch (e) {
                                alert("No access rights to complete this operation.");
                            }
                        } else if (ret.status == "0") {
                            try {
                                $.smallBox({
                                    title: "Get Object Failed",
                                    content: "<i class='fa fa-clock-o'></i> <i>Failed to complete this operation.</i>",
                                    color: "#C46A69",
                                    iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                    timeout: 4000
                                });

                            } catch (e) {
                                alert("Failed to complete this operation.");
                            }
                        }

                    },

                });

                //Load Client Monitoring Profile Here
                //...................
                $('#dt_basic').dataTable({
                    "sPaginationType": "bootstrap_full",
                    "bFilter": false,
                    "bAutoWidth": false,
                    "bPaginate": false,
                    "bInfo": true,
                    "sDom": 'r<"dt-top-row"lf><"dt-wrapper"<"datatable-scroll"t>><"dt-row dt-bottom-row"<"row"<"col-sm-6"i><"col-sm-6 text-right"p>>>',
                    "bServerSide": false,
                    "aaSorting": [[2, "desc"]],
                    "sAjaxSource": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/ListClientMonitoring/",
                    "fnServerData": function (sSource, aoData, fnCallback) {
                    aoData.push({ "name": "clientID", "value": <%=Request["id1"]%> });
                    aoData.push({ "name": "dt", "value": dateFormat(new Date(), "yyyymmddHHMMss") });

                    Pace.restart();
                    $.ajax({
                        "type": "POST",
                        "dataType": 'json',
                        "url": sSource,
                        "data": aoData,
                        "success": function (data) {
                            fnCallback(data);
                        }
                    });
                },
                "fnDrawCallback": function ( oSettings ) {
                    /* Need to redo the counters if filtered or sorted */
                    if ( oSettings.bSorted || oSettings.bFiltered )
                    {
                        for ( var i=0, iLen=oSettings.aiDisplay.length ; i<iLen ; i++ )
                        {
                            $('td:eq(0)', oSettings.aoData[ oSettings.aiDisplay[i] ].nTr ).html( i+1 );
                        }
                    }
                },
                "aoColumns": [
                    {
                        "mData": null,
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "80",
                        "sClass": "center"
                    },
                    {
                        "mDataProp": "ProfileName",
                        "bSearchable": false,
                        "bSortable": true,
                        "sWidth": "400"
                    },
                    {
                        "mDataProp": "EffectiveDate",
                        "bSearchable": false,
                        "bSortable": true,
                        "sWidth": "300",
                        "iDataSort": 3,
                        "bUseRendered": false,
                        "fnRender": function (oObj) {
                            if (oObj.aData["EffectiveDate"] == null) return '';
                            var date = new Date(parseInt(oObj.aData["EffectiveDate"].substr(6)));
                            return dateFormat(date, "dd/mm/yyyy");
                        }
                    },
                    {
                        "mDataProp": "EffectiveDate",
                        "bSearchable": false,
                        "bSortable": true,
                        "bVisible": false,
                        "bUseRendered": false,
                        "fnRender": function (oObj) {
                            if (oObj.aData["EffectiveDate"] == null) return '';
                            var date = new Date(parseInt(oObj.aData["EffectiveDate"].substr(6)));
                            return dateFormat(date, "yyymmdd");
                        }
                    },
                    {
                        "mData": null,
                        "bSearchable": false,
                        "bSortable": false,
                        "fnRender": function (oObj) {
                            return '<a id="del_item_' + oObj.aData["MonitoringProfileId"] + '" class="btn btn-danger btn-xs" href="javascript:deleteRow(' + oObj.aData["MonitoringProfileId"] + ');"><i class="glyphicon glyphicon-trash"></i></a>';
                        }
                    }
                ],

            });


                /* END TABLE TOOLS */

            $.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
                // DataTables 1.10 compatibility - if 1.10 then versionCheck exists.
                // 1.10s API has ajax reloading built in, so we use those abilities
                // directly.
                if ($.fn.dataTable.versionCheck) {
                    var api = new $.fn.dataTable.Api(oSettings);

                    if (sNewSource) {
                        api.ajax.url(sNewSource).load(fnCallback, !bStandingRedraw);
                    }
                    else {
                        api.ajax.reload(fnCallback, !bStandingRedraw);
                    }
                    return;
                }

                if (sNewSource !== undefined && sNewSource !== null) {
                    oSettings.sAjaxSource = sNewSource;
                }

                // Server-side processing should just call fnDraw
                if (oSettings.oFeatures.bServerSide) {
                    this.fnDraw();
                    return;
                }

                this.oApi._fnProcessingDisplay(oSettings, true);
                var that = this;
                var iStart = oSettings._iDisplayStart;
                var aData = [];

                this.oApi._fnServerParams(oSettings, aData);

                oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
                    /* Clear the old information from the table */
                    that.oApi._fnClearTable(oSettings);

                    /* Got the data - add it to the table */
                    var aData = (oSettings.sAjaxDataProp !== "") ?
                        that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

                    for (var i = 0 ; i < aData.length ; i++) {
                        that.oApi._fnAddData(oSettings, aData[i]);
                    }

                    oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

                    that.fnDraw();

                    if (bStandingRedraw === true) {
                        oSettings._iDisplayStart = iStart;
                        that.oApi._fnCalculateEnd(oSettings);
                        that.fnDraw(false);
                    }

                    that.oApi._fnProcessingDisplay(oSettings, false);

                    /* Callback user function - for event handlers etc */
                    if (typeof fnCallback == 'function' && fnCallback !== null) {
                        fnCallback(oSettings);
                    }
                }, oSettings);
            };

            $.fn.dataTableExt.oApi.fnStandingRedraw = function (oSettings, idx) {
                if (oSettings.oFeatures.bServerSide === false) {
                    var before = oSettings._iDisplayStart - idx;

                    oSettings.oApi._fnReDraw(oSettings);

                    // iDisplayStart has been reset to zero - so lets change it back
                    oSettings._iDisplayStart = before;
                    oSettings.oApi._fnCalculateEnd(oSettings);
                }
                oSettings._iDisplayStart = oSettings._iDisplayStart - (idx * oSettings._iDisplayLength);
                // draw the 'current' page
                oSettings.oApi._fnDraw(oSettings);
            };

            } 

            $("#btnSubmit").on("click", function (event) {
                event.preventDefault(); // will work!
            });


            $("#btnSubmitMonitoring").on("click", function (event) {
                event.preventDefault(); // will work!
            });

            
        });
        // Initial Data
        function initialData() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/InitDataForEdit/",
                "success": function(data) {
                    $('#ddlReferenceClient').append(unescape(data.listMainAppClients));
                    $('#ddlLocation').append(unescape(data.listLocation));
                },

            });
            loadListMonitoringProfile();
        }

        function loadListMonitoringProfile() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MonitoringProfile/List/",
                "data": "{'activeList' : true, 'excludeClientID' :  <%=Request["id1"]%>, 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                "success": function(data) {
                    $('#ddlMonitoringProfile').append(unescape(data.ddlMonitoringProfile));
                },

            });
        }


        function newMonitoring() {
            if (!$('#monitoringprofile-form').valid()) return;
            tmpObj = function () {
                this.clientID = $('#id1').val();
                this.monitoringProfileID = $('#ddlMonitoringProfile').val();
                this.effectiveDate = $('#txtProfileEffectiveDate').val();
                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var updObj = new tmpObj();
            Pace.restart();

            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/AddClientMonitoring/",
                "data": JSON.stringify(updObj),
                "success": function (ret) {

                    if (ret.status == "1") {
                        try {
                            $.smallBox({
                                title: "Update Complete",
                                content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                                color: "#659265",
                                iconSmall: "fa fa-check fa-2x fadeInRight animated",
                                timeout: 2000
                            });

                        } catch (e) {
                            alert("This operation is complete.");
                        }

                        var oTable = $('#dt_basic').dataTable();
                        oTable.fnReloadAjax();

                        $('#myModal').modal('toggle');
                        $('#monitoringprofile-form')[0].reset();
                        $("#txtProfileEffectiveDate").datepicker('setDate', new Date());
                        $('#ddlMonitoringProfile').html("");
                        loadListMonitoringProfile();

                    } else if (ret.status == "-1") {

                        try {
                            $.smallBox({
                                title: "Update Failed!",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert(ret.error);
                        }
                    } else if (ret.status == "0") {
                        try {
                            $.smallBox({
                                title: "Update Failed",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert(ret.error);
                        }
                    }

                },

            });
        }

        function update() {

            if (!$('#smartForm').valid()) return;

            tmpObj = function () {
                this.id = $('#id1').val();
                this.m = $('#m').val();
                this.clientCode = $('#txtClientCode').val();
                this.clientTypeID = $('#ddlClientTypeID').val();
                this.useLocalInfo = $('#cbxUseLocalInfo').is(':checked');
                this.referenceClientID = $('#ddlReferenceClient').val();
                this.ipAddress = $('#txtIPAddress').val();
                this.locationID = $('#ddlLocation').val();
                this.hasMonitoringAgent = $('#cbxHasMonitoringAgent').is(':checked');
                this.activeList = $('#cbxActiveList').is(':checked');
                this.status = $('#cbxEnable').is(':checked');
                this.effectiveDate = $('#txtEffectiveDate').val();
                this.expiredDate = $('#txtExpiredDate').val();
                this.state = $('#ddlState').val();
                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");

                //(int? id, string m, string clientCode, int? clientTypeID
                //, int? referenceClientID, bool? activeList, bool? status
                //, DateTime? effectiveDate, DateTime? expiredDate, int? state)

            };
            var updObj = new tmpObj();
            Pace.restart();

            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/UpdateClient/",
                "data": JSON.stringify(updObj),
                "success": function (ret) {

                    if (ret.status == "1") {
                        try {
                            $.smallBox({
                                title: "Update Complete",
                                content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                                color: "#659265",
                                iconSmall: "fa fa-check fa-2x fadeInRight animated",
                                timeout: 2000
                            });

                        } catch (e) {
                            alert("This operation is complete.");
                        }

                        setTimeout(function () {
                            window.location.href = 'ClientList.aspx';
                        }, 1000);

                    } else if (ret.status == "-1") {

                        try {
                            $.smallBox({
                                title: "Update Failed!",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert(ret.error);
                        }
                    } else if (ret.status == "0") {
                        try {
                            $.smallBox({
                                title: "Update Failed",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert(ret.error);
                        }
                    }

                },

            });
        }


        function deleteRow(id) {
            Row = function (id) {
                this.clientID = <%=Request["id1"]%>;
                this.monitoringProfileID = id;
                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var delRow = new Row(id);

            $.SmartMessageBox({
                title: "Delete Confirmation",
                content: "Are you sure you want to delete this item?",
                buttons: '[No][Yes]'
            }, function (ButtonPressed) {
                if (ButtonPressed === "Yes") {
                    $.ajax({
                        "type": "POST",
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/DeleteClientMonitoring",
                        "data": JSON.stringify(delRow),
                        "success": function (data) {
                            if (data == "-1") {
                                try {
                                    $.smallBox({
                                        title: "Access Denied!",
                                        content: "<i class='fa fa-clock-o'></i> <i>No Access Rights to delete!</i>",
                                        color: "#C46A69",
                                        iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                        timeout: 4000
                                    });

                                } catch (e) {
                                    alert("No Access Rights to delete!");
                                }
                            } else if (data == "1") {
                                var oTable = $('#dt_basic').dataTable();
                                oTable.fnReloadAjax();
                                $('#ddlMonitoringProfile').html("");
                                loadListMonitoringProfile();

                                try {
                                    $.smallBox({
                                        title: "Delete Complete",
                                        content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                                        color: "#659265",
                                        iconSmall: "fa fa-check fa-2x fadeInRight animated",
                                        timeout: 4000
                                    });

                                } catch (e) {
                                    alert("This operation is complete.");
                                }
                            } else if (data == "0") {
                                try {
                                    $.smallBox({
                                        title: "Delete Failed",
                                        content: "<i class='fa fa-clock-o'></i> <i>Failed to complete this operation.</i>",
                                        color: "#C46A69",
                                        iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                        timeout: 4000
                                    });

                                } catch (e) {
                                    alert("Failed to complete this operation.");
                                }
                            }

                        },

                    });
                }

            });
        }

    </script>



</asp:content>
