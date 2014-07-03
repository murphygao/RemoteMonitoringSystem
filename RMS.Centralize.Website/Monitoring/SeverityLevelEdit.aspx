<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeverityLevelEdit.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.SeverityLevelEdit" %>
<%@ Import Namespace="System.Web.Optimization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="content">

        <div class="row">
            <div class="col-xs-12 col-sm-10 col-md-10 col-lg-6">
                <h1 class="page-title txt-color-blueDark">
                    <i class="fa fa-bar-chart-o fa-fw "></i>
                    Remote Monitoring
							<span>> 
								Severity Level
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
                            <h2>Severity Level Setup</h2>

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
                                                <label class="label">Severity Level Code</label>
                                                <label class="input">
                                                    <input type="text" id="txtSeverityLevelCode" name="txtSeverityLevelCode" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Severity Level Name</label>
                                                <label class="input">
                                                    <input type="text" id="txtSeverityLevelName" name="txtSeverityLevelName" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-4">
                                                <label class="label">Order List</label>
                                                <label class="input">
                                                    <input type="text" id="txtOrderList" name="txtOrderList" class="input-sm" value="1000">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field, Number Only</strong>
                                                </div>

                                            </section>
                                            <section class="col col-4">
                                                <label class="label">Color Label</label>
                                                <label class="select">
                                                    <select id="ddlColorLabel" class="input-sm">
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
                                                    <input type="checkbox" id="cbxActiveList" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Active List</span></label>
                                            </section>
                                        </div>

                                         <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Default Action Profile</label>
                                                <label class="select">
                                                    <select id="ddlActionProfile" class="input-sm">
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
                                                    <input type="checkbox" id="cbxActionRepeatable">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Action Repeatable</span></label>
                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Action Repeat Interval (minute)</label>
                                                <label class="input">
                                                    <input type="text" id="txtActionRepeatInterval" name="txtActionRepeatInterval" class="input-sm" value="60">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field, Number Only</strong>
                                                </div>

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
                                        <input type="hidden" id="hLoadDataDone" />
                                        <input type="hidden" id="hActionProfileID" />
                                        <input type="hidden" id="hColorCode" />
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

    <%: Scripts.Render("~/bundles/defaultJs") %>
    <%: Scripts.Render("~/bundles/myJs") %>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- PAGE RELATED PLUGIN(S) 
		<script src="..."></script>-->

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!
        initialData();

        $(document).ready(function () {

            var fullPath = "<%=HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/"))%>/SeverityLevelList.aspx";
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

            // PAGE RELATED SCRIPTS

            var $checkoutForm = $('#smartForm').validate({
                // Rules for form validation
                rules: {
                    txtSeverityLevelCode: {
                        required: true
                    },
                    txtSeverityLevelName: {
                        required: true
                    },
                    txtOrderList: {
                        required: true,
                        number: true,
                    },
                    ddlColorLabel: {
                        required: true
                    },
                    ddlActionProfile: {
                        required: true
                    },
                    txtActionRepeatInterval: {
                        required: true,
                        number: true,
                    },
                },
                messages: {
                    txtSeverityLevelCode: {
                        required: 'Please enter severity level code'
                    },
                    txtSeverityLevelName: {
                        required: 'Please enter severity level name'
                    },
                    txtOrderList: {
                        required: 'Please enter order list',
                        number: 'Please enter number only'
                    },
                    ddlColorLabel: {
                        required: 'Please select color label',
                    },
                    ddlActionProfile: {
                        required: 'Please select action profile'
                    },
                    txtActionRepeatInterval: {
                        required: 'Please enter order list',
                        number: 'Please enter number only'
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
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SeverityLevel/Get/",
                    "data": "{'id' : " + <%=Request["id1"]%> + ", 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                    "success": function (ret) {

                        if (ret.status == "1") {

                            var myData = JSON.parse(ret.data);
                            $('#txtSeverityLevelCode').val(myData.LevelCode);
                            $('#txtSeverityLevelName').val(myData.LevelName);
                            $('#txtOrderList').val(myData.OrderList);
                            $('#cbxActiveList').attr('checked', myData.ActiveList);

                            $('#ddlActionProfile').val(myData.DefaultActionProfileId);
                            $('#cbxActionRepeatable').attr('checked', myData.ActionRepeatable);
                            $('#txtActionRepeatInterval').val(myData.ActionRepeatInterval);
                            $('#ddlColorLabel').val(myData.ColorCode);
                            $('#id1').val(myData.SeverityLevelId);
                            $('#m').val("e");
                            $('#hLoadDataDone').val("1");
                            $('#hActionProfileID').val(myData.DefaultActionProfileId);
                            $('#hColorCode').val(myData.ColorCode);

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

        });

        function initialData() {
            loadActionProfile();
            loadColorLabel();
        }


        function loadActionProfile() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/ActionProfile/List/",
                "data": "{'activeList' : true, 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                "success": function (data) {

                    $.each(data.ddlActionProfiles, function (index, item) {
                        $('#ddlActionProfile').append($('<option></option>').val(item.ActionProfileId).html(item.ActionProfileName));
                        if (item.Email != null && item.Email != '')
                            $('#ddlActionProfile').append($('<option disabled></option>').html('&nbsp;&nbsp;&nbsp;&nbsp;' + item.Email));
                        if (item.Sms != null && item.Sms != '')
                            $('#ddlActionProfile').append($('<option disabled></option>').html('&nbsp;&nbsp;&nbsp;&nbsp;' + item.Sms));
                    });

                    if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {
                        if ($('#hLoadDataDone').val() == "1") {
                            $('#ddlActionProfile').val($('#hActionProfileID').val());
                        }
                    }
                },

            });
        }

        function loadColorLabel() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MasterTable/ListColorLabels/",
                "data": "{'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                "success": function(data) {
                    $.each(data.listColorLabels, function(index, item) {
                        $('#ddlColorLabel').append($('<option class="label ' + item.ColorName + '" style="color: #fff"></option>').val(item.ColorCode).html(item.ColorCode));
                    });

                    if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {
                        if ($('#hLoadDataDone').val() == "1") {
                            $('#ddlColorLabel').val($('#hColorCode').val());
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
                this.levelCode = $('#txtSeverityLevelCode').val();
                this.levelName = $('#txtSeverityLevelName').val();
                this.orderList = $('#txtOrderList').val();
                this.activeList = $('#cbxActiveList').is(':checked');
                this.defaultActionProfileID = $('#ddlActionProfile').val();
                this.repeatable = $('#cbxActionRepeatable').is(':checked');
                this.repeatableInterval = $('#txtActionRepeatInterval').val();
                this.colorCode = $('#ddlColorLabel').val();

                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var updObj = new tmpObj();
            Pace.restart();

            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/SeverityLevel/Update/",
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
                            window.location.href = 'SeverityLevelList.aspx';
                        }, 1000);

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
                            title: "Update Failed",
                            content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                            color: "#C46A69",
                            iconSmall: "fa fa-times fa-2x fadeInRight animated",
                            timeout: 4000
                        });

                    }

                },

            });
        }

    </script>



</asp:Content>
