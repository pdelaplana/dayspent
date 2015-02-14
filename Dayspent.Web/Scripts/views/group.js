app.views.add('group', function (context) {

    // register route
    app.router.registerRoute('#/group', function (context) {
        context.loadLocation('/group', initialize);

    });

    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle('Group');
        app.ui.appSideBar.selected('group');

        ui.addPart('groupReports', new GroupReportsViewModel(model)).bindTo('#GroupReports');


    }

}());