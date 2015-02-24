<%@ Page Title="" Language="C#" MasterPageFile="~/SmartAdmin.Master" AutoEventWireup="true" CodeBehind="AllClientStatusReport.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.AllClientStatusReport" %>

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
								All Client Status Report
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

            <!-- row -->
            <div class="row">
                <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="jarviswidget" id="wid-id-1" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-sortable="false">
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
                            <h2>Search Criteria</h2>

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

                                <form id="searhForm" class="smart-form">
                                    <fieldset>
                                        
                                        <section class="col col-md-6 col-xs-12">
                                            <label class="select">
                                                <select id="ddlMonitoringStatus" class="input-sm">
                                                    <option value="0">Monitoring Status</option>
                                                    <option value="0">All</option>
                                                    <option value="1">Problem</option>
                                                </select>
                                                <i></i>
                                            </label>
                                        </section>

                                        <section class="col col-md-12 col-xs-12">
                                            <button type="submit" class="btn btn-primary" style="padding: 5px 16px">
                                                Search
                                            </button>
                                            <button type="button" class="btn btn-default" style="padding: 5px 16px" onclick="this.form.reset();DoResetForm();">
                                                Reset
                                            </button>
                                        </section>
                                    </fieldset>

                                </form>

                            </div>
                            <!-- end widget content -->

                        </div>
                        <!-- end widget div -->
                        <input type="hidden" id="hdMonitoringStatus" value="0"/>
                        
                    </div>
                </article>
            </div>
            <!-- row -->
            <div class="row">
                <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-2" data-widget-togglebutton="false" data-widget-sortable="false" data-widget-deletebutton="false" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-custombutton="false">
                        <header>
                            <span class="widget-icon"><i class="fa fa-edit"></i></span>
                            <h2>All Client Status</h2>

                        </header>
                        <div id="divResultArea" class="row"></div>
                    </div>
                </article>
            </div>
            <!-- end row -->

        </section>
        <!-- end widget grid -->

    </div>

    <!--================================================== -->

    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->

    <script type="text/javascript">
        var appPath = '<%=Request.ApplicationPath%>';
    </script>
    <%: Scripts.Render("~/bundles/defaultJs") %>
    <%: Scripts.Render("~/bundles/datatableJs") %>
    <%: Scripts.Render("~/bundles/myJs") %>
    

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- PAGE RELATED PLUGIN(S) 
		<script src="..."></script>-->
    
    <style type="text/css">
        .popover.bottom{margin-top: -10px}
    </style>

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!

        

        var strStatusBlock = "<div class=\"col-xs-6 col-sm-2 col-md-1\">"
            + " <a href=\"javascript:ViewClientReport(::clientid::);\" style=\"text-decoration: none\" rel=\"popover-hover\" data-placement=\"::tooltipposition::\" data-original-title=\"<div style=\'font-size:11px; margin: -5px -10px -5px -10px; width: 150px\'>::tooltipheader::</div>\" data-content=\"<div style=\'font-size:11px; margin: -5px -10px -5px -10px\'>::tooltipdetail::</div>\" data-html=\"true\">"
            + "     <div class=\"panel panel-::colorstatus::\">"
            + "         <div class=\"panel-heading no-padding\" style=\"padding: 10px 2px 10px 2px !important\">"
            + "             <h3 class=\"panel-title\" style=\"font-size: 10px!important; text-align: center;\">::clientcode::</h3>"
            + "         </div>"
            + "         <div class=\"panel-footer no-padding\">"
            + "             <table style=\"width: 100%; font-weight: bold;\">"
            + "                 <tr>"
            + "                     <td style=\"width: 50%; text-align: center; border-right: 2px solid #aaaaaa; color: green;\">::okmessage::</td>"
            + "                     <td style=\"width: 50%; text-align: center; color: darkred;\">::issuemessage::</td>"
            + "                 </tr>"
            + "             </table>"
            + "         </div>"
            + "     </div>"
            + " </a>"
            + "</div>";

        $(document).ready(function () {

            if (!$('nav li:has(a[href="<%=HttpContext.Current.Request.Url.AbsolutePath%>"])').hasClass("active")) {

                $('nav').find('li.active:first').removeClass('active');

                //var index = window.location.pathname.lastIndexOf("/") + 1;
                //var filename = window.location.pathname.substr(index);

                var _obj = $('ul li a[href="<%=HttpContext.Current.Request.Url.AbsolutePath%>"]');
                _obj.parent().addClass('active');

                $('nav li:has(a[href="<%=HttpContext.Current.Request.Url.AbsolutePath%>"])').addClass("active");

                $('nav').find("li.active").each(function () {
                    $(this).parents("ul").slideDown(0);
                    $(this).parents("ul").parent("li").find("b:first").html('<em class="fa fa-collapse-o"></em>');
                    $(this).parents("ul").parent("li").addClass("open");
                });
            }
            pageSetUp();
            drawBreadCrumb();
            DoResetForm();
            PresetDataBeforeSubmit();
            updateInterval = 8000;
            initialData();

            /*
         * BASIC
         */


            $('#searhForm').submit(function (event) {
                /* stop form from submitting normally */
                event.preventDefault();

                PresetDataBeforeSubmit();
                clearInterval(updateInterval);
                Pace.restart();
                initialData();
            });

        });

        function initialData() {
            tmpObj = function () {
                this.ddlMonitoringStatus = $('#hdMonitoringStatus').val();
                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var updObj = new tmpObj();
            clearInterval(updateInterval);
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/SearchSummaryStatusAllClients/",
                "data": JSON.stringify(updObj),
                "success": function (data) {
                    ShowBlockStatus(data);
                    $("[rel=popover-hover]").popover({
                        trigger: "hover"
                    });

                },
            });
        }

        function ViewClientReport(id) {
            var params = new Array();
            params["id"] = id;

            post_to_url("ClientReport.aspx", params, null);
        }

        function DoResetForm() {

        }

        function PresetDataBeforeSubmit() {
            $('#hdMonitoringStatus').val($('#ddlMonitoringStatus').val());
        }

        function ShowBlockStatus(ret) {
            var json = ret.data;
            var htmlStatus = "";
            var k = 0;
            for (var i = 0; i < json.length; i++) {
                k++;
                var obj = json[i];
                var tmp = strStatusBlock
                    .replace("::clientid::", obj.ClientID)
                    .replace("::clientcode::", obj.ClientCode)
                    .replace("::okmessage::", obj.CounterOK)
                    .replace("::issuemessage::", obj.CounterNotOK);

                var strDateTime = "";
                if (obj.OldestErrorMessageDateTime != null) {
                    var date = new Date(parseInt(obj.OldestErrorMessageDateTime.substr(6)));
                    strDateTime = dateFormat(date, "dd/mm/yyyy HH:MM:ss");
                }
                tmp = tmp.replace("::tooltipheader::", obj.LocationName).replace("::tooltipdetail::", obj.sRMSStatus + "<br/>" + strDateTime).trim();

                //Off Duty
                if (obj.iRMSStatus >= 20 && obj.iRMSStatus < 30) {
                    tmp = tmp.replace("::colorstatus::", "teal");
                } //Down
                else if ((obj.iRMSStatus >= 0 && obj.iRMSStatus < 10) || obj.CounterNotOK > 0) {
                    tmp = tmp.replace("::colorstatus::", "red");
                }
                //OK
                else if (obj.iRMSStatus >= 10 && obj.iRMSStatus < 20) {
                    tmp = tmp.replace("::colorstatus::", "greenLight");
                }
                //Internal Problem
                else if (obj.iRMSStatus >= 30) {
                    tmp = tmp.replace("::colorstatus::", "orange");
                }

                if (k <= 12) {
                    tmp = tmp.replace("::tooltipposition::", "bottom");
                } else {
                    tmp = tmp.replace("::tooltipposition::", "top");
                }

                htmlStatus += tmp;
            }
            $('#divResultArea').html(htmlStatus);
            //console.log(htmlStatus);

            setTimeout(initialData, updateInterval);

        }

    </script>

</asp:Content>
