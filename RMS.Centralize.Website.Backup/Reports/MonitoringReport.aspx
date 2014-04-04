<%@ Page Title="" Language="C#" MasterPageFile="~/SmartAdmin.Master" AutoEventWireup="true" CodeBehind="MonitoringReport.aspx.cs" Inherits="Bootstrap.Reports.MonitoringReport" %>

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
								Monitoring Report
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

                                <form class="smart-form" method="POST">
                                    <fieldset>
                                        <section class="col col-md-6 col-xs-12">
                                            <label class="input">
                                                <i class="icon-append fa fa-calendar"></i>
                                                <input type="text" class="input-sm" name="txtMessageDate" id="txtMessageDate" placeholder="Message Date">
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <select multiple style="width: 100%" class="select2 input-sm">
                                                <option value="0" selected="selected">Select All Message Group</option>
                                                <optgroup label="Technical Message">
                                                    <option value="1">CPU</option>
                                                    <option value="2">Memory</option>
                                                    <option value="3">Disk</option>
                                                    <option value="4">ATM Card Reader</option>
                                                    <option value="5">Barcode Reader</option>
                                                    <option value="6">Card Dispenser</option>
                                                    <option value="7">Signature Pad</option>
                                                    <option value="8">Encrypted Pin Pad</option>
                                                    <option value="9">ID Card Scanner</option>
                                                    <option value="10">Keyboard</option>
                                                    <option value="11">Printer</option>
                                                    <option value="12">Scanner</option>
                                                    <option value="13">Smartcard Reader</option>
                                                    <option value="14">Thermal Printer</option>
                                                    <option value="15">Web Camera</option>
                                                </optgroup>
                                                <optgroup label="Busniess Message">
                                                    <option value="16">General</option>
                                                </optgroup>
                                            </select>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="input">
                                                <input type="text" class="input-sm" id="txtClientCode" placeholder="Client Code" />
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="input">
                                                <input type="text" class="input-sm" id="txtMessage" placeholder="Message" />
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="select">
                                                <select class="input-sm">
                                                    <option value="0">Location</option>
                                                    <option value="1">0001 - เยาวราช</option>
                                                    <option value="2">0002 - สามยอด</option>
                                                    <option value="3">0003 - สำเพ็ง</option>
                                                    <option value="4">1144 - สำนักงานใหญ่ 2</option>
                                                </select>
                                                <i></i>
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="select">
                                                <select class="input-sm">
                                                    <option value="0">Status</option>
                                                    <option value="1">Issue</option>
                                                    <option value="2">Solved</option>
                                                </select>
                                                <i></i>
                                            </label>
                                        </section>

                                        <section class="col col-md-12 col-xs-12">
                                            <button type="submit" class="btn btn-primary" style="padding: 5px 16px">
                                                Search
                                            </button>
                                            <button type="button" class="btn btn-default" style="padding: 5px 16px" onclick="this.form.reset();">
                                                Reset
                                            </button>
                                        </section>
                                    </fieldset>

                                </form>

                            </div>
                            <!-- end widget content -->

                        </div>
                        <!-- end widget div -->

                    </div>
                </article>
            </div>
            <!-- row -->
            <div class="row">

                <!-- NEW WIDGET START -->
                <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <!-- Widget ID (each widget will need unique ID)-->
                    <div class="jarviswidget jarviswidget-color-blueDark" id="wid-id-2"
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
                            <h2>Result</h2>
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
                                            <th>Client Code</th>
                                            <th>Location</th>
                                            <th>IP Address</th>
                                            <th style="text-align: center">Status</th>
                                            <th>Message Group</th>
                                            <th>Severity Level</th>
                                            <th>Message</th>
                                            <th>Message Detail</th>
                                            <th>&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td style="text-align: center">1</td>
                                            <td style="text-align: center">04/03/2014  16:56:20</td>
                                            <td>K201403-001</td>
                                            <td>0001 - เยาวราช</td>
                                            <td>192.168.1.110</td>
                                            <td style="text-align: center"><span class="label bg-color-redLight">Issue</span></td>
                                            <td>Disk</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_DISK_SPACE</td>
                                            <td>C: 980MB</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">2</td>
                                            <td style="text-align: center">04/03/2014  16:33:12</td>
                                            <td>K201403-004</td>
                                            <td>1144 - สำนักงานใหญ่ 2</td>
                                            <td>192.168.1.131</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_PAPER</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">3</td>
                                            <td style="text-align: center">04/03/2014 16:28:30</td>
                                            <td>K201403-002</td>
                                            <td>0002 - สามยอด</td>
                                            <td>192.168.1.121</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_PAPER</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">4</td>
                                            <td style="text-align: center">04/03/2014 15:44:10</td>
                                            <td>K201403-004</td>
                                            <td>1144 - สำนักงานใหญ่ 2</td>
                                            <td>192.168.1.131</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>CPU</td>
                                            <td><span class="label bg-color-red">Critical Lv1</span></td>
                                            <td>OVER_CPU_USAGE</td>
                                            <td>> 90% CPU Usage</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">5</td>
                                            <td style="text-align: center">04/03/2014 15:18:52</td>
                                            <td>K201403-001</td>
                                            <td>0001 - เยาวราช</td>
                                            <td>192.168.1.110</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>ATM Card Reader</td>
                                            <td><span class="label bg-color-yellow">Warning Lv2</span></td>
                                            <td>LOW_CARD</td>
                                            <td></td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">6</td>
                                            <td style="text-align: center">04/03/2014 14:30:31</td>
                                            <td>K201403-003</td>
                                            <td>0003 - สำเพ็ง</td>
                                            <td>192.168.1.151</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label bg-color-red">Critical Lv1</span></td>
                                            <td>END_PAPER</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">7</td>
                                            <td style="text-align: center">04/03/2014 13:25:40</td>
                                            <td>K201403-003</td>
                                            <td>0003 - สำเพ็ง</td>
                                            <td>192.168.1.151</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>Thermal Printer</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_PAPER</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">8</td>
                                            <td style="text-align: center">04/03/2014 11:51:29</td>
                                            <td>K201403-002</td>
                                            <td>0002 - สามยอด</td>
                                            <td>192.168.1.121</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>ATM Card Reader</td>
                                            <td><span class="label bg-color-yellow">Warning Lv2</span></td>
                                            <td>LOW_CARD</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">9</td>
                                            <td style="text-align: center">04/03/2014 10:39:35</td>
                                            <td>K201403-001</td>
                                            <td>0001 - เยาวราช</td>
                                            <td>192.168.1.110</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>Memory</td>
                                            <td><span class="label bg-color-blueLight">Warning Lv1</span></td>
                                            <td>LOW_MEMORY</td>
                                            <td>Low Memory</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">10</td>
                                            <td style="text-align: center">04/03/2014 10:28:10</td>
                                            <td>K201403-002</td>
                                            <td>0002 - สามยอด</td>
                                            <td>192.168.1.121</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>Business</td>
                                            <td><span class="label bg-color-orange">Warning Lv3</span></td>
                                            <td>HOST_TIME_OUT</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center">11</td>
                                            <td style="text-align: center">04/03/2014 09:58:40</td>
                                            <td>K201403-002</td>
                                            <td>0002 - สามยอด</td>
                                            <td>192.168.1.121</td>
                                            <td style="text-align: center"><span class="label label-success">Solved</span></td>
                                            <td>ATM Card Reader</td>
                                            <td><span class="label bg-color-red">Critical Lv1</span></td>
                                            <td>DEVICE_NOT_FOUND</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
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

            <!-- end row -->

        </section>
        <!-- end widget grid -->

    </div>

    <!--================================================== -->

    <!-- PACE LOADER - turn this on if you want ajax loading to show (caution: uses lots of memory on iDevices)-->

    <%: Scripts.Render("~/bundles/defaultJs") %>
    <%: Scripts.Render("~/bundles/datatableJs") %>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- PAGE RELATED PLUGIN(S) 
		<script src="..."></script>-->

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!

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

                /*
             * BASIC
             */
                $('#dt_basic').dataTable({
                    "sPaginationType": "bootstrap_full",
                    "bFilter": false,
                    "bAutoWidth": false,
                    "bPaginate": true,
                    "bInfo": true,
                    "sScrollX": "100%",
                    "sScrollXInner": "100%",
                    "aoColumns": [
                        {
                            // #
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "80",
                            "sClass": "center"
                        },
                        {
                            // Date
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "180",
                            "sClass": "center"
                        },
                        {
                            // Client Code
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "120"
                        },
                        {
                            // Location
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "220"
                        },
                        {
                            // IP Address
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "100",
                        },
                        {
                            //status
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "120",
                            "sClass": "center"
                        },
                        {
                            //group
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "120",
                        },
                        {
                            //severity
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "150",
                            "sClass": "center"
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
                            "mData": null,
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "80",
                            "sClass": "center",
                            "fnRender": function (oObj) {
                                return '<a id="edit_item_' + oObj.aData["ActionProfileId"] + '" class="btn btn-primary btn-xs" href="clientreport.aspx?id=1"><i class="glyphicon glyphicon-search"></i></a>';
                            }
                        }
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
            });
    </script>

</asp:Content>
