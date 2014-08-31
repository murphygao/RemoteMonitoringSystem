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
                                    <dt class="padding-10 margin-bottom-5">Client Code</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblClientCode"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">Client Type</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblClientType"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">Location</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblLocation"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">IP Address</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblIPAddress"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">Monitoring State</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblState"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">Created Date</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblCreatedDate"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">Last Update</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblUpdatedDate"></label></dd>
                                    <dt class="padding-10 margin-bottom-5">Device Profile</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight"><label id="lblProfileName"></label></dd>
                                    <div style="display: none">
                                    <dt class="padding-10 margin-bottom-5">Contact Detail</dt>
                                    <dd class="padding-10 margin-bottom-5 bg-color-grayLight">Tel.: <label id="lblTelephone"></label><br />
                                        Mobile: <label id="lblMobile"></label><br />
                                        Email: <label id="lblEmail"></label></dd></div>
                                    <dt class="padding-10 margin-bottom-5">&nbsp;</dt>
                                    <dd class="padding-top-10 padding-bottom-10"><button type="button" class="btn btn-default" style="float: left;" onclick="window.history.go(-1); return false;">&nbsp;&nbsp;Back&nbsp;&nbsp;</button></dd>
                                </dl>

                            </div>
                            <!-- end widget content -->
                            
                       

                        </div>
                        <!-- end widget div -->

                    </div>
                </article>
                
                <article class="col-xs-7 col-sm-7 col-md-7 col-lg-7">
                                    <span class="monitoring-liveupdate-1" style="right: 60px"> <span class="onoffswitch-title">Live switch</span> <span class="onoffswitch">
										        <input type="checkbox" name="start_interval" class="onoffswitch-checkbox" id="start_interval">
										        <label class="onoffswitch-label" for="start_interval"> 
											        <span class="onoffswitch-inner" data-swchon-text="ON" data-swchoff-text="OFF"></span> 
											        <span class="onoffswitch-switch"></span> </label> </span> </span>

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

                                <table id="dt_device_status" class="table table-hover" style="width: 100%;word-wrap:break-word; ">
                                    <tbody>
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
                                            <th>Message</th>
                                            <th>Message Detail</th>
                                            <th>Status</th>
                                            <th>Solved Date</th>
                                            <th>Action Type</th>
                                            <th>Last Action Date</th>
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

            <!-- end -->

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
            $("#dt_device_status").dataTable({
                sPaginationType: "bootstrap_full", bFilter: !1, bAutoWidth: !1, bPaginate: !1, sDom: "t", bInfo: !1, bSort: !1, bScrollCollapse: !0, bServerSide: !0, iDisplayLength: 50, aaSorting: [[1, "asc"]], sAjaxSource: "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/GetCurrentDeviceStatus/", fnServerData: function (a, b, c) {
                    b.push({ name: "clientID", value: '<%=Request["id"]%>' }); b.push({ name: "dt", value: dateFormat(new Date, "yyyymmddHHMMss") }); Pace.restart(); $.ajax({
                        type: "POST",
                        dataType: "json", url: a, data: b, success: function (a) { if (1 == a.status) c(a); else try { $.smallBox({ title: "System Failed", content: "<i class='fa fa-clock-o'></i> <i>" + a.error + "</i>", color: "#C46A69", iconSmall: "fa fa-times fa-2x fadeInRight animated", timeout: 4E3 }) } catch (b) { alert(a.error) } }
                    })
                }, aoColumns: [{ mDataProp: "DeviceDescription", bSearchable: !1, bSortable: !1, sWidth: "200", sClass: "td-device-status-name width200", fnRender: function (a) { return htmlEscape(a.aData.DeviceDescription) } }, {
                    mDataProp: "LevelName", bSearchable: !1,
                    bSortable: !1, sWidth: "120", sClass: "center td-device-status width120", fnRender: function (a) { return "Good" == a.aData.Message ? a.aData.ColorTagStart.replace('class="', 'class="device-severity-status ') + "Good" + a.aData.ColorTagEnd : a.aData.ColorTagStart.replace('class="', 'class="device-severity-status ') + a.aData.LevelName + a.aData.ColorTagEnd }
                }, { mDataProp: "Message", bSearchable: !1, bSortable: !1, sWidth: "180", sClass: "td-device-status width180", fnRender: function (a) { return "Good" == a.aData.Message ? "" : htmlEscape(a.aData.Message) } },
                { mDataProp: "LastActionType", bSearchable: !1, bSortable: !1, sClass: "td-device-status width90", sWidth: "90", fnRender: function (a) { return "" == a.aData.LastActionType ? "" : a.aData.LastActionType } }, { mDataProp: "LastActionDateTime", bSearchable: !1, bSortable: !1, sClass: "td-device-status width150", sWidth: "150", fnRender: function (a) { return null != a.aData.LastActionDateTime ? (a = new Date(parseInt(a.aData.LastActionDateTime.substr(6))), dateFormat(a, "dd/mm/yyyy HH:MM:ss")) : "" } }, {
                    mData: null, bSearchable: !1, bSortable: !1, sWidth: "40",
                    sClass: "td-device-status width40", fnRender: function (a) { return "" != a.aData.Message ? '<a id="btnResend" class="btn btn-xs btn-primary" href="javascript:Resend(' + a.aData.ReportID + ');" style="line-height: inherit!important;"><i class="fa fa-mail-forward"></i></a>' : "" }
                }]
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
                "bScrollCollapse": true,
                "sDom": 'r<"dt-top-row"lf><"dt-wrapper"<"datatable-scroll"t>><"dt-row dt-bottom-row"<"row"<"col-sm-6"i><"col-sm-6 text-right"p>>>',
                "bServerSide": true,
                "aaSorting": [[ 1, "desc" ]],
                "sAjaxSource": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/SearchClientMonitoring/",
                    "fnServerData": function (sSource, aoData, fnCallback) {
                        aoData.push({ "name": "clientID", "value": '<%=Request["id"]%>' });
                        aoData.push({ "name": "dt", "value": dateFormat(new Date(), "yyyymmddHHMMss") });
                        Pace.restart();
                        $.ajax({
                            "type": "POST",
                            "dataType": 'json',
                            "url": sSource,
                            "data": aoData,
                            "success": function (data) {
                                if (data.status == 1) {
                                    fnCallback(data);
                                } else {
                                    try {
                                        $.smallBox({
                                            title: "System Failed",
                                            content: "<i class='fa fa-clock-o'></i> <i>" + data.error + "</i>",
                                            color: "#C46A69",
                                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                            timeout: 4000
                                        });

                                    } catch (e) {
                                        alert(data.error);
                                    }
                                }
                            }
                        });
                    },
                    "aoColumns": [
                    {
                        // #
                        "mDataProp": "RowNum",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "50",
                        "sClass": "center"
                    },
                    {
                        // Date
                        "mDataProp": "EventDateTime",
                        "bSearchable": false,
                        "bSortable": true,
                        "sWidth": "150",
                        "sClass": "center",
                        "fnRender": function(oObj) {
                            var date = new Date(parseInt(oObj.aData["EventDateTime"].substr(6)));
                            return dateFormat(date, "dd/mm/yyyy HH:MM:ss");
                        }
                    },
                    {
                        //group
                        "mDataProp": "MessageGroupName",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "180",
                    },
                    {
                        //message
                        "mDataProp": "Message",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "170",
                    },
                    {
                        //message detail
                        "mDataProp": "DeviceDescription",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "250",
                        "fnRender": function (oObj) {
                            var content = '';
                            if (oObj.aData["DeviceDescription"] != null)
                                content = oObj.aData["DeviceDescription"];
                            if (oObj.aData["MessageRemark"] != null) {
                                if (oObj.aData["DeviceDescription"] != null && oObj.aData["DeviceDescription"] != '')
                                    content = content + ' - ';
                                 content = content + oObj.aData["MessageRemark"];
                            }
                            return content;
                        }

                    },
                    {
                        //status
                        "mDataProp": "Status",
                        "bSearchable": false,
                        "bSortable": true,
                        "sClass": "center",
                        "sWidth": "50",
                        "fnRender": function (oObj) {
                            if (oObj.aData["Status"] == 1) {
                                return '<span class="label bg-color-redLight">Issue</span>';
                            } else if (oObj.aData["Status"] == 0) {
                                return '<span class="label label-success">Solved</span>';
                            } else if (oObj.aData["Status"] == 2) {
                                return '<span class="label label-warning">Warning</span>';
                            }
                        }
                    },
                    {
                        // last action date
                        "mDataProp": "UpdatedDate",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "200",
                        "sClass": "center",
                        "bUseRendered": false,
                        "fnRender": function(oObj) {
                            if (oObj.aData["Status"] == 0 && oObj.aData["UpdatedDate"] != null) {
                                var date = new Date(parseInt(oObj.aData["UpdatedDate"].substr(6)));
                                return dateFormat(date, "dd/mm/yyyy HH:MM:ss");
                            }
                            return "";
                        }
                    },
                    {
                        //action
                        "mDataProp": "LastActionType",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "100"
                    },
                    {
                        // last action date
                        "mDataProp": "LastActionDateTime",
                        "bSearchable": false,
                        "bSortable": false,
                        "sWidth": "220",
                        "sClass": "center",
                        "fnRender": function(oObj) {
                            if (oObj.aData["LastActionDateTime"] != null) {
                                var date = new Date(parseInt(oObj.aData["LastActionDateTime"].substr(6)));
                                return dateFormat(date, "dd/mm/yyyy HH:MM:ss");
                            }
                            return "";
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


            /* live switch */
            $('input[type="checkbox"]#start_interval').click(function () {
                if ($(this).prop('checked')) {
                    $on = true;
                    updateInterval = 10000;
                    update();
                } else {
                    clearInterval(updateInterval);
                    $on = false;
                }
            });

            function update() {
                if ($on == true) {
                    var oTable = $('#dt_device_status').dataTable();
                    oTable.fnDraw();
                    oTable = $('#dt_basic').dataTable();
                    oTable.fnDraw();
                    //Pace.restart(); 
                    setTimeout(update, updateInterval);

                } else {
                    clearInterval(updateInterval);
                }

            }

            var $on = false;

            $(document).ajaxStart(function() { Pace.restart(); });
        });

        function Resend(id) {
            Row = function(id) {
                this.id = id;
                this.sentType = "ManualSending";
                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var resendAction = new Row(id);
            var oTableDS = $('#dt_device_status').dataTable();
            var oTable = $('#dt_basic').dataTable();
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/ResendAction",
                "data": JSON.stringify(resendAction),
                "success": function(data) {
                    oTableDS.fnStandingRedraw(0);

                    oTable.fnStandingRedraw(0);

                    if (data.status) {
                        try {
                            $.smallBox({
                                title: "Resend Complete",
                                content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                                color: "#659265",
                                iconSmall: "fa fa-check fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert("This operation is complete.");
                        }
                    } else {
                        try {
                            $.smallBox({
                                title: "Resend Failed",
                                content: "<i class='fa fa-clock-o'></i> <i>Failed to complete this operation. " + data.errorMessage + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert("Failed to complete this operation. " + data.errorMessage);
                        }
                    }

                },
                "error": function(xhr, textStatus, errorThrown) {
                    if (errorThrown == 'Not Found') errorThrown = '404 Service Not Found.';
                    try {
                        $.smallBox({
                            title: "Resend Failed",
                            content: "<i class='fa fa-clock-o'></i> <i>" + errorThrown + "</i>",
                            color: "#C46A69",
                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                            timeout: 4000
                        });

                    } catch (e) {
                        alert(errorThrown);
                    }
                },

            });

        }

        function PrepareClientInfo() {
            {
                var clientID = '<%=Request["id"]%>';

                if (clientID != '') {
                    Pace.restart();
                    $.ajax({
                        "type": "POST",
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/GetClientInfo",
                        "data": "{'id' : " + clientID + ", 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                        "success": function(ret) {

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

                                if (myData.State == 1)
                                    $('#lblState').text('Normal');
                                else if (myData.State == 2)
                                    $('#lblState').text('Maintenance');

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
        }

    </script>



</asp:Content>
