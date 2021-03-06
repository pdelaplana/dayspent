﻿app.views.add('statusReport', function (context) {

    // register route
    app.router.registerRoute('#/myreports', function (context) {
        context.loadLocation('/statusreport', initialize);

    });

    
    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle('Status Report');
        app.ui.appSideBar.selected('myreports');
       
        ui.addPart('myReports', new MyReportsViewModel(model)).bindTo('#MyReports');

        $(window).resize();

        $('[data-role=dropdown]').dropdown();
    }

}());