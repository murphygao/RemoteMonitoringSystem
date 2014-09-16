<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebsiteMonitoringEdit.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.WebsiteMonitoringEdit" %>

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
								Website Monitoring
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
                            <h2>Website Monitoring Setup</h2>

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
                                                    <input type="text" id="txtClientCode" name="txtClientCode" class="input-sm" readonly="readonly">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>, <strong>Maxlength</strong> is 50 characters.
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">IP Address</label>
                                                <label class="input">
                                                    <input type="text" id="txtIPAddress" name="txtIPAddress" class="input-sm" readonly="readonly">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>, <strong>Maxlength</strong> is 50 characters.
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">

                                            <section class="col col-4">
                                                <label class="label">Website Monitoring Protocol</label>
                                                <label class="select">
                                                    <select id="ddlWebsiteMonitoringProtocolID" class="input-sm">
                                                    </select>
                                                    <i></i>
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>

                                            </section>

                                            <section class="col col-4">
                                                <label id="lblPortNumber" class="label">Port Number</label>
                                                <label class="input">
                                                    <input type="text" id="txtPortNumber" name="txtPortNumber" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Number Only</strong>, <strong>Range</strong> Between 1 and 65535
                                                </div>

                                            </section>
                                        </div>
                                        
                                        <div class="row">
                                            <section class="col col-8">
                                                <label id="lblDomainName" class="label">Host Name / URL</label>
                                                <label class="input">
                                                    <input type="text" id="txtDomainName" name="txtDomainName" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Maxlength</strong> is 500 characters.
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-4">
                                                <label class="toggle">
                                                    <input type="checkbox" id="cbxEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Active Status</span></label>
                                            </section>
                                        </div>

                                    </fieldset>

                                    <footer>
                                        <button type="submit" class="btn btn-primary" style="float: left;" ID="btnSubmit" onclick="update(); return false;">Submit</button>
                                        <button type="button" class="btn btn-default" style="float: left;" onclick="window.history.go(-1); return false;">
                                            Back
                                        </button>
                                        <input type="hidden" id="id1" />
                                        <input type="hidden" id="m" />
                                        <input type="hidden" id="hClientID" />
                                        <input type="hidden" id="hLoadDataDone" />
                                        <input type="hidden" id="hWebsiteMonitoringProtocolID" />
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
        </section>
    </div>


    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->

    <script type="text/javascript">
        var appPath = '<%=Request.ApplicationPath%>';
    </script>
    <%: Scripts.Render("~/bundles/defaultJs") %>
    <%: Scripts.Render("~/bundles/myJs") %>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- PAGE RELATED PLUGIN(S) 
		<script src="..."></script>-->
    
    <style>
        .txt-color-gray {
            color: #B0B0B0 !important;
        }
    </style>

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!

        $(document).ready(function () {

            var fullPath = "<%=HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/"))%>/ClientList.aspx";
            if (!$('nav li:has(a[href="' + fullPath + '"])').hasClass("active")) {

                $('nav').find('li.active:first').removeClass('active');

                //var index = window.location.pathname.lastIndexOf("/") + 1;
                //var filename = window.location.pathname.substr(index);

                var _obj = $('ul li a[href="' + fullPath + '"]');
                _obj.parent().addClass('active');

                $('nav li:has(a[href="' + fullPath + '"])').addClass("active");

                $('nav').find("li.active").each(function () {
                    $(this).parents("ul").slideDown(0);
                    $(this).parents("ul").parent("li").find("b:first").html('<em class="fa fa-collapse-o"></em>');
                    $(this).parents("ul").parent("li").addClass("open");
                });
            }


            pageSetUp();
            drawBreadCrumb();
            initialData();

            // PAGE RELATED SCRIPTS

            var $checkoutForm = $('#smartForm').validate({
                // Rules for form validation
                rules: {
                    txtClientCode: {
                        required: true,
                        maxlength: 50
                    },
                    txtIPAddress: {
                        required: true,
                        maxlength: 50
                    },
                    txtPortNumber: {
                        number: true,
                        range: [1, 65535]
                    },
                    txtDomainName: {
                        maxlength: 500
                    },
                },
                messages: {
                    txtClientCode: {
                        required: 'Please enter client code',
                        maxlength: 'The field MaxLength is 50.'
                    },
                    txtIPAddress: {
                        required: 'Please enter IP Address',
                        maxlength: 'The field MaxLength is 50.'
                    },
                    txtPortNumber: {
                        number: 'Please enter number only',
                        range: 'The field range between 1 and 65535.'
                    },
                    txtDomainName: {
                        maxlength: 'The field MaxLength is 500.'
                    },
                },

                errorPlacement: function (error, element) {
                    error.insertAfter(element.parent());
                }

            });

            if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {

                Pace.restart();
                $.ajax({
                    "type": "POST",
                    "dataType": 'json',
                    "contentType": "application/json; charset=utf-8",
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/WebsiteMonitoring/Get/",
                    "data": "{'id' : " + <%=Request["id1"]%> + ", 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                    "success": function (ret) {

                        if (ret.status == "1") {

                            var myData = JSON.parse(ret.data);
                            $('#txtPortNumber').val(myData.PortNumber);
                            $('#txtDomainName').val(myData.DomainName);
                            $('#ddlWebsiteMonitoringProtocolID').val(myData.WebsiteMonitoringProtocolId);
                            $('#cbxEnable').attr('checked', myData.Enable);

                            $('#id1').val(myData.WebsiteMonitoringId);
                            $('#m').val("e");
                            $('#hLoadDataDone').val("1");
                            $('#hWebsiteMonitoringProtocolID').val(myData.WebsiteMonitoringProtocolId);

                            
                            $('#ddlWebsiteMonitoringProtocolID option:not(:selected)').attr('disabled', true);
                            
                            SetupForWebsiteMonitoringProtocol(myData.WebsiteMonitoringProtocolId);

                        } else if (ret.status == "-1") {

                            $.smallBox({
                                title: "Access Denied!",
                                content: "<i class='fa fa-clock-o'></i> <i>No access rights to complete this operation.</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } else if (ret.status == "0") {
                            $.smallBox({
                                title: "Get Object Failed",
                                content: "<i class='fa fa-clock-o'></i> <i>Failed to complete this operation.</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        }

                    },

                });

            }

            $("#btnSubmit").on("click", function (event) {
                event.preventDefault(); // will work!
            });


            $("#ddlWebsiteMonitoringProtocolID").change(function (e) {
                $('#txtPortNumber').removeAttr('disabled');
                $('#txtPortNumber').parent().removeClass('state-disabled');
                $('#lblPortNumber').removeClass('txt-color-gray');

                $('#txtDomainName').removeAttr('disabled');
                $('#txtDomainName').parent().removeClass('state-disabled');
                $('#lblDomainName').removeClass('txt-color-gray');

                SetupForWebsiteMonitoringProtocol(this.value);

            });

        });

        function initialData() {
            loadClient();
            loadWebsiteMonitoringProtocol();
            Pace.restart();

        }


        function loadClient() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/GetClient/",
                "data": "{'id' : '<%=Request["clientid"]%>', 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                "success": function (ret) {
                    if (ret.status == "1") {

                        var myData = JSON.parse(ret.data);

                        $('#txtClientCode').val(myData.ClientCode);

                        $('#txtIPAddress').val(myData.IpAddress);
                        $('#hClientID').val(myData.ClientId);

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
        }

        function loadWebsiteMonitoringProtocol() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MasterTable/ListLOVByListName/",
                "data": "{'ListName' : 'WebsiteMonitoringProtocol', 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                "success": function (data) {
                    var optgroup = "";
                    $.each(data.listofvalue, function (index, item) {
                        if (optgroup != item.PItemId) {
                            $('#ddlWebsiteMonitoringProtocolID').append('<optgroup label="' + item.PItemValue + '" >');
                            optgroup = item.PItemId;
                        }

                        if (item.ItemId == 5 || item.ItemId == 6) {
                            $('#ddlWebsiteMonitoringProtocolID').append($('<option disabled></option>').val(item.ItemId).html('&nbsp;&nbsp;' + item.ItemValue));

                        } else {
                            $('#ddlWebsiteMonitoringProtocolID').append($('<option></option>').val(item.ItemId).html('&nbsp;&nbsp;' + item.ItemValue));
                        }
                    });

                    // Set Default

                    if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {
                        if ($('#hLoadDataDone').val() == "1") {
                            $('#ddlWebsiteMonitoringProtocolID').val($('#hWebsiteMonitoringProtocolID').val());
                            $('#ddlWebsiteMonitoringProtocolID option:not(:selected)').attr('disabled', true);

                        }
                    } else {
                         $('#ddlWebsiteMonitoringProtocolID').val(1);
                    }

                    SetupForWebsiteMonitoringProtocol($("#ddlWebsiteMonitoringProtocolID").val());

                },

            });

        }

        $.validator.addMethod(
            "regex",
            function(value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            },
            "Please check your input."
        );


        function update() {

            if (!$('#smartForm').valid()) return;

            tmpObj = function () {
                this.id = $('#id1').val();
                this.m = $('#m').val();
                this.clientID = $('#hClientID').val();
                this.portNumber = $('#txtPortNumber').val();
                this.domainName = $('#txtDomainName').val();
                this.websiteMonitoringProtocolID = $('#ddlWebsiteMonitoringProtocolID').val();
                this.enable = $('#cbxEnable').is(':checked');

                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var updObj = new tmpObj();
            Pace.restart();

            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/WebsiteMonitoring/Update/",
                "data": JSON.stringify(updObj),
                "success": function (ret) {

                    if (ret.status == "1") {
                        $.smallBox({
                            title: "Update Complete",
                            content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                            color: "#659265",
                            iconSmall: "fa fa-check fa-2x fadeInRight animated",
                            timeout: 2000
                        });

                        setTimeout(function () {
                            window.history.go(-1);
                        }, 1500);

                    } else if (ret.status == "-1") {

                        $.smallBox({
                            title: "Update Failed!",
                            content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                            color: "#C46A69",
                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                            timeout: 4000
                        });

                    } else if (ret.status == "0") {
                        $.smallBox({
                            title: "Update Failed!!",
                            content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                            color: "#C46A69",
                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                            timeout: 4000
                        });

                    }

                },

            });
        }

        function SetupForWebsiteMonitoringProtocol(id) {
            if (id == '1') {
                // Uptime - Ping
                $('#txtPortNumber').val('');
                $('#txtPortNumber').attr('disabled', 'disabled');
                $('#txtPortNumber').parent().addClass('state-disabled');
                $('#lblPortNumber').addClass('txt-color-gray');

                $('#txtDomainName').val('');
                $('#txtDomainName').attr('disabled', 'disabled');
                $('#txtDomainName').parent().addClass('state-disabled');
                $('#lblDomainName').addClass('txt-color-gray');

            } else if (id == '2') {
                // Uptime - Telnet
                $('#txtDomainName').val('');
                $('#txtDomainName').attr('disabled', 'disabled');
                $('#txtDomainName').parent().addClass('state-disabled');
                $('#lblDomainName').addClass('txt-color-gray');
            } else if (id == '3') {
                // Uptime - Http
                $('#txtPortNumber').val('');
                $('#txtPortNumber').attr('disabled', 'disabled');
                $('#txtPortNumber').parent().addClass('state-disabled');
                $('#lblPortNumber').addClass('txt-color-gray');
            } else if (id == '4') {
                // Uptime - Https
                $('#txtPortNumber').val('');
                $('#txtPortNumber').attr('disabled', 'disabled');
                $('#txtPortNumber').parent().addClass('state-disabled');
                $('#lblPortNumber').addClass('txt-color-gray');
            } else if (id == '5') {
                // Webpage - Http
                $('#txtPortNumber').val('');
                $('#txtPortNumber').attr('disabled', 'disabled');
                $('#txtPortNumber').parent().addClass('state-disabled');
                $('#lblPortNumber').addClass('txt-color-gray');
            } else if (id == '6') {
                // Webpage - Https
                $('#txtPortNumber').val('');
                $('#txtPortNumber').attr('disabled', 'disabled');
                $('#txtPortNumber').parent().addClass('state-disabled');
                $('#lblPortNumber').addClass('txt-color-gray');
            }
        }

    </script>



</asp:content>
