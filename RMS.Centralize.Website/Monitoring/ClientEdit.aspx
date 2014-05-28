﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientEdit.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.ClientEdit" %>
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
                                                    </select>
                                                    <i></i>
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>
                                                </div>
                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Reference Client</label>
                                                <label class="input">
                                                    <input type="text" id="txtReferenceClient" name="txtReferenceClient" class="input-sm">
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
                                        <button type="button" class="btn btn-default" style="float: left;" onclick="window.location='MessageAction.aspx';">
                                            Back
                                        </button>
                                        <input type="hidden" id="id1" />
                                        <input type="hidden" id="m" />

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

    <%: Scripts.Render("~/bundles/myJs") %>
    <%: Scripts.Render("~/bundles/defaultJs") %>

    <!-- JS TOUCH : include this plugin for mobile drag / drop touch events
		<script src="js/plugin/jquery-touch/jquery.ui.touch-punch.min.js"></script> -->

    <!-- PAGE RELATED PLUGIN(S) 
		<script src="..."></script>-->

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!
        initialData();

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

            // PAGE RELATED SCRIPTS

            var response;
            $.validator.addMethod(
                "uniqueClientCode",
                function (value, element) {
                    $.ajax({
                        "type": "POST",
                        "dataType": 'json',
                        "contentType": "application/json; charset=utf-8",
                        "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/ExistingClientCode/",
                        "data": "{'clientCode' : '" + value + "'}",
                        success: function (ret) {
                            if (ret.isSuccess) {

                                response = (ret.aaData == '0') ? true : false;
                            } else {
                                response = false;
                            }
                        }
                    });
                    return response;
                },
                "Username is Already Taken"
            );

            var $checkoutForm = $('#smartForm').validate({
                // Rules for form validation
                rules: {
                    txtClientCode: {
                        required: true,
                        uniqueClientCode: true
                    },
                    txtReferenceClient: {
                        required: true
                    },
                    txtEffectiveDate: {
                        required: true,
                        dateISO: true
                    },
                    txtExpiredDate: {
                        dateISO: true
                    },
                },
                messages: {
                    txtClientCode: {
                        required: 'Please enter client code',
                        uniqueClientCode: 'This client code is taken already'
                     },
                    txtReferenceClient: {
                        required: 'Please enter reference client'
                    },
                    txtEffectiveDate: {
                        required: 'Please enter effective date',
                        dateISO: 'Please enter effective date in DATE format'
                    },
                    txtExpiredDate: {
                        dateISO: 'Please enter expired date in DATE format'
                    },
                },

                errorPlacement: function (error, element) {
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
                onSelect: function (selectedDate) {
                    $('#txtExpiredDate').datepicker('option', 'minDate', selectedDate);
                },
                onClose: function (selectedDate) {
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
                onSelect: function (selectedDate) {
                    $('#txtEffectiveDate').datepicker('option', 'maxDate', selectedDate);
                },
                onClose: function (selectedDate) {
                    if (selectedDate == '') selectedDate = new Date(2099, 12, 0);
                    $('#txtEffectiveDate').datepicker('option', 'maxDate', selectedDate);
                }

            });

            $("#txtEffectiveDate").datepicker('setDate', new Date());
            $('#txtExpiredDate').datepicker('option', 'minDate', new Date());

            if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {

                Pace.restart();
                $.ajax({
                    "type": "POST",
                    "dataType": 'json',
                    "contentType": "application/json; charset=utf-8",
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Client/GetClient/",
                    "data": "{'id' : " + <%=Request["id1"]%> + "}",
                    "success": function (ret) {

                        if (ret.status == "1") {

                            var myData = JSON.parse(ret.data);

                            $('#txtClientCode').val(myData.ClientCode);
                            $('#ddlClientTypeID').val(myData.ClientTypeId);
                            $('#txtReferenceClient').val(myData.ReferenceClientId);

                            $('#cbxActiveList').attr('checked', myData.ActiveList);
                            $('#cbxEnable').attr('checked', myData.Enable);

                            if (myData.EffectiveDate != '') {
                                var eff = new Date(myData.EffectiveDate.substr(0, 10));
                                $("#txtEffectiveDate").datepicker('setDate', eff);
                                $('#txtExpiredDate').datepicker('option', 'minDate', eff);

                            }
                            if (myData.txtExpiredDate != '') {
                                var exp = new Date(myData.ExpiredDate.substr(0, 10));
                                $("#txtExpiredDate").datepicker('setDate', exp);
                                $('#txtEffectiveDate').datepicker('option', 'maxDate', exp);
                            }

                            $('#id1').val(myData.MessageId);
                            $('#m').val("e");


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




        });

        function initialData() {
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MessageAction/InitDataForMeesageEdit/",
                "success": function (data) {
                    $('#ddlMessageGroupID').append(unescape(data.messageGroup));
                    $('#ddlSeverityLevelID').append(unescape(data.severityLevel));
                },

            });
        }

        function update() {

            event.preventDefault();

            if (!$('#smartForm').valid()) return;

            tmpObj = function () {
                this.id = $('#id1').val();
                this.m = $('#m').val();
                this.messageGroupID = $('#ddlMessageGroupID').val();
                this.message = $('#txtMessage').val();
                this.severityLevelID = $('#ddlSeverityLevelID').val();
                this.ReadOnly = false;
                this.activeList = $('#cbxActiveList').is(':checked');
                this.activeStatus = $('#cbxActiveStatus').is(':checked');
            };
            var updObj = new tmpObj();
            Pace.restart();

            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MessageAction/UpdateMessage/",
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
                            window.location.href = 'MessageAction.aspx';
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