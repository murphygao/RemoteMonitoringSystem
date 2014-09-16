<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebsiteMonitoringList.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.WebsiteMonitoringList" %>
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
								Website Monitoring
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

            <div class="row" style="display: none;">
                <article class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                    <div class="jarviswidget" id="websitemonitoring-id-0" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-sortable="false">
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
                                            <label class="input">
                                                <input type="text" class="input-sm" id="txtClientID" placeholder="Client ID" />
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                        </section>

                                        <section class="col col-md-12 col-xs-12">
                                            <button type="submit" id="btnSubmit" class="btn btn-primary" style="padding: 5px 16px">
                                                Search
                                            </button>
                                            <button type="button" id="btnReset" class="btn btn-default" style="padding: 5px 16px" onclick="this.form.reset();">
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
                    <div class="jarviswidget jarviswidget-color-blueDark" id="websitemonitoring-id-1"
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

                                <table id="dt_basic" class="table table-striped table-hover" >
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">#</th>
                                            <th>Client Code</th>
                                            <th>IP Address</th>
                                            <th style="text-align: center">Type</th>
                                            <th style="text-align: center">Protocol</th>
                                            <th>Host Name / URL</th>
                                            <th style="text-align: center">Port</th>
                                            <th style="text-align: center">Active</th>
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
        <!-- end widget grid -->

        <div class="row form-horizontal">
            <div class="col-xs-12">
                <div class="form-group">
                    <div class="col-md-8">
                        <button type="button" class="btn btn-primary" id="aNew" onclick="toAddNew('<%=Request["clientID"]%>')">Add New</button>
                        <button type="button" class="btn btn-default" onclick="window.history.go(-1); return false;">&nbsp;&nbsp;Back&nbsp;&nbsp;</button>
                    </div>
                </div>
            </div>
        </div>

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

            /*
             * BASIC
             */
            if ("<%=Request["clientID"]%>" == "") {
                try {
                    $.smallBox({
                        title: "System Failed",
                        content: "<i class='fa fa-clock-o'></i> <i>Parameter ClientID cannot be null.</i>",
                        color: "#C46A69",
                        iconSmall: "fa fa-times fa-2x fadeInRight animated",
                        timeout: 4000
                    });

                } catch (e) {
                    alert(data.error);
                }
                setTimeout(function () {
                    window.location.href = 'ClientList.aspx';
                }, 2000);
            } else
                $('#dt_basic').dataTable({
                    "sPaginationType": "bootstrap_full",
                    "bFilter": false,
                    "bAutoWidth": false,
                    "bPaginate": true,
                    "bInfo": true,
                    "sDom": 'r<"dt-top-row"lf><"dt-wrapper"<"datatable-scroll"t>><"dt-row dt-bottom-row"<"row"<"col-sm-6"i><"col-sm-6 text-right"p>>>',
                    "bServerSide": true,
                    "iDisplayLength": 10,
                    "sAjaxSource": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/WebsiteMonitoring/List/",
                    "fnServerData": function(sSource, aoData, fnCallback) {
                        aoData.push({ "name": "clientID", "value": "<%=Request["clientID"]%>" });
                        aoData.push({ "name": "dt", "value": dateFormat(new Date(), "yyyymmddHHMMss") });

                        Pace.restart();
                        $.ajax({
                            "type": "POST",
                            "dataType": 'json',
                            "url": sSource,
                            "data": aoData,
                            "success": function(data) {
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
                                    setTimeout(function () {
                                        window.location.href = 'ClientList.aspx';
                                    }, 2000);
                                }
                            }
                        });
                    },
                    "fnDrawCallback": function(oSettings) {
                        /* Need to redo the counters if filtered or sorted */
                        if (oSettings.bSorted || oSettings.bFiltered) {
                            for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                                $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                            }
                        }
                    },
                    "aoColumns": [
                        {
                            "mData": null,
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "50",
                            "sClass": "center"
                        },
                        {
                            "mDataProp": "ClientCode",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "80",
                            "fnRender": function(oObj) {
                                return htmlEscape(oObj.aData["ClientCode"]);
                            }
                        },
                        {
                            "mDataProp": "ClientIPAddress",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "60",
                            "fnRender": function(oObj) {
                                return htmlEscape(oObj.aData["ClientIPAddress"]);
                            }
                        },
                        {
                            "mDataProp": "WebsiteMonitoringTypeValue",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "50",
                            "sClass": "center"
                        },
                        {
                            "mDataProp": "WebsiteMonitoringProtocolValue",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "50",
                            "sClass": "center"
                        },
                        {
                            "mDataProp": "DomainName",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "200",
                            "fnRender": function(oObj) {
                                return htmlEscape(oObj.aData["DomainName"]);
                            }
                        },
                        {
                            "mDataProp": "PortNumber",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "50",
                            "sClass": "center"
                        },
                        {
                            "mDataProp": "Enable",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "50",
                            "sClass": "center",
                            "fnRender": function(oObj) {
                                if (oObj.aData["Enable"]) {
                                    return '<span class="label label-success">ON</span>';
                                } else {
                                    return '<span class="label label-danger">OFF</span>';
                                }
                            }
                        },
                        {
                            "mData": null,
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "80",
                            "sClass": "nowrap",
                            "fnRender": function(oObj) {
                                var str = '';
                                str += '<a id="edit_item_' + oObj.aData["WebsiteMonitoringId"] + '" class="btn btn-primary btn-xs" href="javascript:toEditRow(' + oObj.aData["WebsiteMonitoringId"] + ', ' + oObj.aData["ClientId"]  + ')"><i class="glyphicon glyphicon-edit"></i></a>';
                                str += '&nbsp;<a id="del_item_' + oObj.aData["WebsiteMonitoringId"] + '" class="btn btn-danger btn-xs" href="javascript:deleteRow(' + oObj.aData["WebsiteMonitoringId"] + ');"><i class="glyphicon glyphicon-trash"></i></a>';
                                return str;
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

            $('#searhForm').submit(function (event) {
                /* stop form from submitting normally */
                event.preventDefault();

                var oTable = $('#dt_basic').dataTable();
                Pace.restart(); oTable.fnDraw();

            });
        });

        function deleteRow(id) {
            Row = function (id) {
                this.id = id;
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
                        "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/WebsiteMonitoring/Delete",
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
                                if (oTable.fnGetData().length == 1)
                                    oTable.fnStandingRedraw(1);
                                else
                                    oTable.fnStandingRedraw(0);

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



        function toAddNew(clientid) {
            var params = new Array();
            params["clientid"] = clientid;

            post_to_url("WebsiteMonitoringEdit.aspx", params, null);
        }

        function toEditRow(id, clientid) {
            var params = new Array();
            params["m"] = 'e';
            params["id1"] = id;
            params["clientid"] = clientid;

            post_to_url("WebsiteMonitoringEdit.aspx", params, null);
        }

    </script>


</asp:Content>

