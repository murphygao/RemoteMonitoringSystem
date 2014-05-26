﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientList.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.ClientList" %>
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
								Client Monitoring Setup
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
                    <div class="jarviswidget" id="wid-client-setup-list-1" data-widget-colorbutton="false" data-widget-editbutton="false" data-widget-deletebutton="false" data-widget-fullscreenbutton="false" data-widget-custombutton="false" data-widget-sortable="false">
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
                                                <i class="icon-append fa fa-calendar"></i>
                                                <input type="text" class="input-sm" name="txtAsOfDate" id="txtAsOfDate" placeholder="As Of Date"/>
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="select">
                                                <select id="dllClientType" class="input-sm">
                                                    <option value="">Client Type</option>
                                                    <option value="1">Kiosk</option>
                                                    <option value="2">Agent</option>
                                                </select>
                                                <i></i>
                                            </label>
                                        </section>

                                         <section class="col col-md-6 col-xs-12">
                                            <label class="input">
                                                <input type="text" class="input-sm" id="txtClientCode" placeholder="Client Code" />
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="select">
                                                <select id="ddlClientStatus" class="input-sm">
                                                    <option value="">Status</option>
                                                    <option value="true">Enable</option>
                                                    <option value="false">Disable</option>
                                                </select>
                                                <i></i>
                                            </label>
                                        </section>

                                        <section class="col col-md-6 col-xs-12">
                                            <label class="input">
                                                <input type="text" class="input-sm" id="txtIPAddress" placeholder="IP Address" />
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
                    <div class="jarviswidget jarviswidget-color-blueDark" id="wid-client-list-2"
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
                                            <th>Type</th>
                                            <th>Client Code</th>
                                            <th>Location</th>
                                            <th>IP Address</th>
                                            <th style="text-align: center">Status</th>
                                            <th>Effective Date</th>
                                            <th>Expired Date</th>
                                            <th>Monitoring Profile</th>
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

        <div class="row form-horizontal">
            <div class="col-xs-12">
                <div class="form-group">
                    <div class="col-md-8">
                        <a class="btn btn-primary" id="aNew" href="ClientEdit.aspx">Add New</a>
                    </div>
                </div>
            </div>
        </div>

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
                "aaSorting": [[2, "asc"]],
                "iDisplayLength": 10,
                "sAjaxSource": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/Search/",
                    "fnServerData": function (sSource, aoData, fnCallback) {
                        aoData.push({ "name": "txtAsOfDate", "value": $('#txtAsOfDate').val() });
                        aoData.push({ "name": "dllClientType", "value": $('#dllClientType').val() });
                        aoData.push({ "name": "txtClientCode", "value": $('#txtClientCode').val() });
                        aoData.push({ "name": "ddlClientStatus", "value": $('#ddlClientStatus').val() });
                        aoData.push({ "name": "txtIPAddress", "value": $('#txtIPAddress').val() });
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
                            // Client Type
                            "mDataProp": "ClientType",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "100"
                        },
                        {
                            // Client Code
                            "mDataProp": "ClientCode",
                            "bSearchable": false,
                            "bSortable": true,
                            "sWidth": "150"
                        },
                        {
                            // Location
                            "mDataProp": "LocationCode",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "200",
                            "fnRender": function (oObj) {
                                var loc = '';
                                if (oObj.aData["LocationCode"] != null)
                                    loc = oObj.aData["LocationCode"];
                                if (oObj.aData["LocationName"] != null)
                                    loc = loc + ' ' + oObj.aData["LocationName"];
                                if (loc == '')
                                    loc = 'N/A';
                                return loc;
                            }
                        },
                        {
                            // IP Address
                            "mDataProp": "IPAddress",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "100",
                        },
                        {
                            //status
                            "mDataProp": "Enable",
                            "bSearchable": false,
                            "bSortable": true,
                            "sWidth": "120",
                            "sClass": "center",
                            "fnRender": function (oObj) {
                                if (oObj.aData["Enable"]) {
                                    return '<span class="label label-success">Enable</span>';
                                } else {
                                    return '<span class="label label-danger">Disable</span>';
                                }
                            }
                        },
                        {
                            // EffectiveDate
                            "mDataProp": "EffectiveDate",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "140",
                            "sClass": "center",
                            "fnRender": function (oObj) {
                                if (oObj.aData["EffectiveDate"] == null) return '';
                                var date = new Date(parseInt(oObj.aData["EffectiveDate"].substr(6)));
                                return dateFormat(date, "dd/mm/yyyy");
                            }

                        },
                        {
                            // ExpiredDate
                            "mDataProp": "ExpiredDate",
                            "bSearchable": false,
                            "bSortable": false,
                            "sWidth": "140",
                            "sClass": "center",
                            "fnRender": function (oObj) {
                                if (oObj.aData["ExpiredDate"] == null) return '';
                                var date = new Date(parseInt(oObj.aData["ExpiredDate"].substr(6)));
                                return dateFormat(date, "dd/mm/yyyy");
                            }

                        },
                        {
                            // Monitoring Profile
                            "mDataProp": "ProfileName",
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
                                return '<a id="edit_item_' + oObj.aData["ClientID"] + '" class="btn btn-primary btn-xs" href="javascript:retun;ViewClientReport(' + oObj.aData["ClientID"] + ')"><i class="glyphicon glyphicon-search"></i></a>';
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

            $('#txtAsOfDate').datepicker({
                dateFormat: 'yy.mm.dd',
                prevText: '<i class="fa fa-chevron-left"></i>',
                nextText: '<i class="fa fa-chevron-right"></i>',
                maxDate: '0',
                changeMonth: true,
                changeYear: true,
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

            function ViewClientReport(id) {
                var params = new Array();
                params["id"] = id;

                post_to_url("ClientReport.aspx", params, null);
            }

    </script>

</asp:Content>