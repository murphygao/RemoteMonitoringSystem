<%@ Page Title="" Language="C#" MasterPageFile="~/SmartAdmin.Master" AutoEventWireup="true" CodeBehind="ClientReport.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.ClientReport" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- MAIN CONTENT -->
    <div id="content">

        <div class="row">
            <div class="col-xs-12 col-sm-7 col-md-7 col-lg-4">
                <h1 class="page-title txt-color-blueDark">
                    <i class="fa fa-bar-chart-o fa-fw "></i>
                    Remote Monitoring
							<span>> 
								Client Report
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
        <!-- widget grid -->
        <section id="widget-grid" class="">

            <!-- -->
            <div class="row">
                <article class="col-xs-5 col-sm-5 col-md-5 col-lg-5">
                    <div class="jarviswidget" id="wid-client-info-1" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-sortable="false">
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
                            <span class="widget-icon"><i class="fa fa-info-circle"></i></span>
                            <h2>Client Information</h2>

                        </header>

                        <!-- widget div-->
                        <div>

                            <!-- widget edit box -->
                            <div class="jarviswidget-editbox">
                                <!-- This area used as dropdown edit box -->

                            </div>
                            <!-- end widget edit box -->

                            <!-- widget content -->
                            <div class="widget-body">

                                <dl class="dl-horizontal">
                                    <dt class="padding-15 margin-bottom-5">Client Code</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblClientCode"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">Client Type</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblClientType"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">Location</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblLocation"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">IP Address</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblIPAddress"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">Installation Date</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblCreatedDate"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">Last Update</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblUpdatedDate"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">Device Profile</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight"><label id="lblProfileName"></label></dd>
                                    <dt class="padding-15 margin-bottom-5">Contact Detail</dt>
                                    <dd class="padding-15 margin-bottom-5 bg-color-grayLight">Tel.: <label id="lblTelephone"></label><br />
                                        Mobile: <label id="lblMobile"></label><br />
                                        Email: <label id="lblEmail"></label></dd>
                                </dl>

                            </div>
                            <!-- end widget content -->

                        </div>
                        <!-- end widget div -->

                    </div>
                </article>
                
                <article class="col-xs-7 col-sm-7 col-md-7 col-lg-7">
                    <div class="jarviswidget" id="wid-client-device-status-1" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-sortable="false">
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
                            <span class="widget-icon"><i class="fa fa-gears"></i></span>
                            <h2>Current Device Status</h2>

                        </header>

                        <!-- widget div-->
                        <div>

                            <!-- widget edit box -->
                            <div class="jarviswidget-editbox">
                                <!-- This area used as dropdown edit box -->

                            </div>
                            <!-- end widget edit box -->

                            <!-- widget content -->
                            <div class="widget-body">

                                <table id="dt_device_status" class="table table-hover">
                                    <tbody>
                                        <tr>
                                            <td class="td-device-status-name">Alive</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">CPU</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Memory</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Disk</td>
                                            <td class="td-device-status"><span class="label bg-color-blueLight device-severity-status">Warning Lv1</span></td>
                                            <td class="td-device-status">LOW_DISK_SPACE</td>
                                            <td class="td-device-status">Email</td>
                                            <td class="td-device-status">04/03/2014 16:52:07</td>
                                            <td class="td-device-status"><a id="btnResend" class="btn btn-xs btn-primary" href="javascript:void(0);" style="line-height: inherit!important;"><i class="fa fa-mail-forward"></i></a></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">ATM Card Reader</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Barcode Reader</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Card Dispenser</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Electronic Signature Pad</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Encrypted pin Pad</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">ID Card Scanner</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Keyboard</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Printer</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Scanner</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Smarcard Reader</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Thermal Printer</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-device-status-name">Web Camera</td>
                                            <td class="td-device-status"><span class="label label-success device-severity-status">Good</span></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                            <td class="td-device-status"></td>
                                        </tr>
                                        <tr>
                                            <td class="td-hidden-row" style="width: 200px"></td>
                                            <td class="td-hidden-row" style="width: 90px"></td>
                                            <td class="td-hidden-row" style="width: 150px"></td>
                                            <td class="td-hidden-row" style="width: 100px"></td>
                                            <td class="td-hidden-row" style="width: 130px"></td>
                                            <td class="td-hidden-row" style="width: 50px"></td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <!-- end widget content -->

                        </div>
                        <!-- end widget div -->

                    </div>
                </article>
            </div>
            <!-- -->
            <div class="row">

                <!-- NEW WIDGET START -->
                <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget jarviswidget-color-blueDark" id="wid-client-status-history"
                        data-widget-togglebutton="true"
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
                            <h2>Status History</h2>
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
                                            <th style="text-align: center">Date</th>
                                            <th>Message Group</th>
                                            <th>Status</th>
                                            <th>Message</th>
                                            <th>Message Detail</th>
                                            <th>Action Type</th>
                                            <th>Last Action Date</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="text-align: center">1</td>
                                            <td style="text-align: center">04/03/2014  16:52:07</td>
                                            <td>Disk</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_DISK_SPACE</td>
                                            <td>C:</td>
                                            <td>Email</td>
                                            <td>04/03/2014 16:52:07</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">2</td>
                                            <td style="text-align: center">04/03/2014 20:29:44</td>
                                            <td>Printer</td>
                                            <td><span class="label label-success">Good</span></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">3</td>
                                            <td style="text-align: center">03/03/2014 20:26:20</td>
                                            <td>Printer</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_PAPER</td>
                                            <td>Paper Near End</td>
                                            <td>Email</td>
                                            <td>03/03/2014 20:26:20</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">4</td>
                                            <td style="text-align: center">03/03/2014 15:20:00</td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label label-success">Good</span></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">5</td>
                                            <td style="text-align: center">03/03/2014 15:18:52</td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label bg-color-red">Critical</span></td>
                                            <td>END_PAPER</td>
                                            <td>Out of Paper</td>
                                            <td>Email, SMS</td>
                                            <td>03/03/2014 15:18:52</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">6</td>
                                            <td style="text-align: center">03/03/2014 14:16:10</td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_PAPER</td>
                                            <td>Paper Near End</td>
                                            <td>Email</td>
                                            <td>03/03/2014 14:16:10</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">7</td>
                                            <td style="text-align: center">01/03/2014 12:22:10</td>
                                            <td>ATM Card Reader</td>
                                            <td><span class="label label-success">Good</span></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">8</td>
                                            <td style="text-align: center">01/03/2014 12:11:50</td>
                                            <td>ATM Card Reader</td>
                                            <td><span class="label bg-color-orange">Warning Lv2</span></td>
                                            <td>LOW_CARD</td>
                                            <td>ATM Card Near End</td>
                                            <td>Email</td>
                                            <td>01/03/2014 12:11:50</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">9</td>
                                            <td style="text-align: center">28/02/2014  16.26:06</td>
                                            <td>CPU</td>
                                            <td><span class="label label-success">Good</span></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">10</td>
                                            <td style="text-align: center">28/02/2014  16.25:20</td>
                                            <td>CPU</td>
                                            <td><span class="label bg-color-red">Critical</span></td>
                                            <td>OVER_CPU_USAGE</td>
                                            <td>> 90% CPU Usage</td>
                                            <td>Email, SMS</td>
                                            <td>28/02/2014  16.25:20</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">11</td>
                                            <td style="text-align: center">28/02/2014 10:29:44</td>
                                            <td>Printer</td>
                                            <td><span class="label label-success">Good</span></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">12</td>
                                            <td style="text-align: center">28/02/2014 10:26:20</td>
                                            <td>Printer</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_PAPER</td>
                                            <td>Paper Near End</td>
                                            <td>Email</td>
                                            <td>03/03/2014 20:26:20</td>
                                        </tr>
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

            <!-- end -->

        </section>
        <!-- end widget grid -->

    </div>

    <!--================================================== -->

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

        $(document).ready(function () {

            var fullPath = "<%=HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/"))%>/MonitoringReport.aspx";
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


            PrepareClientInfo();

            /*
            * Summary Device
            */
            $('#dt_device_status').dataTable({
                "sPaginationType": "bootstrap_full",
                "bFilter": false,
                "bAutoWidth": false,
                "bPaginate": false,
                "sDom": 't',
                "bInfo": false,
                "bSort": false,
                "aoColumns": [
                {
                    // device name
                    "bSearchable": false,
                    "bSortable": false,
                    "sWidth": "220",
                },
                {
                    // status
                    "bSearchable": false,
                    "bSortable": false,
                    "sWidth": "120",
                    "sClass": "center"

                },
                {
                    // message
                    "bSearchable": false,
                    "bSortable": false,
                    "sWidth": "250",
                },
                {
                    // action
                    "bSearchable": false,
                    "bSortable": false,
                    "sWidth": "150"
                },
                {
                    // action date
                    "bSearchable": false,
                    "bSortable": false,
                    "sWidth": "150"
                },
                {
                    // resend
                    "bSearchable": false,
                    "bSortable": false,
                    "sWidth": "50",
                    "sClass": "center"
                }   

                ],


            });

            $("#dt_device_status thead").remove();


            /*
            * Last Monitoring
            */
            $('#dt_basic').dataTable({
                "sPaginationType": "bootstrap_full",
                "bFilter": false,
                "bAutoWidth": false,
                "bPaginate": true,
                "bInfo": true,
                "sScrollX": "100%",
                "sScrollXInner": "150%",
                "aoColumns": [
                    {
                        // #
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "50",
                        "sClass": "center"
                    },
                    {
                        // Date
                        "bSearchable": false,
                        "bSortable": true,
                        "sWidth": "150",
                        "sClass": "center"
                    },
                    {
                        //group
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "180",
                    },
                    {
                        //status
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "120",
                    },
                    {
                        //message
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "170",
                    },
                    {
                        //message detail
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "200",
                    },
                    {
                        //action
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "100"
                    },
                    {
                        // last action date
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "220",
                        "sClass": "center"
                    },
                ],



            });


            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd;
            }

            if (mm < 10) {
                mm = '0' + mm;
            }

            today = dd + '.' + mm + '.' + yyyy;

            //$('#txtMessageDate').attr("placeholder", today);

            $('#txtMessageDate').datepicker({
                dateFormat: 'dd.mm.yy',
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                maxDate: '0'
            });
            //$("#txtMessageDate").datepicker().datepicker("setDate", new Date());

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

            var oTable = $('#dt_basic').dataTable();
            $('#aSearch').click(function () { oTable.fnDraw(); });

            $('#btnResend').click(function(e) {
                $.smallBox({
                    title: "Resend in Queue Complete",
                    content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                    color: "#659265",
                    iconSmall: "fa fa-check fa-2x fadeInRight animated",
                    timeout: 4000
                });
            });
        });


        function PrepareClientInfo() {
            {
                var clientID = '<%=Request["id"]%>';

                $.ajax({
                    "type": "POST",
                    "dataType": 'json',
                    "contentType": "application/json; charset=utf-8",
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/GetClientInfo",
                    "data": "{'id' : " + clientID + "}",
                    "success": function (ret) {

                        if (ret.status == "1") {

                            var myData = JSON.parse(ret.data);

                            $('#lblClientCode').text(myData.ClientCode);
                            $('#lblClientType').text(myData.ClientType);

                            var loc = '';
                            if (myData.LocationCode != null)
                                loc = myData.LocationCode;
                            if (myData.LocationName != null)
                                loc = loc + ' ' + myData.LocationName;
                            if (loc == '')
                                loc = 'N/A';
                            $('#lblLocation').text(loc);

                            $('#lblIPAddress').text(myData.IPAddress);

                            var createdDate = new Date(myData.CreatedDate);
                            $('#lblCreatedDate').text(dateFormat(createdDate, "dd/mm/yyyy HH:mm:ss", true));
                            var updatedDate = new Date(myData.UpdatedDate);
                            $('#lblUpdatedDate').text(dateFormat(updatedDate, "dd/mm/yyyy HH:mm:ss", true));

                            $('#lblProfileName').text(myData.ProfileName);

                            var tmp = myData.Telephone;
                            if (tmp == null) tmp = 'N/A';
                            $('#lblTelephone').text(tmp);

                            tmp = myData.Mobile;
                            if (tmp == null) tmp = 'N/A';
                            $('#lblMobile').text(tmp);

                            tmp = myData.Email;
                            if (tmp == null) tmp = 'N/A';
                            $('#lblEmail').text(tmp);

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
                                title: "Get Client Information Failed",
                                content: "<i class='fa fa-clock-o'></i> <i>Failed to complete this operation.</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        }

                    },

                    });
                }
        }

    </script>



</asp:Content>
