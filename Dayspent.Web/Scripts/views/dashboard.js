app.views.add('dashboard', function (context) {

    // register route
    app.router.registerRoute('#/dashboard', function (context) {
        context.loadLocation('/dashboard', initialize);

    });

    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle('Dashboard');
        app.ui.appSideBar.selected('dashboard');

        ui.addPart('dashboard', new DailyUpdateViewModel()).bindTo('#Dashboard');


        //ui.addPart('profileForm', new ProfileViewModel(model)).bindTo('#ProfileForm');

        $('[data-role=dropdown]').dropdown();
        $('[data-role=progress-bar]').progressbar();
        $(window).resize(function () {
            $('.page-region').height($(window).height() - 50);
            
        })
        $(window).resize();

    }

}());