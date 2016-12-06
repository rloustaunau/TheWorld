/* site.js */

(function () {

   
    //var ele = $("#username");
    //                // using old javascript ; cross browser functionality - var ele = document.getElementById("username");
    //ele.text('Rafael Loustaunau2');

    //                // var main = document.getElementById("main");
    //var main = $('#main');

    //main.on('mouseenter', function () {
    //    $("#main").css("background-color", "blue");       
    //})

    //main.on('mouseleave', function () {
    //    $("#main").css("background-color", "");     
    //})


    //var menuItems = $('ul.menu li a');

    //menuItems.on('click', function () {
    //    alert('hello');
    //});

    var $sidebarAndWrapper = $("#sidebar,#wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if ($sidebarAndWrapper.hasClass("hide-sidebar")) {
            //$(this).text("Show Sidebar");
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            //$(this).text("Hide Sidebar");      
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });
    
})();

