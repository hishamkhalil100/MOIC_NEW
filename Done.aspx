<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Done.aspx.cs" Inherits="Done" %>

<!DOCTYPE html>
<!-- 
    Template Name: Metronic - Responsive Admin Dashboard Template build with Twitter Bootstrap 3.3.7
    Version: 4.7.1
    Author: KeenThemes
    Website: http://www.keenthemes.com/
    Contact: support@keenthemes.com
    Follow: www.twitter.com/keenthemes
    Dribbble: www.dribbble.com/keenthemes
    Like: www.facebook.com/keenthemes
    Purchase: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
    Renew Support: http://themeforest.net/item/metronic-responsive-admin-dashboard-template/4021469?ref=keenthemes
    License: You must have a valid license purchased only from themeforest(the above link) in order to legally use the theme for your project.
    -->
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en" dir="rtl">
<!--<![endif]-->
<!-- BEGIN HEAD -->

<head>
    <meta charset="utf-8" />
    <title>مكتبة الملك فهد الوطنية</title>
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/simple-line-icons/simple-line-icons.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/bootstrap/css/bootstrap-rtl.min.css" rel="stylesheet" type="text/css" />
    <link href="../Forms/assets/global/plugins/bootstrap-switch/css/bootstrap-switch-rtl.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN THEME GLOBAL STYLES -->
    <link href="../Forms/assets/global/css/components-rtl.min.css" rel="stylesheet" id="style_components" type="text/css" />
    <link href="../Forms/assets/global/css/plugins-rtl.min.css" rel="stylesheet" type="text/css" />
    <!-- END THEME GLOBAL STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="../Forms/assets/pages/css/error-rtl.min.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL STYLES -->
    <!-- BEGIN THEME LAYOUT STYLES -->
    <!-- END THEME LAYOUT STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
    <style type="text/css">
        body {
            background: url('../Forms/assets/layouts/layout2/img/Background.png') no-repeat fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            background-position: center;
            -o-background-size: cover;
            background-size: cover;
        }

        h1 {
            text-align: center;
        }

        h2 {
            text-align: center;
        }

        .img1 {
            width: 15%;
        }

        .img2 {
            width: 25%;
        }

        .img3 {
            width: 25%;
            margin-top: 20%;
        }

        @media screen and (max-width: 768px) {
            body {
                background: url('../Forms/assets/layouts/layout2/img/Background2.png') no-repeat fixed;
                -webkit-background-size: cover;
                -moz-background-size: cover;
                background-position: center;
                -o-background-size: cover;
                background-size: cover;
            }

            .img1 {
                width: 50%;
            }

            .img2 {
                width: 80%;
            }

            .img3 {
                width: 80%;
                margin-top: 80%;
            }
        }

        @media only screen and (min-device-width : 768px) and (max-device-width : 1024px) {
            body {
                background: url('../Forms/assets/layouts/layout2/img/Background.png') no-repeat fixed;
                -webkit-background-size: cover;
                -moz-background-size: cover;
                background-position: center;
                -o-background-size: cover;
                background-size: cover;
            }

            h1 {
                text-align: center;
            }

            h2 {
                text-align: center;
            }

            .img1 {
                width: 35%;
            }

            .img2 {
                width: 60%;
            }

            .img3 {
                width: 75%;
                margin-top: 70%;
            }
        }
    </style>
</head>
<!-- END HEAD -->
<body>
    <div>
        <!-- <img src="../Forms/assets/layouts/layout2/img/Background.png" class="img-responsive" alt="">-->
    </div>
    <div>
        <h1>
            <img src="../Forms/assets/layouts/layout2/img/head1.png" class="img1" />
        </h1>
        <h2>
            <img src="../Forms/assets/layouts/layout2/img/head2.png" class="img2" />
	    </h2>
		
		<h2>
            <img src="assets/layouts/layout2/img/error6.png" id="imgError" class="img2"></h2>
		
        <h2>
            <a href="http://www.kfnl.gov.sa">
                <img src="../Forms/assets/layouts/layout2/img/Layer 4.png" class="img3" /></a>
        </h2>
    </div>
    <!--[if lt IE 9]>
    <script src="../Forms/assets/global/plugins/respond.min.js"></script>
    <script src="../Forms/assets/global/plugins/excanvas.min.js"></script> 
    <script src="../Forms/assets/global/plugins/ie8.fix.min.js"></script> 
    <![endif]-->
    <!-- BEGIN CORE PLUGINS -->
    <script src="../Forms/assets/global/plugins/jquery.min.js" type="text/javascript"></script>
    <script src="../Forms/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../Forms/assets/global/plugins/js.cookie.min.js" type="text/javascript"></script>
    <script src="../Forms/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="../Forms/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="../Forms/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
    <!-- END CORE PLUGINS -->
    <!-- BEGIN THEME GLOBAL SCRIPTS -->
    <script src="../Forms/assets/global/scripts/app.min.js" type="text/javascript"></script>
    <!-- END THEME GLOBAL SCRIPTS -->
    <!-- BEGIN THEME LAYOUT SCRIPTS -->
    <!-- END THEME LAYOUT SCRIPTS -->
    <!-- Google Code for Universal Analytics -->
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
    <noscript>
        <iframe src="//www.googletagmanager.com/ns.html?id=GTM-W276BJ"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <script>(function (w, d, s, l, i) {
    w[l] = w[l] || []; w[l].push({
        'gtm.start':
            new Date().getTime(), event: 'gtm.js'
    }); var f = d.getElementsByTagName(s)[0],
    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
    '//www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
})(window, document, 'script', 'dataLayer', 'GTM-W276BJ');</script>
    <!-- End -->
</body>


</html>
