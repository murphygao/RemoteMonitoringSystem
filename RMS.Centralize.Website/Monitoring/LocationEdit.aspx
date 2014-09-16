<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocationEdit.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.ClientEdit" %>

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
								Monitoring Location
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
                    <div class="jarviswidget" id="locationedit-id-0" data-widget-colorbutton="false" data-widget-sortable="false" data-widget-editbutton="false" data-widget-fullscreenbutton="false" data-widget-togglebutton="false" data-widget-deletebutton="false" data-widget-custombutton="false">
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
                            <h2>Location Setup</h2>

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
                                                <label class="label">Location Code</label>
                                                <label class="input">
                                                    <input type="text" id="txtLocationCode" name="txtLocationCode" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>, <strong>Maxlength</strong> is 50 characters.
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Location Name</label>
                                                <label class="input">
                                                    <input type="text" id="txtLocationName" name="txtLocationName" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>, <strong>Maxlength</strong> is 250 characters.
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
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Monday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxMondayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxMondayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="mondayPeriod-value">20 - 60</span>)</label>
												<div id="mondayPeriod" class="noUiSlider"></div>
                                            </section>
                                        </div>


                                        <div class="row">
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Tuesday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxTuesdayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxTuesdayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="tuesdayPeriod-value">20 - 60</span>)</label>
												<div id="tuesdayPeriod" class="noUiSlider"></div>
                                            </section>
                                        </div>


                                        <div class="row">
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Wednesday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxWednesdayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxWednesdayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="wednesdayPeriod-value">20 - 60</span>)</label>
												<div id="wednesdayPeriod" class="noUiSlider"></div>
                                            </section>
                                        </div>


                                        <div class="row">
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Thursday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxThursdayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxThursdayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>
                                            
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="thursdayPeriod-value">20 - 60</span>)</label>
												<div id="thursdayPeriod" class="noUiSlider"></div>
                                            </section>
                                        </div>


                                        <div class="row">
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Friday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxFridayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxFridayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="fridayPeriod-value">20 - 60</span>)</label>
												<div id="fridayPeriod" class="noUiSlider"></div>
                                            </section>
                                        </div>


                                        <div class="row">
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Saturday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxSaturdayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxSaturdayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="saturdayPeriod-value">20 - 60</span>)</label>
												<div id="saturdayPeriod" class="noUiSlider"></div>
                                            </section>
                                        </div>


                                        <div class="row">
                                            <section class="col col-10" style="margin-bottom: 0" >
                                                    <label class="label" style="font-weight: 600">Sunday</label>
                                            </section>
                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxSundayEnable" checked="checked">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Enable</span></label>
                                            </section>

                                            <section class="col col-4">
                                                    <label class="toggle">
                                                    <input type="checkbox" id="cbxSundayWholeDay">
                                                    <i data-swchon-text="ON" data-swchoff-text="OFF"></i><span style="font-size: 13px!important; font-weight: 400!important;">Whole Day</span></label>
                                            </section>

                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
												<label class="label">Monitoring Period (<span id="sundayPeriod-value">20 - 60</span>)</label>
												<div id="sundayPeriod" class="noUiSlider"></div>
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
                                        <input type="hidden" id="currentClientCode" />
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
        .noUi-target[disabled] .noUi-base{background:#eee}
        .slider-tooltip {
	        display: block;
	        position: absolute;
	        border: 1px solid #D9D9D9;
	        font: 400 12px/12px Arial;
	        border-radius: 3px;
	        background: #fff;
	        top: -35px;
	        padding: 5px;
	        left: -9px;
	        text-align: center;
	        width: 50px;
        }
        .slider-tooltip strong {
	        display: block;
	        padding: 2px;
        }
    </style>

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!

        $(document).ready(function () {

            var fullPath = "<%=HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/"))%>/LocationList.aspx";
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
                    txtLocationCode: {
                        required: true,
                        maxlength: 50
                    },
                    txtLocationName: {
                        required: true,
                        maxlength: 250
                    },
                },
                messages: {
                    txtLocationCode: {
                        required: 'Please enter location code',
                        maxlength: 'The field MaxLength is 50.'
                    },
                    txtLocationName: {
                        required: 'Please enter location name',
                        maxlength: 'The field MaxLength is 250.'
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
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Location/Get/",
                    "data": "{'id' : " + <%=Request["id1"]%> + ", 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                    "success": function (ret) {

                        if (ret.status == "1") {

                            var myData = JSON.parse(ret.data);

                            $('#txtLocationCode').val(myData.LocationCode);
                            $('#txtLocationName').val(myData.LocationName);
                            $('#cbxActiveList').attr('checked', myData.ActiveList);

                            $('#cbxMondayEnable').attr('checked', myData.MondayEnable);
                            $('#cbxMondayWholeDay').attr('checked', myData.MondayWholeDay);
                            $("#mondayPeriod").val([parseTimeToMinute(myData.MondayStart), parseTimeToMinute(myData.MondayEnd)], { set: true });
                            if (myData.MondayWholeDay) $("#mondayPeriod").attr('disabled', 'disabled');

                            $('#cbxTuesdayEnable').attr('checked', myData.TuesdayEnable);
                            $('#cbxTuesdayWholeDay').attr('checked', myData.TuesdayWholeDay);
                            $("#tuesdayPeriod").val([parseTimeToMinute(myData.TuesdayStart), parseTimeToMinute(myData.TuesdayEnd)], { set: true });
                            if (myData.TuesdayWholeDay) $("#tuesdayPeriod").attr('disabled', 'disabled');

                            $('#cbxWednesdayEnable').attr('checked', myData.WednesdayEnable);
                            $('#cbxWednesdayWholeDay').attr('checked', myData.WednesdayWholeDay);
                            $("#wednesdayPeriod").val([parseTimeToMinute(myData.WednesdayStart), parseTimeToMinute(myData.WednesdayEnd)], { set: true });
                            if (myData.WednesdayWholeDay) $("#wednesdayPeriod").attr('disabled', 'disabled');

                            $('#cbxThursdayEnable').attr('checked', myData.ThursdayEnable);
                            $('#cbxThursdayWholeDay').attr('checked', myData.ThursdayWholeDay);
                            $("#thursdayPeriod").val([parseTimeToMinute(myData.ThursdayStart), parseTimeToMinute(myData.ThursdayEnd)], { set: true });
                            if (myData.ThursdayWholeDay) $("#thursdayPeriod").attr('disabled', 'disabled');

                            $('#cbxFridayEnable').attr('checked', myData.FridayEnable);
                            $('#cbxFridayWholeDay').attr('checked', myData.FridayWholeDay);
                            $("#fridayPeriod").val([parseTimeToMinute(myData.FridayStart), parseTimeToMinute(myData.FridayEnd)], { set: true });
                            if (myData.FridayWholeDay) $("#fridayPeriod").attr('disabled', 'disabled');

                            $('#cbxSaturdayEnable').attr('checked', myData.SaturdayEnable);
                            $('#cbxSaturdayWholeDay').attr('checked', myData.SaturdayWholeDay);
                            $("#saturdayPeriod").val([parseTimeToMinute(myData.SaturdayStart), parseTimeToMinute(myData.SaturdayEnd)], { set: true });
                            if (myData.SaturdayWholeDay) $("#saturdayPeriod").attr('disabled', 'disabled');

                            $('#cbxSundayEnable').attr('checked', myData.SundayEnable);
                            $('#cbxSundayWholeDay').attr('checked', myData.SundayWholeDay);
                            $("#sundayPeriod").val([parseTimeToMinute(myData.SundayStart), parseTimeToMinute(myData.SundayEnd)], { set: true });
                            if (myData.SundayWholeDay) $("#sundayPeriod").attr('disabled', 'disabled');

                            $('#id1').val(myData.LocationId);
                            $('#m').val("e");


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

            $("#btnSubmit").on("click", function (event) {
                event.preventDefault(); // will work!
            });


            $("#mondayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true
            });

            $("#mondayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#mondayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#mondayPeriod-value');
                },
            });
            $("#mondayPeriod").val([480, 1200], { set: true });
            $('#cbxMondayWholeDay').click(function() {
                toggle(this, $("#mondayPeriod"));
            });

            $("#tuesdayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true
            });

            $("#tuesdayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#tuesdayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#tuesdayPeriod-value');
                },
            });
            $("#tuesdayPeriod").val([480, 1200], { set: true });
            $('#cbxTuesdayWholeDay').click(function () {
                toggle(this, $("#tuesdayPeriod"));
            });

            $("#wednesdayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true
            });

            $("#wednesdayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#wednesdayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#wednesdayPeriod-value');
                },
            });
            $("#wednesdayPeriod").val([480, 1200], { set: true });
            $('#cbxWednesdayWholeDay').click(function () {
                toggle(this, $("#wednesdayPeriod"));
            });


            $("#thursdayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true
            });

            $("#thursdayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#thursdayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#thursdayPeriod-value');
                },
            });
            $("#thursdayPeriod").val([480, 1200], { set: true });
            $('#cbxThursdayWholeDay').click(function () {
                toggle(this, $("#thursdayPeriod"));
            });


            $("#fridayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true
            });

            $("#fridayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#fridayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#fridayPeriod-value');
                },
            });
            $("#fridayPeriod").val([480, 1200], { set: true });
            $('#cbxFridayWholeDay').click(function () {
                toggle(this, $("#fridayPeriod"));
            });


            $("#saturdayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true
            });

            $("#saturdayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#saturdayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#saturdayPeriod-value');
                },
            });
            $("#saturdayPeriod").val([480, 1200], { set: true });
            $('#cbxSaturdayWholeDay').click(function () {
                toggle(this, $("#saturdayPeriod"));
            });


            $("#sundayPeriod").noUiSlider({
                range: { 'min': [0], 'max': [1439] },
                start: [600, 1200],
                behaviour: 'drag-tap',
                step: 5,
                connect: true,
            });

            $("#sundayPeriod").on({
                set: function () {
                    var values = $(this).val(); setPeriodValue(values, '#sundayPeriod-value');
                },
                slide: function () {
                    var values = $(this).val(); setPeriodValue(values, '#sundayPeriod-value');
                },
            });
            $("#sundayPeriod").val([480, 1200], { set: true });
            $('#cbxSundayWholeDay').click(function () {
                toggle(this, $("#sundayPeriod"));
            });

        });

        var customToolTip = $.Link({
            target: '-tooltip-<div class="slider-tooltip"></div>',
            method: function (value) {

                // The tooltip HTML is 'this', so additional
                // markup can be inserted here.
                $(this).html(
                    '<span>' + minuteToTimeStr(value) + '</span>'
                );
            }
        });

        function toggle(cbx, slider) {
            //var slider = obj;
            if (cbx.checked) {
                slider.attr('disabled', 'disabled');
            } else {
                slider.removeAttr('disabled');
            }
        }

        function setPeriodValue(val, objValue) {
            var values = val;
            $(objValue).text(minuteToTimeStr(values[0]) + ' - ' + minuteToTimeStr(values[1]));
        }

        function minuteToTimeStr(val) {
            var hours1 = Math.floor(val / 60);
            var minutes1 = val - (hours1 * 60);

            if (hours1 < 10) hours1 = '0' + hours1;
            if (minutes1 < 10) minutes1 = '0' + minutes1;

            if (minutes1 == 0) minutes1 = '00';

            return hours1 + ':' + minutes1;
        }

        function parseTimeToMinute(s) {
            if (s == null) return 0;
            var date = new Date(s.replace('T', ' '));
            var time = dateFormat(date, "HH:MM");
            var c = time.split(':');
            return parseInt(c[0]) * 60 + parseInt(c[1]);
        }

        function update() {

            if (!$('#smartForm').valid()) return;

            tmpObj = function () {
                this.id = $('#id1').val();
                this.m = $('#m').val();
                this.locationCode = $('#txtLocationCode').val();
                this.locationName = $('#txtLocationName').val();
                this.activeList = $('#cbxActiveList').is(':checked');

                var periods;

                this.mondayEnable = $('#cbxMondayEnable').is(':checked');
                this.mondayWholeDay = $('#cbxMondayWholeDay').is(':checked');
                periods = $('#mondayPeriod').val();
                this.mondayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.mondayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

                this.tuesdayEnable = $('#cbxTuesdayEnable').is(':checked');
                this.tuesdayWholeDay = $('#cbxTuesdayWholeDay').is(':checked');
                periods = $('#tuesdayPeriod').val();
                this.tuesdayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.tuesdayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

                this.wednesdayEnable = $('#cbxWednesdayEnable').is(':checked');
                this.wednesdayWholeDay = $('#cbxWednesdayWholeDay').is(':checked');
                periods = $('#wednesdayPeriod').val();
                this.wednesdayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.wednesdayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

                this.thursdayEnable = $('#cbxThursdayEnable').is(':checked');
                this.thursdayWholeDay = $('#cbxThursdayWholeDay').is(':checked');
                periods = $('#thursdayPeriod').val();
                this.thursdayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.thursdayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

                this.fridayEnable = $('#cbxFridayEnable').is(':checked');
                this.fridayWholeDay = $('#cbxFridayWholeDay').is(':checked');
                periods = $('#fridayPeriod').val();
                this.fridayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.fridayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

                this.saturdayEnable = $('#cbxSaturdayEnable').is(':checked');
                this.saturdayWholeDay = $('#cbxSaturdayWholeDay').is(':checked');
                periods = $('#saturdayPeriod').val();
                this.saturdayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.saturdayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

                this.sundayEnable = $('#cbxSundayEnable').is(':checked');
                this.sundayWholeDay = $('#cbxSundayWholeDay').is(':checked');
                periods = $('#sundayPeriod').val();
                this.sundayStart = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[0]) + ":00";
                this.sundayEnd = dateFormat(new Date(), "yyyy-mm-dd") + " " + minuteToTimeStr(periods[1]) + ":00";

            };
            var updObj = new tmpObj();
            Pace.restart();

            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/Location/Update/",
                "data": JSON.stringify(updObj),
                "success": function (ret) {

                    if (ret.status == "1") {
                        try {
                            $.smallBox({
                                title: "Update Complete",
                                content: "<i class='fa fa-clock-o'></i> <i>This operation is complete.</i>",
                                color: "#659265",
                                iconSmall: "fa fa-check fa-2x fadeInRight animated",
                                timeout: 2000
                            });

                        } catch (e) {
                            alert("This operation is complete.");
                        }

                        setTimeout(function () {
                            window.location.href = 'LocationList.aspx';
                        }, 1500);

                    } else if (ret.status == "-1") {

                        try {
                            $.smallBox({
                                title: "Update Failed!",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert(ret.error);
                        }
                    } else if (ret.status == "0") {
                        try {
                            $.smallBox({
                                title: "Update Failed",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });

                        } catch (e) {
                            alert(ret.error);
                        }
                    }

                },

            });
        }

    </script>



</asp:content>
