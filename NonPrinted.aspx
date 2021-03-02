<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NonPrinted.aspx.cs" Inherits="NonPrinted" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>مكتبة الملك فهد الوطنية</title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/bootstrap/css/bootstrap-rtl.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/bootstrap-switch/css/bootstrap-switch-rtl.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL PLUGINS -->
    <link href="../Forms/assets/global/plugins/select2/css/select2.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/select2/css/select2-bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/fancybox/source/jquery.fancybox.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/jquery-file-upload/blueimp-gallery/blueimp-gallery.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL PLUGINS -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../Forms/assets/global/css/components-rtl.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Forms/assets/global/css/plugins-rtl.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <link href="../Forms/assets/layouts/layout2/css/layout-rtl.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/layouts/layout2/css/themes/blue-rtl.min.css" rel="stylesheet" type="text/css" id="style_color" />
    <link href="../Forms/assets/layouts/layout2/css/custom-rtl.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
      <script src='https://www.google.com/recaptcha/api.js?hl=ar'></script>
    <script type="text/javascript">
        function addImageUpload() {

            var count = parseInt($("#count")[0].title);
            if (count <= 10) {
                var template = "<div class=\"form-group \"><label class=\"control-label col-md-3\">صورة من قائمة المحتويات أو المقدمة " + count + "</label><div class=\"col-md-9\"><div class=\"fileinput fileinput-new\" data-provides=\"fileinput\"><div class=\"fileinput-new thumbnail\" style=\"width: 200px; height: 150px;\"><img src=\"../Forms/assets/noimage.png\" alt=\"\" /></div><div class=\"fileinput-preview fileinput-exists thumbnail\" style=\"max-width: 200px; max-height: 150px;\"></div><div><span class=\"btn default btn-file\"><span class=\"fileinput-new\">اختر صورة </span><span class=\"fileinput-exists\">تغير </span><input id=\"image" + count + "\"  type=\"file\" name=\"image" + count + "\" accept=\"image/*\"/></span><a href=\"javascript:;\" class=\"btn red fileinput-exists\" data-dismiss=\"fileinput\">حذف </a></div></div></div></div>";
                $("#filesUpload").append(template);
                $("#count")[0].title = count + 1;
                storeContent();
            } else {
                alert("لقد بلغت الحد الاقصي من عدد الصور");
            }

        };


    </script>
    <script type="text/javascript">

        function storeContent() {
            $('#<%= hcfCount.ClientID %>').val($('#count')[0].title);
        }
    </script>

</head>
<body class="page-header-fixed page-sidebar-closed-hide-logo page-container-bg-solid">
    <!-- BEGIN HEADER -->
    <div class="page-header navbar navbar-fixed-top">
        <!-- BEGIN HEADER INNER -->
        <div class="page-header-inner ">
            <!-- BEGIN LOGO -->
            <div class="page-logo">

                <img src="../Forms/assets/layouts/layout2/img/logo1.png" alt="logo" class="logo-default" />

            </div>
            <!-- END LOGO -->


            <!-- BEGIN PAGE TOP -->
            <div class="page-top">
                <!-- BEGIN HEADER SEARCH BOX -->
                <!-- DOC: Apply "search-form-expanded" right after the "search-form" class to have half expanded search box -->
                <img src="../Forms/assets/layouts/layout2/img/logo2.png" alt="logo" style="max-width: 100%; margin-top: 4px; margin-right: 15px;" />

            </div>
            <!-- END PAGE TOP -->
        </div>
        <!-- END HEADER INNER -->
    </div>
    <!-- END HEADER -->


    <label id="chocee" style="visibility: hidden" title="اختر"></label>
    <label id="count" runat="server" style="visibility: hidden" title="2"></label>
    <label id="requ" style="visibility: hidden" title="الرجاء تعبئة هذا الحقل"></label>
    <label id="step" style="visibility: hidden" title="خطوة"></label>
    <label id="to" style="visibility: hidden" title="من"></label>
    <!-- BEGIN HEADER & CONTENT DIVIDER -->
    <div class="clearfix"></div>
    <!-- END HEADER & CONTENT DIVIDER -->
    <!-- BEGIN CONTAINER -->
    <div class="page-container">
        <!-- BEGIN SIDEBAR -->

        <!-- END SIDEBAR -->
        <!-- BEGIN CONTENT -->
        <div class="page-content-wrapper">
            <!-- BEGIN CONTENT BODY -->
            <div class="page-content">
                <div class="row">
                    <div class="col-md-12">

                        <div class="portlet light " id="form_wizard_1">
                            <div class="portlet-title">
                                <div class="caption">
                                    <i class=" icon-layers font-red"></i>
                                    <span class="caption-subject font-red bold uppercase">نموذج طلب تسجيل مادةغير مطبوعة	
 -
                                            <span class="step-title">خطوة 1 من 2 </span>
                                    </span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <form runat="server" class="form-horizontal" action="#" id="submit_form" enctype="multipart/form-data">
                                    <asp:HiddenField ID="hcfCount" runat="server" Value="1" />
                                    <div class="form-wizard">
                                        <div class="form-body">
                                            <ul class="nav nav-pills nav-justified steps">
                                                <li>
                                                    <a href="#tab1" data-toggle="tab" class="step">
                                                        <span class="number">1 </span>
                                                        <span class="desc">
                                                            <i class="fa fa-check"></i>بيانات العمل </span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#tab2" data-toggle="tab" class="step active">
                                                        <span class="number">2 </span>
                                                        <span class="desc">
                                                            <i class="fa fa-check"></i>بيانات المودع </span>
                                                    </a>
                                                </li>

                                            </ul>
                                            <div id="bar" class="progress progress-striped" role="progressbar">
                                                <div class="progress-bar progress-bar-success"></div>
                                            </div>
                                            <div class="tab-content">
                                                <div class="alert alert-danger display-none">
                                                    <button class="close" data-dismiss="alert"></button>
                                                    من فضلك ادخل بيانات جميع الحقول
                                                </div>
                                                <div class="alert alert-success display-none">
                                                    <button class="close" data-dismiss="alert"></button>
                                                    تم الأنتهاء بتجاح
                                                </div>
                                                <div class="tab-pane active" id="tab1">
                                                    <h3 class="block">الرجاء ادخال معلومات الناشر والكتاب</h3>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            عنوان العمل
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtTitle" runat="server" type="text" class="form-control" name="booktitle" required="required" maxlength="254"/>
                                                            <span class="help-block">الرجاء ادخال عنوان العمل </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            اسم المُنتِج
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtproductName" type="text" runat="server" class="form-control" name="pubname" required="required" maxlength="254"/>
                                                            <span class="help-block">الرجاء ادخال اسم المنتج </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            المدينة
                                                            <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <select name="pubCity" id="pubCity" runat="server" class="form-control" required="required">
                                                            </select>
                                                            <span class="help-block">الرجاء اختيار مدينة المنتج  </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            تاريخ الإنتاج
                                                            <span class="required">* </span>
                                                        </label>
                                                        <label class="control-label col-md-1">
                                                            هجري
                                                        </label>
                                                        <div class="  col-md-1">
                                                            <select name="pubCity" id="hijry" runat="server" class="form-control" required="required">
                                                            </select>
                                                        </div>

                                                        <label class="control-label col-md-1">
                                                            ميلادي
                                                        </label>
                                                        <div class="col-md-1">
                                                            <select name="pubCity" id="gerg" runat="server" class="form-control" required="required">
                                                            </select>
                                                        </div>
                                                    </div>
                                                     <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            شكل الوعاء
                                                            <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <select name="pubCity" id="subjectID" runat="server" class="form-control" required="required">
                                                            </select>
                                                            <span class="help-block">الرجاء اختيار مدينة المنتج  </span>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            موضوع العمل
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <select name="pubCity" id="typeID" runat="server" class="form-control" required="required"></select>
                                                        </div>
                                                        <div id="form_gender_error2"></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            عدد الاجزاء
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txttotalNoEpisode" runat="server" type="text" class="form-control" maxlength="254"/>
                                                        </div>


                                                        <label class="control-label col-md-2">
                                                            رقم الجزء المودع
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtepisodeNo" runat="server" type="text" class="form-control" maxlength="254"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            المعد / المشاركون في العمل
                                                            <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <textarea id="txtAuthors" runat="server" class="form-control" rows="3" name="remarks" required="required" maxlength="254"></textarea>
                                                        </div>
                                                    </div>

                                                    <div id="filesUpload">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-3">
                                                                صورة من قائمة المحتويات أو المقدمة
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-9">
                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                    <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                                                        <img src="../Forms/assets/noimage.png" alt="" />
                                                                    </div>
                                                                    <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                                                    <div>
                                                                        <span class="btn default btn-file"><span class="fileinput-new">اختر صورة </span><span class="fileinput-exists">تغير </span>
                                                                            <input id="image1" type="file" name="image1" required="required" accept=".pdf,image/*" /></span><a href="javascript:;" class="btn red fileinput-exists" data-dismiss="fileinput">حذف </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3"></label>
                                                        <div class="col-md-4">
                                                            <button type="button" class="btn dark" onclick="addImageUpload();">اضف صورةاخرى</button>
                                                        </div>
                                                    </div>
                                                    <div id="filesUpload2">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-3">
                                                               صورة فسح وزارة الثقافة والإعلام
                                                                <span class="required">* </span>
                                                            </label>
                                                            <div class="col-md-9">
                                                                <div class="fileinput fileinput-new" data-provides="fileinput">
                                                                    <div class="fileinput-new thumbnail" style="width: 200px; height: 150px;">
                                                                        <img src="../Forms/assets/noimage.png" alt="" />
                                                                    </div>
                                                                    <div class="fileinput-preview fileinput-exists thumbnail" style="max-width: 200px; max-height: 150px;"></div>
                                                                    <div>
                                                                        <span class="btn default btn-file"><span class="fileinput-new">اختر صورة </span><span class="fileinput-exists">تغير </span>
                                                                            <input id="moicImg" type="file" name="moicImg" required="required" accept=".pdf,image/*" /></span><a href="javascript:;" class="btn red fileinput-exists" data-dismiss="fileinput">حذف </a>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                
                                                <div class="tab-pane" id="tab2">
                                                    <h3 class="block">ادخل بيانات المودع</h3>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            هاتف
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtDepPhone" runat="server" type="text" class="form-control" maxlength="254"/>
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            فاكس
                                                        </label>
                                                        <div class="col-md-1">
                                                            <input id="txtDepFax" runat="server" type="text" class="form-control" maxlength="254"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            ص.ب
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtDepPost" runat="server" type="text" class="form-control" maxlength="254"/>
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            المدينة
                                                        </label>
                                                        <div class="col-md-1">
                                                            <select name="pubCity" id="deptCity" runat="server" class="form-control">
                                                            </select>
                                                            <span class="help-block">الرجاء اختيار مدينة المودع  </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            الرمز البريدي
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtDepZip" runat="server" type="text" class="form-control" maxlength="254"/>
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                        </label>
                                                        <div class="col-md-1">
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            رقم الهوية
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtDepNID" runat="server" type="text" class="form-control" name="card_number" required="required" maxlength="254"/>
                                                            <span class="help-block"></span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            اسم مقدم الطلب
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtDepName" runat="server" type="text" placeholder="" class="form-control" required="required" maxlength="254"/>
                                                            <span class="help-block"></span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            بريد مقدم الطلب
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtDepEmail" runat="server" type="email" placeholder="" class="form-control" required="required" maxlength="254"/>
                                                            <span class="help-block"></span>
                                                            <div class="g-recaptcha" data-sitekey="6LfSaHcUAAAAAC3PhpICujn4KlT4TY3P0Faz9O6A" ></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="row">
                                                <div class="col-md-offset-3 col-md-9">
                                                    <a href="javascript:;" class="btn default button-previous">
                                                        <i class="fa fa-angle-right"></i>السابق </a>
                                                    <a href="javascript:;" class="btn btn-outline green button-next">التالي
                                                            <i class="fa fa-angle-left"></i>
                                                    </a>
                                                    <asp:Button runat="server" ID="btnSubmet" class="btn green button-submit fa fa-check" Text="الانتهاء" OnClick="btnSubmet_Click"></asp:Button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END CONTENT BODY -->
    </div>
    <!-- END CONTENT -->
    <!-- BEGIN QUICK SIDEBAR -->
    <a href="javascript:;" class="page-quick-sidebar-toggler">
        <i class="icon-login"></i>
    </a>

    <!-- END QUICK SIDEBAR -->
    </div>
    <!-- END CONTAINER -->
    <!-- BEGIN FOOTER -->
    <div class="page-footer">
        <div class="page-footer-inner">
            <label id="lblyear">2016</label>
            &copy; مكتبة الملك فهد الوطنية
                        <div class="scroll-to-top">
                            <i class="icon-arrow-up"></i>
                        </div>
        </div>
        <!-- END FOOTER -->
        <!--[if lt IE 9]>
<script src="../Forms/assets/global/plugins/respond.min.js"></script>
<script src="../Forms/assets/global/plugins/excanvas.min.js"></script> 
<script src="../Forms/assets/global/plugins/ie8.fix.min.js"></script> 
<![endif]-->
        <!-- BEGIN CORE PLUGINS -->
        <script src="../Forms/assets/pages/scripts/arabic.js"></script>
        <script src="../Forms/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="../Forms/assets/global/plugins/select2/js/select2.full.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery-validation/js/additional-methods.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/bootstrap-wizard/jquery.bootstrap.wizard.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/fancybox/source/jquery.fancybox.pack.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN THEME GLOBAL SCRIPTS -->
        <script src="../Forms/assets/global/scripts/app.min.js" type="text/javascript"></script>
        <!-- END THEME GLOBAL SCRIPTS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="../Forms/assets/pages/scripts/form-wizard.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/pages/scripts/form-input-mask.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/global/plugins/bootstrap-fileinput/bootstrap-fileinput.js" type="text/javascript"></script>
        <!-- END PAGE LEVEL SCRIPTS -->
        <!-- BEGIN THEME LAYOUT SCRIPTS -->
        <script src="../Forms/assets/layouts/layout2/scripts/layout.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/layouts/layout2/scripts/demo.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/layouts/global/scripts/quick-sidebar.min.js" type="text/javascript"></script>
        <script src="../Forms/assets/layouts/global/scripts/quick-nav.min.js" type="text/javascript"></script>
        
        <!-- END THEME LAYOUT SCRIPTS -->
        <!-- Google Code for Universal Analytics -->
        <script type="text/javascript">
            document.getElementById("lblyear").innerHTML = new Date().getFullYear();
        </script>
        <script>
            (function (i, s, o, g, r, a, m) {
                i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                    (i[r].q = i[r].q || []).push(arguments)
                }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
            })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');
            ga('create', 'UA-37564768-1', 'auto');
            ga('send', 'pageview');
        </script>
        <!-- End -->

        <!-- Google Tag Manager -->
        <noscript><iframe src="//www.googletagmanager.com/ns.html?id=GTM-W276BJ"
height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
        <script>(function (w, d, s, l, i) {
    w[l] = w[l] || []; w[l].push({
        'gtm.start':
        new Date().getTime(), event: 'gtm.js'
    }); var f = d.getElementsByTagName(s)[0],
    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
    '//www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
})(window, document, 'script', 'dataLayer', 'GTM-W276BJ');</script>
        <!-- End -->
        
        <script>

            var inputEmail = document.querySelector('#txtAuthors');

            inputEmail.onkeyup = function (e) {
                var max = 254; // The maxlength you want

                if (inputEmail.value.length > max) {
                    inputEmail.value = inputEmail.value.substring(0, max);
                }

            };
        </script>
    </div>
</body>
</html>
