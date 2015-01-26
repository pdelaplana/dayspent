app.views.add('dashboard', function (context) {

    // register route
    app.router.registerRoute('#/statistics', function (context) {
        context.loadLocation('/statistics', initialize);

    });

    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle('Statistics');

        app.ui.appSideBar.selected('statistics');

        //ui.addPart('profileForm', new ProfileViewModel(model)).bindTo('#ProfileForm');
        $(window).resize(function () {
            $('.page-region').height($(window).height() - 20);
            $('#MyReports .row').height($(window).height() - 20);

        })
        $(window).resize();

    }

}());