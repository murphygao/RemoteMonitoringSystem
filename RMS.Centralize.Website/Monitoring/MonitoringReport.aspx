<%@ Page Title="" Language="C#" MasterPageFile="~/SmartAdmin.Master" AutoEventWireup="true" CodeBehind="MonitoringReport.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.MonitoringReport" %>
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

                                <form id="searhForm" class="smart-form">
                                    <fieldset>
                                        <section class="col col-md-3 col-xs-6">
                                            <label class="input">
                                                <i class="icon-append fa fa-calendar"></i>
                                                <input type="text" class="input-sm" name="txtStartEventDate" id="txtStartEventDate" placeholder="Start Event Date"/>
                                            </label>
                                        </section>

                                        <section class="col col-md-3 col-xs-6">
                                            <label class="input">
                                                <i class="icon-append fa fa-calendar"></i>
                                                <input type="text" class="input-sm" name="txtEndEventDate" id="txtEndEventDate" placeholder="End Event Date"/>
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <select id="ddlMessageGroup" name="ddlMessageGroup" multiple style="width: 100%" class="select2 input-sm">
                                                <option value="" selected="selected">Select All Message Group</option>
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
                                            <label class="input">
                                                <input type="text" class="input-sm" id="txtLocation" placeholder="Location" />
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="select">
                                                <select id="ddlMessageStatus" name="ddlMessageStatus" multiple style="width: 100%" class="select2 input-sm">
                                                    <option value="">Select All Status</option>
                                                    <option value="2" selected="selected">Business Warning</option>
                                                    <option value="1" selected="selected">Device Issue</option>
                                                    <option value="0">Device Solved</option>
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
                <span class="monitoring-liveupdate-1"> <span class="onoffswitch-title txt-color-white">Live switch</span> <span class="onoffswitch">
										        <input type="checkbox" name="start_interval" class="onoffswitch-checkbox" id="start_interval">
										        <label class="onoffswitch-label" for="start_interval"> 
											        <span class="onoffswitch-inner" data-swchon-text="ON" data-swchoff-text="OFF"></span> 
											        <span class="onoffswitch-switch"></span> </label> </span> </span>

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
    <%: Scripts.Render("~/bundles/myJs") %>
    
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
                    "bScrollCollapse": true,
                    "bServerSide": true,
                    "aaSorting": [[1, "desc"]],
                    "iDisplayLength": 50,
                    "sAjaxSource": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SummaryReport/SearchMonitoringReport/",
                    "fnServerData": function (sSource, aoData, fnCallback) {
                        aoData.push({ "name": "txtStartEventDate", "value": $('#txtStartEventDate').val() });
                        aoData.push({ "name": "txtEndEventDate", "value": $('#txtEndEventDate').val() });
                        aoData.push({ "name": "ddlMessageGroup", "value": $('#ddlMessageGroup').val() });
                        aoData.push({ "name": "txtClientCode", "value": $('#txtClientCode').val() });
                        aoData.push({ "name": "txtMessage", "value": $('#txtMessage').val() });
                        aoData.push({ "name": "txtLocation", "value": $('#txtLocation').val() });
                        aoData.push({ "name": "ddlMessageStatus", "value": $('#ddlMessageStatus').val() });
                        Pace.restart();
                        $.ajax({
                            "type": "GET",
                            "dataType": 'json',
                            "contentType": "application/json; charset=utf-8",
                            "url": sSource,
                            "data": aoData,
                            "success": function (data) {
                                fnCallback(data);
                            }
                        });
                    },
                    "aoColumns": [
                        {
                            // #
                            "mDataProp": "RowNum",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "80",
                            "sClass": "center"
                        },
                        {
                            // Date
                            "mDataProp": "EventDateTime",
                            "bSearchable": false,
                            "bSortable": true,
                            "sWidth": "180",
                            "sClass": "center",
                            "fnRender": function (oObj) {
                                var date = new Date(parseInt(oObj.aData["EventDateTime"].substr(6)));
                                return dateFormat(date, "dd/mm/yyyy HH:MM:ss");
                            }

                        },
                        {
                            // Client Code
                            "mDataProp": "ClientCode",
                            "bSearchable": false,
                            "bSortable": true,
                            "sWidth": "120"
                        },
                        {
                            // Location
                            "mDataProp": "Location",
                            "bSearchable": false,
                            "bSortable": true,
                            "sWidth": "220"
                        },
                        {
                            // IP Address
                            "mDataProp": "ClientIpAddress",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "100",
                        },
                        {
                            //status
                            "mDataProp": "Status",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "120",
                            "sClass": "center",
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
                            //group
                            "mDataProp": "MessageGroupName",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "120",
                        },
                        {
                            //severity
                            "mDataProp": "LevelName",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "150",
                            "sClass": "center",
                            "fnRender": function(oObj) {
                                return oObj.aData["ColorTagStart"] + oObj.aData["LevelName"] + oObj.aData["ColorTagEnd"];
                            }
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
                            "mDataProp": "MessageRemark",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "200",
                            "fnRender": function (oObj) {
                                var content = '';
                                if (oObj.aData["DeviceDescription"] != null)
                                    content = oObj.aData["DeviceDescription"];
                                if (oObj.aData["MessageRemark"] != null) {
                                    if (oObj.aData["DeviceDescription"] != null)
                                        content = content + ' - ';
                                    content = content + oObj.aData["MessageRemark"];
                                }
                                return content;
                            }
                        },
                        {
                            "mData": null,
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "80",
                            "sClass": "center",
                            "fnRender": function (oObj) {
                                return '<a id="edit_item_' + oObj.aData["ClientId"] + '" class="btn btn-primary btn-xs" href="javascript:ViewClientReport(' + oObj.aData["ClientId"] + ')"><i class="glyphicon glyphicon-search"></i></a>';
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

                today = yyyy + '.' + mm + '.' + dd;

                //$('#txtMessageDate').attr("placeholder", today);

                $('#txtStartEventDate').datepicker({
                    dateFormat: 'yy-mm-dd',
                    prevText: '<i class="fa fa-chevron-left"></i>',
                    nextText: '<i class="fa fa-chevron-right"></i>',
                    maxDate: '0',
                    changeMonth: true,
                    changeYear: true,
                    onSelect: function (selectedDate) {
                        $('#txtEndEventDate').datepicker('option', 'minDate', selectedDate);
                    },
                    onClose: function (selectedDate) {
                        $('#txtEndEventDate').datepicker('option', 'minDate', selectedDate);
                    }
                });

                $('#txtEndEventDate').datepicker({
                    dateFormat: 'yy-mm-dd',
                    prevText: '<i class="fa fa-chevron-left"></i>',
                    nextText: '<i class="fa fa-chevron-right"></i>',
                    maxDate: '0',
                    changeMonth: true,
                    changeYear: true,
                    onSelect: function (selectedDate) {
                        $('#txtStartEventDate').datepicker('option', 'maxDate', selectedDate);
                    },
                    onClose: function (selectedDate) {
                        if (selectedDate == '') selectedDate = '0';
                        $('#txtStartEventDate').datepicker('option', 'maxDate', selectedDate);
                    }

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

                $('#searhForm').submit(function (event) {
                    /* stop form from submitting normally */
                    event.preventDefault();

                    var oTable = $('#dt_basic').dataTable();
                    Pace.restart(); oTable.fnDraw();

                });

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

                        var oTable = $('#dt_basic').dataTable();
                        Pace.restart(); oTable.fnDraw();
                        setTimeout(update, updateInterval);

                    } else {
                        clearInterval(updateInterval);
                    }

                }

                var $on = false;
        });

        function ViewClientReport(id) {
            var params = new Array();
            params["id"] = id;

            post_to_url("ClientReport.aspx", params, null);
        }

    </script>

</asp:Content>
