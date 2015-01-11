app.views.add('dashboard', function (context) {

    // register route
    app.router.registerRoute('#/dashboard', function (context) {
        context.loadLocation('/dashboard/', initialize);

    });

    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle('Dashboard');

        app.ui.appSideBar.selected('dashboard');

        //ui.addPart('profileForm', new ProfileViewModel(model)).bindTo('#ProfileForm');


    }

}());