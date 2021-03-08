<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintedGov.aspx.cs" Inherits="Printed" %>

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
                var template = "<div class=\"form-group \"><label class=\"control-label col-md-3\">صورة من قائمة المحتويات أو المقدمة " + count + "</label><div class=\"col-md-9\"><div class=\"fileinput fileinput-new\" data-provides=\"fileinput\"><div class=\"fileinput-new thumbnail\" style=\"width: 200px; height: 150px;\"><img src=\"../Forms/assets/noimage.png\" alt=\"\" /></div><div class=\"fileinput-preview fileinput-exists thumbnail\" style=\"max-width: 200px; max-height: 150px;\"></div><div><span class=\"btn default btn-file\"><span class=\"fileinput-new\">اختر صورة </span><span class=\"fileinput-exists\">تغير </span><input id=\"image" + count + "\"  type=\"file\" name=\"image" + count + "\" accept=\".pdf,image/*\"/></span><a href=\"javascript:;\" class=\"btn red fileinput-exists\" data-dismiss=\"fileinput\">حذف </a></div></div></div></div>";
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
            <div class="page-top" >
                <!-- BEGIN HEADER SEARCH BOX -->
                <!-- DOC: Apply "search-form-expanded" right after the "search-form" class to have half expanded search box -->
                 <img src="../Forms/assets/layouts/layout2/img/logo2.png" alt="logo" style="max-width:100%;margin-top: 4px;margin-right: 15px;" />
                
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
                                    <span class="caption-subject font-red bold uppercase"> نموذج طلب تسجيل مادة مطبوعة
 -
                                            <span class="step-title">خطوة 1 من 4 </span>
                                    </span>
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <form runat="server" class="form-horizontal" action="#" id="submit_form"  enctype="multipart/form-data">
                                    <asp:HiddenField ID="hcfCount" runat="server" Value="1" />
                                    <div class="form-wizard">
                                        <div class="form-body">
                                            <ul class="nav nav-pills nav-justified steps">
                                                <li>
                                                    <a href="#tab1" data-toggle="tab" class="step">
                                                        <span class="number">1 </span>
                                                        <span class="desc">
                                                            <i class="fa fa-check"></i>بيانات المؤلف </span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#tab2" data-toggle="tab" class="step">
                                                        <span class="number">2 </span>
                                                        <span class="desc">
                                                            <i class="fa fa-check"></i>بيانات الناشر </span>
                                                    </a>
                                                </li>
                                                <li>
                                                    <a href="#tab3" data-toggle="tab" class="step active">
                                                        <span class="number">3 </span>
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
                                                    <h3 class="block">من فضلك تحقق من بيانات المولف</h3>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            اسم المؤلف
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtAuthName" type="text" runat="server" required="required" class="form-control  " name="authname"  maxlength="254"/>
                                                            <span class="help-block">الرجاء ادخال اسم المؤلف </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">الجنسية</label>
                                                        <div class="col-md-4">
                                                            <select runat="server" name="countries" id="countries_list" class="form-control">
                                                                <option value=""></option>
                                                                <option value="1">المملكة العربية السعودية</option>
                                                                <option value="2">الإمارات العربية المتحدة</option>
                                                                <option value="3">البحرين</option>
                                                                <option value="4">اليمن</option>
                                                                <option value="5">الكويت</option>
                                                                <option value="6">عمان</option>
                                                                <option value="7">قطر</option>
                                                                <option value="8">مصر</option>
                                                                <option value="9">الأردن</option>
                                                                <option value="10">السودان</option>
                                                                <option value="11">فلسطين</option>
                                                                <option value="12">الجزائر</option>
                                                                <option value="13">لبنان</option>
                                                                <option value="15">أثيوبيا</option>
                                                                <option value="16">أذربيجان</option>
                                                                <option value="17">أرمينيا</option>
                                                                <option value="18">إريتريا</option>
                                                                <option value="19">أسبانيا</option>
                                                                <option value="20">أستراليا</option>
                                                                <option value="21">إستونيا</option>
                                                                <option value="22">إفريقياالوسطى</option>
                                                                <option value="23">أفغانستان</option>
                                                                <option value="24">ألبانيا</option>
                                                                <option value="25">ألمانيا</option>
                                                                <option value="26">أندورا</option>
                                                                <option value="27">أندونيسيا</option>
                                                                <option value="28">أنغولا</option>
                                                                <option value="29">أورجواي</option>
                                                                <option value="30">أوزبكستان</option>
                                                                <option value="31">أوغندا</option>
                                                                <option value="32">أوكرانيا</option>
                                                                <option value="33">إيران</option>
                                                                <option value="34">أيرلندا</option>
                                                                <option value="35">أيسلندا</option>
                                                                <option value="36">إيطاليا</option>
                                                                <option value="37">الأرجنتين</option>
                                                                <option value="38">الإكوادور</option>
                                                                <option value="39">البرازيل</option>
                                                                <option value="40">البرتغال</option>
                                                                <option value="41">البهاما</option>
                                                                <option value="42">البوسنةوالهرسك</option>
                                                                <option value="43">التشيك</option>
                                                                <option value="44">الدانمارك</option>
                                                                <option value="45">الدومينكان</option>
                                                                <option value="46">الرأسالأخضر</option>
                                                                <option value="47">السلفادور</option>
                                                                <option value="48">السنغال</option>
                                                                <option value="49">السويد</option>
                                                                <option value="50">الصومال</option>
                                                                <option value="51">الصين</option>
                                                                <option value="52">الغابون</option>
                                                                <option value="53">الفلبين</option>
                                                                <option value="54">الكاميرون</option>
                                                                <option value="55">الكونغو</option>
                                                                <option value="56">الكونغوالديمقراطية</option>
                                                                <option value="57">المالديف</option>
                                                                <option value="58">المجر</option>
                                                                <option value="59">المغرب</option>
                                                                <option value="60">المكسيك</option>
                                                                <option value="61">المملكةالمتحدة</option>
                                                                <option value="62">النرويج</option>
                                                                <option value="63">النمسا</option>
                                                                <option value="64">النيجر</option>
                                                                <option value="65">الهند</option>
                                                                <option value="66">الولاياتالمتحدةالأمريكية</option>
                                                                <option value="67">اليابان</option>
                                                                <option value="68">اليونان</option>
                                                                <option value="69">باراغواي</option>
                                                                <option value="70">باكستان</option>
                                                                <option value="71">بربادوس</option>
                                                                <option value="72">بروناي</option>
                                                                <option value="73">بلجيكا</option>
                                                                <option value="74">بلغاريا</option>
                                                                <option value="75">بنغلاديش</option>
                                                                <option value="76">بنما</option>
                                                                <option value="77">بنين</option>
                                                                <option value="78">بوتان</option>
                                                                <option value="79">بوتسوانا</option>
                                                                <option value="80">بوركينافاسو</option>
                                                                <option value="81">بوروندي</option>
                                                                <option value="82">بولندا</option>
                                                                <option value="83">بوليفيا</option>
                                                                <option value="84">بيرو</option>
                                                                <option value="85">تايلاند</option>
                                                                <option value="86">تايوان</option>
                                                                <option value="87">تركيا</option>
                                                                <option value="88">ترينيدادوتوباغو</option>
                                                                <option value="89">تشاد</option>
                                                                <option value="90">تشيلي</option>
                                                                <option value="91">تنزانيا</option>
                                                                <option value="92">توغو</option>
                                                                <option value="93">توفالو</option>
                                                                <option value="94">تونس</option>
                                                                <option value="95">جامايكا</option>
                                                                <option value="96">جرينادا</option>
                                                                <option value="97">جزرالقمر</option>
                                                                <option value="98">جنوبأفريقيا</option>
                                                                <option value="99">جورجيا</option>
                                                                <option value="100">جيبوتي</option>
                                                                <option value="101">دومينيكا</option>
                                                                <option value="102">رواندا</option>
                                                                <option value="103">روسيا</option>
                                                                <option value="104">روسياالبيضاء</option>
                                                                <option value="105">رومانيا</option>
                                                                <option value="106">زامبيا</option>
                                                                <option value="107">زمبابوي</option>
                                                                <option value="108">ساحلالعاج</option>
                                                                <option value="109">ساموا</option>
                                                                <option value="110">سانتوميوبرينسيبي</option>
                                                                <option value="111">سانمارينو</option>
                                                                <option value="112">سانتفنسنتوجزرغرينادين</option>
                                                                <option value="113">سانتكيتسونيفس</option>
                                                                <option value="114">سانتلوسيا</option>
                                                                <option value="115">سريلانكا</option>
                                                                <option value="116">سلوفاكيا</option>
                                                                <option value="117">سلوفينيا</option>
                                                                <option value="118">سنغافورة</option>
                                                                <option value="119">سوازيلاند</option>
                                                                <option value="120">سوريا</option>
                                                                <option value="121">سورينام</option>
                                                                <option value="122">سويسرا</option>
                                                                <option value="123">سيراليون</option>
                                                                <option value="124">سيشيل</option>
                                                                <option value="125">طاجكستان</option>
                                                                <option value="126">غامبيا</option>
                                                                <option value="127">غانا</option>
                                                                <option value="128">غواتيمالا</option>
                                                                <option value="129">غويانا</option>
                                                                <option value="130">غينيا</option>
                                                                <option value="131">غينيا-بيساو</option>
                                                                <option value="132">غينياالاستوائية</option>
                                                                <option value="133">غينياالفرنسية</option>
                                                                <option value="134">فانواتو</option>
                                                                <option value="135">فرنسا</option>
                                                                <option value="136">فنزويلا</option>
                                                                <option value="137">فنلندا</option>
                                                                <option value="138">فيتنام</option>
                                                                <option value="139">فيجي</option>
                                                                <option value="140">قبرص</option>
                                                                <option value="141">كازاخستان</option>
                                                                <option value="142">كرواتيا</option>
                                                                <option value="143">كمبوديا</option>
                                                                <option value="144">كندا</option>
                                                                <option value="145">كوبا</option>
                                                                <option value="146">كورياالجنوبية</option>
                                                                <option value="147">كورياالشمالية</option>
                                                                <option value="148">كوستاريكا</option>
                                                                <option value="149">كولومبيا</option>
                                                                <option value="150">كيريباس</option>
                                                                <option value="151">كينيا</option>
                                                                <option value="152">لاتفيا</option>
                                                                <option value="153">لاوس</option>
                                                                <option value="154">لوكسمبورج</option>
                                                                <option value="155">ليبيا</option>
                                                                <option value="156">ليبيريا</option>
                                                                <option value="157">ليتوانيا</option>
                                                                <option value="158">ليختنشتاين</option>
                                                                <option value="159">ليسوتو</option>
                                                                <option value="160">مالاوي</option>
                                                                <option value="161">مالطا</option>
                                                                <option value="162">مالي</option>
                                                                <option value="163">ماليزيا</option>
                                                                <option value="164">مدغشقر</option>
                                                                <option value="165">مقدونيا</option>
                                                                <option value="166">منغوليا</option>
                                                                <option value="167">موريتانيا</option>
                                                                <option value="168">موريشيوس</option>
                                                                <option value="169">موزمبيق</option>
                                                                <option value="170">مولدافيا</option>
                                                                <option value="171">موناكو</option>
                                                                <option value="172">ميانمار</option>
                                                                <option value="173">ميكرونيسيا</option>
                                                                <option value="174">ناميبيا</option>
                                                                <option value="175">نورو</option>
                                                                <option value="176">نيبال</option>
                                                                <option value="177">نيجيريا</option>
                                                                <option value="178">نيكاراغوا</option>
                                                                <option value="179">نيوزيلندا</option>
                                                                <option value="180">هايتي</option>
                                                                <option value="181">هندوراس</option>
                                                                <option value="182">هنغاريا</option>
                                                                <option value="183">هولندا</option>
                                                                <option value="184">يوغسلافيا</option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">سنة الميلاد</label>
                                                        <div class="col-md-4">
                                                            <input runat="server" class="form-control" id="mask_date2" type="text" />

                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="tab2">
                                                    <h3 class="block">الرجاء ادخال معلومات الناشر والكتاب</h3>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            عنوان الكتاب
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtBookTitle" runat="server" type="text" class="form-control" name="booktitle"  required="required"  maxlength="254"/>
                                                            <span class="help-block">الرجاء ادخال عنوان الكتاب </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            اسم الناشر
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtPubName" type="text" runat="server" class="form-control" name="pubname"  required="required"  maxlength="254"/>
                                                            <span class="help-block">الرجاء ادخال اسم الناشر  </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">المدينة
                                                            <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <select name="pubCity" id="pubCity" runat="server" class="form-control" required="required">
                                                            </select>
                                                            <span class="help-block">الرجاء اختيار مدينة الناشر  </span>
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
                                                            نوع العمل
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <div class="radio-list">
                                                                <select name="pubCity" id="types" runat="server" class="form-control" required="required">

                                                            </select>
                                                            </div>
                                                            <div id="form_gender_error"></div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            رقم الطبعة
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtEditionNO" runat="server" type="text" class="form-control"  maxlength="254"/>
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            عدد الصفحات
                                                        </label>
                                                        <div class="col-md-1">
                                                            <input id="txtPagesNo" runat="server" type="text" class="form-control"  maxlength="254"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            عدد الاجزاء
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtNumOfParts" runat="server" type="text" class="form-control"  maxlength="254"/>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            مقاس الكتب
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtBooSize" runat="server" type="text" class="form-control"  maxlength="254"/>
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            سعر الكتاب
                                                        </label>
                                                        <div class="col-md-1">
                                                            <input id="txtBookPrice" runat="server" type="text" class="form-control" maxlength="254" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            اسم السلسلة
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtSeriesName" runat="server" type="text" class="form-control"  maxlength="254"/>
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            رقمها
                                                        </label>
                                                        <div class="col-md-1">
                                                            <input id="txtSeriesNum" runat="server" type="number" class="form-control" maxlength="254" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">مستخلص العمل
                                                            <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <textarea id="txtRemarks" runat="server" class="form-control" rows="3" name="remarks" required="required" maxlength="254"></textarea>
                                                        </div>
                                                    </div>

                                                    <div id="filesUpload">
                                                        <div class="form-group">
                                                            <label class="control-label col-md-3">صورة من قائمة المحتويات أو المقدمة
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
                                                                            <input id="image1" type="file" name="image1" required="required" accept=".pdf,image/*"/></span><a href="javascript:;" class="btn red fileinput-exists" data-dismiss="fileinput">حذف </a>
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
                                                </div>


                                                <div class="tab-pane" id="tab3">
                                                    <h3 class="block">ادخل بيانات المودع</h3>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            هاتف
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtDepPhone" runat="server" type="text" class="form-control" maxlength="254" />
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            فاكس
                                                        </label>
                                                        <div class="col-md-1">
                                                            <input id="txtDepFax" runat="server" type="text" class="form-control" maxlength="254" />
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            ص.ب
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtDepPost" runat="server" type="text" class="form-control" maxlength="254" />
                                                        </div>

                                                        <label class="control-label col-md-2">
                                                            المدينة
                                                        </label>
                                                        <div class="col-md-1">
                                                            <select name="pubCity" id="deptCity" runat="server" class="form-control"  maxlength="254">
                                                               
                                                            </select>
                                                            <span class="help-block">الرجاء اختيار مدينة المودع  </span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            الرمز البريدي
                                                        </label>

                                                        <div class="col-md-1">
                                                            <input id="txtDepZip" runat="server" type="text" class="form-control"  maxlength="254"/>
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
                                                            <input id="txtDepNID" runat="server" type="text" class="form-control" name="card_number" required="required"  maxlength="254"/>
                                                            <span class="help-block"></span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            اسم مقدم الطلب
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtDepName" runat="server" type="text" placeholder="" class="form-control" required="required"  maxlength="254"/>
                                                            <span class="help-block"></span>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            بريد مقدم الطلب
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <input id="txtDepEmail" runat="server" type="email" placeholder="" class="form-control" required="required"  maxlength="254"/>
                                                            <span class="help-block"></span>
                                                        
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="control-label col-md-3">
                                                            إقرار
                                                                <span class="required">* </span>
                                                        </label>
                                                        <div class="col-md-4">
                                                            <div class="checkbox-list">
                                                                <label for="ckConfermation">
                                                                     <input id="ckConfermation" runat="server" type="checkbox" placeholder=""  required="required" />
                                                                    أتعهد بإيداع نسختين من العمل في مكتبة الملك فهد الوطنية فور الانتهاء من طباعته بالإضافة إلى ايداع نسخة إلكترونية من العمل مخزنة على قرص ( سي دي ) <a  id="hrefDetails">للمزيد اضغط هنا</a>
                                                                </label>
                                                                <label id="lblDetails" style="visibility:hidden;color:darkred">
                                                                    يعاقب من يخالف أحكام هذا النظام بغرامة لاتتجاوز ثلاثة الاف ريال مع الزامه بايداع النسخ المطلوبة من العمل وفقا لهذا النظام
                                                                </label>
                                                            </div>
                                                           
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
            <label id ="lblyear">2016</label> &copy; مكتبة الملك فهد الوطنية
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
        <script src="assets/pages/scripts/arabic.js"></script>
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

            var inputEmail = document.querySelector('#txtRemarks');

            inputEmail.onkeyup = function (e) {
                var max = 254; // The maxlength you want

                if (inputEmail.value.length > max) {
                    inputEmail.value = inputEmail.value.substring(0, max);
                }

            };
        </script>
        <script>
            var hrefDetails = document.getElementById('hrefDetails')
            hrefDetails.onclick = showDetails

            function showFoo() {
                var aDetails = document.getElementById('aDetails')
                if (aDetails.style.visibility == "hidden")
                    aDetails.style.visibility = "visible"
                else 
                    aDetails.style.visibility = "hidden"
                    return false;
                }
        </script>
    </div>
</body>
</html>
