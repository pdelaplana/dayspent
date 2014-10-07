app.views.add('profile', function (context) {
    
    // register route
    app.router.registerRoute('#/profile', function (context) {
        context.loadLocation('/profile/', initialize);

    });

    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle("My Profile");

        ui.addPart('profileForm', new ProfileViewModel(model)).bindTo('#ProfileForm');


    }

}());