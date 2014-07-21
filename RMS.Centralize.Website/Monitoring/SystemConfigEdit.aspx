<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemConfigEdit.aspx.cs" Inherits="RMS.Centralize.Website.Monitoring.SystemConfigEdit" %>

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
								System Configuration
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
                            <h2>System Configuration Setup</h2>

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
                                                <label class="label">Name</label>
                                                <label class="input state-disabled">
                                                    <input type="text" id="txtName" name="txtName" class="input-sm" readonly="readonly">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>, <strong>Maxlength</strong> is 50 characters.
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Value</label>
                                                <label class="input">
                                                    <input type="text" id="txtValue" name="txtValue" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Required Field</strong>, <strong>Maxlength</strong> is 1000 characters.
                                                </div>

                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Default Value</label>
                                                <label class="input state-disabled">
                                                    <input type="text" id="txtDefaultValue" name="txtDefaultValue" class="input-sm" readonly="readonly">
                                                </label>
                                                <div class="note">
                                                    <strong>Maxlength</strong> is 1000 characters.
                                                </div>
                                            </section>
                                        </div>

                                        <div class="row">
                                            <section class="col col-8">
                                                <label class="label">Description</label>
                                                <label class="input">
                                                    <input type="text" id="txtDescription" name="txtDescription" class="input-sm">
                                                </label>
                                                <div class="note">
                                                    <strong>Maxlength</strong> is 1000 characters.
                                                </div>
                                            </section>
                                        </div>

                                    </fieldset>

                                    <footer>
                                        <button type="submit" class="btn btn-primary" style="float: left;" id="btnSubmit" onclick="update();">Submit</button>
                                        <button type="button" class="btn btn-default" style="float: left;" onclick="window.history.go(-1); return false;">
                                            Back
                                        </button>
                                        <input type="hidden" name="id1" />
                                        <input type="hidden" name="m" />

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

    <script type="text/javascript">

        // DO NOT REMOVE : GLOBAL FUNCTIONS!

        $(document).ready(function () {

            var fullPath = "<%=HttpContext.Current.Request.Url.AbsolutePath.Substring(0, HttpContext.Current.Request.Url.AbsolutePath.LastIndexOf("/"))%>/SystemConfigList.aspx";
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
                    txtName: {
                        required: true,
                        maxlength: 50
                    },
                    txtValue: {
                        maxlength: 1000
                    },
                    txtDefaultValue: {
                        maxlength: 1000
                    },
                    txtDescription: {
                        maxlength: 1000
                    },
                },
                messages: {
                    txtName: {
                        required: 'Please enter name',
                        maxlength: 'The field MaxLength is 50.'
                    },
                    txtValue: {
                        maxlength: 'The field MaxLength is 1000.'
                    },
                    txtDefaultValue: {
                        maxlength: 'The field MaxLength is 1000.'
                    },
                    txtDescription: {
                        maxlength: 'The field MaxLength is 1000.'
                    },
                },

                errorPlacement: function (error, element) {
                    error.insertAfter(element.parent());
                }

            });

            $.validator.addMethod(
                    "regex",
                    function (value, element, regexp) {
                        var re = new RegExp(regexp);
                        return this.optional(element) || re.test(value);
                    },
                    "Please check your input."
            );

            if ("<%=Request["m"]%>" == "e" && "<%=Request["id1"]%>" != "") {

                var name = '<%=Request["id1"]%>';
                Pace.restart();
                $.ajax({
                    "type": "POST",
                    "dataType": 'json',
                    "contentType": "application/json; charset=utf-8",
                    "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MasterTable/GetSystemConfig/",
                    "data": "{'name' : '" + name + "', 'dt' : " + dateFormat(new Date(), "yyyymmddHHMMss") + "}",
                    "success": function (ret) {

                        if (ret.status == "1") {
                            var myData = JSON.parse(ret.data);

                            $('#txtName').val(myData.Name);
                            $('#txtValue').val(myData.Value);

                            $('#txtDefaultValue').val(myData.DefaultValue);
                            $('#txtDescription').val(myData.Description);

                            $('#id1').val(myData.Name);
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
                                    title: "Get Action Profile Failed",
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
        });

        function update() {

            if (!$('#smartForm').valid()) return;

            tmpObj = function () {
                this.id = $('#id1').val();
                this.m = $('#m').val();
                this.name = $('#txtName').val();
                this.value = $('#txtValue').val();
                this.defaultValue = $('#txtDefaultValue').val();
                this.description = $('#txtDescription').val();
                this.dt = dateFormat(new Date(), "yyyymmddHHMMss");
            };
            var updObj = new tmpObj();
            Pace.restart();
            $.ajax({
                "type": "POST",
                "dataType": 'json',
                "contentType": "application/json; charset=utf-8",
                "url": "<%= HttpContext.Current.Request.ApplicationPath %>/Monitoring/MasterTable/UpdateSystemConfig/",
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
                            alert('This operation is complete.');
                        }
                        setTimeout(function () {
                            window.location.href = 'SystemConfigList.aspx';
                        }, 1000);

                    } else if (ret.status == "-1") {
                        try {
                            $.smallBox({
                                title: "Update Failed!",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });
                        } catch (ex) {
                            alert(ret.error);
                        }

                    } else if (ret.status == "0") {
                        try {
                            $.smallBox({
                                title: "Update Failed!",
                                content: "<i class='fa fa-clock-o'></i> <i>" + ret.error + "</i>",
                                color: "#C46A69",
                                iconSmall: "fa fa-times fa-2x fadeInRight animated",
                                timeout: 4000
                            });
                        } catch (ex) {
                            alert(ret.error);
                        }
                    }

                },

            });
        }


    </script>

</asp:Content>
