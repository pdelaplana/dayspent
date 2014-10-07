$.timeline = {
    sidebar: null,
    stream: null
};


app.views.add('timeline', function (context) {
    
    // register routes

    app.router.registerRoute('#/timeline/:id/reports', function (context) {
        var id = this.params['id'];
        context.loadLocation('/timeline/' + this.params['id'], initialize);
    });


    app.router.registerRoute('#/timeline/:id/activities', function (context) {
        var id = this.params['id'];
        context.loadLocation('/timeline/' + this.params['id'], initialize);
    });

    app.router.registerRoute('#/timeline/:id', function (context) {
        var id = this.params['id'];
        context.loadLocation('/timeline/' + this.params['id'], initialize);
    });

    app.router.registerRoute('#/timeline/activities', function (context) {
        var id = this.params['id'];
        context.loadLocation('/timeline/' + this.params['id'], initialize);
    });

    app.router.registerRoute('#/timeline/reports', function (context) {
        var id = this.params['id'];
        if ($.timeline.sidebar == null) {
            context.loadLocation('/timeline/', initialize);
        }
        loadReports();
        $.timeline.sidebar.viewing('Reports');

    });


    app.router.registerRoute('#/timeline', function (context) {
        context.loadLocation('/timeline/', initialize);
   
    });

    
    
    var initialize = function () {

        var ui = app.ui.extend();
        ui.setWindowTitle("My Timeline");

        //ui.addPart('timeline', new TimelineViewModel()).bindTo('#Timeline');
        ui.addPart('sidebar', new TimelineSidebar()).bindTo('#TimelineSidebar');
        
        $('[data-role=dropdown]').dropdown();

        $(window).resize(function () {
            $('#TimelineSidebar').height($(window).height()-45);
        })
        $(window).resize();

        ui.sidebar.viewActivityStream();
        
    }

}());