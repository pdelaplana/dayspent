app.views.add('dashboard', function (context) {

    // register route
    app.router.registerRoute('#/dashboard', function (context) {
        dashboard.loadContentArea = initOverview;
        context.loadLocation('/dashboard', dashboard.initialize);
    });

    app.router.registerRoute('#/dashboard/overview', function (context) {

        function init() {

        }
        if ($('#DashboardMenu').length == 0) {
            dashboard.loadContentArea = function () {
                context.loadTargetLocation('/dashboard/overview', initOverview, '.content-area');
            };
            context.loadLocation('/dashboard', dashboard.initialize);
        }
        else 
            context.loadTargetLocation('/dashboard/overview', initOverview, '.content-area');
        
    });

    app.router.registerRoute('#/dashboard/tags', function (context) {
        if ($('#DashboardMenu').length == 0) {
            dashboard.loadContentArea = function () {
                context.loadTargetLocation('/dashboard/tags', initTags, '.content-area');
            };
            context.loadLocation('/dashboard', dashboard.initialize);
        }
        else
            context.loadTargetLocation('/dashboard/tags', initTags, '.content-area');
    });

    var dashboard = {
        menu: {
            ready: ko.observable(false),
            selected: ko.observable('overview')
        },
        initialize: function(){
            var ui = app.ui.extend();
            ui.setWindowTitle('Dashboard');
            app.ui.appSideBar.selected('dashboard');

            ui.addPart('menu', dashboard.menu).bindTo('#DashboardMenu');
            dashboard.menu.ready(true);
            dashboard.loadContentArea();
        },
        loadContentArea: function () { }
    }

    
   

    var initOverview = function () {
        var ui = app.ui.extend();
        
        ui.addPart('overview', new DailyUpdateViewModel()).bindTo('#Overview');
        dashboard.menu.selected('overview');
        
        $(window).resize();
    }

    var initTags = function () {

        var ui = app.ui.extend();
        
        ui.addPart('overview', new TagSummaryViewModel()).bindTo('#TagSummary');
        
        dashboard.menu.selected('tags');
    }

}());